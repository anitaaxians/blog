using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogManagementAPI.Data.Model
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        public bool IsActive { get; set; } = true; // Soft delete

        public ICollection<Blog>? Blogs { get; set; } = new List<Blog>(); // 1-to-Many relation
    }
}
