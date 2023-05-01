using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace OnBusyness.Model
{
    [Table("tblHtmlTemplates")]
    [Index(nameof(TemplateId), Name = "templateID_UNIQUE", IsUnique = true)]
    public partial class TblHtmlTemplate
    {
        [Key]
        [Column("templateID")]
        public int TemplateId { get; set; }
        [Required]
        [Column("templateName")]
        [StringLength(45)]
        public string TemplateName { get; set; }
        [Column("templateType")]
        public int TemplateType { get; set; }
    }
}
