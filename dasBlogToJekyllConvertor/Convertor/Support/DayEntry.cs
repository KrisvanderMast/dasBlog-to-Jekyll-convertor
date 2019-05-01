using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Convertor.Support
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "urn:newtelligence-com:dasblog:runtime:data")]
    [XmlRoot(Namespace = "urn:newtelligence-com:dasblog:runtime:data", IsNullable = false)]
    public partial class DayEntry
    {
        public DateTime Date { get; set; }

        [XmlArrayItem("Entry", IsNullable = false)]
        public DayEntryEntry[] Entries { get; set; }
    }
}
