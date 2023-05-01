using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace OnBusyness.Model
{
    [Table("tblOurProduct")]
    [Index(nameof(ProductId), Name = "productID_UNIQUE", IsUnique = true)]
    public partial class TblOurProduct
    {
        public TblOurProduct()
        {
            TblContactProductInterests = new HashSet<TblContactProductInterest>();
            TblLoyalContactProductIntrests = new HashSet<TblLoyalContactProductIntrest>();
            TblProductBullets = new HashSet<TblProductBullet>();
        }

        [Key]
        [Column("productID")]
        public int ProductId { get; set; }
        [Required]
        [Column("productName")]
        [StringLength(500)]
        public string ProductName { get; set; }
        [Column("isDeleted")]
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(TblContactProductInterest.TblOurProductProduct))]
        public virtual ICollection<TblContactProductInterest> TblContactProductInterests { get; set; }
        [InverseProperty(nameof(TblLoyalContactProductIntrest.TblOurProductProduct))]
        public virtual ICollection<TblLoyalContactProductIntrest> TblLoyalContactProductIntrests { get; set; }
        [InverseProperty(nameof(TblProductBullet.TblOurProductProduct))]
        public virtual ICollection<TblProductBullet> TblProductBullets { get; set; }
    }
}
