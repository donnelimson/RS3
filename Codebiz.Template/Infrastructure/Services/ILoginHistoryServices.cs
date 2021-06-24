using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using System.Net;

namespace Infrastructure.Services
{
    public interface ILoginHistoryServices
    {
        LoginHistory GetByAppUserIdAndSessionId(int appUserid, string sessionid);
        LoginHistory GetByAppUserId(int appUserid);


        void InsertOrUpdate(LoginHistory entity, int loginHistoryId);
        IEnumerable<LoginHistory> GetAllNullLogoutsByAppUserId(int id);
        IEnumerable<LoginHistory> GetAll();
    }

    public class LoginHistoryServices : ILoginHistoryServices
    {
        private readonly ILoginHistoryRepository _loginHistoryRepository;

        public LoginHistoryServices(

            ILoginHistoryRepository loginHistoryRepository

        )
        {
            _loginHistoryRepository = loginHistoryRepository;
        }

        public IEnumerable<LoginHistory> GetAll()
        {
            return _loginHistoryRepository.GetAll.AsEnumerable();
        }

        public LoginHistory GetByAppUserIdAndSessionId(int appUserid, string sessionid)
        {
            return _loginHistoryRepository.GetAll
                .Where(a => a.AppUserId == appUserid && a.SessionID == sessionid)
                .OrderByDescending(x => x.LoginHistoryId)
                .FirstOrDefault();

        }
        public LoginHistory GetByAppUserId(int appUserid)
        {
            return _loginHistoryRepository.GetAll
                .Where(a => a.AppUserId == appUserid)
                .OrderByDescending(x => x.LoginHistoryId)
                .FirstOrDefault();

        }
        public void InsertOrUpdate(LoginHistory entity, int loginHistoryId)
        {

            _loginHistoryRepository.InsertOrUpdate(entity);
        }

        public IEnumerable<LoginHistory> GetAllNullLogoutsByAppUserId(int id)
        {
            return _loginHistoryRepository.GetAll.Where(x => x.AppUserId == id && x.LogoutDateTime == null).AsEnumerable();
        }

    }
}
