using System.ComponentModel.DataAnnotations;
using testAPI.Context;

namespace testAPI.Datatables
{
    public class SysEmailTemplate : BaseTable
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string EmailTemplateName { get; set; } = string.Empty;
        [Required]
        public string Subject { get; set; } = string.Empty;
        [Required]
        public string Body { get; set; } = string.Empty;
        public string Attactment { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
