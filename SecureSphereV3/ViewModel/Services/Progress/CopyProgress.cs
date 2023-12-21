using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureSphereV2.ViewModel.Services.Progress
{
    public class CopyProgress
    {
        public double Speed { get; }
        public string RemainingTime { get; }
        public int ProgressPercentage { get; }
        public CopyState State { get; }
        public double TotalSize { get; }
        public double RemainingSize { get; }

        public CopyProgress(double speed, double remainingTime, int progressPercentage, CopyState state, double totalSize, double remainingSize)
        {
            Speed = speed;
            RemainingTime = FormatTimeSpan(TimeSpan.FromSeconds(remainingTime));
            ProgressPercentage = progressPercentage;
            State = state;
            TotalSize = totalSize / (1024 * 1024); // Convert to MB
            RemainingSize = remainingSize / (1024 * 1024); // Convert to MB
        }

        private string FormatTimeSpan(TimeSpan timeSpan)
        {
            if (timeSpan.TotalDays >= 1)
            {
                return $"{(int)timeSpan.TotalDays} jours";
            }
            else if (timeSpan.TotalHours >= 1)
            {
                return $"{(int)timeSpan.TotalHours} heures";
            }
            else if (timeSpan.TotalMinutes >= 1)
            {
                return $"{(int)timeSpan.TotalMinutes} minutes";
            }
            else
            {
                return $"{(int)timeSpan.TotalSeconds} secondes";
            }
        }
    }

    public enum CopyState
    {
        InProgress,
        Completed
    }
}
