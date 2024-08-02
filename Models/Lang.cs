using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class Lang
{
    [Key]
    [Column("lang_id")]
    public int LangId { get; set; }

    [Column("value")]
    [StringLength(100)]
    public string? Value { get; set; }

    [InverseProperty("Lang")]
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
