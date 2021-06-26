using Codebiz.Domain.Common.Model.DTOs.RS3;
using Codebiz.Domain.Common.Model.Filter.RS3;
using Infrastructure.Repository.Common;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Common
{
    public interface ITicketService
    {
        IPagedList<TicketIndexDTO> GetAllOpenTickets(TicketFilter filter);
    }
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public IPagedList<TicketIndexDTO> GetAllOpenTickets(TicketFilter filter)
        {
            return _ticketRepository.GetAllOpenTickets(filter);
        }
    }
}
