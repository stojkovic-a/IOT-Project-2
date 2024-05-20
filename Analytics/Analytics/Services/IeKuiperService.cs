namespace Analytics.Services
{
    public interface IeKuiperService
    {

        public Task PrepareDataAsync();

        public Task RecieveDataFromKuiperAsync();


        public Task InitializeKuiperAsync();
    }
}
