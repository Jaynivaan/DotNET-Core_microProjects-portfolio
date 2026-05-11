//gs
using System;

namespace Day11.BackgroundJobs.Models
{
    //This model represents the internal state
    // of the background worker system.

    //This shape the actual truth about state of system
    public class JobStatus
    {
        //wheteher working is currently active
        public bool IsRunning { get; set; }

        //Last heart beat time stamp from worker
        public DateTime LastUpdated { get; set; }
        
        //How many pulse cycles executed
        public int PulseCount { get; set; }
    }
}