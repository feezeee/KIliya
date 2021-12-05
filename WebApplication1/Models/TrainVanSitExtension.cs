namespace WebApplication1.Models
{
    public static class TrainVanSitExtension
    {
        public static string GetInfo(this TrainVanSit trainVanSit)
        {
            return $"Поезд {trainVanSit.Train?.GetInfo()} - Вагон {trainVanSit.Van?.Name} - Место {trainVanSit.SitPlace?.Name}";
        }
    }
}
