using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WorkMate.Feature
{
    internal class WeeklyReporting
    {
        public void SendEmailWithScreenshot()
        {
            // 화면 캡처
            var screenBitmap = CaptureScreen();

            // 이미지 파일로 저장
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "screenshot.png");
            screenBitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

            // 이메일 설정
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("your-email@example.com");
            mail.To.Add("recipient-email@example.com");
            mail.Subject = "주간 리포트 스크린샷";
            mail.Body = "첨부된 스크린샷을 확인해주세요.";

            // 이미지 파일 첨부
            Attachment attachment = new Attachment(filePath);
            mail.Attachments.Add(attachment);

            // SMTP 설정 (예: Gmail)
            SmtpClient smtp = new SmtpClient("smtp.example.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("your-email@example.com", "your-password"),
                EnableSsl = true,
            };

            // 이메일 보내기
            smtp.Send(mail);
        }

        private Bitmap CaptureScreen()
        {
            // 화면 캡처 함수
            var width = (int)SystemParameters.VirtualScreenWidth;
            var height = (int)SystemParameters.VirtualScreenHeight;

            var screenBitmap = new Bitmap(width, height);
            var graphics = Graphics.FromImage(screenBitmap);
            graphics.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(width, height));

            return screenBitmap;
        }
    }
}