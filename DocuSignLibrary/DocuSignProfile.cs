using DocuSign.eSign.Api;
using DocuSign.eSign.Model;
using DocuSignLibrary.Models;
using System;
using System.Collections.Generic;

namespace DocuSignLibrary
{
    public class DocuSignProfile
    {
        public DocuSignProfile(Credentials credentials)
        {
            this.DocuSignCredentials = credentials;
        }

        public Credentials DocuSignCredentials { get; set; }

        public IEnumerable<Envelope> ListEnvelopes(int numberOfItems)
        {
            var docuSignClient = new DocuSignClient(this.DocuSignCredentials);
            var accountId = docuSignClient.AccountId;

            var fromDate = DateTime.UtcNow;
            fromDate = fromDate.AddDays(-30);
            string fromDateStr = fromDate.ToString("o");

            // set a filter for the envelopes we want returned using the fromDate and count properties
            var options = new EnvelopesApi.ListStatusChangesOptions()
            {
                count = numberOfItems.ToString(),
                fromDate = fromDateStr
            };

            EnvelopesApi envelopesApi = new EnvelopesApi();
            return envelopesApi.ListStatusChanges(accountId, options).Envelopes;
        }
    }
}
