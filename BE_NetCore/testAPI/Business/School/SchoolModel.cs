namespace testAPI.Business.School
{
    public class SchoolModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string SchoolName { get; set; } = string.Empty;
        public string Address { get; set; }
        public string Phone { get; set; }
        public Guid DistrictId { get; set; }
        public Guid ProvinceId { get; set; }
        public Guid WardId { get; set; }
    }
}
