using EventInfo.DTOs;

namespace EventInfo.Services
{
    public interface IEventService
    {
        public Fields GetLatestInfo();
        public Events GetLastEvents();
        public Task ReceiveDataAsync();
    }
}
