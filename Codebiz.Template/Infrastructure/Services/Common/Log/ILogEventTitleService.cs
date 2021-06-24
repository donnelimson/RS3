
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface ILogEventTitleService
    {
        LogEventTitle GetById(int id);

        LogEventTitle GetName(string name);

        IEnumerable<LogEventTitle> GetAll();
    }

    public class LogEventTitleService : ILogEventTitleService
    {
        private readonly ILogEventTitleRepository _logEventTitleRepository;

        public LogEventTitleService(ILogEventTitleRepository logEventTitleRepository)
        {
            _logEventTitleRepository = logEventTitleRepository;
        }

        public LogEventTitle GetById(int id)
        {
            return _logEventTitleRepository.GetAll.Where(a => a.Id == id).FirstOrDefault();
        }

        public LogEventTitle GetName(string name)
        {
            return _logEventTitleRepository.GetAll.Where(a => a.Name == name).FirstOrDefault();
        }

        public IEnumerable<LogEventTitle> GetAll()
        {
            return _logEventTitleRepository.GetAll.AsEnumerable();
        }
    }
}
