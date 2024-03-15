using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Enums;

namespace ProductService.Data.Entities;

// public record Product(Guid Id, string Name, Guid Sku);

public class Product
{
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid Sku { get; set; }
    
    public Boolean Active { get; set; }
    public DateTime? DisabledDate { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public ProductCategory ProductCategory { get; set; }
}