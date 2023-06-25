namespace GccVoiceMigrationTool
{
    public class RootCommandOptions
    {
        public string SourceDirectory { get; init; }
        public string CompareDirectory { get; init; }
        public string DestinationDirectory { get; init; }
        public string[] SourceLocale { get; init; }
    }
}
