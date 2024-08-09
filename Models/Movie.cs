using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class Movie
{
    [Key]
    [Column("movie_id")]
    public int MovieId { get; set; }

    [Column("movie_name")]
    [StringLength(200)]
    public string? MovieName { get; set; }

    [Column("movie_origin_name")]
    [StringLength(200)]
    [Unicode(false)]
    public string? MovieOriginName { get; set; }

    [Column("slug")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Slug { get; set; }

    [Column("movie_content", TypeName = "ntext")]
    public string? MovieContent { get; set; }

    [Column("thumb_url")]
    [Unicode(false)]
    public string? ThumbUrl { get; set; }

    [Column("type_id")]
    public int? TypeId { get; set; }

    [Column("status_id")]
    public int? StatusId { get; set; }

    [Column("is_sub_docquyen")]
    public bool? IsSubDocquyen { get; set; }

    [Column("is_chieurap")]
    public bool? IsChieurap { get; set; }

    [Column("is_trending")]
    public bool? IsTrending { get; set; }

    [Column("movie_time")]
    [StringLength(200)]
    public string? MovieTime { get; set; }

    [Column("episode_current")]
    [StringLength(200)]
    public string? EpisodeCurrent { get; set; }

    [Column("episode_total")]
    [StringLength(200)]
    public string? EpisodeTotal { get; set; }

    [Column("quality")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Quality { get; set; }

    [Column("lang_id")]
    public int? LangId { get; set; }

    [Column("year_release_id")]
    public int? YearReleaseId { get; set; }

    [Column("view_number")]
    public int? ViewNumber { get; set; }

    [Column("trailer_url")]
    [StringLength(500)]
    [Unicode(false)]
    public string? TrailerUrl { get; set; }

    [Column("actor_name")]
    [StringLength(100)]
    public string? ActorName { get; set; }

    [Column("director_name")]
    [StringLength(100)]
    public string? DirectorName { get; set; }

    [Column("created", TypeName = "datetime")]
    public DateTime? Created { get; set; }

    [Column("modified", TypeName = "datetime")]
    public DateTime? Modified { get; set; }

    [InverseProperty("Movie")]
    public virtual ICollection<Episode> Episodes { get; set; } = new List<Episode>();

    [InverseProperty("Movie")]
    public virtual ICollection<Follow> Follows { get; set; } = new List<Follow>();

    [ForeignKey("LangId")]
    [InverseProperty("Movies")]
    public virtual Lang? Lang { get; set; }

    [InverseProperty("Movie")]
    public virtual ICollection<MovieCategory> MovieCategories { get; set; } = new List<MovieCategory>();

    [InverseProperty("Movie")]
    public virtual ICollection<MovieCountry> MovieCountries { get; set; } = new List<MovieCountry>();

    [ForeignKey("StatusId")]
    [InverseProperty("Movies")]
    public virtual Status? Status { get; set; }

    [ForeignKey("TypeId")]
    [InverseProperty("Movies")]
    public virtual Type? Type { get; set; }

    [ForeignKey("YearReleaseId")]
    [InverseProperty("Movies")]
    public virtual YearRelease? YearRelease { get; set; }
}
