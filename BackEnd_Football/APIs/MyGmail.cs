using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace BackEnd_Football.APIs
{
    public class MyGmail
    {
        public string mainEmail = "quyhongq@gmail.com";
        public string username = "quyhongq@gmail.com";
        public string password = "bsmxwtnvroqfxtry";
        public int port = 587;
        public string host = "smtp.gmail.com";


        public async Task<string> sendEmailNotification(string receiver)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("QĐN System", "quyhongq@gmail.com"));
            message.Subject = "Cảnh báo từ QĐN";
           
                message.To.Add(new MailboxAddress("", receiver));
            
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = string.Format("<p>Bạn đã vi phạm một số quy định tại hệ thống kết nối đam mê đá banh của QĐN.  </p><br><p>ĐÂY LÀ EMAIL CẢNH BÁO, NẾU VI PHẠM NHIỀU LẦN BẠN CÓ THỂ BỊ CẤM ĐĂNG BÀI</p>", DateTime.Now.ToString());
            message.Body = bodyBuilder.ToMessageBody();



            SmtpClient client = new SmtpClient();

            try
            {
                await client.ConnectAsync(host, port, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(username, password);


                await client.SendAsync(message);
               
            }
            catch(Exception e)
            {
                return "Lỗi" + e.Message;
            }
            client.Disconnect(true);

            return "THÀNH CÔNG";
        }
    }
}
