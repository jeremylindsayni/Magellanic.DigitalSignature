using DocuSign.eSign.Api;
using DocuSign.eSign.Model;
using DocuSignLibrary.Models;
using System.Linq;

namespace DocuSignLibrary
{
    public class DocuSignDocumentGenerator
    {
        public DocuSignDocumentGenerator(Credentials credentials, EmailTemplate emailTemplate, TemplateFacade docuSignTemplate)
        {
            this.DocuSignCredentials = credentials;
            this.EmailTemplate = emailTemplate;
            this.DocuSignTemplate = docuSignTemplate;
        }

        public Credentials DocuSignCredentials { get; set; }

        public EmailTemplate EmailTemplate { get; set; }

        public TemplateFacade DocuSignTemplate { get; set; }
        
        public void GenerateDocument(string name, string email)
        {
            var docuSignClient = new DocuSignClient(this.DocuSignCredentials);
            var accountId = docuSignClient.AccountId;

            // assign recipient to template role by setting name, email, and role name.  Note that the
            // template role name must match the placeholder role name saved in your account template.  
            var templateRoles = this.DocuSignTemplate.TemplateRoleNames.Select(m => new TemplateRole
            {
                Email = email,
                Name = name,
                RoleName = m
            }).ToList();

            // create a new envelope which we will use to send the signature request
            var envelope = new EnvelopeDefinition
            {
                EmailSubject = this.EmailTemplate.Subject,
                EmailBlurb = this.EmailTemplate.MessageBody,
                TemplateId = this.DocuSignTemplate.TemplateId,
                TemplateRoles = templateRoles,
                Status = "sent"
            };

            // |EnvelopesApi| contains methods related to creating and sending Envelopes (aka signature requests)
            var envelopesApi = new EnvelopesApi();
            var envelopeSummary = envelopesApi.CreateEnvelope(accountId, envelope);
        }
    }
}
