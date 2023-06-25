namespace GccVoiceMigrationTool.Tests
{
    public class FakeGcWavPath : IGcWavPath
    {
        public string FullName { get; init; }
        public string Name { get; init; }
        public string Locale { get; init; }
        public string NameWithoutLocale { get; init; }
    }
}
