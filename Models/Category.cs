using System;
using System.ComponentModel.DataAnnotations;

namespace ContactPlus.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Category Name")]
        public string Name { get; set; } = string.Empty;

        public virtual ApplicationUser? ApplicationUser { get; set; }
        public ICollection<Contact> Contacts { get; set; } = new HashSet<Contact>();
    }
}

