//gs

using Day14.BlazorJobDashboard.Models;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Day14.BlazorJobDashboard.Services
{

    //api calling logic live here welcome..
    public class JobApiService : IJobApiService
    {
        //HttpClient is the bridge between
        //blazor front end and Backend program "Day11.BackgroundJobs"

        private readonly HttpClient _httpClient;
        //dependency inj give http client automatically
        public JobApiService (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //Real api call happen here
        public async Task<JobStatusDto> GetJobStatusAsync()
        {
            //small delay added to visualize loading state later.
         // await Task.Delay(9000);

            var response = await _httpClient.GetFromJsonAsync<ApiResponseDto<JobStatusDto>>(
                "http://localhost:5064/job-status"
            );

            return response?.Data ?? new JobStatusDto();

        }

    }
}