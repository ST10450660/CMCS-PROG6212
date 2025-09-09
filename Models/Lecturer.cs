using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace CMCS.Web.Models
{
    public class Lecturer
    {
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress, StringLength(200)]
        public string Email { get; set; } = string.Empty;

        public ICollection<Claim> Claims { get; set; } = new List<Claim>();
    }
}