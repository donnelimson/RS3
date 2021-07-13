using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using System.Web.Mvc;
using Codebiz.Domain.Common.Model.Enums;
using Domain.Repository;

namespace Infrastructure.Services
{
    public interface IFileServices
    {
        void CreateFile(Stream stream,string fullFileName);
    }

    public class FileServices : IFileServices
    {
        private readonly IConfigSettingService _configSettingService;

        public FileServices(
            IConfigSettingService configSettingService)
        {
            _configSettingService = configSettingService;
        }

        public void CreateFile(Stream stream, string fullFileName)
        {
            using (var fs = new FileStream(fullFileName, FileMode.Create, FileAccess.ReadWrite))
            {
                stream.CopyTo(fs);
                fs.Flush();
            }
        }
    }
}
