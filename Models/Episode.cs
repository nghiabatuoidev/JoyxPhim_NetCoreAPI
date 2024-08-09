using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class Episode
{
    [Key]
    [Column("episode_id")]
    public int EpisodeId { get; set; }

    [Column("movie_id")]
    public int? MovieId { get; set; }

    [Column("name")]
    [StringLength(200)]
    public string? Name { get; set; }

    [Column("slug")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Slug { get; set; }

    [Column("episode_name")]
    [StringLength(200)]
    public string? EpisodeName { get; set; }

    [Column("created", TypeName = "datetime")]
    public DateTime? Created { get; set; }

    [Column("modified", TypeName = "datetime")]
    public DateTime? Modified { get; set; }

    [InverseProperty("Episode")]
    public virtual ICollection<EpisodeServer> EpisodeServers { get; set; } = new List<EpisodeServer>();

    [ForeignKey("MovieId")]
    [InverseProperty("Episodes")]
    public virtual Movie? Movie { get; set; }
}
