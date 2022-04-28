using System;
using ver1;

namespace Zadanie_3
{
    public class MultifunctionalDevice : BaseDevice, IMultifunctionalDevice
    {
        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;
        public int SendCounter { get; private set; } = 0;
        public string DateFormat = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
        private Printer Printer;
        private Scanner Scanner;

        public MultifunctionalDevice(Printer printer, Scanner scanner)
        {
            this.Printer = printer;
            this.Scanner = scanner;
        }

        public void Print(in IDocument document)
        {
            if (this.GetState() == IDevice.State.off)
                return;

            bool disable = false;
            if (this.Printer.GetState() == IDevice.State.off)
            {
                disable = true;
                this.Printer.PowerOn();
            }

            ++this.PrintCounter;
            this.Printer.Print(in document);

            if (disable)
                this.Printer.PowerOff();
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            if (this.GetState() == IDevice.State.off)
            {
                document = null;
                return;
            }

            bool disable = false;
            if (this.Scanner.GetState() == IDevice.State.off)
            {
                disable = true;
                this.Scanner.PowerOn();
            }

            ++this.ScanCounter;
            this.Scanner.Scan(out document, formatType);

            if (disable)
                this.Scanner.PowerOff();
        }

        public void Send(in IDocument document)
        {
            if (GetState() == IDevice.State.off)
                return;
            Console.WriteLine("{0} Send: {1}", DateFormat, document.GetFileName());
            ++SendCounter;
        }
    }
}