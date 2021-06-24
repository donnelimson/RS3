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
                new { UserGroup = "Administrator", Permissions = context.Permissions.ToList()},
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