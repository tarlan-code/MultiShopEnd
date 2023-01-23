namespace MultiShopEnd.Abstractions.Email
{
    public interface IEmailService
    {
        public void SendEmail(string email,string subject,string body,bool IsHtml = false);
    }
}
