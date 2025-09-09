using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace CMCS.Web.Models
{
    public class Claim
    {
        public int Id { get; set; }

        [Required]
        public int LecturerId { get; set; }
        public Lecturer Lecturer { get; set; } = null!;

        [Range(0, 744)] // max hours in a month
        public decimal HoursWorked { get; set; }

        [Range(0.01, 5000)]
        public decimal HourlyRate { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Computed total (Hours * Rate)
        public decimal TotalAmount => Math.Round(HoursWorked * HourlyRate, 2);

        public ICollection<Document> Documents { get; set; } = new List<Document>();
        public ICollection<ClaimStatusHistory> StatusHistory { get; set; } = new List<ClaimStatusHistory>();
    }
}