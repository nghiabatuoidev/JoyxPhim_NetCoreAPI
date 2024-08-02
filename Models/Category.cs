using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class Category
{
    [Key]
    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("category_name")]
    [StringLength(200)]
    public string? CategoryName { get; set; }

    [Column("slug")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Slug { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<MovieCategory> MovieCategories { get; set; } = new List<MovieCategory>();
}
