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

namespace Infrastructure.Services.Common
{
    public interface ITicketService
    {
        IPagedList<TicketIndexDTO> GetAllOpenTickets(TicketFilter filter);
        void AddOrUpdate(TicketAddOrUpdateDTO viewModel, int currentAppUserId);
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
        public void AddOrUpdate(TicketAddOrUpdateDTO viewModel, int currentAppUserId)
        {
            var ticket = _ticketRepository.GetById(viewModel.Id);
            if (ticket == null)
            {
                ticket = new Ticket();
            }
            ticket.Title = viewModel.Title;
            ticket.Description = viewModel.Description;
            ticket.Priority = viewModel.Priority;
            ticket.ClientId = viewModel.ClientId;
            ticket.GuessClientAddress = viewModel.ClientAddress;
            ticket.GuessClientEmail = viewModel.ClientEmail;
            ticket.GuessClientName = viewModel.Client;
            ticket.TechnicianId = viewModel.TechnicianId;
            _ticketRepository.InsertOrUpdate(ticket);
            AddOrUpdateTicketAttachment(ticket.Attachments, viewModel.Attachments, ticket, currentAppUserId);
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
                    attachments.ContentFile.ContentFileTypeId = (int)ContentFileTypes.ItemMasterDataAttachment;

                    _contentFileService.MoveAttachment(folderPath, attachment.tempName);
                }
                var attachmentIds = modelAttachments != null
                ? modelAttachments.Select(a => a.TicketAttachmentId).ToList()
                : new List<int>();
                var attachmenForDelete = ticketAttachment.Where(a => !attachmentIds.Contains(a.Id));
                foreach (var item in attachmenForDelete.ToList())
                {
                    _contentFileService.DeleteFile(folderPath, item.ContentFile.FileName);
                    item.ContentFile.IsActive = false;
                    item.IsActive = false;
                }
            }
        }
    }
}
