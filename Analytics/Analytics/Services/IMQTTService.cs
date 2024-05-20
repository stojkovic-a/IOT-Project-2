namespace Analytics.Services
{
    public interface IMQTTService
    {
        public Task ConnectAsync();
        public Task DisconnectAsync();
        public Task SubscribeToTopicAsync(string topic);
        public Task UnsubscribeFromTopicAsync(string topic);
        public void AddTopicHandler(string topic, Action<ArraySegment<byte>> handler);
        public void Listen();

        public Task PublishMessageAsync(string topic, string payloadSerialized);


    }
}
