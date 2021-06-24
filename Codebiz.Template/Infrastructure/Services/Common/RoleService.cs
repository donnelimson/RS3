using Codebiz.Domain.Common.Model.DTOs;
using Infrastructure.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Common
{
    public interface IRoleService
    {
        List<Select2LookUpDTO> GetRolesForSelect2LookUp();
    }
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(
        IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public List<Select2LookUpDTO> GetRolesForSelect2LookUp()
        {
            return _roleRepository.GetRolesForSelect2LookUp();
        }
    }
}
