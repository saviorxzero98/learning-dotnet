using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreConsole.Entities
{
    [Table("Book")]
    public class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public double Price { get; set; }

        public bool HasStock { get; set; }

        public DateTime CreateDate { get; set; } 
    }
}
