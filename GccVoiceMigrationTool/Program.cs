using System.CommandLine;
using System.IO.Abstractions;

namespace GccVoiceMigrationTool
{
    class Program
    {
        static void Main(string[] args)
        {
            RootCommandFactory.Create(delegate(RootCommandOptions options)
            {
                var fileSystem = new FileSystem();
                var gcWavDirectoryUtils = new GcWavDirectoryUtils(fileSystem.Directory, fileSystem.File);

                gcWavDirectoryUtils.Join(
                    options.SourceDirectory,
                    options.CompareDirectory,
                    options.DestinationDirectory,
                    options.SourceLocale);
            }).Invoke(args);
        }
    }
}
