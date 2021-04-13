using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vxlapi_NET;
using System.Threading;
using System.Runtime.InteropServices;

namespace vector
{
    public class Vector
    {
        private XLDriver _CANDemo = new XLDriver();
        private XLClass.xl_driver_config _driverConfig = new XLClass.xl_driver_config();
        private XLDefine.XL_HardwareType hwType = XLDefine.XL_HardwareType.XL_HWTYPE_NONE;
        private uint hwIndex = 0;
        private uint hwChannel = 0;
        private int portHandle = -1;
        private int eventHandle = -1;
        private UInt64 accessMask = 0;
        private UInt64 permissionMask = 0;
        private UInt64 txMask = 0;
        //private UInt64 rxMask = 0;
        private int txCi = 0;
        private int rxCi = 0;
        private String appName = "xlCANdemoNET";
        //private Thread rxThread;
        //private bool blockRxThread = false;
        private uint canFdModeNoIso = 0;      // Global CAN FD ISO (default) / no ISO mode flag
        public void Init(uint Iappchannel)
        {
            try
            {
                XLDefine.XL_Status status;
                status = _CANDemo.XL_OpenDriver();
                status = _CANDemo.XL_GetDriverConfig(ref _driverConfig);
                if (_CANDemo.XL_GetApplConfig(appName, Iappchannel, ref hwType, ref hwIndex, ref hwChannel, XLDefine.XL_BusTypes.XL_BUS_TYPE_CAN) != XLDefine.XL_Status.XL_SUCCESS)
                {
                    //在Application中xlCANdemoNET下创建虚拟通道，Iappchannel不能重复,需要在vector tool中手动关联硬件通道
                    _CANDemo.XL_SetApplConfig(appName, Iappchannel, XLDefine.XL_HardwareType.XL_HWTYPE_NONE, 0, 0, XLDefine.XL_BusTypes.XL_BUS_TYPE_CAN);

                }

                if (!GetAppChannelAndTestIsOk(Iappchannel, ref txMask, ref txCi))
                {
                    throw new Exception("Can not connect the hardware channel!");
                }

                accessMask = txMask;
                permissionMask = accessMask;

                status = _CANDemo.XL_OpenPort(ref portHandle, appName, accessMask, ref permissionMask, 16000, XLDefine.XL_InterfaceVersion.XL_INTERFACE_VERSION_V4, XLDefine.XL_BusTypes.XL_BUS_TYPE_CAN);

                XLClass.XLcanFdConf canFdConf = new XLClass.XLcanFdConf();

                // arbitration bitrate
                canFdConf.arbitrationBitRate = 500000;
                canFdConf.tseg1Abr = 63;
                canFdConf.tseg2Abr = 16;
                canFdConf.sjwAbr = 16;

                // data bitrate
                canFdConf.dataBitRate = 2000000;
                canFdConf.tseg1Dbr = 14;
                canFdConf.tseg2Dbr = 5;
                canFdConf.sjwDbr = 4;

                //if (canFdModeNoIso > 0)
                //{
                //    canFdConf.options = (byte)XLDefine.XL_CANFD_ConfigOptions.XL_CANFD_CONFOPT_NO_ISO;
                //}
                //else
                //{
                //    canFdConf.options = 0;
                //}

                status = _CANDemo.XL_CanFdSetConfiguration(portHandle, accessMask, canFdConf);
                status = _CANDemo.XL_SetNotification(portHandle, ref eventHandle, 1);
                status = _CANDemo.XL_ActivateChannel(portHandle, accessMask, XLDefine.XL_BusTypes.XL_BUS_TYPE_CAN, XLDefine.XL_AC_Flags.XL_ACTIVATE_RESET_CLOCK);
            }
            catch (Exception)
            {

                throw;
            }


        }

