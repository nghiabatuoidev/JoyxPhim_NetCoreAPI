using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class EpisodeServer
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("server_id")]
    public int? ServerId { get; set; }

    [Column("episode_id")]
    public int? EpisodeId { get; set; }

    [Column("link_embed")]
    [StringLength(200)]
    [Unicode(false)]
    public string? LinkEmbed { get; set; }

    [ForeignKey("EpisodeId")]
    [InverseProperty("EpisodeServers")]
    public virtual Episode? Episode { get; set; }

    [ForeignKey("ServerId")]
    [InverseProperty("EpisodeServers")]
    public virtual Server? Server { get; set; }
}
