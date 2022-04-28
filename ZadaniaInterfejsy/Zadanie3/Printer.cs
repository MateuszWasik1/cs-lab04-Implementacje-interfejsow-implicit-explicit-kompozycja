using System;
using ver1;

namespace Zadanie_3
{
    public class Printer : BaseDevice, IPrinter
    {
        public int PrintCounter { get; private set; } = 0;
        public string DateFormat = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");

        public void Print(in IDocument document)
        {
            if (GetState() == IDevice.State.off)
                return;
            Console.WriteLine("{0} Print: {1}", DateFormat, document.GetFileName());
            ++PrintCounter;
        }
    }
}