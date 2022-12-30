using Atlassian.Jira;
using Atlassian.Jira.Remote;
using JiraExport;
using Migration.Common.Log;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace JiraExport
{
    public class JiraServiceWrapper : IJiraServiceWrapper
    {
        private readonly Jira _jira;

        public IIssueFieldService Fields => _jira.Fields;
        public IIssueService Issues => _jira.Issues;
        public IIssueLinkService Links => _jira.Links;
        public IJiraRestClient RestClient => _jira.RestClient;
        public IJiraUserService Users => _jira.Users;

        public JiraServiceWrapper(JiraSettings jiraSettings)
        {
            try
            {
                Logger.Log(LogLevel.Info, "Connecting to Jira...");

                _jira = Jira.CreateRestClient(jiraSettings.Url, jiraSettings.UserID, jiraSettings.Pass);
                _jira.RestClient.RestSharpClient.AddDefaultHeader("X-Atlassian-Token", "no-check");
                _jira.RestClient.RestSharpClient.AddDefaultHeaders(ParseHeaders(jiraSettings.Headers));
                if (jiraSettings.UsingJiraCloud)
                    _jira.RestClient.Settings.EnableUserPrivacyMode = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Could not connect to Jira!", LogLevel.Critical);
            }
        }

        private Dictionary<string, string> ParseHeaders(List<string> headers)
        {
            var headerDict = new Dictionary<string, string>();
            foreach (var header in headers)
            {
                var keyValue = header.Split(':');
                var key = keyValue[0].Trim();
                var value = keyValue[1].Trim();
                headerDict.Add(key, value);
            }
            return headerDict;
        }
    }
}
