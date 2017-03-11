using System.Collections.Generic;

namespace DocuSignLibrary.Models
{
    public class TemplateFacade
    {
        public TemplateFacade(string templateId, IList<string> templateRoleNames)
        {
            this.TemplateId = templateId;
            this.TemplateRoleNames = templateRoleNames;
        }

        public IList<string> TemplateRoleNames { get; set; }

        public string TemplateId { get; set; }
    }
}
