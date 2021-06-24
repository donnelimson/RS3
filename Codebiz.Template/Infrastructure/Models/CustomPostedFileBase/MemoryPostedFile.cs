using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure.CustomPostedFileBase
{
    public class MemoryPostedFile : HttpPostedFileBase
    {
        private readonly byte[] FileBytes;
        private string FilePath;
        private string contentType;

        public MemoryPostedFile(byte[] fileBytes, string path, string contentType, string fileName = null)
        {
            this.FilePath = path;
            this.contentType = contentType;
            this.FileBytes = fileBytes;
            this._FileName = fileName;
            this._Stream = new MemoryStream(fileBytes);
        }

        public override int ContentLength
        {
            get { return FileBytes.Length; }
        }
        public override string ContentType
        {
            get { return contentType; }
        }
        public override String FileName
        {
            get { return _FileName; }
        }
        private String _FileName;
        public override Stream InputStream
        {
            get
            {
                if (_Stream == null)
                {
                    _Stream = new FileStream(_FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                }
                return _Stream;
            }
        }
        private Stream _Stream;
        public override void SaveAs(string filename)
        {
            System.IO.File.WriteAllBytes(filename, System.IO.File.ReadAllBytes(FilePath));
        }
    }
}
