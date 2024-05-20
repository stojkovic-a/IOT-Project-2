using Analytics.DTOs;

namespace Analytics.Services
{
    public interface IAnalyticsService
    {
        public Task ReceiveDataAsync();

        //public Task PublishDataAsync();

        public void Analysis(Fields? data);
    }
}
