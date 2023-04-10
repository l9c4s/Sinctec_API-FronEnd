using SendGrid;
using SendGrid.Helpers.Mail;
using System.Web;

namespace WebAPIs.Email
{
    public class SengridServices
    {
        public async Task<bool> Execute(string Email,string Url,int IdEmail)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("lucas.santos@koode.io", "SendGrid");
            var subject = "Confirmar Conta";
            var to = new EmailAddress(Email, "UserNovo");
            var plainTextContent = "Confirme sua Conta";
            var htmlContent = LerEmailAssets(Email, Url, IdEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
      
            return response.IsSuccessStatusCode;
        }

        public static string LerEmailAssets(string email, string url,int idEmail)
        {
            string htmlEmail = File.ReadAllText($"D:\\Teste\\Financeiro\\API_DDD_2023\\WebAPIs\\Email\\{idEmail}.html");
            htmlEmail = htmlEmail.Replace("[EMAIL_CLIENTE]", email);
            htmlEmail = htmlEmail.Replace("[LINK_CONFIRMAR]", url);
            return htmlEmail;
        }
    }
}
