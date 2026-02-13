using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElectroStore.Core.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
