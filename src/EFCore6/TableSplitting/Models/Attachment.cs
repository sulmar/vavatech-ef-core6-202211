﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace TableSplitting.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }

    public class Attachment : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }

        public override string ToString() => $"{Title} {Description} {FileName} {ContentType} {Content.Length}";

    }

    public class AttachmentHeader : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public AttachmentContent Content { get; set; }

        public override string ToString() => $"{Title} {Description} {FileName}";
    }

    public class AttachmentContent : BaseEntity
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }

        public override string ToString() => $"{FileName} {ContentType} {Content.Length}";
    }


}
