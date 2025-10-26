using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Item
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public int Price { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public int Ram { get; set; }
    
    public int Storage { get; set; }

    // Parameterless constructor for Entity Framework
    public Item()
    {
        CreatedAt = DateTime.Now;
    }

    public Item(string name, string description, int price, DateTime createdAt)
    {
        this.Name = name;
        this.Description = description;
        this.Price = price;
        this.CreatedAt = createdAt;
    }
}