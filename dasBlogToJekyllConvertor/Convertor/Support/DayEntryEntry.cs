using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Convertor.Support
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "urn:newtelligence-com:dasblog:runtime:data")]
    public partial class DayEntryEntry
    {
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string EntryId { get; set; }
        public object Description { get; set; }
        public string Title { get; set; }
        public string Categories { get; set; }
        public string Author { get; set; }
        public bool IsPublic { get; set; }
        public bool Syndicated { get; set; }
        public bool ShowOnFrontPage { get; set; }
        public bool AllowComments { get; set; }
        public object Attachments { get; set; }
        public object Crossposts { get; set; }
    }
}
