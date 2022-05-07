using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadanie_2;
using System;
using System.IO;
using ver1;

namespace Zadanie2UnitTests
{
    public class ConsoleRedirectionToStringWriter : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleRedirectionToStringWriter()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOutput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }

    [TestClass]
    public class UnitTestMultifunctionalDevice
    {
        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOff()
        {
            var multiFDev = new MultifunctionalDevice();
            multiFDev.PowerOff();

            Assert.AreEqual(IDevice.State.off, multiFDev.GetState());
        }

        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOn()
        {
            var multiFDev = new MultifunctionalDevice();
            multiFDev.PowerOn();

            Assert.AreEqual(IDevice.State.on, multiFDev.GetState());
        }

        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOn()
        {
            var multiFDev = new MultifunctionalDevice();
            multiFDev.PowerOn();

            var consoleOut = Console.Out;
            consoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("PDF.pdf");
                multiFDev.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(consoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOff()
        {
            var multiFDev = new MultifunctionalDevice();
            multiFDev.PowerOff();

            var consoleOut = Console.Out;
            consoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("PDF.pdf");
                multiFDev.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(consoleOut, Console.Out);
        }


        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOn()
        {
            var multiFDev = new MultifunctionalDevice();
            multiFDev.PowerOn();

            var consoleOut = Console.Out;
            consoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multiFDev.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(consoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOff()
        {
            var multiFDev = new MultifunctionalDevice();
            multiFDev.PowerOff();

            var consoleOut = Console.Out;
            consoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multiFDev.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(consoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_Scan_FormatTypeDocument()
        {
            var multiFDev = new MultifunctionalDevice();
            multiFDev.PowerOn();

            var consoleOut = Console.Out;
            consoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multiFDev.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("ImageScan1.jpg"));

                multiFDev.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("TextScan2.txt"));

                multiFDev.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("PDFScan3.pdf"));

            }
            Assert.AreEqual(consoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_ScanAndPrint_DeviceOn()
        {
            var multiFDev = new MultifunctionalDevice();
            multiFDev.PowerOn();

            IDocument docFax = new PDFDocument("fax.pdf");

            var consoleOut = Console.Out;
            consoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multiFDev.Send(in docFax);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("fax.pdf"));
            }
            Assert.AreEqual(consoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_ScanAndPrint_DeviceOff()
        {
            var multiFDev = new MultifunctionalDevice();
            multiFDev.PowerOff();

            IDocument docFax = new PDFDocument("fax.pdf");

            var consoleOut = Console.Out;
            consoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multiFDev.Send(in docFax);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("fax.pdf"));
            }
            Assert.AreEqual(consoleOut, Console.Out);
        }
    }
}