namespace testAPI.Business.EmailTemplate
{
    public class EmailTemplateModel
    {
        public Guid Id { get; set; }
        public string EmailTemplateName { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Attactment { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
