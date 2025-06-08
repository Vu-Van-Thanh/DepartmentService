using DepartmentService.Entities;
using MailKit.Net.Imap;
using System.Net.Mail;
using MailKit;
using MailKit.Search;
using MimeKit;
using DepartmentService.Repositories;


namespace DepartmentService.Services
{
    public class GmailScannerService
    {
        private readonly ICVRepository cVRepository;
        public GmailScannerService(ICVRepository _cVRepository)
        {
            cVRepository = _cVRepository;
        }

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

                        
                        var relativePath = Path.Combine("AppliedCV", fileName).Replace("\\", "/");
                        attachmentPaths.Add(relativePath);
                    }
                }

                string? position = null;
                string? type = null;
                string? status = "PENDING"; 
                string? applyDate = null;
                if (!string.IsNullOrEmpty(message.Subject))
                {
                    var parts = message.Subject.Split(" - ");
                    if (parts.Length >= 3 && parts[0].Trim() == "APPLIEDCV")
                    {
                        position = parts[1].Trim();
                        type = parts[2].Trim();
                    }
                }
                result.Add(new AppliedCv
                {
                    FromMail = message.From.Mailboxes.FirstOrDefault()?.Address,
                    Header = message.Subject,
                    Body = message.TextBody ?? message.HtmlBody,
                    Attachment = string.Join("|", attachmentPaths),
                    Status = status,
                    Position = position,
                    Type = type,
                    ApplyDate = applyDate

                });
            }


            await cVRepository.AddRange(result);
            await client.DisconnectAsync(true);
            return result;
        }
    }


}
