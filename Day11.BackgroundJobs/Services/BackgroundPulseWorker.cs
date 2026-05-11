//gs
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Day11.BackgroundJobs.Services
{
    //This worker runs in the background continuously
    // the microsoft extentions.Hosting framework starts this automatically.

    public class BackgroundPulseWorker : BackgroundService
    {
        //why this class inheriting from background service and btw whatis  this background service..
        //Microsoft already build the engine for long running workers.
        //we just need to describe what work to repeat that all 
        //framework handles:
        //startup
        //lifecycle
        //cancellation
        //shutdown

        /////////////////////////
        //Worker need access to status service.
        private readonly IJobStatusService _jobStatusService;

        //constructor injection
        //DI container give the service automatically 
        public BackgroundPulseWorker(IJobStatusService jobStatusService)
        {
            _jobStatusService = jobStatusService;
        }

        //this method runs continuously in the background
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //loop continues until cancellation is requested or else till app is shut down.
            while(!stoppingToken.IsCancellationRequested)
            {
                //update Heartbeat state
                _jobStatusService.RecordPulse();
                Console.WriteLine("Background Pulse recorded");
                //wait 9 seconds before next heart beat.
                await Task.Delay(9000, stoppingToken);

            }
        }
        //backend workers usually  monitors, clean, refresh, sync, process queues continuously 
    }
}