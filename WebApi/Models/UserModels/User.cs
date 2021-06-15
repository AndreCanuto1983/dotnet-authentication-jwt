using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.AccessModels
{
    public class User : IdentityUser
    {
        public User()
        {
            CreationDate = DateTime.UtcNow;
            UpdateDate = CreationDate;
        }

        public string Name { get; set; }

        public string City { get; set; }

        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
