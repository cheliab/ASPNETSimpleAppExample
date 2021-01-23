using System;

namespace WebApplication.Services
{
    public class TimeService
    {
        public string Time { get; }
        
        public TimeService()
        {
            Time = DateTime.Now.ToString("hh:mm:ss");
        }
    }
}