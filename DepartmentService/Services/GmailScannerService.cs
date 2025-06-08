using DepartmentService.Entities;
using MailKit.Net.Imap;
using System.Net.Mail;
using MailKit;
using MailKit.Search;
using MimeKit;


namespace DepartmentService.Services
{
    public class GmailScannerService
    {
       
        public async Task<List<AppliedCv>> ScanAsync()
        {
            var result = new List<AppliedCv>();
            var _attachmentPath  = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AppliedCV");

            using var client = new ImapClient();
            await client.ConnectAsync("imap.gmail.com", 993, true);
            await client.AuthenticateAsync("vut4262@gmail.com", "saec mwus ioht gwau");

            var inbox = client.Inbox;
            await inbox.OpenAsync(FolderAccess.ReadOnly);

            var messages = await inbox.SearchAsync(SearchQuery.SubjectContains("APPLIEDCV"));

            foreach (var uid in messages)
            {
                var message = await inbox.GetMessageAsync(uid);
                var attachmentPaths = new List<string>();

                foreach (var attachment in message.Attachments)
                {
                    if (attachment is MimePart part)
                    {
                        var fileName = $"{Guid.NewGuid()}_{part.FileName}";
                        var filePath = Path.Combine(_attachmentPath, fileName);

                        using var stream = File.Create(filePath);
                        await part.Content.DecodeToAsync(stream);

                        // đường dẫn tương đối để lưu trong DB
                        var relativePath = Path.Combine("AppliedCV", fileName).Replace("\\", "/");
                        attachmentPaths.Add(relativePath);
                    }
                }

                result.Add(new AppliedCv
                {
                    FromMail = message.From.Mailboxes.FirstOrDefault()?.Address,
                    Header = message.Subject,
                    Body = message.TextBody ?? message.HtmlBody,
                    Attachment = string.Join("|", attachmentPaths)
                });
            }

            await client.DisconnectAsync(true);
            return result;
        }
    }
}