        //[DllImport("kernel32.dll", SetLastError = true)]
        //static extern int WaitForSingleObject(int handle, int timeOut);
        public void WriteMsg(byte[] data)
        {
            try
            {
                if (data.Length >= 64)
                {
                    XLClass.xl_canfd_event_collection xlEventCollection = new XLClass.xl_canfd_event_collection(1);
                    xlEventCollection.xlCANFDEvent[0].tag = XLDefine.XL_CANFD_TX_EventTags.XL_CAN_EV_TAG_TX_MSG;
                    xlEventCollection.xlCANFDEvent[0].tagData.canId = 0x2DB;
                    xlEventCollection.xlCANFDEvent[0].tagData.dlc = XLDefine.XL_CANFD_DLC.DLC_CAN_CANFD_8_BYTES;
                    xlEventCollection.xlCANFDEvent[0].tagData.msgFlags = XLDefine.XL_CANFD_TX_MessageFlags.XL_CAN_TXMSG_FLAG_BRS | XLDefine.XL_CANFD_TX_MessageFlags.XL_CAN_TXMSG_FLAG_EDL;

                    for (int i = 0; i < 64; i++)
                    {
                        xlEventCollection.xlCANFDEvent[0].tagData.data[i] = data[i];
                    }

                    uint messageCounterSent = 0;
                    _CANDemo.XL_CanTransmitEx(portHandle, txMask, ref messageCounterSent, xlEventCollection);
                }
                else
                {
                    throw new Exception("The bytes array is too short! ");
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
        public byte[] ReadMsg()
        {
            // Create new object containing received data 
            XLClass.XLcanRxEvent receivedEvent = new XLClass.XLcanRxEvent();

            // Result of XL Driver function calls
            XLDefine.XL_Status xlStatus = XLDefine.XL_Status.XL_SUCCESS;

            // Result values of WaitForSingleObject 
            XLDefine.WaitResults waitResult = new XLDefine.WaitResults();


            // Note: this thread will be destroyed by MAIN
            // Wait for hardware events
            //waitResult = (XLDefine.WaitResults)WaitForSingleObject(eventHandle, 1000);

            // If event occurred...

            // ...init xlStatus first
            xlStatus = XLDefine.XL_Status.XL_SUCCESS;

            // afterwards: while hw queue is not empty...


            // ...receive data from hardware.
            xlStatus = _CANDemo.XL_CanReceive(portHandle, ref receivedEvent);

            //  If receiving succeed....
            if (xlStatus == XLDefine.XL_Status.XL_SUCCESS)
            {
                return receivedEvent.tagData.canRxOkMsg.data;
            }
            else
            {
                return null;
            }

        }



        // -----------------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieve the application channel assignment and test if this channel can be opened
        /// </summary>
        // -----------------------------------------------------------------------------------------------
        private bool GetAppChannelAndTestIsOk(uint appChIdx, ref UInt64 chMask, ref int chIdx)
        {
            XLDefine.XL_Status status = _CANDemo.XL_GetApplConfig(appName, appChIdx, ref hwType, ref hwIndex, ref hwChannel, XLDefine.XL_BusTypes.XL_BUS_TYPE_CAN);
            if (status != XLDefine.XL_Status.XL_SUCCESS)
            {
                Console.WriteLine("XL_GetApplConfig      : " + status);
            }

            chMask = _CANDemo.XL_GetChannelMask(hwType, (int)hwIndex, (int)hwChannel);
            chIdx = _CANDemo.XL_GetChannelIndex(hwType, (int)hwIndex, (int)hwChannel);
            if (chIdx < 0 || chIdx >= _driverConfig.channelCount)
            {
                // the (hwType, hwIndex, hwChannel) triplet stored in the application configuration does not refer to any available channel.
                return false;
            }

            if ((_driverConfig.channel[chIdx].channelBusCapabilities & XLDefine.XL_BusCapabilities.XL_BUS_ACTIVE_CAP_CAN) == 0)
            {
                // CAN is not available on this channel
                return false;
            }

            if (canFdModeNoIso > 0)
            {
                if ((_driverConfig.channel[chIdx].channelCapabilities & XLDefine.XL_ChannelCapabilities.XL_CHANNEL_FLAG_CANFD_BOSCH_SUPPORT) == 0)
                {
                    Console.WriteLine("{0} ({1}) does not support CAN FD NO-ISO", _driverConfig.channel[chIdx].name.TrimEnd(' ', '\0'),
                        _driverConfig.channel[chIdx].transceiverName.TrimEnd(' ', '\0'));
                    return false;
                }
            }
            else
            {
                if ((_driverConfig.channel[chIdx].channelCapabilities & XLDefine.XL_ChannelCapabilities.XL_CHANNEL_FLAG_CANFD_ISO_SUPPORT) == 0)
                {
                    Console.WriteLine("{0} ({1}) does not support CAN FD ISO", _driverConfig.channel[chIdx].name.TrimEnd(' ', '\0'),
                        _driverConfig.channel[chIdx].transceiverName.TrimEnd(' ', '\0'));
                    return false;
                }
            }

            return true;
        }

        public void Close()
        {
            _CANDemo.XL_ClosePort(portHandle);
            _CANDemo.XL_CloseDriver();
        }
    }
}
