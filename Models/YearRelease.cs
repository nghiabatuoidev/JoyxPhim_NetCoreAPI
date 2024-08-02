using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class YearRelease
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("number_year")]
    public int? NumberYear { get; set; }

    [InverseProperty("YearRelease")]
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
