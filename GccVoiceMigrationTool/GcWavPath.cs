using System.IO;

namespace GccVoiceMigrationTool
{
    public class GcWavPath : IGcWavPath
    {
        public string FullName { get; private set; }
        public string Name { get; private set; }
        public string Locale { get; private set; }
        public string NameWithoutLocale { get; private set; }

        private GcWavPath()
        {
        }

        public static GcWavPath Parse(string path)
        {
            var localizedPath = new GcWavPath();
            localizedPath.FullName = path;
            localizedPath.Name = Path.GetFileName(path);

            string extension = Path.GetExtension(localizedPath.Name);
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(localizedPath.Name);

            int localeIndex = nameWithoutExtension.IndexOf('@');
            if (localeIndex >= 0)
            {
                localizedPath.Locale = nameWithoutExtension.Substring(localeIndex + 1);
                localizedPath.NameWithoutLocale = nameWithoutExtension.Substring(0, localeIndex);
            }
            else
            {
                localizedPath.Locale = string.Empty;
                localizedPath.NameWithoutLocale = nameWithoutExtension;
            }

            localizedPath.NameWithoutLocale += extension;

            return localizedPath;
        }
    }
}
