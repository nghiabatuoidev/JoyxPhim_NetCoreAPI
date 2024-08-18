using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

[Table("Genre")]
public partial class Genre
{
    [Key]
    [Column("type_id")]
    public int TypeId { get; set; }

    [Column("value")]
    [StringLength(100)]
    public string? Value { get; set; }

    [InverseProperty("Type")]
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
