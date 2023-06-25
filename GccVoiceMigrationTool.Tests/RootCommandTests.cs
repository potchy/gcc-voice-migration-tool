using System.CommandLine;
using FluentAssertions;
using NUnit.Framework;

namespace GccVoiceMigrationTool.Tests
{
    [TestFixture]
    public class RootCommandTests
    {
        [TestCase("-s", "-c", "-d", "-l")]
        [TestCase("--source", "--compare", "--destination", "--locale")]
        public void Invoke(string arg1, string arg2, string arg3, string arg4)
        {
            var expected = new RootCommandOptions
            {
                SourceDirectory = "c:\\source",
                CompareDirectory = "c:\\compare",
                DestinationDirectory = "c:\\destination",
                SourceLocale = new[] { string.Empty, "br", "us", "kr" }
            };

            RootCommandOptions actual = null;

            RootCommandFactory
                .Create(options => actual = options)
                .Invoke($"{arg1} c:\\source {arg2} c:\\compare {arg3} c:\\destination {arg4} \"\" br us kr");

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
