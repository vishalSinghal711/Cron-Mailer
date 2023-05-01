using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace OnBusyness.Model
{
    [Table("tblContacts")]
    [Index(nameof(ContactEmail), Name = "contactEmail_UNIQUE", IsUnique = true)]
    [Index(nameof(ContactId), Name = "contactID_UNIQUE", IsUnique = true)]
    [Index(nameof(ContactNumber), Name = "contactNumber_UNIQUE", IsUnique = true)]
    public partial class TblContact
    {
        public TblContact()
        {
            TblContactProductInterests = new HashSet<TblContactProductInterest>();
        }

        [Key]
        [Column("contactID")]
        public int ContactId { get; set; }
        [Column("contactEmail")]
        [StringLength(100)]
        public string ContactEmail { get; set; }
        [Column("contactNumber")]
        [StringLength(10)]
        public string ContactNumber { get; set; }
        [Required]
        [Column("contactName")]
        [StringLength(45)]
        public string ContactName { get; set; }
        [Column("isDeleted")]
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(TblContactProductInterest.TblContactsContact))]
        public virtual ICollection<TblContactProductInterest> TblContactProductInterests { get; set; }
    }
}
