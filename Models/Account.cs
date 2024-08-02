using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class Account
{
    [Key]
    [Column("account_id")]
    public int AccountId { get; set; }

    [Column("email")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("password")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Password { get; set; }

    [Column("phone")]
    public int? Phone { get; set; }

    [Column("picture_url")]
    [StringLength(100)]
    [Unicode(false)]
    public string? PictureUrl { get; set; }

    [Column("full_name")]
    [StringLength(100)]
    public string? FullName { get; set; }

    [Column("firstname")]
    [StringLength(50)]
    public string? Firstname { get; set; }

    [Column("lastname")]
    [StringLength(50)]
    public string? Lastname { get; set; }

    [Column("google_id")]
    [StringLength(100)]
    [Unicode(false)]
    public string? GoogleId { get; set; }

    [Column("facebook_id")]
    [StringLength(100)]
    [Unicode(false)]
    public string? FacebookId { get; set; }

    [Column("created", TypeName = "datetime")]
    public DateTime? Created { get; set; }

    [Column("modified", TypeName = "datetime")]
    public DateTime? Modified { get; set; }

    [Column("role")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Role { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Follow> Follows { get; set; } = new List<Follow>();
}
