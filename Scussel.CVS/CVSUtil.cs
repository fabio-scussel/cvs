using System.Diagnostics;
using System.IO;

namespace Scussel.CVS
{
    /// <summary>
    /// CSS wrapper class
    /// </summary>
    public sealed class CVSUtil
    {
        private string _executablePath;

        /// <summary>
        /// Instantiates a new object
        /// </summary>
        /// <param name="executablePath">The path and name of the cvs executable file</param>
        public CVSUtil(string executablePath) => 
            _executablePath = executablePath;

        /// <summary>
        /// Adds a file to the repository
        /// </summary>
        /// <param name="file">The filename with its complete path</param>
        public bool AddFile(string file)
        {
            if (!File.Exists(file)) throw new FileNotFoundException();
            string fileName = Path.GetFileName(file);
            string path = Path.GetDirectoryName(file);
            string addCommand = $"add \"{file}\"";
            return Execute(path, addCommand);
        }

        /// <summary>
        /// Edits a file
        /// </summary>
        /// <param name="file">The filename with its complete path</param>
        public bool Edit(string file)
        {
            if (!File.Exists(file)) throw new FileNotFoundException();
            string fileName = Path.GetFileName(file);
            string path = Path.GetDirectoryName(file);
            string editCommand = $"edit \"{file}\"";
            return Execute(path, editCommand);
        }

        /// <summary>
        /// Unedits a file
        /// </summary>
        /// <param name="file">The filename with its complete path</param>
        public bool Unedit(string file)
        {
            if (!File.Exists(file)) throw new FileNotFoundException();
            string fileName = Path.GetFileName(file);
            string path = Path.GetDirectoryName(file);
            string editCommand = $"unedit \"{file}\"";
            return Execute(path, editCommand);
        }

        /// <summary>
        /// Refresh a folder content
        /// </summary>
        /// <param name="folder">The selected folder</param>
        /// <param name="options">The options for the update command</param>
        public bool UpdateFolder(string folder, UpdateOptions options)
        {
            if (!Directory.Exists(folder)) throw new DirectoryNotFoundException();

            string updateCommand = "update";
            if (options.Prune) updateCommand += " -P";
            if (options.CreateDirectories) updateCommand += " -d";

            return Execute(folder, updateCommand);
        }

        /// <summary>
        /// Calls the cvs executable 
        /// </summary>
        private bool Execute(string path, string command)
        {
            if (!File.Exists(_executablePath))
                throw new FileNotFoundException("The CVS executable file was not found.");

            Process updateProcess = Process.Start(new ProcessStartInfo()
            {
                WorkingDirectory = path,
                FileName = _executablePath,
                Arguments = command,
                UseShellExecute = false,
                CreateNoWindow = true
            });

            updateProcess.WaitForExit();
            return updateProcess.ExitCode == 0;
        }
    }
}
