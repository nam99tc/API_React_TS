using System.ComponentModel.DataAnnotations;
using testAPI.Context;

namespace testAPI.Datatables
{
    public class SysSMSTemplate : BaseTable
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string SmsTemplateName { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
