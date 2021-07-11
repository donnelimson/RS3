using Codebiz.Domain.Common.Model.DTOs.RS3;
using Codebiz.Domain.Common.Model.Filter.RS3;
using Infrastructure.Repository.Common;
using PagedList;
using Codebiz.Domain.Common.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.Enums;
using Codebiz.Domain.Common.Model.ViewModel;
using System.Web.Mvc;
using System.IO;
using Codebiz.Domain.Common.Model.Filter;

namespace Infrastructure.Services.Common
{
    public interface ITicketService
    {
        IPagedList<TicketIndexDTO> GetAllOpenTickets(TicketFilter filter);
        Ticket AddOrUpdate(TicketAddOrUpdateDTO viewModel, int currentAppUserId, bool isClient);
        ViewTicketDTO GetTicketDetailsById(int id, UrlHelper Url);
        IPagedList<TicketCFLDTO> GetMyTickets(LookUpFilter filter, int currentAppuserId);
        void SubmitComment(CommentAddDTO model, int currentAppuserId, string currentUsername);
        bool ResolveOrReopenTicket(int id, int currentAppuserId, string currentUserName);
        void TakeTicket(int id, int currentAppUserId, string currentUserName);
    }
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IConfigSettingService _configSettingService;
        private readonly IFileTypeService _fileTypeService;
        private readonly IContentFileService _contentFileService;
        public TicketService(ITicketRepository ticketRepository, IConfigSettingService configSettingService, IFileTypeService fileTypeService, IContentFileService contentFileService)
        {
            _ticketRepository = ticketRepository;
            _configSettingService = configSettingService;
            _fileTypeService = fileTypeService;
            _contentFileService = contentFileService;
        }
        public IPagedList<TicketIndexDTO> GetAllOpenTickets(TicketFilter filter)
        {
            return _ticketRepository.GetAllOpenTickets(filter);
        }
        public Ticket AddOrUpdate(TicketAddOrUpdateDTO viewModel, int currentAppUserId, bool isClient)
        {
            var ticket = _ticketRepository.GetById(viewModel.Id);
            if (ticket == null)
            {
                ticket = new Ticket();
                ticket.CreatedByAppUserId = currentAppUserId;
            }
            InsertTicketLog(ticket, viewModel.LogComment, currentAppUserId);
            ticket.Title = viewModel.Title;
            ticket.Description = viewModel.Description;
            ticket.Priority = viewModel.Priority;
            if (isClient)
            {
                ticket.ClientId = currentAppUserId;
            }
            else
            {
                ticket.ClientId = viewModel.ClientId;
            }
           
            ticket.GuessClientAddress = viewModel.ClientAddress;
            ticket.GuessClientEmail = viewModel.ClientEmail;
            ticket.GuessClientName = viewModel.Client;
            ticket.TechnicianId = viewModel.TechnicianId;
            _ticketRepository.InsertOrUpdate(ticket);
            AddOrUpdateTicketAttachment(ticket.Attachments, viewModel.Attachments, ticket, currentAppUserId);
            return ticket;
        }
        public void SubmitComment(CommentAddDTO model, int currentAppuserId, string currentUserName)
        {
            var ticket = _ticketRepository.GetById(model.Id);
            ticket.ModifiedOn = DateTime.Now;
            ticket.ModifiedByAppUserId = currentAppuserId;
            if (model.IsResolved)
            {
                ticket.TicketStatus = "R";
            }
            ticket.Comments.Add(new TicketComment
            {
                CreatedByAppUserId=currentAppuserId,
                Comment = model.Comment
            });
            model.Title = ticket.Title;
            model.Email = ticket.AppUserClient == null ? ticket.GuessClientEmail : ticket.AppUserClient.Email;
            InsertTicketLog(ticket, currentUserName + " commented on the ticket", currentAppuserId);
            _ticketRepository.InsertOrUpdate(ticket);
        }
        public void TakeTicket(int id, int currentAppUserId, string currentUserName)
        {
            var ticket = _ticketRepository.GetById(id);
            ticket.TechnicianId = currentAppUserId;
            InsertTicketLog(ticket, "Ticket has been taken by "+currentUserName,currentAppUserId);
            _ticketRepository.InsertOrUpdate(ticket);
        }
        public ViewTicketDTO GetTicketDetailsById(int id, UrlHelper Url)
        {
            var ticket = _ticketRepository.GetById(id);
            var data = new ViewTicketDTO();
            if (ticket != null)
            {
                data.Id = ticket.Id;
                data.ClientId = ticket.AppUserClient == null ? (int?)null : ticket.ClientId;
                data.Client = ticket.AppUserClient == null ? ticket.GuessClientName : ticket.AppUserClient.FullName;
                data.ClientAddress = ticket.AppUserClient == null ? ticket.GuessClientAddress : "test";
                data.ClientEmail = ticket.AppUserClient == null ? ticket.GuessClientEmail : ticket.AppUserClient.Email;
                data.TechnicianId = ticket.AppUserTechnician == null ? (int?)null : ticket.TechnicianId;
                data.Title = ticket.Title;
                data.Description = ticket.Description;
                data.Technician = ticket.AppUserTechnician?.FullName;
                data.TechnicianEmail = ticket.AppUserTechnician?.Email;
                data.Priority = ticket.Priority;
                data.IsResolved = ticket.TicketStatus == "R";
                data.Logs = ticket.Logs.Select(a => a.CreatedOn.ToString("yyyy-MM-dd hh:mm tt")+" "+ a.Message).ToList();
                data.Attachments = ticket.Attachments
               .Where(p => p.IsActive)
               .Select(p => new AttachmentViewModel
               {
                   tempName = p.ContentFile.FileName,
                   name = p.ContentFile.OrigFileName,
                   url = Path.Combine(p.ContentFile.ContentFileType.ConfigSettingFolder.Value, "temp", p.ContentFile.FileName),
                   type = p.ContentFile.FileType.MimeType,
                   size = p.ContentFile.Size,
                   thumbnailUrl = Url.Action("CheckThumbUpload", "FileUpload", new
                   {
                       area = "",
                       type = p.ContentFile.FileType.MimeType,
                       p.ContentFile.FileName,
                       Folder = p.ContentFile.ContentFileType.ConfigSettingFolder.Value
                   }),
                   deleteUrl = Url.Action("DeleteFile", "FileUpload", new
                   {
                       area = "",
                       file = p.ContentFile.FileName,
                       folderPath = Path.Combine(p.ContentFile.ContentFileType.ConfigSettingFolder.Value, "temp")
                   }),
                   deleteType = "GET",
                   isPdf = p.ContentFile.FileType.MimeType == "application/pdf",
                   isWord = p.ContentFile.FileType.MimeType == "application/msword",
                   downloadUrl = Url.Action("DownloadAttachment", "FileUpload", new
                   {
                       area = "",
                       filename = p.ContentFile.FileName,
                       folder = p.ContentFile.ContentFileType.ConfigSettingFolder.Value
                   }),
               }).ToList();
                data.Comments = ticket.Comments.Select(a => new CommentDTO
                {
                    Comment = a.Comment,
                    CreatedOn = a.CreatedOn,
                    Name = a.CreatedByAppUser.FullName
                }).ToList();
            }
            else
                data = null;
            return data;
        }
        public bool ResolveOrReopenTicket(int id, int currentAppuserId, string currentUserName)
        {
            var ticket = _ticketRepository.GetById(id);
            var action = "";
            if (ticket.TicketStatus == "O")
            {
                ticket.TicketStatus = "R";
                action = "resolved";
            }
            else
            {
                ticket.TicketStatus = "O";
                action = "reopened";
            }
            ticket.ModifiedByAppUserId = currentAppuserId;
            ticket.ModifiedOn = DateTime.Now;
            InsertTicketLog(ticket, currentUserName + " "+ action+" the ticket", currentAppuserId);
            _ticketRepository.InsertOrUpdate(ticket);
            return ticket.TicketStatus == "R";
        }
        public IPagedList<TicketCFLDTO> GetMyTickets(LookUpFilter filter, int currentAppuserId)
        {
            return _ticketRepository.GetMyTickets(filter, currentAppuserId);
        }
        private void AddOrUpdateTicketAttachment(ICollection<TicketAttachment> ticketAttachment,
        List<TicketAttachmentDTO> modelAttachments, Ticket ticket, int appuserId)
        {
            var folderPath = _configSettingService.GetByName(ConfigurationSettings.TicketAttachmentFolder.ToString()).Value;

            if (ticketAttachment != null)
            {
                foreach (var attachment in modelAttachments)
                {
                    var attachments = attachment.TicketAttachmentId == 0 ? null : ticket.Attachments.FirstOrDefault(p =>
                    p.Id == attachment.TicketAttachmentId);

                    if (attachments == null)
                    {
                        attachments = new TicketAttachment();
                        attachments.ContentFile = new ContentFile();
                        attachments.IsActive = true;
                        attachments.Name = attachment.name;
                        attachments.ContentFile.CreatedOn = DateTime.Now;
                        attachments.ContentFile.CreatedByAppUserId = appuserId;
                        ticketAttachment.Add(attachments);
                    }
                    var fileType = _fileTypeService.GetByType(attachment.type);
                    attachments.ContentFile.FileTypeId = fileType?.FileTypeId;
                    attachments.ContentFile.FileName = attachment.tempName;
                    attachments.ContentFile.OrigFileName = attachment.name;
                    attachments.ContentFile.Size = attachment.size;
                    attachments.ContentFile.ContentFileTypeId = (int)ContentFileTypes.TicketAttachmentFolder;

                    _contentFileService.MoveAttachment(folderPath, attachment.tempName);
                }
                var attachmentIds = modelAttachments != null
                ? modelAttachments.Select(a => a.TicketAttachmentId).ToList()
                : new List<int>();
                var attachmenForDelete = ticketAttachment.Where(a => !attachmentIds.Contains(a.Id));
                foreach (var item in attachmenForDelete.ToList())
                {
                   // _contentFileService.DeleteFile(folderPath, item.ContentFile.FileName);
                    item.ContentFile.IsActive = false;
                    item.IsActive = false;
                }
            }
        }
        private void InsertTicketLog(Ticket ticket, string comment, int currentAppuserId)
        {
            ticket.Logs.Add(new TicketLog
            {
                CreatedByAppUserId = currentAppuserId,
                Message = comment
            });
        }
    }
}
