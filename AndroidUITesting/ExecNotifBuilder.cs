using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidUITesting
{
    class ExecNotifBuilder
    {
        public int Passed { get; set; }
        public int Failed { get; set; }
        public List<string> TestsFailedName { get; set; }
        public double time { get; set; }
    }
}