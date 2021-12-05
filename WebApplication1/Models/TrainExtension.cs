using System.Text;

namespace WebApplication1.Models
{
    public static class TrainExtension
    {
        public static string GetInfo(this Train train)
        {
            string str = $"{train?.Name}";
            return str;
        }
    }
}
