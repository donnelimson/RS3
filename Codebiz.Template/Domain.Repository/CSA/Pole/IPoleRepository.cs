using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.DTOs.Pole;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IPoleRepository : IRepositoryBase<Pole>
    {
        IPagedList<PoleDTO> SearchPole(PoleFilter filter);
        IEnumerable<Pole> GetPolesByCityTownId(int id);
        Pole GetById(int id);
        bool CheckIfPoleNoIsExist(string poleNo, int id = 0);
        bool CheckIfCodeIsName(string name, int id = 0);
        PoleLatitudeLongtitudeDTO GetLocationOfPole(string poleName);
        IEnumerable<PoleToExcel> GetDataForExportingToExcel(PoleFilter filter);
        IEnumerable<Pole> GetAllPole();
        PoleLatitudeLongtitudeDTO GetLocationById(int id);
        IPagedList<PoleLookUpDTO> GetPoleLookUpList(LookUpFilter filter);

        List<PoleLookUpDTO> GetByPoleNo(int provinceId, int cityTownId, int barangayId, string poleNo);
    }
}
