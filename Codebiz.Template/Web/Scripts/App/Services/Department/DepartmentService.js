angular.module("MetronicApp").factory('DepartmentService', ['CommonService', function (commonService) {
    return {
        GetDepartment: function (args) {
            return commonService.GetData(args, document.Department + 'GetDepartment');
        },
        AddDepartment: function (data) {
            return commonService.PostData(data, document.Department + 'AddDepartment');
        },
        UpdateDepartment: function (data) {
            return commonService.PostData(data, document.Department + 'UpdateDepartment');
        },
        ToggleDepartmentActiveStatus: function (data) {
            return commonService.PostData(data, document.Department + 'ToggleDepartmentActiveStatus');
        },
        departmentDelete: function (data) {
            return commonService.PostData(data, document.Department + 'DeleteDepartment');
        },
        GetDepartmentDetails: function (args) {
            return commonService.GetData(args, document.Department + 'GetDepartmentDetails');
        },
        ExportDataToExcelFile: function (args) {
            return commonService.GetData(args, document.Department + 'ExportDataToExcelFile');
        },
        GetDivisionCategoryByDivisionId: function (args, index) {
            return commonService.GetData(args, document.Department + 'GetDivisionCategoryByDivisionId',"CategoryId"+index);
        }
    };
}]);
