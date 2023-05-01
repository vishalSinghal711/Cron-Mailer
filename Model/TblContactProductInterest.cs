using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace OnBusyness.Model
{
    [Table("tblContactProductInterest")]
    [Index(nameof(ContactProductId), Name = "contactProductID_UNIQUE", IsUnique = true)]
    [Index(nameof(TblContactsContactId), Name = "fk_tblContactProductInterest_tblContacts1_idx")]
    [Index(nameof(TblOurProductProductId), Name = "fk_tblContactProductInterest_tblOurProduct1_idx")]
    public partial class TblContactProductInterest
    {
        [Key]
        [Column("contactProductID")]
        public int ContactProductId { get; set; }
        [Column("tblOurProduct_productID")]
        public int TblOurProductProductId { get; set; }
        [Column("tblContacts_contactID")]
        public int TblContactsContactId { get; set; }

        [ForeignKey(nameof(TblContactsContactId))]
        [InverseProperty(nameof(TblContact.TblContactProductInterests))]
        public virtual TblContact TblContactsContact { get; set; }
        [ForeignKey(nameof(TblOurProductProductId))]
        [InverseProperty(nameof(TblOurProduct.TblContactProductInterests))]
        public virtual TblOurProduct TblOurProductProduct { get; set; }
    }
}
