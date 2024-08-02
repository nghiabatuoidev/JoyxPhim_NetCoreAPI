using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class MovieCategory
{
    [Key]
    [Column("_id")]
    public int Id { get; set; }

    [Column("movie_id")]
    public int? MovieId { get; set; }

    [Column("category_id")]
    public int? CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("MovieCategories")]
    public virtual Category? Category { get; set; }

    [ForeignKey("MovieId")]
    [InverseProperty("MovieCategories")]
    public virtual Movie? Movie { get; set; }
}
