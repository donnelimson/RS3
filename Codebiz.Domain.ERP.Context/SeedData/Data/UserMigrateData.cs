using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DataModel.CSA;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Codebiz.Domain.ERP.Context.SeedData
{
    public static class UserMigrateData
    {
        public static void Seed_Users(DbTrackerContext context)
        {
            if (!context.AppUsers.Any(p => p.Username != "admin"))
            {
                try
                {
                    var resFile = "Codebiz.Domain.ERP.Context.MigrationTemplate.Employee.txt";
                    var col_list = Helpers.ReadAndMapFile<AppUserMigrateData, int>(resFile);

                    col_list.ForEach(item =>
                    {
                        var user = context.AppUsers.FirstOrDefault(p => p.Username == item.Username);

                        if (user == null)
                        {
                            var appUser = new AppUser
                            {
                                Username = item.Username,
                                PasswordHash = "07d8cada9b0b50464914625cb1a28a47e7e95afb:2b37f7a149ae82552504732b5df3201c263eb45e",
                                CreatedByAppUserId = 1,
                                CreatedOn = DateTime.Now,
                                AccessFailedCount = 0,
                                IsActive = true
                            };

                            var employeeData = context.Employees.FirstOrDefault(x => x.FirstName.Trim().ToUpper() == item.FirstName.Trim().ToUpper());
                            appUser.EmployeeId = employeeData.EmployeeId;

                            context.AppUsers.AddOrUpdate(x => x.Username, appUser);
                            context.SaveChanges();
                        }
                    });
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public static void Seed_Employees(DbTrackerContext context)
        {
            if (!context.Employees.Any(p => p.FirstName != "Admin"))
            {
                try
                {
                    var positioncounter = 1;
                    var resFile = "Codebiz.Domain.ERP.Context.MigrationTemplate.Employee.txt";
                    var col_list = Helpers.ReadAndMapFile<AppUserMigrateData, int>(resFile);
                    var seeddingNo = 1;

                    col_list.ForEach(item =>
                    {
                        var employee = context.Employees.FirstOrDefault(p => (p.FirstName + p.LastName).ToLower() == (item.FirstName + item.LastName).ToLower());
                        var userData = context.AppUsers.FirstOrDefault(a => a.Username == item.Username);

                        if (employee == null)
                        {
                            seeddingNo++;

                            employee = new Employee
                            {
                                EmployeeNo = "EMP-" + seeddingNo.ToString().PadLeft(7, '0'),
                                BadgeNo = seeddingNo.ToString().PadLeft(7, '0'),
                                LastName = item.LastName,
                                FirstName = item.FirstName,
                                MiddleName = item.MiddleName,
                                Email = item.Email,
                                CreatedByAppUserId = 1,
                                CreatedOn = DateTime.Now,
                                IsActive = true
                            };

                            #region Position

                            var position = context.Positions.FirstOrDefault(x => x.Name.Trim().ToUpper() == item.Position.Trim().ToUpper());
                            if (position == null)
                            {
                                context.Positions
                                    .AddOrUpdate(a => new { a.Code },
                                          new Position
                                          {
                                              Code = positioncounter++.ToString().PadLeft(4, '0'),
                                              Name = item.Position,
                                              IsManager = item.Manager == "YES",
                                              IsHeadOfficer = item.Head == "YES",
                                              IsActive = true,
                                              CreatedByAppUserId = 1,
                                              CreatedOn = DateTime.Now
                                          });
                                context.SaveChanges();
                            }

                            #endregion

                            #region Division

                            if (!string.IsNullOrEmpty(item.DivisionCode.Trim()))
                            {
                                var division = context.Divisions.FirstOrDefault(p => p.Code == item.DivisionCode.Trim());
                                if (division == null)
                                {
                                    context.Divisions.AddOrUpdate(a => new { a.Code },
                                       new Division
                                       {
                                           Name = item.Division.Trim(),
                                           Code = item.DivisionCode.Trim(),
                                           CreatedByAppUserId = 1,
                                           IsActive = true,
                                           CreatedOn = DateTime.Now,
                                       });
                                    context.SaveChanges();
                                }

                                division = context.Divisions.FirstOrDefault(p => p.Code == item.DivisionCode.Trim());
                                var categories = new List<DivisionType>();
                                if (item.DivisionCode.Trim() == "PDAM" || item.DivisionCode.Trim() == "PD&DA")
                                {
                                    var name = Helpers.GetEnumDescription(DivisionTypeEnums.Planning).ToUpper();
                                    var planning = context.DivisionTypes.FirstOrDefault(p => p.Name.ToUpper() == name);
                                    if (planning != null)
                                        categories.Add(planning);
                                }

                                if (item.DivisionCode.Trim() == "SEMTD")
                                {
                                    var name = Helpers.GetEnumDescription(DivisionTypeEnums.Metering).ToUpper();
                                    var metering = context.DivisionTypes.FirstOrDefault(p => p.Name.ToUpper() == name);
                                    if (metering != null)
                                        categories.Add(metering);

                                    var transformerName = Helpers.GetEnumDescription(DivisionTypeEnums.Transformer).ToUpper();
                                    var transformer = context.DivisionTypes.FirstOrDefault(p => p.Name.ToUpper() == transformerName);
                                    if (transformer != null)
                                        categories.Add(transformer);
                                }

                                if (item.DivisionCode.Trim() == "COMD")
                                {
                                    var name = Helpers.GetEnumDescription(DivisionTypeEnums.Construction).ToUpper();
                                    var construction = context.DivisionTypes.FirstOrDefault(p => p.Name.ToUpper() == name);
                                    if (construction != null)
                                        categories.Add(construction);
                                }

                                foreach (var category in categories)
                                {
                                    if (!division.Categories.Any(p => p.DivisionTypeId == category.Id))
                                    {
                                        division.Categories.Add(
                                            new DivisionCategory
                                            {
                                                DivisionTypeId = category.Id
                                            });
                                    }
                                }
                            }

                            #endregion

                            #region Department

                            if (!string.IsNullOrEmpty(item.DepartmentCode.Trim()))
                            {
                                var department = context.Departments.FirstOrDefault(p => p.Code == item.DepartmentCode.Trim());
                                if (department == null)
                                {
                                    context.Departments.AddOrUpdate(a => new { a.Code },
                                       new Department
                                       {
                                           Name = item.Department.Trim(),
                                           Code = item.DepartmentCode.Trim(),
                                           CreatedByAppUserId = 1,
                                           IsActive = true,
                                           CreatedOn = DateTime.Now,
                                       });
                                    context.SaveChanges();
                                }
                            }

                            #endregion

                            #region Add office department

                            var departmentData = context.Departments.FirstOrDefault(p => p.Code == item.DepartmentCode.Trim());
                            var office = context.Offices.FirstOrDefault(p => p.Code == item.OfficeCode);
                            if (office != null)
                            {
                                var existOfficeDepartment = office.OfficeDepartments
                                                .Any(p => p.OfficeId == office.OfficeId &&
                                                          p.DepartmentId == departmentData.DepartmentId);
                                if (!existOfficeDepartment)
                                {
                                    office.OfficeDepartments.Add(
                                        new OfficeDepartment
                                        {
                                            OfficeId = office.OfficeId,
                                            DepartmentId = departmentData.DepartmentId
                                        });

                                    context.SaveChanges();
                                }
                            }

                            #endregion

                            #region Department detail

                            var positionData = context.Positions.FirstOrDefault(x => x.Name.Trim().ToUpper() == item.Position.Trim().ToUpper());
                            var divisionData = context.Divisions.FirstOrDefault(p => p.Code == item.DivisionCode.Trim());

                            var existDepartmentDetails = departmentData.Details
                                .Any(p => p.DepartmentId == departmentData.DepartmentId &&
                                          p.PositionId == positionData.PositionId);

                            if (!existDepartmentDetails)
                            {
                                departmentData.Details.Add(
                                    new DepartmentDetail
                                    {
                                        DepartmentId = departmentData.DepartmentId,
                                        PositionId = positionData.PositionId,
                                        DivisionId = divisionData?.DivisionId
                                    });
                                context.SaveChanges();
                            }

                            #endregion

                            var officeData = context.Offices.FirstOrDefault(p => p.Code.Trim().ToUpper() == item.OfficeCode.Trim().ToUpper());
                            employee.OfficeId = officeData != null ? officeData.OfficeId : (int?)null;

                            employee.PositionId = positionData != null ? positionData.PositionId : (int?)null;
                            employee.DepartmentId = departmentData != null ? departmentData.DepartmentId : (int?)null;
                            employee.DivisionId = divisionData != null ? divisionData.DivisionId : (int?)null;

                            context.Employees.AddOrUpdate(x => x.Email, employee);
                            context.SaveChanges();
                        }

                        //if (userData == null)
                        //{
                        //    userData = new AppUser
                        //    {
                        //        Username = item.Username,
                        //        EmployeeId = employee.EmployeeId,
                        //        PasswordHash = "07d8cada9b0b50464914625cb1a28a47e7e95afb:2b37f7a149ae82552504732b5df3201c263eb45e",
                        //        CreatedByAppUserId = 1,
                        //        CreatedOn = DateTime.Now,
                        //        AccessFailedCount = 0,
                        //        IsActive = true
                        //    };

                        //    context.SaveChanges();
                        //}
                        //else
                        //{
                        //    userData.EmployeeId = employee.EmployeeId;
                        //}
                    });
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }


        public static void Seed_UserGroups(DbTrackerContext context)
        {
            if (!context.UserGroups.Any())
            {
                var usergroups = new List<string>{
                    "Dashboard",
                    "Administrator",
                    "Account",
                    "View account details",
                    "Applicant",
                    "Member",
                    "View consumer details",
                    "PMOS",
                    "HW Inspector",
                    "Issue for connection",
                    "For Connection",
                    "For Disconnection",
                    "Burial Assistance",
                    "Change of Name",
                    "Discount application",
                    "Job order request",
                    "Job order request view",
                    "Job order request can change acted upon",
                    "Job order request create direct",
                    "Job order request create indirect",
                    "Job order request receive",
                    "Job order request assigned/endorse",
                    //"Job order request forward",
                    "Job order request view details",
                    "Job order complaint",
                    "Job order complaint view",
                    "Job order complaint can change acted upon",
                    "Job order complaint create direct",
                    "Job order complaint create indirect",
                    "Job order complaint receive",
                    "Job order complaint assigned/endorse",
                    "Job order complaint view details",
                    //"Job order complaint forward",
                    "Job order for assigned to employee",
                    "Job order for process",
                    "Approval",
                    "Payment",
                };

                foreach (var item in usergroups)
                {
                    context.UserGroups.AddOrUpdate(a => a.Name,
                    new UserGroup
                    {
                        Name = item,
                        Description = item,
                        IsActive = true,
                        CreatedOn = DateTime.Now,
                        CreatedByAppUserId = 1
                    });

                    context.SaveChanges();
                }
            }
        }
        public static void Seed_User_PermissionGroup(DbTrackerContext context)
        {
            var permissions = new[]
           {
                new {
                    UserGroup = "Dashboard",
                    Permissions = context.Permissions
                        .Where(p =>p.PermissionGroup.Name == PermissionGroupData.Dashboard).ToList() },
                new { UserGroup = "Administrator", Permissions = context.Permissions.ToList() },
                new {
                    UserGroup = "Account",
                    Permissions = context.Permissions
                        .Where(p => p.Code == PermissionData.UserCanViewConsumerAccountsData ||
                                    p.Code == PermissionData.UserCanAddConsumerAccountData ||
                                    p.Code == PermissionData.UserCanEditConsumerAccountData ||
                                    p.Code == PermissionData.UserCanEndorseReassignToForHWInspection ||
                                    p.Code == PermissionData.UserCanRecommendAccountForApproval ||
                                    p.Code == PermissionData.UserCanAddSubstationAndFeederToAccountData ||
                                    p.Code == PermissionData.UserCanExportAccountsViewList ||
                                    p.Code == PermissionData.UserCanRecommendForPaymentMemberAccountApplication ||
                                    p.Code == PermissionData.UserCanViewConsumerAccountDetails).ToList() },
                new {
                    UserGroup = "View account details",
                    Permissions = context.Permissions
                        .Where(p => p.Code == PermissionData.UserCanViewConsumerAccountDetails).ToList() },
                new {
                    UserGroup = "Applicant",
                    Permissions = context.Permissions
                        .Where(p => p.Code == PermissionData.UserCanViewApplicantsData ||
                                    p.Code == PermissionData.UserCanExportApplicantViewList ||
                                    p.Code == PermissionData.UserCanAddApplicantData ||
                                    p.Code == PermissionData.UserCanEditApplicantData ||
                                    p.Code == PermissionData.UserCanViewApplicantConsumerDetails).ToList() },
                new {
                    UserGroup = "Member",
                    Permissions = context.Permissions
                        .Where(p =>p.PermissionGroup.Name == PermissionGroupData.Member).ToList() },
                    new {
                    UserGroup = "View consumer details",
                    Permissions = context.Permissions
                        .Where(p => p.Code == PermissionData.UserCanViewApplicantConsumerDetails).ToList() },
                new {
                    UserGroup = "PMOS",
                    Permissions = context.Permissions
                        .Where(p =>p.PermissionGroup.Name == PermissionGroupData.PreMembershipOrientationSeminar).ToList() },
                new {
                    UserGroup = "HW Inspector",
                    Permissions = context.Permissions
                        .Where(p =>p.PermissionGroup.Name == PermissionGroupData.HouseWiringInspection).ToList() },
                new {
                    UserGroup = "Issue for connection",
                    Permissions = context.Permissions
                        .Where(p => p.PermissionGroup.Name == PermissionGroupData.IssueForConnection).ToList() },
                new {
                    UserGroup = "For Connection",
                    Permissions = context.Permissions
                        .Where(p => p.PermissionGroup.Name == PermissionGroupData.ForConnection).ToList() },
                new {
                    UserGroup = "For Disconnection",
                    Permissions = context.Permissions
                        .Where(p => p.PermissionGroup.Name == PermissionGroupData.ForDisconnection).ToList() },
                new {
                    UserGroup = "Burial Assistance",
                    Permissions = context.Permissions
                        .Where(p => p.PermissionGroup.Name == PermissionGroupData.BurialAssistance).ToList() },
                new {
                    UserGroup = "Change of Name",
                    Permissions = context.Permissions
                        .Where(p => p.PermissionGroup.Name == PermissionGroupData.ChangeOfName).ToList() },
                new {
                    UserGroup = "Discount application",
                    Permissions = context.Permissions
                        .Where(p => p.PermissionGroup.Name == PermissionGroupData.DiscountApplication).ToList() },
                new {
                    UserGroup = "Approval",
                    Permissions = context.Permissions
                        .Where(p => p.PermissionGroup.Name == PermissionGroupData.Approval).ToList() },
                new {
                    UserGroup = "Payment",
                    Permissions = context.Permissions
                        .Where(p => p.PermissionGroup.Name == PermissionGroupData.Payment ||
                                    p.PermissionGroup.Name == PermissionGroupData.PaymentEntry).ToList() },
                new {
                    UserGroup = "Job order request",
                    Permissions = context.Permissions
                        .Where(p => p.Code == PermissionData.UserCanCreateJobOrderRequestDirect ||
                                    p.Code == PermissionData.UserCanCreateJobOrderRequestIndirect ||
                                    p.Code == PermissionData.UserCanEditJobOrderRequest ||
                                    p.Code == PermissionData.UserCanDeleteJobOrderRequest).ToList() },
                 new {
                    UserGroup = "Job order request view",
                    Permissions = context.Permissions
                        .Where(p => p.Code == PermissionData.UserCanViewJobOrderRequest ||
                                    p.Code == PermissionData.UserCanExportJobOrderRequest).ToList() },
                 new {
                    UserGroup = "Job order request can change acted upon",
                    Permissions = context.Permissions
                        .Where(p =>  p.Code == PermissionData.UserCanEditJobOrderRequestApplicationActedUponField).ToList() },
                new {
                    UserGroup = "Job order request create direct",
                    Permissions = context.Permissions
                        .Where(p =>  p.Code == PermissionData.UserCanCreateJobOrderRequestDirect).ToList() },
                new {
                    UserGroup = "Job order request create indirect",
                    Permissions = context.Permissions
                        .Where(p => p.Code == PermissionData.UserCanCreateJobOrderRequestIndirect).ToList() },
                new {
                    UserGroup = "Job order request receive",
                    Permissions = context.Permissions
                        .Where(p => p.Code == PermissionData.UserCanReceiveJobOrderRequest).ToList() },
                new {
                    UserGroup = "Job order request assigned/endorse",
                    Permissions = context.Permissions
                        .Where(p => p.Code == PermissionData.UserCanAssignJobOrderRequestToDivision).ToList() },
                //new {
                //    UserGroup = "Job order request forward",
                //    Permissions = context.Permissions
                //        .Where(p => p.Code == PermissionData.UserCanForwardJobOrderRequest).ToList() },
                new {
                    UserGroup = "Job order complaint",
                    Permissions = context.Permissions
                        .Where(p => p.Code == PermissionData.UserCanCreateJobOrderComplaintDirect ||
                                    p.Code == PermissionData.UserCanCreateJobOrderComplaintIndirect ||
                                    p.Code == PermissionData.UserCanEditJobOrderComplaintData ||
                                    p.Code == PermissionData.UserCanDeleteJobOrderComplaintData).ToList() },
                new {
                    UserGroup = "Job order complaint view",
                    Permissions = context.Permissions
                        .Where(p => p.Code == PermissionData.UserCanViewJobOrderComplaintData ||
                                    p.Code == PermissionData.UserCanExportJobOrderComplaintData).ToList() },
                 new {
                    UserGroup = "Job order complaint can change acted upon",
                    Permissions = context.Permissions
                        .Where(p =>  p.Code == PermissionData.UserCanEditJobOrderComplaintActedUponField).ToList() },
                new {
                    UserGroup = "Job order complaint create direct",
                    Permissions = context.Permissions
                        .Where(p =>  p.Code == PermissionData.UserCanCreateJobOrderComplaintDirect).ToList() },
                new {
                    UserGroup = "Job order complaint create indirect",
                    Permissions = context.Permissions
                        .Where(p => p.Code == PermissionData.UserCanCreateJobOrderComplaintIndirect).ToList() },
                new {
                    UserGroup = "Job order complaint receive",
                    Permissions = context.Permissions
                        .Where(p => p.Code == PermissionData.UserCanReceiveJobOrderComplaint).ToList() },
                new {
                    UserGroup = "Job order complaint assigned/endorse",
                    Permissions = context.Permissions
                        .Where(p => p.Code == PermissionData.UserCanAssignJobOrderComplaintDataToDivision).ToList() },
                //new {
                //    UserGroup = "Job order complaint forward",
                //    Permissions = context.Permissions
                //        .Where(p => p.Code == PermissionData.UserCanForwardJobOrderComplaint).ToList() },
                 new {
                    UserGroup = "Job order for assigned to employee",
                    Permissions = context.Permissions
                        .Where(p => p.PermissionGroup.Name == PermissionGroupData.AssignedJobOrderToEmployee).ToList() },
                 new {
                    UserGroup = "Job order for process",
                    Permissions = context.Permissions
                        .Where(p => p.PermissionGroup.Name == PermissionGroupData.ProcessJobOrder).ToList() },
                 new {
                    UserGroup = "Job order request view details",
                    Permissions = context.Permissions
                        .Where(p => p.Code == PermissionData.UserCanViewJobOrderComplaintDetails).ToList() },
                 new {
                    UserGroup = "Job order complaint view details",
                    Permissions = context.Permissions
                        .Where(p => p.Code == PermissionData.UserCanViewJobOrderRequestDetails).ToList() },
            };

            foreach (var item in permissions)
            {
                // Permissiom
                var adminUserGroup = context.UserGroups.FirstOrDefault(a => a.Name == item.UserGroup);

                if (adminUserGroup != null)
                {
                    if (adminUserGroup.Permissions != null)
                        adminUserGroup.Permissions.Clear();

                    adminUserGroup.Permissions = item.Permissions;

                    context.SaveChanges();
                }
            }

            // User Group
            var users = new[]
            {
                new {
                    Usernames = new List<string>{ "admin" },
                    UserGroups =  context.UserGroups.Where(p => p.Name == "Administrator").ToList() },
                new {
                    Usernames = context.AppUsers.Where(p => p.Employee.Position.IsManager).Select(p => p.Username).ToList(),
                    UserGroups =  context.UserGroups
                        .Where(p =>  p.Name == "Dashboard" || 
                                     p.Name == "Approval" ||
                                     p.Name == "View account details" ||
                                     p.Name == "View consumer details").ToList() },
                 new {
                    Usernames = context.AppUsers.Where(p => p.Employee.Office.IsMainOffice && p.Employee.Department.Code == "ISD" && p.Employee.Position.Name.Trim().ToUpper() == "MDT OFFICER").Select(p => p.Username).ToList(),
                    UserGroups =  context.UserGroups
                        .Where(p => p.Name == "Dashboard" ||
                                    p.Name == "Account" ||
                                    p.Name == "Applicant" ||
                                    p.Name == "Member" ||
                                    p.Name == "PMOS" ||
                                    p.Name == "Burial Assistance" ||
                                    p.Name == "Change of Name"||
                                    p.Name == "Discount application" ||
                                    p.Name == "Job order request" ||
                                    p.Name == "Job order request view" ||
                                    p.Name == "Job order request create indirect" ||
                                    p.Name == "Job order request can change acted upon" ||
                                    p.Name == "Job order complaint" ||
                                    p.Name == "Job order complaint view" ||
                                    p.Name == "Job order complaint create indirect" ||
                                    p.Name == "Job order complaint can change acted upon" ||
                                    p.Name == "Job order request view details" ||
                                    p.Name == "Job order complaint view details" ||
                                    p.Name == "Approval").ToList() },
                  new {
                    Usernames =  context.AppUsers.Where(p => p.Employee.Position.Name.Trim().ToUpper() == "HW INSPECTOR").Select(p => p.Username).ToList(),
                    UserGroups =  context.UserGroups
                        .Where(p => p.Name == "Dashboard" ||
                                    p.Name == "View account details" ||
                                    p.Name == "View consumer details" ||
                                    p.Name == "HW Inspector").ToList() },
                  new {
                    Usernames = context.AppUsers.Where(p => p.Employee.Position.Name.Trim().ToUpper() == "COM SECTION HEAD").Select(p => p.Username).ToList(),
                    UserGroups =  context.UserGroups
                        .Where(p => p.Name == "Dashboard"||
                                    p.Name == "Issue for connection" ||
                                    p.Name == "Job order for assigned to employee" ||
                                    p.Name == "Job order request view details" ||
                                    p.Name == "Job order complaint view details" ||
                                    p.Name == "View account details" ||
                                    p.Name == "View consumer details").ToList() },
                  new {
                    Usernames = context.AppUsers.Where(p => p.Employee.Position.Name.Trim().ToUpper() == "TELLER").Select(p => p.Username).ToList(),
                    UserGroups =  context.UserGroups
                        .Where(p => p.Name == "Dashboard"||
                                    p.Name == "Payment"||
                                    p.Name == "View account details" ||
                                    p.Name == "View consumer details").ToList() },
                  new {
                    Usernames = context.AppUsers.Where(p => p.Employee.Office.IsMainOffice && p.Employee.Department.Code == "TSD" && p.Employee.Position.Name.Trim().ToUpper() == "RECORDS OFFICER").Select(p => p.Username).ToList(),
                    UserGroups =  context.UserGroups
                        .Where(p => p.Name == "Dashboard"||
                                    p.Name == "Job order request"||
                                    p.Name == "Job order request view"||
                                    p.Name == "Job order request create direct"||
                                    p.Name == "Job order request receive"||
                                    p.Name == "Job order request assigned/endorse"||
                                    //p.Name == "Job order request forward"||
                                    p.Name == "Job order complaint"||
                                    p.Name == "Job order complaint view"||
                                    p.Name == "Job order complaint create direct"||
                                    p.Name == "Job order complaint receive"||
                                    p.Name == "Job order complaint assigned/endorse"||
                                    //p.Name == "Job order complaint forward"||
                                    p.Name == "Job order request view details" ||
                                    p.Name == "Job order complaint view details" ||
                                    p.Name == "View account details" ||
                                    p.Name == "View consumer details").ToList() },
                  new {
                    Usernames = context.AppUsers.Where(p => p.Employee.Position.Name.Trim().ToUpper() == "LINEMAN").Select(p => p.Username).ToList(),
                    UserGroups =  context.UserGroups
                        .Where(p => p.Name == "Dashboard" ||
                                    p.Name == "For Connection" ||
                                    p.Name == "For Disconnection" ||
                                    p.Name == "Job order for process" ||
                                    p.Name == "Job order request view details" ||
                                    p.Name == "Job order complaint view details" ||
                                    p.Name == "View account details" ||
                                    p.Name == "View consumer details").ToList() },
                new {
                    Usernames = context.AppUsers
                        .Where(p => !p.Employee.Office.IsMainOffice && 
                                    (p.Employee.Position.Name.Trim().ToUpper() == "RECORDS OFFICER" || p.Employee.Position.Name.Trim().ToUpper() == "MDT OFFICER")).Select(p => p.Username).ToList(),
                    UserGroups =  context.UserGroups
                        .Where(p => p.Name == "Dashboard" ||
                                    p.Name == "Account" ||
                                    p.Name == "Applicant" ||
                                    p.Name == "Member" ||
                                    p.Name == "PMOS" ||
                                    p.Name == "Burial Assistance" ||
                                    p.Name == "Change of Name"||
                                    p.Name == "Discount application" ||
                                    p.Name == "Job order request" ||
                                    p.Name == "Job order request view" ||
                                    p.Name == "Job order request create indirect" ||
                                    p.Name == "Job order request can change acted upon" ||
                                    p.Name == "Job order request create direct"||
                                    p.Name == "Job order request receive"||
                                    p.Name == "Job order request assigned/endorse"||
                                    //p.Name == "Job order request forward"||
                                    p.Name == "Job order complaint" ||
                                    p.Name == "Job order complaint view" ||
                                    p.Name == "Job order complaint create indirect" ||
                                    p.Name == "Job order complaint can change acted upon" ||
                                    p.Name == "Job order complaint create direct"||
                                    p.Name == "Job order complaint receive"||
                                    p.Name == "Job order complaint assigned/endorse"||
                                    p.Name == "Job order request view details" ||
                                    p.Name == "Job order complaint view details"
                                    /*p.Name == "Job order complaint forward"*/).ToList() },
            };

            foreach (var data in users)
            {
                foreach (var username in data.Usernames)
                {
                    var user = context.AppUsers.FirstOrDefault(a => a.Username == username);
                    if (user != null)
                    {
                        if (user.UserGroups != null)
                            user.UserGroups.Clear();

                        user.UserGroups = data.UserGroups;

                        context.SaveChanges();
                    }
                }
                    
            }
        }
    }
}