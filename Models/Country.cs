using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class Country
{
    [Key]
    [Column("country_id")]
    public int CountryId { get; set; }

    [Column("country_name")]
    [StringLength(200)]
    public string? CountryName { get; set; }

    [Column("slug")]
    [StringLength(200)]
    public string? Slug { get; set; }

    [InverseProperty("Country")]
    public virtual ICollection<MovieCountry> MovieCountries { get; set; } = new List<MovieCountry>();
}
