using System.IO;
using System.Xml;

namespace darkstar.reporting.OpenDocument.Xml
{
    public abstract class AbstractOdfXmlDocument : XmlDocument
    {
        private readonly OdfNamespaceManager _nsmanager;

        public AbstractOdfXmlDocument(Stream stream) : base()
        {
            this.Load(stream);
            _nsmanager = new OdfNamespaceManager(this.NameTable);
        }

        public OdfNamespaceManager NamespaceManager => _nsmanager;
    }
}