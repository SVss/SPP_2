using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XMLParserWinForms
{
    class FileInfo
    {
        public string FileName { get; set; }

        public bool Saved { get; set; }

        public XmlDocument Document { get; set; }

        public FileInfo(string fileName)
        {
            this.FileName = fileName;
            this.Saved = false;
        }
    }
}
