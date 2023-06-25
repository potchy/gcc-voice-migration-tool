namespace GccVoiceMigrationTool
{
    public interface IGcWavPath
    {
        string FullName { get; }
        string Name { get; }
        string Locale { get; }
        string NameWithoutLocale { get; }
    }
}