//gs
using Day11.BackgroundJobs.Models;

namespace Day11.BackgroundJobs.Services
{
    //this interface is the contract where the menu of services available from the class JobStatusService
    //why using this file ...?? because other part of program come to menu and ask for the service...
    //going to kitchen looking for the concrete class  is too intimidating.... who does that.. lol
    public interface IJobStatusService
    {
        //Menu item 1: worker will call this menu item to update heartbeat
        void RecordPulse();//why void because it never bring any return ..action of this item is just acting not returning a value back.

        //Menu item 2: enpoint will order this menu item to read current status 
        // GetStatus() retrieves current system State.
        JobStatus GetStatus();

        //in short RecordPulse() change state
        //GetStatus() reads current state.

    }
}