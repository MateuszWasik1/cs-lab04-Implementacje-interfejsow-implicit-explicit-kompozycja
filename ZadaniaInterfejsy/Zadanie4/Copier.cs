using System;
using ver1;
using static ver1.IDevice;

namespace Zadanie_4
{
    public class Copier : IPrinter, IScanner
    {
        protected State state = State.off;
        public int Counter { get; private set; }
        public State GetState()
        {
            if (state == State.off)
                return State.off;
            if (IScanner.ScannerState == State.standby && IPrinter.PrinterState == State.standby)
                return State.standby;
            return State.on;
        }

        void IDevice.SetState(State state)
        {
            if (this.state == State.off && state == State.on)
                Counter++;
            IScanner.ScannerState = state;
            IPrinter.PrinterState = state;
            this.state = state;
        }
        public void StandbyOn()
        {
            IScanner.ScannerState = State.standby;
            IPrinter.PrinterState = State.standby;
            this.state = State.standby;
        }
        public void StandbyOff()
        {
            IScanner.ScannerState = State.on;
            IPrinter.PrinterState = State.on;
            this.state = State.on;
        }
        public void GetCounter()
        {
            Console.WriteLine($"Number of scan: {IScanner.ScanCounter}");
            Console.WriteLine($"Number of print: {IPrinter.PrintCounter}");
            Console.WriteLine($"Number of copier: {Counter}");
        }
    }
}