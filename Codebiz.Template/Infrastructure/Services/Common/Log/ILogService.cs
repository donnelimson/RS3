using PagedList;
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Common.Model.DTOs;
using Infrastructure.Utilities.QueryHelper;
using System.Collections;
using System.Reflection;

namespace Infrastructure.Services
{
    public interface ILogService
    {
        Log GetById(int id);

        IEnumerable<Log> GetAll();
        IPagedList<AuditLogsDTO> Search(AuditLogsFilter auditLogsFilter);
        StringBuilder BaseModelLogCreator(dynamic model, dynamic viewModel);
        void AddAttachmentLog(int logEventTitleId, List<FIleNameAndUrl> fIleNameAndUrls);
    }

    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(
            ILogRepository logRepository
        )
        {
            _logRepository = logRepository;
        }

        public IEnumerable<Log> GetAll()
        {
            return _logRepository.GetAll.AsEnumerable();
        }

        public Log GetById(int id)
        {
            return _logRepository.GetAll.Where(a => a.Id == id).FirstOrDefault();
        }

        public IPagedList<AuditLogsDTO> Search(AuditLogsFilter auditLogsFilter)
        {
            var data = _logRepository.GetAll;

            if (!string.IsNullOrEmpty(auditLogsFilter.ActivityId))
            {
                data = data.Where(m => m.ActivityId.Contains(auditLogsFilter.ActivityId));
            }

            if (auditLogsFilter.CreatedOnFrom != null && auditLogsFilter.CreatedOnTo != null)
            {
                data = data.Where(m => m.Date >= auditLogsFilter.CreatedOnFrom && m.Date <= auditLogsFilter.CreatedOnTo);
            }

            if (!string.IsNullOrEmpty(auditLogsFilter.User))
            {
                data = data.Where(m => m.User.Contains(auditLogsFilter.User));
            }

            if (!string.IsNullOrEmpty(auditLogsFilter.Thread))
            {
                data = data.Where(m => m.Thread.Contains(auditLogsFilter.Thread));
            }

            if (auditLogsFilter.Level != null && auditLogsFilter.Level.Count > 0)
            {
                data = data.Where(m => auditLogsFilter.Level.Contains(m.Level));
            }
            if (auditLogsFilter.LogEventTitleId != null)
            {
                data = data.Where(m => m.LogEventTitleId == auditLogsFilter.LogEventTitleId);
            }
            if (!string.IsNullOrEmpty(auditLogsFilter.Logger))
            {
                data = data.Where(m => m.Logger.Contains(auditLogsFilter.Logger));
            }

            if (auditLogsFilter.LogEventTitleId != null)
            {
                data = data.Where(m => m.LogEventTitleId == auditLogsFilter.LogEventTitleId);
            }

            if (!string.IsNullOrEmpty(auditLogsFilter.Message))
            {
                data = data.Where(m => m.Message.Contains(auditLogsFilter.Message));
            }

            if (!string.IsNullOrEmpty(auditLogsFilter.Exception))
            {
                data = data.Where(m => m.Exception.Contains(auditLogsFilter.Exception));
            }

            auditLogsFilter.FilteredRecordCount = data.Count();

            var dataDTO = data.Select(a => new AuditLogsDTO
            {
                Id = a.Id,
                ActivityId = a.ActivityId,
                Date = a.Date,
                User = a.User,
                Thread = a.Thread,
                Level = a.Level,
                Logger = a.Logger,
                Message = a.Message,
                Exception = a.Exception,
                LogEventTitle = a.LogEventTitle.Description,
                Attachments = a.Attachments,
               FileUploadAttachments = a.FileUploadAttachments,
               Changes = a.Changes

            });

            dataDTO = QueryHelper.Ordering(dataDTO, auditLogsFilter.SortColumn, auditLogsFilter.SortDirection != "asc", false);

            return dataDTO.ToPagedList(auditLogsFilter.Page, auditLogsFilter.PageSize);

        }
        public StringBuilder BaseModelLogCreator(object model, object viewModel)
        {
            StringBuilder logger = new StringBuilder();
            foreach (var item in viewModel.GetType().GetProperties().Where(x=>x.PropertyType == typeof(string) || x.PropertyType == typeof(Int32) || x.PropertyType == typeof(decimal) || x.PropertyType == typeof(bool) || x.PropertyType == typeof(Int32?) || x.PropertyType == typeof(decimal?)))
            {
                try
                {
                    var modelValue = model.GetType().GetProperty(item.Name).GetValue(model, null);
                    var viewModelValue = item.GetValue(viewModel, null);
                    if (!modelValue.Equals(viewModelValue))
                    {
                        logger.Append("Field: " + item.Name + " changed from " + modelValue + " to " + viewModelValue + " ");
                    }  
                }
                catch
                {
                    continue;
                }
            }
            foreach (var item in viewModel.GetType().GetProperties().Where(x => x.PropertyType != typeof(string) && x.PropertyType != typeof(Int32) && x.PropertyType != typeof(decimal) && x.PropertyType != typeof(bool) && x.PropertyType != typeof(Int32?) && x.PropertyType != typeof(decimal?)))
            {
                try
                {
                    var modelValue = model.GetType().GetProperty(item.Name).GetValue(model, null);
                    var viewModelValue = item.GetValue(viewModel, null);
                    logger.Append(IcollectionComparison(modelValue, viewModelValue, item.Name).ToString());
                }
                catch(Exception ex)
                {
                    continue;
                }
            }
            return logger;
        }
        public StringBuilder IcollectionComparison(object model, object viewModel, string collectionName)
        {
            StringBuilder logger = new StringBuilder();
            IList viewModelCollection = (IList)viewModel;
            IList modelCollection = (IList)model;
            var counter = 0;
            foreach (var item in viewModelCollection)
            {
                foreach(var property in item.GetType().GetProperties())
                {
                    try
                    {
                        var viewModelValue = property.GetValue(item, null);
                        if (collectionName.Contains("Attachment"))
                        {
                            if (modelCollection.Count <= counter)
                            {
                                viewModelValue = item.GetType().GetProperty("name", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(item, null);
                                var wasap = item.GetType().GetProperty("thumbnailurl", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(item, null).ToString();
                                logger.Append("Collection: " + collectionName + " [" + "FileName" + "] " + " added file " + viewModelValue);

                                AjaxResult.Attachments.Add(new FIleNameAndUrl { 
                                    FileName = viewModelValue.ToString(),
                                    FileUploadUrl = item.GetType().GetProperty("thumbnailurl", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(item, null).ToString()
                                  });
                                break;
                            }
                        }
                        else
                        {
                            var modelValue = modelCollection[counter].GetType().GetProperty(property.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(modelCollection[counter], null);
                            if (!modelValue.Equals(viewModelValue) && viewModelValue != null)
                            {
                                logger.Append("Collection: " + collectionName + " [" + property.Name + "] " + " changed from " + modelValue + " to " + viewModelValue + " ");
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        continue;
                    }
            
                }
                counter++;

            }
            return logger;
        }
        public void AddAttachmentLog(int logEventTitleId, List<FIleNameAndUrl> fIleNameAndUrls)
        {
            var log = _logRepository.GetByLogEventTitleId(logEventTitleId);
         //   var log = new Log();
            foreach(var file in fIleNameAndUrls)
            {
                log.LogAttachments.Add(new Codebiz.Domain.Common.Model.DataModel.Log.LogAttachment
                {
                    //FileName = file.FIleName,
                    //FileUrl = file.FIleUrl
                });
            }
            _logRepository.InsertOrUpdate(log);
        }
    }
}
