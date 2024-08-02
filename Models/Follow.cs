using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class Follow
{
    [Key]
    [Column("follow_id")]
    public int FollowId { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("movie_id")]
    public int? MovieId { get; set; }

    [Column("created", TypeName = "datetime")]
    public DateTime? Created { get; set; }

    [Column("modified", TypeName = "datetime")]
    public DateTime? Modified { get; set; }

    [ForeignKey("MovieId")]
    [InverseProperty("Follows")]
    public virtual Movie? Movie { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Follows")]
    public virtual Account? User { get; set; }
}
