using Codebiz.Domain.Common.Model.DataModel;
using Codebiz.Domain.Common.Model.DTOs.RS3;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Common.Model.Filter.RS3;
using Codebiz.Domain.Repository;
using Domain.Context;
using Infrastructure.Utilities.QueryHelper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Common
{
    public interface ITicketRepository : IRepositoryBase<Ticket>
    {
        IPagedList<TicketIndexDTO> GetAllOpenTickets(TicketFilter filter);
        Ticket GetById(int id);
        IPagedList<TicketCFLDTO> GetMyTickets(LookUpFilter filter, int currentAppuserId);
    }
    public class TicketRepository : RepositoryBase<Ticket>, ITicketRepository
    {
        public TicketRepository(AppCommonContext context) : base(context)
        {
        }
        public Ticket GetById(int id)
        {
            return GetAll.Where(x => x.Id == id).FirstOrDefault();
        }
        public override void InsertOrUpdate(Ticket entity)
        {
            if (entity.Id.Equals(0))
            {
                entity.TicketNo = GenerateTicketNo();
                entity.TicketStatus = "O";
                this.Context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                this.Context.Entry(entity).State = EntityState.Modified;
            }
        }
   
        public IPagedList<TicketIndexDTO> GetAllOpenTickets(TicketFilter filter)
        {
            var data = GetData(filter);
            filter.FilteredRecordCount = data.Count();
            var dataDTO = data.Select(a => new TicketIndexDTO
            {
                TicketNo = a.TicketNo,
                Status = a.TicketStatus,
                ClientName = a.AppUserClient != null
                    ? a.AppUserClient.LastName + ", " + a.AppUserClient.FirstName + (a.AppUserClient.MiddleName == null ? "" : " " + a.AppUserClient.MiddleName) + (a.AppUserClient.Suffix == null ? "" : " " + a.AppUserClient.Suffix)
                    : a.GuessClientName,
                AssignedTo = a.AppUserTechnician != null
                    ? a.AppUserTechnician.LastName + ", " + a.AppUserTechnician.FirstName + (a.AppUserTechnician.MiddleName == null ? "" : " " + a.AppUserTechnician.MiddleName) + (a.AppUserTechnician.Suffix == null ? "" : " " + a.AppUserTechnician.Suffix)
                    : "N/A",
                CreatedOn = a.CreatedOn,
                LastUpdatedBy = a.ModifiedByAppUser!= null ? a.ModifiedByAppUser.LastName + ", " + a.ModifiedByAppUser.FirstName + (a.ModifiedByAppUser.MiddleName == null ? "" : " " + a.ModifiedByAppUser.MiddleName) + (a.ModifiedByAppUser.Suffix == null ? "" : " " + a.ModifiedByAppUser.Suffix)
                    : a.CreatedByAppUser.LastName + ", " + a.CreatedByAppUser.FirstName + (a.CreatedByAppUser.MiddleName == null ? "" : " " + a.CreatedByAppUser.MiddleName) + (a.CreatedByAppUser.Suffix == null ? "" : " " + a.CreatedByAppUser.Suffix),
                LastUpdatedOn = a.ModifiedOn == null ? a.CreatedOn : a.ModifiedOn,
                CreatedBy = a.CreatedByAppUser.LastName + ", " + a.CreatedByAppUser.FirstName + (a.CreatedByAppUser.MiddleName == null ? "" : " " + a.CreatedByAppUser.MiddleName) + (a.CreatedByAppUser.Suffix == null ? "" : " " + a.CreatedByAppUser.Suffix),
                Priority = a.Priority,
                Title =a.Title,
                Id=a.Id
            });
            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);
            return dataDTO.ToPagedList(filter.Page, filter.PageSize);
        }
        private IQueryable<Ticket> GetData(TicketFilter filter)
        {
            var data = GetAll.Where(x => x.IsActive);
            if (!string.IsNullOrEmpty(filter.TicketNo))
            {
                data = data.Where(x => x.TicketNo.Contains(filter.TicketNo));
            }
            if (!string.IsNullOrEmpty(filter.CreatedBy))
            {
                filter.CreatedBy = filter.CreatedBy.Trim();

                data = data.Where(a => (a.CreatedByAppUser.LastName + ", " +
                                a.CreatedByAppUser.FirstName +
                                (a.CreatedByAppUser.MiddleName != null ? " " + a.CreatedByAppUser.MiddleName : "") +
                                (a.CreatedByAppUser.Suffix != null ? " " + a.CreatedByAppUser.Suffix : "")).Trim().Contains(filter.CreatedBy));
            }
            if (!string.IsNullOrEmpty(filter.Technician))
            {
                filter.Technician = filter.Technician.Trim();

                data = data.Where(a => (a.AppUserTechnician.LastName + ", " +
                                a.AppUserTechnician.FirstName +
                                (a.AppUserTechnician.MiddleName != null ? " " + a.AppUserTechnician.MiddleName : "") +
                                (a.AppUserTechnician.Suffix != null ? " " + a.AppUserTechnician.Suffix : "")).Trim().Contains(filter.Technician));
            }

            if (!string.IsNullOrEmpty(filter.ConcernWhom))
            {
                filter.ConcernWhom = filter.ConcernWhom.Trim();

                data = data.Where(a => (a.AppUserClient.LastName + ", " +
                                a.AppUserClient.FirstName +
                                (a.AppUserClient.MiddleName != null ? " " + a.AppUserClient.MiddleName : "") +
                                (a.AppUserClient.Suffix != null ? " " + a.AppUserClient.Suffix : "")).Trim().Contains(filter.ConcernWhom));
            }
            if (!string.IsNullOrEmpty(filter.Status))
            {
                data = data.Where(x => x.TicketStatus == filter.Status);
            }
            if (filter.Priority != null)
            {
                data = data.Where(x => x.Priority == filter.Priority);
            }
            if (filter.CreatedOnFrom != null && filter.CreatedOnTo != null)
            {
                data = data.Where(a => DbFunctions.TruncateTime(a.CreatedOn) >= filter.CreatedOnFrom && DbFunctions.TruncateTime(a.CreatedOn) <= filter.CreatedOnTo);
            }
            if (filter.TechnicianId != null)
            {
                data = data.Where(x => x.TechnicianId == filter.TechnicianId);

            }
            return data;
        }
        public IPagedList<TicketCFLDTO> GetMyTickets(LookUpFilter filter, int currentAppuserId)
        {
            var data = GetAll.Where(x => x.TechnicianId == currentAppuserId || x.ClientId == currentAppuserId);
            if (!string.IsNullOrEmpty(filter.Searcher))
            {
                filter.Searcher = filter.Searcher.Trim();
                data = data.Where(a => a.TicketNo.Contains(filter.Searcher) || a.Title.Contains(filter.Searcher)
                || (a.AppUserClient.LastName + ", " +
                                a.AppUserClient.FirstName +
                                (a.AppUserClient.MiddleName != null ? " " + a.AppUserClient.MiddleName : "") +
                                (a.AppUserClient.Suffix != null ? " " + a.AppUserClient.Suffix : "")).Trim().Contains(filter.Searcher) ||
                                 (a.AppUserTechnician.LastName + ", " +
                                a.AppUserTechnician.FirstName +
                                (a.AppUserTechnician.MiddleName != null ? " " + a.AppUserTechnician.MiddleName : "") +
                                (a.AppUserTechnician.Suffix != null ? " " + a.AppUserTechnician.Suffix : "")).Trim().Contains(filter.Searcher) ||
                                a.CreatedOn.ToString().Contains(filter.Searcher));
            }
            filter.FilteredRecordCount = data.Count();
            var dataDTO = data.Select(a => new TicketCFLDTO
            {
               ClientName = a.AppUserClient.FirstName +
                                (a.AppUserClient.MiddleName != null ? " " + a.AppUserClient.MiddleName : "") +
                                (a.AppUserClient.Suffix != null ? " " + a.AppUserClient.Suffix : ""),
               TechnicianName = a.AppUserTechnician.FirstName +
                                (a.AppUserTechnician.MiddleName != null ? " " + a.AppUserTechnician.MiddleName : "") +
                                (a.AppUserTechnician.Suffix != null ? " " + a.AppUserTechnician.Suffix : ""),
               TicketNo = a.TicketNo,
               Title  = a.Title,
               CreatedOn = a.CreatedOn,
               Id = a.Id,
               Status =a.TicketStatus
            });
            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);
            return dataDTO.ToPagedList(filter.Page, filter.PageSize);
        }
        private string GenerateTicketNo()
        {
            var latestId = GetAll.OrderByDescending(x => x.Id).FirstOrDefault()?.Id;
            return (latestId == null ? "1":(latestId+1).ToString()).PadLeft(7,'0');
        }
    }
}
