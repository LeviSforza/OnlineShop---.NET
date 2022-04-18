using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lista10.Models
{
    public class Category
    {
        [Required]
        public int CategoryID { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "Too long name!")]
        public string Name { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

    }
}
