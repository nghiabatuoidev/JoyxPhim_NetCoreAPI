using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class Server
{
    [Key]
    [Column("server_id")]
    public int ServerId { get; set; }

    [Column("value")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Value { get; set; }

    [InverseProperty("Server")]
    public virtual ICollection<EpisodeServer> EpisodeServers { get; set; } = new List<EpisodeServer>();
}
