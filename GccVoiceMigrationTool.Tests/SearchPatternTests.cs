using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using NUnit.Framework;

namespace GccVoiceMigrationTool.Tests
{
    [TestFixture]
    public class SearchPatternTests
    {
        [TestFixture]
        public class Wav
        {
            private const string SearchPath = @"c:\char_zero4";
            private MockFileSystem _mockFileSystem;

            [SetUp]
            public void SetUp()
            {
                _mockFileSystem = new MockFileSystem();
            }

            [Test]
            public void FileHasWavExtension_ReturnIt()
            {
                _mockFileSystem.AddFile(Path.Combine(SearchPath, "Voice_ZERO3_END.wav"), MockFileData.NullObject);

                IEnumerable<string> files = _mockFileSystem.Directory.EnumerateFiles(SearchPath, SearchPattern.Wav);

                files.Should().ContainSingle();
            }

            [Test]
            public void FileHasSomeOtherExtension_ReturnEmpty()
            {
                _mockFileSystem.AddFile(Path.Combine(SearchPath, "SkillIconID1421.dds"), MockFileData.NullObject);

                IEnumerable<string> files = _mockFileSystem.Directory.EnumerateFiles(SearchPath, SearchPattern.Wav);

                files.Should().BeEmpty();
            }
        }
    }
}