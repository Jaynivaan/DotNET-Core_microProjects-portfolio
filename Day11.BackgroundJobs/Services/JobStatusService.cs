//gs

using Day11.BackgroundJobs.Models;
using System;

namespace Day11.BackgroundJobs.Services
{
    //This class stores and manages
    // the current heart beat state of the system

    //This is application memory state management

    public class JobStatusService : IJobStatusService
    {
        //internal state of worker system
        //using readonly here as we need assured only once implementation
        private readonly JobStatus _status;

        //constructor runs once when service is created.
        public JobStatusService()
        {
            //initial default values
            _status = new JobStatus
            {
                IsRunning = false,
                LastUpdated = DateTime.UtcNow,
                PulseCount = 0
            };
        }

        //worker call this repeatedly
       //record status
        public void RecordPulse()
        {
            _status.IsRunning = true;
            
            _status.LastUpdated = DateTime.UtcNow;

            _status.PulseCount++;
        }


        //get status
        //Endpoint read current state
        public JobStatus GetStatus()
        {
            return _status;
        }
    }
}