using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace OnBusyness.Model
{
    [Table("tblLoyalContactProductIntrest")]
    [Index(nameof(TblLoyalContactLoyalContactId), Name = "fk_tblLoyalContactProductIntrest_tblLoyalContact1_idx")]
    [Index(nameof(TblOurProductProductId), Name = "fk_tblLoyalContactProductIntrest_tblOurProduct1_idx")]
    [Index(nameof(LcpId), Name = "lcpID_UNIQUE", IsUnique = true)]
    public partial class TblLoyalContactProductIntrest
    {
        [Key]
        [Column("lcpID")]
        public int LcpId { get; set; }
        [Column("tblOurProduct_productID")]
        public int TblOurProductProductId { get; set; }
        [Column("tblLoyalContact_loyalContactID")]
        public int TblLoyalContactLoyalContactId { get; set; }

        [ForeignKey(nameof(TblLoyalContactLoyalContactId))]
        [InverseProperty(nameof(TblLoyalContact.TblLoyalContactProductIntrests))]
        public virtual TblLoyalContact TblLoyalContactLoyalContact { get; set; }
        [ForeignKey(nameof(TblOurProductProductId))]
        [InverseProperty(nameof(TblOurProduct.TblLoyalContactProductIntrests))]
        public virtual TblOurProduct TblOurProductProduct { get; set; }
    }
}
