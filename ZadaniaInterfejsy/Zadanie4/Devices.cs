using System;
using System.Threading;

namespace ver1
{
    public interface IDevice
    {
        enum State { on, off, standby };
        State GetState();
        void PowerOn() => SetState(State.on);
        void PowerOff() => SetState(State.off);
        void StandbyOn() => SetState(State.standby);
        void StandbyOff() => SetState(State.on);
        abstract protected void SetState(State state);
        int Counter { get; }
    }


    public interface IPrinter : IDevice
    {
        public static State PrinterState = State.off;
        public static int PrintCounter;
        void Print(in IDocument document)
        {
            if (PrinterState == State.on || PrinterState == State.standby)
            {
                PrinterState = State.on;
                if (PrintCounter > 0 && PrintCounter % 3 == 0)
                {
                    Console.WriteLine("The printer is cooling down, please wait five seconds");
                    Thread.Sleep(5000);
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss")} Print {PrintCounter}: {document.GetFileName()}");
                PrintCounter++;
                PrinterState = State.standby;
            }
        }
        public new State GetState() => PrinterState;
        public new State SetState(State state) => PrinterState = state;
    }

    public interface IScanner : IDevice
    {
        public static State ScannerState = State.off;
        public static int ScanCounter;
        public new State SetState(State state) => ScannerState = state;
        public new State GetState() => ScannerState;
        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            document = null;
            if (ScannerState == State.on || ScannerState == State.standby)
            {
                ScannerState = State.on;
                if (ScanCounter > 0 && ScanCounter % 2 == 0)
                {
                    Console.WriteLine("The scanner is cooling down, please wait five seconds");
                    Thread.Sleep(5000);
                }

                string fileType = "";
                switch (formatType)
                {
                    case IDocument.FormatType.JPG:
                        fileType = $"ImageScan{ScanCounter}.jpg";
                        document = new ImageDocument(fileType);
                        break;
                    case IDocument.FormatType.PDF:
                        fileType = $"PDFScan{ScanCounter}.pdf";
                        document = new PDFDocument(fileType);
                        break;
                    case IDocument.FormatType.TXT:
                        fileType = $"TextScan{ScanCounter}.txt";
                        document = new TextDocument(fileType);
                        break;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss")} Scan: {fileType}");
                ScanCounter++;
                ScannerState = State.standby;
            }
        }
    }
}