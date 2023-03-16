using System.ComponentModel.DataAnnotations;
using testAPI.Context;

namespace testAPI.Datatables
{
    public class SysDemoUser : BaseTable<SysDemoUser>
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Fullname { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        [Phone]
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
