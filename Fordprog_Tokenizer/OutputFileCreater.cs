using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Principal;

namespace Fordprog_Tokenizer
{
    class OutputFileCreater
    {

        public string outputFileName { get; private set; }

        private StreamWriter sw;

        public OutputFileCreater(string FileName)
        {
            outputFileName = FileName == "" ? "Program_output.cs" : (FileName + "_output.cs");
            sw = new StreamWriter(outputFileName);
        }

        public void addItemToString(string token)
        {
            sw.WriteLine(token);
        }

        public void fileCloser()
        {
            sw.Close();
        }

    }
}
