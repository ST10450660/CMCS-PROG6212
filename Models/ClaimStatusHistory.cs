using System;
using System.ComponentModel.DataAnnotations;

namespace CMCS.Web.Models
{
    public class ClaimStatusHistory
    {
        public int Id { get; set; }

        [Required]
        public int ClaimId { get; set; }
        public Claim Claim { get; set; } = null!;

        public ClaimStatus FromStatus { get; set; }
        public ClaimStatus ToStatus { get; set; }

        [StringLength(300)]
        public string? ActionedBy { get; set; } // e.g., Coordinator/Manager name

        [StringLength(500)]
        public string? Comment { get; set; }

        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    }
}