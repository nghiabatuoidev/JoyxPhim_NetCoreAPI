using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

[Table("AccountRole")]
public partial class AccountRole
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("account_id")]
    public int? AccountId { get; set; }

    [Column("role_id")]
    public int? RoleId { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("AccountRoles")]
    public virtual Account? Account { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("AccountRoles")]
    public virtual Role? Role { get; set; }
}
