using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace OnBusyness.Model
{
    [Table("tblFestival")]
    [Index(nameof(FestivalId), Name = "festivalID_UNIQUE", IsUnique = true)]
    public partial class TblFestival
    {
        [Key]
        [Column("festivalID")]
        public int FestivalId { get; set; }
        [Required]
        [Column("festivalName")]
        [StringLength(45)]
        public string FestivalName { get; set; }
        [Column("festivalDate", TypeName = "date")]
        public DateTime FestivalDate { get; set; }
    }
}
