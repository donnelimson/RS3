using Codebiz.Domain.Common.Model;
using Domain.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Services
{
    public interface IEmployeePhotoService
    {
        EmployeePhoto GetById(int Id);
        void InsertOrUpdate(EmployeePhoto employeePhoto, int id);
        IEnumerable<EmployeePhoto> GetPhotoById(int id);
        EmployeePhoto GetCurrentCroppedPhotoById(int Id);
        EmployeePhoto GetCurrentPhotoById(int id);
    }

    public class EmployeePhotoService : IEmployeePhotoService
    {
        private readonly IEmployeePhotoRepository _employeePhotoRepository;

        public EmployeePhotoService(
           IEmployeePhotoRepository employeePhotoRepository
       )
        {
            _employeePhotoRepository = employeePhotoRepository;
        }

        public IEnumerable<EmployeePhoto> GetPhotoById(int id)
        {
            return _employeePhotoRepository.GetAll.Where(a => a.EmployeeId == id).AsEnumerable();
        }

        
        public EmployeePhoto GetById(int Id)
        {
            return _employeePhotoRepository.GetAll.FirstOrDefault(a => a.EmployeeId == Id);
        }

        public EmployeePhoto GetCurrentCroppedPhotoById(int Id)
        {
            return _employeePhotoRepository.GetAll.OrderByDescending(x => x.EmployeePhotoId).FirstOrDefault(a => a.EmployeeId == Id);
        }

        public void InsertOrUpdate(EmployeePhoto entity, int id) 
        {
            _employeePhotoRepository.InsertOrUpdate(entity);
        }

        public EmployeePhoto GetCurrentPhotoById(int id)
        {
            return _employeePhotoRepository.GetAll.FirstOrDefault(a => a.EmployeeId == id);
        }
    }
}
