using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vector;

namespace ConsoleApplication2
{
    class Program
    {

        static void Main(string[] args)
        {
            Vector _vector = new Vector();
            _vector.Init(0);
            byte[] a = new byte[64];
            for (int i = 0; i < 64; i++)
            {
                a[i] = (byte)i;
            }
            while (true)
            {
                _vector.WriteMsg(a);
                System.Threading.Thread.Sleep(2000);
            }

            var mes = _vector.ReadMsg();
            var mes1 = _vector.ReadMsg();
            var mes2 = _vector.ReadMsg();
            _vector.Close();
        }
    }
}
