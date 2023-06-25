using FluentAssertions;
using NUnit.Framework;

namespace GccVoiceMigrationTool.Tests
{
    [TestFixture]
    public class GcWavPathTests
    {
        private static TestCaseData[] ParseTestCases => new TestCaseData[]
        {
            new(new FakeGcWavPath
            {
                FullName = @"c:\sound_brazil\a_ah@br.wav",
                Name = @"a_ah@br.wav",
                Locale = "br",
                NameWithoutLocale = @"a_ah.wav"
            }),
            new(new FakeGcWavPath
            {
                FullName = @"c:\sound_korea\a_ah.wav",
                Name = @"a_ah.wav",
                Locale = string.Empty,
                NameWithoutLocale = @"a_ah.wav"
            })
        };

        [TestCaseSource(nameof(ParseTestCases))]
        public void Parse(FakeGcWavPath expected)
        {
            GcWavPath actual = GcWavPath.Parse(expected.FullName);
            actual.Should().BeEquivalentTo(expected);
        }
    }
}