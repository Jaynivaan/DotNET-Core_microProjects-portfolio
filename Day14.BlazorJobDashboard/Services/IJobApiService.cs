//gs
using Day14.BlazorJobDashboard.Models;
using System.Threading.Tasks;

namespace Day14.BlazorJobDashboard.Services
{
    /// <summary>
    /// Defines WHAT the JobApiService can do ,, not how it does..
    /// blazor components depends on this abstraction.
    /// </summary>
    public interface IJobApiService
    {
        //Async api call returning jobstatus data
        Task<JobStatusDto> GetJobStatusAsync();
    }
}