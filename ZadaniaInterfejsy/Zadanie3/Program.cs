using System;
using Zadanie_3;

namespace ver1
{
    class Program
    {
        static void Main(string[] args)
        {
            var copier = new Copier(new Printer(), new Scanner());
            copier.PowerOn();
            IDocument doc1 = new TextDocument("text.txt");
            copier.Print(in doc1);

            IDocument doc2;
            copier.Scan(out doc2);

            copier.ScanAndPrint();

            var multifunctional = new MultifunctionalDevice(new Printer(), new Scanner());
            multifunctional.PowerOn();
            IDocument doc3 = new ImageDocument("image.jpg");
            multifunctional.Print(in doc3);

            IDocument doc4;
            multifunctional.Scan(out doc4);

            multifunctional.Send(doc1);

            Console.WriteLine("\nCopier\n");
            Console.WriteLine(copier.Counter);
            Console.WriteLine(copier.PrintCounter);
            Console.WriteLine(copier.ScanCounter);

            Console.WriteLine("\nMultifunctional Device\n");
            Console.WriteLine(multifunctional.ScanCounter);
            Console.WriteLine(multifunctional.PrintCounter);
            Console.WriteLine(multifunctional.SendCounter);
            Console.WriteLine(multifunctional.Counter);
        }
    }
}