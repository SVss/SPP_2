using System.Xml;

namespace XMLParserWinForms
{
    internal class FileInfo
    {
        public string FilePath { get; set; }

        public System.DateTime FileModificationTime { get; set; }

        public string FileName => System.IO.Path.GetFileName(FilePath);

        public bool Saved { get; set; }

        public XmlDocument Document { get; set; }

        public FileInfo(string path)
        {
            FilePath = path;
            FileModificationTime = (new System.IO.FileInfo(path)).LastWriteTime;
            Saved = true;
        }
    }
}
