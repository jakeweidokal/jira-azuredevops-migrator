﻿using NUnit.Framework;

using JiraExport;
using AutoFixture.AutoNSubstitute;
using AutoFixture;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Migration.Jira_Export.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class JiraCommandLineTests
    {
        // use auto fixture to help mock and instantiate with dummy data with nsubsitute. 
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoNSubstituteCustomization() { });
        }

        [Test]
        public void When_calling_execute_with_empty_args_Then_an_exception_is_thrown()
        {
            string[] args = null;

            var sut = new JiraCommandLine(args);


            Assert.That(() => sut.Run(), Throws.InstanceOf<NullReferenceException>());
        }

        [Test]
        public void When_calling_execute_with_args_Then_run_is_executed()
        {
            string[] args = new string[] {
                "-u",
                "alexander.hjelm@solidify.dev",
                "-p",
                "XXXXXXXXXXXXXXXXXXXXXXXX",
                "--url",
                "https://solidifydemo.atlassian.net",
                "--header",
                "key: value",
                "--config",
                "C:\\dev\\jira-azuredevops-migrator\\src\\WorkItemMigrator\\Migration.Tests\\test-config-export.json"
            };

            var sut = new JiraCommandLine(args);

            Assert.That(() => sut.Run(), !Throws.InstanceOf<Exception>());
        }

        [Test]
        public void When_calling_execute_with_no_headers_Then_run_is_executed()
        {
            string[] args = new string[] {
                "-u",
                "alexander.hjelm@solidify.dev",
                "-p",
                "XXXXXXXXXXXXXXXXXXXXXXXX",
                "--url",
                "https://solidifydemo.atlassian.net",
                "--config",
                "C:\\dev\\jira-azuredevops-migrator\\src\\WorkItemMigrator\\Migration.Tests\\test-config-export.json"
            };

            var sut = new JiraCommandLine(args);

            Assert.That(() => sut.Run(), !Throws.InstanceOf<Exception>());
        }

        [Test]
        public void When_calling_execute_with_multiple_headers_Then_run_is_executed()
        {
            string[] args = new string[] {
                "-u",
                "alexander.hjelm@solidify.dev",
                "-p",
                "XXXXXXXXXXXXXXXXXXXXXXXX",
                "--url",
                "https://solidifydemo.atlassian.net",
                "--header",
                "key: value",
                "--header",
                "key2: value2",
                "--config",
                "C:\\dev\\jira-azuredevops-migrator\\src\\WorkItemMigrator\\Migration.Tests\\test-config-export.json"
            };

            var sut = new JiraCommandLine(args);

            Assert.That(() => sut.Run(), !Throws.InstanceOf<Exception>());
        }
    }
}