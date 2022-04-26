using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ver1;

namespace Zadanie_1
{

    public class Copier : IPrinter, IScanner, IDevice
    {
        private IDevice.State state;

        public int PrintCounter { get; set; }
        public int ScanCounter { get; set; }
        public int Counter { get; set; }

        public string FormatedDate = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");

        public IDevice.State GetState()
        {
            return state = IDevice.State.off;
        }

        public void PowerOff()
        {
            if (state == IDevice.State.on)
            {
                state = IDevice.State.off;
                Console.WriteLine("... Device is off !");
            }
        }

        public void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                state = IDevice.State.on;
                Console.WriteLine("Device is on ...");
            }
        }

        public void Print(in IDocument document)
        {
            Console.WriteLine(FormatedDate + " Print: " + document.GetFileName());
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            Console.WriteLine(FormatedDate + " Scan: " + document.GetFileName());
        }

        internal void ScanAndPrint()
        {
            throw new NotImplementedException();
        }
    }
}