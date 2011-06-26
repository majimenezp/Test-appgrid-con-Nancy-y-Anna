using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;

namespace TestLibreria.NancyAnna
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = x => View["Index.cshtml"];
        }
    }
}
