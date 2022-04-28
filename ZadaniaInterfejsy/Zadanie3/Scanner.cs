using System;
using ver1;

namespace Zadanie_3
{
    public class Scanner : BaseDevice, IScanner
    {
        public int ScanCounter { get; private set; } = 0;
        public string DateFormat = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            if (GetState() == IDevice.State.off)
            {
                document = null;
                return;
            }

            ++ScanCounter;
            switch (formatType)
            {
                case IDocument.FormatType.JPG:
                    document = new ImageDocument($"ImageScan{ScanCounter}.jpg");
                    break;
                case IDocument.FormatType.PDF:
                    document = new PDFDocument($"PDFScan{ScanCounter}.pdf");
                    break;
                case IDocument.FormatType.TXT:
                    document = new TextDocument($"TextScan{ScanCounter}.txt");
                    break;
                default:
                    throw new ArgumentException("Undefined file type!");
            }

            Console.WriteLine("{0} Scan: {1}", DateFormat, document.GetFileName());
        }
    }
}