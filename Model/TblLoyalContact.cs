using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace OnBusyness.Model
{
    [Table("tblLoyalContact")]
    [Index(nameof(LoyalContactId), Name = "loyalContactID_UNIQUE", IsUnique = true)]
    public partial class TblLoyalContact
    {
        public TblLoyalContact()
        {
            TblLoyalContactProductIntrests = new HashSet<TblLoyalContactProductIntrest>();
        }

        [Key]
        [Column("loyalContactID")]
        public int LoyalContactId { get; set; }
        [Column("contactNum")]
        [StringLength(10)]
        public string ContactNum { get; set; }
        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [Column("loyalContactName")]
        [StringLength(45)]
        public string LoyalContactName { get; set; }
        [Column("isDeleted")]
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(TblLoyalContactProductIntrest.TblLoyalContactLoyalContact))]
        public virtual ICollection<TblLoyalContactProductIntrest> TblLoyalContactProductIntrests { get; set; }
    }
}
