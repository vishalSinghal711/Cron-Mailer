using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace OnBusyness.Model
{
    [Table("tblProductBullets")]
    [Index(nameof(TblOurProductProductId), Name = "fk_tblProductBullets_tblOurProduct_idx")]
    [Index(nameof(ProductBulletId), Name = "productBulletID_UNIQUE", IsUnique = true)]
    public partial class TblProductBullet
    {
        [Key]
        [Column("productBulletID")]
        public int ProductBulletId { get; set; }
        [Required]
        [Column("bulletName")]
        [StringLength(45)]
        public string BulletName { get; set; }
        [Column("bulletDescription")]
        [StringLength(100)]
        public string BulletDescription { get; set; }
        [Column("isDeleted")]
        public bool IsDeleted { get; set; }
        [Column("tblOurProduct_productID")]
        public int TblOurProductProductId { get; set; }

        [ForeignKey(nameof(TblOurProductProductId))]
        [InverseProperty(nameof(TblOurProduct.TblProductBullets))]
        public virtual TblOurProduct TblOurProductProduct { get; set; }
    }
}
