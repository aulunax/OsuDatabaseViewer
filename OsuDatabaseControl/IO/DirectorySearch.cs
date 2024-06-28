using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsuDatabaseControl.Utils;

namespace OsuDatabaseControl.IO
{
    public class DirectorySearch
    {
        private const int MAX_DEPTH = 5;

        private List<string> foundCandidates = new List<string>();
        private int totalDirectories;
        private int currentProgress;
        private IProgress<int> progress;



        public DirectorySearch(IProgress<int> progress)
        {
            this.progress = progress;
        }


        public List<string> SearchForOsuDirectory()
        {
            string[] drives = Environment.GetLogicalDrives();

            totalDirectories = 0;
            foreach (string drive in drives)
            {
                totalDirectories += CountDirectories(drive, 0);
            }

            if (totalDirectories < 1) {
                return null;
            }

            currentProgress = 0;

            foreach (string drive in drives)
            {
                SearchInDrive(drive);
            }

            if (foundCandidates.Count > 0)
            {
                return foundCandidates;
            }

            return null;
        }

        private void SearchInDrive(string drive)
        {
            try
            {
                SearchRecursively(drive, FilePaths.OSU_DIRECTORY_NAME, 0);
            }
            catch (UnauthorizedAccessException)
            {
                return;
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void SearchRecursively(string directory, string folderName, int depth)
        {
            if (depth >= MAX_DEPTH)
                return;

            string targetDirectory = Path.Combine(directory, folderName);
            if (Directory.Exists(targetDirectory) && IsOsuFolder(targetDirectory))
            {
                foundCandidates.Add(targetDirectory);
            }

            // Recursively search in subdirectories
            string[] subDirectories = Directory.GetDirectories(directory);
            foreach (string subDirectory in subDirectories)
            {
                try
                {
                    SearchRecursively(subDirectory, folderName, depth+1);
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            currentProgress++;
            progress.Report((currentProgress * 100) / totalDirectories);

            return;
        }

        private bool IsOsuFolder(string directory)
        {
            string scoresDatabasePath = Path.Combine(directory, FilePaths.OSU_SCOREDB_FILENAME);
            string osuDatabasePath = Path.Combine(directory, FilePaths.OSU_OSUDB_FILENAME);

            if (File.Exists(scoresDatabasePath) && File.Exists(osuDatabasePath))
            {
                return true;
            }
            return false;
        }

        private int CountDirectories(string directory, int depth)
        {
            if (depth >= MAX_DEPTH)
                return 0;

            int count = 1;

            try
            {
                string[] subDirectories = Directory.GetDirectories(directory);
                foreach (string subDirectory in subDirectories)
                {
                    count += CountDirectories(subDirectory, depth + 1);
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (Exception ex)
            {
            }

            return count;
        }
    }
}
