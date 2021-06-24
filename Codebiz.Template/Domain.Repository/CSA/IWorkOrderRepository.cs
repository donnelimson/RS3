using Codebiz.Domain.Common.Model.DataModel.CSA;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.CSA
{
    public interface IWorkOrderRepository : IRepositoryBase<WorkOrder>
    {
        WorkOrder GetById(int id);
        IEnumerable<WorkOrder> GetAllWorkOrders();
        WorkOrder GetallById(int? workOrderId);
        bool IsWorkOrderCodeExist(string workOrderCode, int id);
        IPagedList<WorkOrderDTO> SearchWorkOrder(WorkOrderFilter workOrderFilter);
        bool IsWorkOrderNameExist(string workOrderName, int id);
        IEnumerable<WorkOrderToExcel> GetDataForExportingToExcel(WorkOrderFilter filter);

    }
}
