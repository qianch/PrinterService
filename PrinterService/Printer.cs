using Seagull.BarTender.Print;
using Seagull.BarTender.Print.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace PrinterService
{
    public class Printer
    {
        public Socket sk;

        public int port;

        public Thread skThread = null;

        public string  name ;

        public Engine engine = null;

        public LabelFormatDocument format = null;
    }

}

