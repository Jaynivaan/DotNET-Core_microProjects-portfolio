//gs
using System;


namespace Day14.BlazorJobDashboard.Models

{

    //This class represents the data coming from backend api
    //blazor component will use this shape to display UI.

    public class JobStatusDto
    {
        public bool IsRunning { get; set; }

        public DateTime LastUpdated { get; set; }

        public int PulseCount { get; set; }

    }
}

