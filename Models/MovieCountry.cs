using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class MovieCountry
{
    [Key]
    [Column("_id")]
    public int Id { get; set; }

    [Column("country_id")]
    public int? CountryId { get; set; }

    [Column("movie_id")]
    public int? MovieId { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("MovieCountries")]
    public virtual Country? Country { get; set; }

    [ForeignKey("MovieId")]
    [InverseProperty("MovieCountries")]
    public virtual Movie? Movie { get; set; }
}
