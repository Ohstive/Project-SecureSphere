using System.IO;
using SecureSphereV2.ViewModel.Services.Log;
using System;
using System.Windows;
using System.Threading.Tasks;
using SecureSphereV2.ViewModel.Services.Progress;


namespace SecureSphereV2.ViewModel.Services.CopyFile
{
   
    internal class CopyWithProgress:ICopyFileHandler
    {
        private readonly ILoger _log;

        public CopyWithProgress(ILoger log)
        {
               _log = log;
        }


        public void DifferentialCopy(string CopieName, string sourcePath, string targetPath, FileInfo file)
        {

            throw new NotImplementedException();
        }

        public void FullCopy(string CopieName, string sourcePath, string targetPath, FileInfo file)
        {
            System.Windows.MessageBox.Show("ProgressCopy");

        }


        private async Task CopyFileAsync(string sourcePath, string destinationDirectory, IProgress<CopyProgress> progress)
        {
            string destinationPath = Path.Combine(destinationDirectory, Path.GetFileName(sourcePath));

            using (var sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            using (var destinationStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
            {
                var buffer = new byte[327680]; // 320 KB buffer (adjust as needed)
                int bytesRead;
                long totalBytesRead = 0;
                long fileSizeInBytes = sourceStream.Length;

                var stopwatch = System.Diagnostics.Stopwatch.StartNew();

                while ((bytesRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await destinationStream.WriteAsync(buffer, 0, bytesRead);
                    totalBytesRead += bytesRead;

                    // Report progress
                    double speed = totalBytesRead / stopwatch.Elapsed.TotalSeconds;
                    double remainingTime = (fileSizeInBytes - totalBytesRead) / speed;
                    int progressPercentage = (int)((totalBytesRead * 100) / fileSizeInBytes);

                    // Report state
                    var copyProgress = new CopyProgress(speed, remainingTime, progressPercentage, CopyState.InProgress, fileSizeInBytes, totalBytesRead);
                    progress.Report(copyProgress);

                    // Pause/Resume handling
                    /*
                    while (isPaused)
                    {
                        await Task.Delay(100);
                    }

                    if (isStopped)
                    {
                        break;
                    }
                    */
                }
                // Report completion
                var completionProgress = new CopyProgress(0, 0, 100, CopyState.Completed, fileSizeInBytes, totalBytesRead);
                progress.Report(completionProgress);
            }
        }

        public string FormatBytes(double bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int suffixIndex = 0;

            while (bytes >= 1024 && suffixIndex < suffixes.Length - 1)
            {
                bytes /= 1024;
                suffixIndex++;
            }

            return $"{bytes:N2} {suffixes[suffixIndex]}/s";
        }


    }
}
