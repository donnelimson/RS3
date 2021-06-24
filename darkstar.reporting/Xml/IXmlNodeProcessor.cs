//作者：李维
//创建时间：2010-09-03
using System.Xml;

namespace darkstar.reporting.Xml
{
    internal interface IXmlNodeProcessor
    {
        void ProcessNode(XmlNode node);
    }
}