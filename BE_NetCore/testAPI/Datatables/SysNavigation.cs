using System.ComponentModel.DataAnnotations;
using testAPI.Context;

namespace testAPI.Datatables
{
    public class SysNavigation : BaseTable
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        [Required]
        public string Code { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Url { get; set; } = "#";
        [Required]
        public string IconClass { get; set; } = string.Empty;
        public int Order { get; set; }
        public bool HasChild { get; set; }
        public string Resource { get; set; } = string.Empty;
        public string ComponentPath { get; set; } = string.Empty;
    }
}
