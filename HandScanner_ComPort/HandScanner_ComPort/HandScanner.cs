using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace HandScanner_ComPort
{
    public class HandScanner
    {
        private SerialPort _device;
        public HandScanner(int port, int baudrate)
        {
            try
            {
                _device = new SerialPort("com" + port, baudrate);
                if (!_device.IsOpen)
                {
                    _device.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Init Failed: " + ex.Message);
            }
        }

        public bool IsBarcodeReady()
        {
            try
            {
                if (_device.BytesToRead > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to read byte from com: " + ex.Message);
            }
        }

        public string GetBarcode()
        {
            try
            {
                var numOfBytes = _device.BytesToRead;
                var receive = new char[numOfBytes];
                var barcode = "";
                _device.Read(receive,0,numOfBytes);
                for (int i = 0; i < receive.Count(); i++)
                {
                    barcode += receive[i];
                }
                _device.DiscardInBuffer();
                return barcode;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get barcode: "+ex.Message);
            }
        }
    }
}
