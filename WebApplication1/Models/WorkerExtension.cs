namespace WebApplication1.Models
{
    public static class WorkerExtension
    {
        public static string GetInfo(this Worker worker)
        {
            return $"{worker?.LastName} {worker?.FirstName}";
        }
    }
}
