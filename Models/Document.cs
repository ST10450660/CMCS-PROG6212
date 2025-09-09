using System;
using System.ComponentModel.DataAnnotations;

namespace CMCS.Web.Models
{
    public class Document
    {
        public int Id { get; set; }

        [Required]
        public int ClaimId { get; set; }
        public Claim Claim { get; set; } = null!;

        [Required, StringLength(260)]
        public string OriginalFileName { get; set; } = string.Empty;

        [Required, StringLength(300)]
        public string StoredFileName { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string ContentType { get; set; } = string.Empty;

        public long SizeBytes { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}