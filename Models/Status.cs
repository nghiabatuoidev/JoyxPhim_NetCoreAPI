using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class Status
{
    [Key]
    [Column("status_id")]
    public int StatusId { get; set; }

    [Column("value")]
    [StringLength(100)]
    public string? Value { get; set; }

    [InverseProperty("Status")]
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
