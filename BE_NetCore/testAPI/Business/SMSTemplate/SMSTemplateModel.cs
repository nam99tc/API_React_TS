using System.ComponentModel.DataAnnotations;

namespace testAPI.Business.SMSTemplate
{
    public class SMSTemplateModel
    {
        public Guid Id { get; set; }
        public string SmsTemplateName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
