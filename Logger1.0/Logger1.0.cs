using System;
using System.IO;
using System.Threading.Tasks;

namespace Logger1._0
{
    public class Logger
    {
        public static string FilePath { get; set; }
        public static bool AddTimestamp { get; set; }

        public Logger(string filePath, bool addTimestamp = true, bool forceMaxLength = true, long maxLength = 10000000)
        {
            FilePath = filePath;
            AddTimestamp = addTimestamp;

            if (!File.Exists(FilePath))
                File.Create(FilePath);

            FileInfo fileInfo = new FileInfo(FilePath);
            if (forceMaxLength && fileInfo.Length > maxLength)
            {
                PurgeFile();
            }
        }

        // Adding a line to the log file
        public static async Task AddLine(string message)
        {
            using StreamWriter file = new(FilePath, append: true);
            if (AddTimestamp)
                await file.WriteLineAsync(DateTime.Now.ToString() + ": " + message);
            else
                await file.WriteLineAsync(message);
        }

        // Adding a line to the log file with a title
        public static async Task AddLine(string title, string message)
        {
            using StreamWriter file = new(FilePath, append: true);
            if (AddTimestamp)
                await file.WriteLineAsync(DateTime.Now.ToString() + ": " + title + ": " + message);
        }

        public void PurgeFile()
        {
            File.WriteAllText(FilePath, string.Empty);
        }
    }
}
