using System;
using System.Collections.Generic;

namespace Race_PRN232_Project.Models
{
    public partial class Image
    {
        public int ImageId { get; set; }
        public int RaceId { get; set; }
        public string Url { get; set; } = null!;
        public string? Caption { get; set; }
        public DateTime? UploadedAt { get; set; }

        public virtual Race Race { get; set; } = null!;
    }
}
