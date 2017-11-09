using Microsoft.Extensions.Configuration;

namespace DotnetPiper.Services
{
    public interface IFirstService
    {
        string WelcomeEquinox();
    }
    public class FirstService : IFirstService
    {
        public string welcomeEquinoxStr { get; set; }
        public FirstService(IConfiguration Configuration)
        {
            welcomeEquinoxStr = Configuration["WelcomeEquinox"];
        }
        public string WelcomeEquinox()
        {
            return welcomeEquinoxStr;
        }
    }
}
