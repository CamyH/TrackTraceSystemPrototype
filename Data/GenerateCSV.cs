using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTrace.Data
{
    class GenerateCSV
    {
        private StreamWriter contact = new StreamWriter(File.OpenWrite(@"D:\C# Coursework\TrackTrace\AddNameHere.csv"));
        private StreamWriter visit = new StreamWriter(File.OpenWrite(@"D:\C# Coursework\TrackTrace\AddNameHere.csv"));

        public void GenerateContactCSV()
        {

        }
        public void GenerateVisitCSV()
        {

        }
    }
}
