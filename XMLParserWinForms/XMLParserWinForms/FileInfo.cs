using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XMLParserWinForms
{
    class FileInfo
    {
        public string FilePath { get; set; }

        public string FileName {
            get
            {
                return System.IO.Path.GetFileName(this.FilePath);
            }
        }

        public bool Saved { get; set; }

        public XmlDocument Document { get; set; }

        public FileInfo(string path)
        {
            this.FilePath = path;
            this.Saved = true;
        }
    }
}
