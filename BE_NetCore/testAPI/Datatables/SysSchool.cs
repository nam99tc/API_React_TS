using System.ComponentModel.DataAnnotations;
using testAPI.Context;

namespace testAPI.Datatables
{
    public class SysSchool : BaseTable
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; } = string.Empty;
        [Required]
        public string SchoolName { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; }
        public string Phone { get; set; }
        public Guid DistrictId { get; set; }
        public Guid ProvinceId { get; set; }
        public Guid WardId { get; set; }
    }
}
