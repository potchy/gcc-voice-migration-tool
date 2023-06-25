using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace GccVoiceMigrationTool
{
    public class GcWavDirectoryUtils
    {
        private readonly IDirectory _directory;
        private readonly IFile _file;

        public GcWavDirectoryUtils(IDirectory directory, IFile file)
        {
            _directory = directory;
            _file = file;
        }

        public void Join(string sourceDirectory, string compareDirectory, string destinationDirectory, params string[] sourceLocale)
        {
            _directory.CreateDirectory(destinationDirectory);

            IEnumerable<GcWavPath> sourceFiles = _directory
                .EnumerateFiles(sourceDirectory, SearchPattern.Wav, SearchOption.AllDirectories)
                .Select(GcWavPath.Parse);

            if (sourceLocale?.Length > 0)
                sourceFiles = sourceFiles.Where(a => sourceLocale.Contains(a.Locale, StringComparer.OrdinalIgnoreCase));

            ILookup<string, GcWavPath> compareFiles = _directory
                .EnumerateFiles(compareDirectory, SearchPattern.Wav, SearchOption.AllDirectories)
                .Select(GcWavPath.Parse)
                .ToLookup(a => a.NameWithoutLocale, StringComparer.OrdinalIgnoreCase);

            foreach (GcWavPath sourceFile in sourceFiles)
                foreach (GcWavPath compareFile in compareFiles[sourceFile.NameWithoutLocale])
                {
                    string outputFile = Path.Combine(destinationDirectory, compareFile.Name);
                    _file.Copy(sourceFile.FullName, outputFile, overwrite: true);
                }
        }
    }
}
