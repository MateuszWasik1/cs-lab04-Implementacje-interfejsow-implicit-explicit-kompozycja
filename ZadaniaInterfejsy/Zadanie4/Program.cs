using System;
using Zadanie_4;

namespace ver1
{
    class Program
    {
        static void Main(string[] args)
        {
            Copier copier = new Copier();
            Console.WriteLine($"The current status of the device: {copier.GetState()}");
            ((IDevice)copier).PowerOn();               
            Console.WriteLine($"The current status of the device: {copier.GetState()}");

            var doc = new PDFDocument("PDF.pdf");
            ((IPrinter)copier).Print(doc);
            ((IPrinter)copier).Print(doc);
            ((IPrinter)copier).Print(doc);
            ((IPrinter)copier).Print(doc);
            ((IPrinter)copier).Print(doc);
            ((IPrinter)copier).Print(doc);
            ((IPrinter)copier).Print(doc);
            IDocument doc1;
            IDocument doc2;
            IDocument doc3;
            ((IScanner)copier).Scan(out doc1, IDocument.FormatType.TXT);
            ((IScanner)copier).Scan(out doc2, IDocument.FormatType.PDF);
            ((IScanner)copier).Scan(out doc3, IDocument.FormatType.JPG);
            ((IScanner)copier).Scan(out doc1, IDocument.FormatType.TXT);
            ((IScanner)copier).Scan(out doc2, IDocument.FormatType.PDF);
            ((IScanner)copier).Scan(out doc3, IDocument.FormatType.JPG);
            ((IScanner)copier).Scan(out doc3, IDocument.FormatType.TXT);
            copier.GetCounter();
            Console.WriteLine($"The current status of the device: {copier.GetState()}");
            ((IDevice)copier).PowerOff();              
            Console.WriteLine($"The current status of the device: {copier.GetState()}");
        }
    }
}