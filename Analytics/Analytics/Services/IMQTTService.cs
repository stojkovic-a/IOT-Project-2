namespace Analytics.Services
{
    public interface IMQTTService
    {
        public Task ConnectAsync();
        public Task DisconnectAsync();
        public Task SubscribeToTopicAsync(string topic);
        public void SetTopicHandler();
        



    }
}
