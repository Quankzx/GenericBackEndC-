using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Services.Library.Entities;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
    public int CreatedBy { get; set; } 
    public int ModifiedBy { get; set; } 
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
}
