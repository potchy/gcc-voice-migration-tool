using System.CommandLine;

namespace GccVoiceMigrationTool
{
    public static class RootCommandFactory
    {
        public delegate void RootCommandHandler(RootCommandOptions options);

        public static RootCommand Create(RootCommandHandler handler)
        {
            var sourceDirectoryOption = new Option<string>(aliases: new[] { "--source", "-s" }) { IsRequired = true };
            var compareDirectoryOption = new Option<string>(aliases: new[] { "--compare", "-c" }) { IsRequired = true };
            var destinationDirectoryOption = new Option<string>(aliases: new[] { "--destination", "-d" }) { IsRequired = true };
            var sourceLocaleOption = new Option<string[]>(aliases: new[] { "--locale", "-l" }) { AllowMultipleArgumentsPerToken = true };

            var rootCommand = new RootCommand
            {
                sourceDirectoryOption,
                compareDirectoryOption,
                destinationDirectoryOption,
                sourceLocaleOption
            };

            rootCommand.SetHandler(delegate(string sourceDirectory, string compareDirectory, string destinationDirectory, string[] sourceLocale)
            {
                handler.Invoke(new RootCommandOptions
                {
                    SourceDirectory = sourceDirectory,
                    CompareDirectory = compareDirectory,
                    DestinationDirectory = destinationDirectory,
                    SourceLocale = sourceLocale
                });
            }, sourceDirectoryOption, compareDirectoryOption, destinationDirectoryOption, sourceLocaleOption);

            return rootCommand;
        }
    }
}
