using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

[Table("Role")]
public partial class Role
{
    [Key]
    [Column("role_id")]
    public int RoleId { get; set; }

    [Column("type")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Type { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<AccountRole> AccountRoles { get; set; } = new List<AccountRole>();

    [ForeignKey("RoleId")]
    [InverseProperty("Role")]
    public virtual Account RoleNavigation { get; set; } = null!;
}
