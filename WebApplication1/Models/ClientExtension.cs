namespace WebApplication1.Models
{
    public static class ClientExtension
    {
        public static string GetInfo(this Client client)
        {
            string str = $"{client.LastName} {client.FirstName}";
            return str;
        }
    }
}
