using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace GccVoiceMigrationTool.Tests
{
    [TestFixture]
    public class GcWavDirectoryUtilsTests
    {
        public class Join
        {
            private const string SourceDirectory = @"c:\source";
            private const string CompareDirectory = @"c:\compare";
            private const string DestinationDirectory = @"c:\destination";

            private List<string> _sourceFiles;
            private List<string> _compareFiles;
            private Mock<IDirectory> _directory;
            private List<FileCopyLog> _logs;
            private GcWavDirectoryUtils _gcWavDirectoryUtils;

            [SetUp]
            public void SetUp()
            {
                _sourceFiles = new List<string>();
                _compareFiles = new List<string>();
                _directory = new Mock<IDirectory>();

                _directory
                    .Setup(a => a.EnumerateFiles(SourceDirectory, SearchPattern.Wav, SearchOption.AllDirectories))
                    .Returns(() => _sourceFiles);

                _directory
                    .Setup(a => a.EnumerateFiles(CompareDirectory, SearchPattern.Wav, SearchOption.AllDirectories))
                    .Returns(() => _compareFiles);

                _logs = new List<FileCopyLog>();
                var file = new Mock<IFile>();

                file
                    .Setup(a => a.Copy(It.IsAny<string>(), It.IsAny<string>(), true))
                    .Callback((string sourceFileName, string destFileName, bool overwrite) => _logs.Add(new FileCopyLog(sourceFileName, destFileName)));

                _gcWavDirectoryUtils = new GcWavDirectoryUtils(_directory.Object, file.Object);
            }

            [Test]
            public void EnsureDestinationDirectoryExists()
            {
                _gcWavDirectoryUtils.Join(SourceDirectory, CompareDirectory, DestinationDirectory);

                _directory.Verify(a => a.CreateDirectory(DestinationDirectory));
            }

            [Test]
            public void CopyFilesToDestinationDirectoryUsingTheSameFileNamesOfTheCompareDirectory()
            {
                _sourceFiles.Add(Path.Combine(SourceDirectory, @"sound_korea\a_cloud.wav"));

                _compareFiles.AddRange(new[]
                {
                    Path.Combine(CompareDirectory, @"sound_brazil\a_cloud@br.wav"),
                    Path.Combine(CompareDirectory, @"sound_korea\a_cloud.wav")
                });

                _gcWavDirectoryUtils.Join(SourceDirectory, CompareDirectory, DestinationDirectory);

                _logs.Should().BeEquivalentTo(new[]
                {
                    new FileCopyLog(
                        sourceFileName: Path.Combine(SourceDirectory, @"sound_korea\a_cloud.wav"),
                        destFileName: Path.Combine(DestinationDirectory, @"a_cloud@br.wav")),

                    new FileCopyLog(
                        sourceFileName: Path.Combine(SourceDirectory, @"sound_korea\a_cloud.wav"),
                        destFileName: Path.Combine(DestinationDirectory, @"a_cloud.wav"))
                });
            }

            [Test]
            public void FileNotPresentInTheCompareDirectory_Skip()
            {
                _sourceFiles.Add(Path.Combine(SourceDirectory, @"sound_korea\definitely_does_not_exist_in_destination.wav"));

                _gcWavDirectoryUtils.Join(SourceDirectory, CompareDirectory, DestinationDirectory);

                _logs.Should().BeEmpty();
            }

            [Test]
            public void CasingDoesNotMatch_CopyAnyway()
            {
                _sourceFiles.Add(Path.Combine(SourceDirectory, @"SoUnD_kOrEa\A_eXpLoSiOnArRoW.wAv"));
                _compareFiles.Add(Path.Combine(CompareDirectory, @"sound_korea\a_explosionarrow.wav"));

                _gcWavDirectoryUtils.Join(SourceDirectory, CompareDirectory, DestinationDirectory);

                _logs.Should().ContainSingle();
            }

            [Test]
            public void LocaleIsSupplied_OnlyCopyFilesThatMatch()
            {
                var expected = Path.Combine(SourceDirectory, @"sound_brazil\hyper_sonic_step@br.wav");

                _sourceFiles.AddRange(new[]
                {
                    expected,
                    Path.Combine(SourceDirectory, @"sound_korea\hyper_sonic_step.wav")
                });

                _compareFiles.Add(Path.Combine(CompareDirectory, @"sound_brazil\hyper_sonic_step@br.wav"));

                _gcWavDirectoryUtils.Join(SourceDirectory, CompareDirectory, DestinationDirectory, sourceLocale: "br");

                _logs.Should().ContainSingle().Which.SourceFileName.Should().Be(expected);
            }
        }
    }
}