angular.module("MetronicApp").factory('EmployeeService', ['CommonService', function (commonService) {
    return {
        GetDetails: function (data) {
            return commonService.GetData(data, document.Employees + 'GetEmployeeDetails', null);
        },
        GetEmployeeByCode: function (args) {
            return commonService.GetData(args, document.Employees + 'GetEmployeeByCode');
        },
        GetAllByOfficeAndPosition: function (args) {
            return commonService.GetData(args, document.baseUrlNoArea + 'api/Employee/GetAllByOfficeAndPosition');
        },
        SearchEmployeeWithLicenseNo: function (args) {
            return commonService.PostData(args, document.baseUrlNoArea + 'api/EmployeeWithLicenseNo/GetList');
        },
        ExportEmployeeWithLicenseNoDataToExcelFile: function (data) {
            return commonService.PostData(data, document.baseUrlNoArea + 'api/EmployeeWithLicenseNo/Export');
        },
        SaveEmployeeLicenseData: function (args) {
            return commonService.PostData(args.model, document.baseUrlNoArea + 'api/EmployeeWithLicenseNo/SaveData');
        },
        GetLicenseRestrictionCode: function (args, id) {
            return commonService.GetData(args, document.Employees + 'GetRestrictionCode', id);
        }
    };
}]);
