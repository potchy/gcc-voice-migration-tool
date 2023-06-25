namespace GccVoiceMigrationTool.Tests
{
    public class FileCopyLog
    {
        public string SourceFileName { get; }
        public string DestFileName { get; }

        public FileCopyLog(string sourceFileName, string destFileName)
        {
            SourceFileName = sourceFileName;
            DestFileName = destFileName;
        }
    }
}
