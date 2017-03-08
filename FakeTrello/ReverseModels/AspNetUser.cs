using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FakeTrello.ReverseModels
{
    public class AspNetUser
    {
        [Key]
        [MaxLength(128)]
        public string Id { get; set; }

        public string Email { get; set; }

        [Required]
        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public string PhoneNumberConfirmed { get; set; }

        [Required]
        public bool TwoFactorEnabled { get; set; }

        public DateTime LockoutEndDateUTC { get; set;}

        [Required]
        public bool LockoutEnabled { get; set; }

        [Required]
        public int AccessFailedCount { get; set; }

        [Required]
        [MaxLength(256)]
        public string UserName { get; set; }

        public ICollection<AspNetRole> Roles { get; set; }


    }
}