using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DataModel.CSA.SupportingDocument;
using Codebiz.Domain.Common.Model.DataModel.Transaction;
using Codebiz.Domain.Common.Model.EnumBaseModels;
using Codebiz.Domain.Common.Model.Enums;
using Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Codebiz.Domain.ERP.Context.SeedData
{
    public static class ManagementMigrateData
    {
        public static void Seed_Office(DbTrackerContext context)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string Office_Department_Division_Csv = "Codebiz.Domain.ERP.Context.CSVSeed.Office.csv";

            using (Stream stream = assembly.GetManifestResourceStream(Office_Department_Division_Csv))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    csvReader.Configuration.HeaderValidated = null;
                    csvReader.Configuration.MissingFieldFound = null;
                    csvReader.Read();
                    csvReader.ReadHeader();

                    while (csvReader.Read())
                    {
                        string officeCode = csvReader.GetField("OfficeCode");
                        string officeName = csvReader.GetField("OfficeName");

                        #region Office

                        var office = context.Offices.FirstOrDefault(p => p.Code == officeCode);
                        if (office == null)
                        {
                            var cityTownCode = officeCode == "MO" ? "GER"
                                : officeCode == "KAL" ? "PUR" : officeCode;
                            var provinceName = officeCode == "CUY" ? "NUEVA ECIJA" : "TARLAC";

                            var province = context.Provinces.FirstOrDefault(x => x.Name.ToUpper() == provinceName);
                            if (province != null)
                            {
                                var city = context.CityTowns.FirstOrDefault(x => x.Abbreviation.ToUpper() == cityTownCode && x.ProvinceId == province.ProvinceId);

                                if (city != null)
                                {
                                    var barangay = context.Barangays.FirstOrDefault(x => x.CityTownId == city.CityTownId);
                                    if (barangay != null)
                                    {
                                        context.Offices
                                            .AddOrUpdate(a => new { a.Code },
                                                new Office
                                                {
                                                    Code = officeCode,
                                                    Name = officeName,
                                                    IsMainOffice = officeCode == "MO",
                                                    BlkLotNo = "N/A",
                                                    Street = "N/A",
                                                    ProvinceId = province.ProvinceId,
                                                    CityTownId = city.CityTownId,
                                                    BarangayId = barangay.BarangayId,
                                                    IsActive = true,
                                                    CreatedByAppUserId = 1,
                                                    CreatedOn = DateTime.Now,
                                                });

                                        context.SaveChanges();
                                    }
                                }
                            }
                        }

                        #endregion
                    }
                }
            }
        }
    
    
        public static void Seed_SupportingDocument(DbTrackerContext context)
        {

            if (!context.SupportingDocuments.Any())
            {
                var transactions = new[]
                {
                    new {
                        Id = (int)TransactionTypeEnum.MemberAccountApplication,
                        Subs = new[]
                        {
                            new {
                                Id = (int)TransactionSubTypes.Small,
                                Documents = new[] {
                                    new { IsRequired = true, Name = "Mayor's Permit for electrical installation" },
                                    new { IsRequired = true, Name = "Fire Inspection Certificate" },
                                    new { IsRequired = true, Name = "Barangay Certificate" },
                                    new { IsRequired = true, Name = "Valid Government Issued ID" }
                                }
                            },
                            new {
                                Id = (int)TransactionSubTypes.Big,
                                Documents = new[] {
                                    new { IsRequired = true, Name = "Electrical Plan signed by Prof Electrical Engineer (PEE)" },
                                    new { IsRequired = true, Name = "Electrical Permit" },
                                    new { IsRequired = true, Name = "Bulding Permit" },
                                    new { IsRequired = true, Name = "Barangay Certificate" }
                                }
                            },
                        }
                    },
                    new {
                        Id = (int)TransactionTypeEnum.DiscountApplication,
                        Subs = new[]
                        {
                            new {
                                Id = (int)TransactionSubTypes.ResidentialSeniorCitizen,
                                Documents = new[] {
                                    new { IsRequired = true, Name = "Statement of Account (atleast 1 year)" },
                                    new { IsRequired = true, Name = "Senior Citizen's ID" },
                                    new { IsRequired = true, Name = "Barangay Certificate" },
                                    new { IsRequired = false, Name = "Clearance from Finance Dept. or Area Office" },
                                    new { IsRequired = true, Name = "Authorization" },
                                    new { IsRequired = true, Name = "Medical Certificate" }
                                }
                            },
                            new {
                                Id = (int)TransactionSubTypes.DswdAccreditedSeniorCitizen,
                                Documents = new[] {
                                    new { IsRequired = true, Name = "Statement of Account (atleast 1 year)" },
                                    new { IsRequired = true, Name = "ID of the Guardian" },
                                    new { IsRequired = false, Name = "Clearance from Finance Dept." },
                                    new { IsRequired = true, Name = "Authorization" },
                                    new { IsRequired = true, Name = "Medical Certificate" }
                                }
                            },
                        }
                    },
                    new {
                        Id = (int)TransactionTypeEnum.ChangeOfName,
                        Subs = new[]
                        {
                              new {
                                Id = (int)TransactionSubTypes.Sale,
                                Documents = new[] {
                                    new { IsRequired = true, Name = "Deed of Sale" },
                                    new { IsRequired = true, Name = "Clearance" },
                                    new { IsRequired = true, Name = "Valid ID of New Member" },
                                    new { IsRequired = true, Name = "Valid ID of Old Member" }
                                }
                            },
                               new {
                                Id = (int)TransactionSubTypes.Death,
                                Documents = new[] {
                                    new { IsRequired = true, Name = "Death Certificate" },
                                    new { IsRequired = true, Name = "Proof of Relationship (Birth Certificate, Marriage Contract, Brgy. Certificate, Notarized Affidavit)" },
                                    new { IsRequired = false, Name = "Authorization of from Living Sibling" },
                                    new { IsRequired = true, Name = "Photocopy of Valid ID's of authorizing party" }
                                }
                            },
                               new {
                                Id = (int)TransactionSubTypes.Waived,
                                Documents = new[] {
                                    new { IsRequired = true, Name = "Letter of Request/Waiver of Old Member" },
                                    new { IsRequired = true, Name = "Valid ID of Old Member" },
                                    new { IsRequired = true, Name = "Clearance" },
                                }
                            },
                                new {
                                Id = (int)TransactionSubTypes.ChangeStatus,
                                Documents = new[] {
                                    new { IsRequired = true, Name = "Marriage Contract" },
                                    new { IsRequired = true, Name = "Letter of Request" },
                                    new { IsRequired = false, Name = "Valid ID" },
                                    new { IsRequired = true, Name = "Clearance" },
                                }
                            }
                        }
                    },
                    new {
                        Id = (int)TransactionTypeEnum.BurialAssistance,
                        Subs = new[]
                        {
                            new {
                                Id = 0,
                                Documents = new[] {
                                    new { IsRequired = true, Name = "LCR Authenticated Death Certificate" },
                                    new { IsRequired = true, Name = "Photocopy of 2 Valid ID of Claimant" },
                                    new { IsRequired = true, Name = "Barangay Certificate" },
                                    new { IsRequired = true, Name = "Valid ID of Deceased Member-Consumer" },
                                    new { IsRequired = false, Name = "Original Copy of Marriage Certificate Contract" },
                                    new { IsRequired = false, Name = "Birth Certificate" }
                                }
                            }
                        }
                    },
                     new {
                        Id = (int)TransactionTypeEnum.CoopVehicle,
                        Subs = new[]
                        {
                            new {
                                Id = 0,
                                Documents = new[] {
                                    new { IsRequired = true, Name = "Official Reciept (OR)" },
                                    new { IsRequired = true, Name = "Certificate of Registration (CR)" },
                                    new { IsRequired = true, Name = "Memorandum of the General Manager" }
                                }
                            }
                        }
                    }
                };

                foreach (var item in transactions)
                {
                    foreach (var subItem in item.Subs)
                    {
                        var subId = subItem.Id > 0 ? subItem.Id : (int?)null;
                        var supportingDoc = context.SupportingDocuments.FirstOrDefault(p => p.TransactionTypeId == item.Id && p.TransactionSubTypeId == subId);
                        if (supportingDoc == null)
                        {
                            supportingDoc = new SupportingDocument
                            {
                                TransactionTypeId = item.Id,
                                TransactionSubTypeId = subId,
                                IsActive = true,
                                IsDeleted = false,
                                CreatedByAppUserId = 1,
                                CreatedOn = DateTime.Now
                            };

                            context.SupportingDocuments.Add(supportingDoc);
                            context.SaveChanges();
                        }
                        else
                        {
                            supportingDoc = context.SupportingDocuments.FirstOrDefault(p => p.TransactionTypeId == item.Id && p.TransactionSubTypeId == subId);
                        }

                        foreach (var docItem in subItem.Documents)
                        {
                            var doc = supportingDoc.Details.FirstOrDefault(p => p.DocumentName == docItem.Name);
                            if (doc == null)
                            {
                                doc = new SupportingDocumentDetail
                                {
                                    DocumentName = docItem.Name,
                                    IsRequired = docItem.IsRequired
                                };

                                supportingDoc.Details.Add(doc);
                            }

                            context.SaveChanges();
                        }
                    }
                }
            }
        }

        #region Transactions
        public static void Seed_Approval_Transactions(DbTrackerContext context)
        {
            context.TransactionTypeCategories.AddOrUpdate(a => a.Name, new TransactionTypeCategory
            {
                Name = "Approval (Approval)",
                Code = "APP",
            });
            context.SaveChanges();
            var category = context.TransactionTypeCategories.FirstOrDefault(p => p.Code == "APP");
            if (category != null)
            {
                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeId = (int)TransactionTypeEnum.Approval,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.Approval),
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                });
                context.SaveChanges();
            }
        

        }
        public static void Seed_CSA_Transactions(DbTrackerContext context)
        {
            context.TransactionTypeCategories.AddOrUpdate(a => a.Name, new TransactionTypeCategory
            {
                Name = "Commercial Sevice Application (CSA)",
                Code = "CSA",
            });

            context.SaveChanges();

            var category = context.TransactionTypeCategories.FirstOrDefault(p => p.Code == "CSA");
            if (category != null)
            {
                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeId = (int)TransactionTypeEnum.MemberAccountApplication,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.MemberAccountApplication),
                    HasApproval = true,
                    IsUsedInSupportingDocuments = true,
                    IsUsedInDocumentNumbering = false,
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                });

                context.SaveChanges();

                var transactionTypeMemberAccountApplication = context.TransactionTypes.FirstOrDefault(p => p.TransactionTypeId == (int)TransactionTypeEnum.MemberAccountApplication);
                if (transactionTypeMemberAccountApplication != null)
                {
                    context.TransactionSubTypes.AddOrUpdate(a => a.TransactionSubTypeId, new TransactionSubType
                    {
                        TransactionSubTypeId = (int)TransactionSubTypes.Small,
                        Description = Helpers.GetEnumDescription(TransactionSubTypes.Small),
                        IsUsedInSupportingDocuments = true,
                        TransactionTypeId = transactionTypeMemberAccountApplication.TransactionTypeId
                    });

                    context.TransactionSubTypes.AddOrUpdate(a => a.TransactionSubTypeId, new TransactionSubType
                    {
                        TransactionSubTypeId = (int)TransactionSubTypes.Big,
                        Description = Helpers.GetEnumDescription(TransactionSubTypes.Big),
                        IsUsedInSupportingDocuments = true,
                        TransactionTypeId = transactionTypeMemberAccountApplication.TransactionTypeId
                    });
                }

                context.SaveChanges();

                #region  Application

                #region  Discount Application

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeId = (int)TransactionTypeEnum.DiscountApplication,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.DiscountApplication),
                    HasApproval = true,
                    IsApplication = true,
                    IsUsedInSupportingDocuments = true,
                    IsUsedInDocumentNumbering = false,
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                });

                context.SaveChanges();

                var transactionTypeDiscountApplication = context.TransactionTypes.FirstOrDefault(p => p.TransactionTypeId == (int)TransactionTypeEnum.DiscountApplication);
                if (transactionTypeDiscountApplication != null)
                {
                    context.TransactionSubTypes.AddOrUpdate(a => a.TransactionSubTypeId, new TransactionSubType
                    {
                        TransactionTypeId = transactionTypeDiscountApplication.TransactionTypeId,
                        TransactionSubTypeId = (int)TransactionSubTypes.ResidentialSeniorCitizen,
                        Description = Helpers.GetEnumDescription(TransactionSubTypes.ResidentialSeniorCitizen),
                        IsUsedInSupportingDocuments = true
                    });

                    context.TransactionSubTypes.AddOrUpdate(a => a.TransactionSubTypeId, new TransactionSubType
                    {
                        TransactionTypeId = transactionTypeDiscountApplication.TransactionTypeId,
                        TransactionSubTypeId = (int)TransactionSubTypes.DswdAccreditedSeniorCitizen,
                        Description = Helpers.GetEnumDescription(TransactionSubTypes.DswdAccreditedSeniorCitizen),
                        IsUsedInSupportingDocuments = true
                    });
                }

                #endregion

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeId = (int)TransactionTypeEnum.NetMetering,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.NetMetering),
                    HasApproval = true,
                    HasSeparateRequestInformation = true,
                    IsApplication = true,
                    ForJobOrder = true,
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.ContestableApplication,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.ContestableApplication),
                    HasApproval = true,
                    IsApplication = true
                });
                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.TransformerRental,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.TransformerRental),
                    HasApproval = true,
                    IsApplication = true,
                    IsUsedInSupportingDocuments = true,
                    HasSeparateRequestInformation=true,
                    ForJobOrder=true
                });
                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.ChangeOfMeter,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.ChangeOfMeter),
                    HasApproval = true,
                    IsUsedInSupportingDocuments = true,
                    HasSeparateRequestInformation = true,
                    ForJobOrder = true,
                    HasPurpose=true,
                    IsRequest=true
                });
                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.SpecialLightingApplication,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.SpecialLightingApplication),
                    HasApproval = true,
                    IsUsedInSupportingDocuments = true,
                    ForJobOrder = true,
                    HasPurpose = true,
                    IsApplication = true
                });
                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.Applicant,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.Applicant),
                    HasApproval = true,
                    IsApplication = true
                });

                context.SaveChanges();

                #endregion

                #region  Request

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.ChangeOfName,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.ChangeOfName),
                    HasApproval = true,
                    IsRequest = true,
                    ForJobOrder = false,
                    IsUsedInSupportingDocuments = true,
                    IsUsedInDocumentNumbering = false
                });

                context.SaveChanges();

                var transactionTypeChangeOfName = context.TransactionTypes.FirstOrDefault(p => p.TransactionTypeId == (int)TransactionTypeEnum.ChangeOfName);
                if (transactionTypeChangeOfName != null)
                {
                    context.TransactionSubTypes.AddOrUpdate(a => a.TransactionSubTypeId, new TransactionSubType
                    {
                        TransactionTypeId = transactionTypeChangeOfName.TransactionTypeId,
                        TransactionSubTypeId = (int)TransactionSubTypes.Sale,
                        Description = Helpers.GetEnumDescription(TransactionSubTypes.Sale),
                        IsUsedInSupportingDocuments = true
                    });

                    context.TransactionSubTypes.AddOrUpdate(a => a.TransactionSubTypeId, new TransactionSubType
                    {
                        TransactionTypeId = transactionTypeChangeOfName.TransactionTypeId,
                        TransactionSubTypeId = (int)TransactionSubTypes.Waived,
                        Description = Helpers.GetEnumDescription(TransactionSubTypes.Waived),
                        IsUsedInSupportingDocuments = true
                    });

                    context.TransactionSubTypes.AddOrUpdate(a => a.TransactionSubTypeId, new TransactionSubType
                    {
                        TransactionTypeId = transactionTypeChangeOfName.TransactionTypeId,
                        TransactionSubTypeId = (int)TransactionSubTypes.Death,
                        Description = Helpers.GetEnumDescription(TransactionSubTypes.Death),
                        IsUsedInSupportingDocuments = true
                    });

                    context.TransactionSubTypes.AddOrUpdate(a => a.TransactionSubTypeId, new TransactionSubType
                    {
                        TransactionTypeId = transactionTypeChangeOfName.TransactionTypeId,
                        TransactionSubTypeId = (int)TransactionSubTypes.ChangeStatus,
                        Description = Helpers.GetEnumDescription(TransactionSubTypes.ChangeStatus),
                        IsUsedInSupportingDocuments = true
                    });
                }

                context.SaveChanges();

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.BurialAssistance,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.BurialAssistance),
                    HasApproval = true,
                    IsRequest = true,
                    IsUsedInSupportingDocuments = true,
                    IsUsedInDocumentNumbering = false
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.ContestableApplication,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.ContestableApplication),
                    HasApproval = true,
                    IsApplication = true,
                    IsUsedInSupportingDocuments = true
                });

                //context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                //{
                //    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                //    TransactionTypeId = (int)TransactionTypeEnum.PaymentCancellation,
                //    Description = Helpers.GetEnumDescription(TransactionTypeEnum.PaymentCancellation),
                //    HasApproval = true,
                //    IsRequest = true,
                //    IsUsedInSupportingDocuments = false,
                //    IsUsedInDocumentNumbering = false
                //});

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.BillingAdjustment,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.BillingAdjustment),
                    HasApproval = true,
                    IsRequest = false,
                    IsOtherRequest = false,
                    IsUsedInSupportingDocuments = false,
                    IsUsedInDocumentNumbering = false
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.ContestableApplication,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.ContestableApplication),
                    HasApproval = true,
                    IsRequest = true,
                    IsOtherRequest = true,
                    IsUsedInSupportingDocuments = false,
                    IsUsedInDocumentNumbering = false
                });

                context.SaveChanges();

                #endregion

                #region JO

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.JobOrderComplaint,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.JobOrderComplaint),
                    IsUsedInSupportingDocuments = true,
                    HasApproval = true
                }); 

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.JobOrderComplaintDirect,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.JobOrderComplaintDirect),
                    HasApproval = true
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.JobOrderComplaintIndirect,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.JobOrderComplaintIndirect),
                    HasApproval = true
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.JobOrderRequest,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.JobOrderRequest),
                    IsUsedInSupportingDocuments = true,
                    HasApproval = true
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.JobOrderReceive,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.JobOrderReceive),
                    HasApproval = true
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.JobOrderRequestDirect,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.JobOrderRequestDirect),
                    HasApproval = true
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.JobOrderRequestIndirect,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.JobOrderRequestIndirect),
                    HasApproval = true
                });

                #endregion

                context.SaveChanges();
            }
        }
        public static void Seed_Inventory_Transactions(DbTrackerContext context)
        {
            context.TransactionTypeCategories.AddOrUpdate(a => a.Name, new TransactionTypeCategory
            {
                Name = "Inventory",
                Code = "INV",
            });

            context.SaveChanges();

            var category = context.TransactionTypeCategories.FirstOrDefault(p => p.Code == "INV");
            if (category != null)
            {
                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.InventoryTransfer,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.InventoryTransfer),
                    HasApproval = true,
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.ItemMasterData,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.ItemMasterData),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.InventoryReceiving,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.InventoryReceiving),
                    HasApproval = true,
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.IGE,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.IGE),
                    HasApproval = true,
                });


                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.IGN,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.IGN),
                    HasApproval = true,
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.IQI,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.IQI),
                    HasApproval = true,
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.IQC,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.IQC),
                    HasApproval = true,
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.IQP,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.IQP),
                    HasApproval = true,
                });


                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.IMV,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.IMV),
                    HasApproval = true,
                });


                context.SaveChanges();
            }
        }
        public static void Seed_BusinessPartner_Transactions(DbTrackerContext context)
        {
            context.TransactionTypeCategories.AddOrUpdate(a => a.Name, new TransactionTypeCategory
            {
                Name = "Business Partner",
                Code = "BP",
            });

            context.SaveChanges();

            var category = context.TransactionTypeCategories.FirstOrDefault(p => p.Code == "BP");
            if (category != null)
            {
                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeId = (int)TransactionTypeEnum.BusinessPartnerCustomer,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.BusinessPartnerCustomer),
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true,
                    IsUsedInSupportingDocuments = true
                });
                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeId = (int)TransactionTypeEnum.BusinessPartnerVendor,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.BusinessPartnerVendor),
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true,
                    IsUsedInSupportingDocuments = true
                });

                context.SaveChanges();
            }
        }
        public static void Seed_Vehicle_Transactions(DbTrackerContext context)
        {
            context.TransactionTypeCategories.AddOrUpdate(a => a.Name, new TransactionTypeCategory
            {
                Name = "Vehicles",
                Code = "VEH",
            });

            context.SaveChanges();

            var category = context.TransactionTypeCategories.FirstOrDefault(p => p.Code == "VEH");
            if (category != null)
            {
                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.VehicleRequest,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.VehicleRequest),
                    HasApproval = true,
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.VehicleInspection,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.VehicleInspection),
                    HasApproval = true,
                });

                context.TransactionTypes.AddOrUpdate(new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.CoopVehicle,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.CoopVehicle),
                    HasApproval = false,
                });

                context.SaveChanges();
            }
        }
        public static void Seed_Production_Transactions(DbTrackerContext context)
        {
            context.TransactionTypeCategories.AddOrUpdate(a => a.Name, new TransactionTypeCategory
            {
                Name = "Production",
                Code = "PRO",
            });

            context.SaveChanges();

            var category = context.TransactionTypeCategories.FirstOrDefault(p => p.Code == "PRO");
            if (category != null)
            {
                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.BOM,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.BOM),
                    HasApproval = true,
                });

                context.SaveChanges();
            }
        }
        public static void Seed_Purchase_Transactions(DbTrackerContext context)
        {
            context.TransactionTypeCategories.AddOrUpdate(a => a.Name, new TransactionTypeCategory
            {
                Name = "Purchase",
                Code = "PUR",
            });

            context.SaveChanges();

            var category = context.TransactionTypeCategories.FirstOrDefault(p => p.Code == "PUR");
            if (category != null)
            {
                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.PQU,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.PQU),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.POR,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.POR),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.PDN,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.PDN),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.PRD,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.PRD),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.PDP,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.PDP),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.PIN,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.PIN),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.PCR,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.PCR),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.MaterialRequest,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.MaterialRequest),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true
                });

                context.SaveChanges();
            }
        }
        public static void Seed_Sale_Transactions(DbTrackerContext context)
        {
            context.TransactionTypeCategories.AddOrUpdate(a => a.Name, new TransactionTypeCategory
            {
                Name = "Sales",
                Code = "SAL",
            });

            context.SaveChanges();

            var category = context.TransactionTypeCategories.FirstOrDefault(p => p.Code == "SAL");
            if (category != null)
            {
                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.SQU,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.SQU),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.SOR,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.SOR),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.SDN,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.SDN),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.SRD,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.SRD),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.SDP,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.SDP),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.SIN,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.SIN),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.SCR,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.SCR),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true
                });

                context.SaveChanges();
            }
        }
        public static void Seed_Material_Transaction(DbTrackerContext context)
        {

            context.TransactionTypeCategories.AddOrUpdate(a => a.Name, new TransactionTypeCategory
            {
                Name = "Material Request",
                Code = "MR",
            });

            context.SaveChanges();

            var category = context.TransactionTypeCategories.FirstOrDefault(p => p.Code == "MR");
            if (category != null)
            {
                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeId = (int)TransactionTypeEnum.MaterialRequest,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.MaterialRequest),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true,
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeId = (int)TransactionTypeEnum.SRV,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.SRV),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true,
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeId = (int)TransactionTypeEnum.PRV,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.PRV),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true,
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                });

                context.SaveChanges();
            }
        }
        public static void Seed_JEN_Transactions(DbTrackerContext context)
        {
            context.TransactionTypeCategories.AddOrUpdate(a => a.Name, new TransactionTypeCategory
            {
                Name = "Journal Transactions",
                Code = "JEN",
            });

            context.SaveChanges();

            var category = context.TransactionTypeCategories.FirstOrDefault(p => p.Code == "JEN");
            if (category != null)
            {
                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.JVO,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.JVO),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.JEN,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.JEN),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true
                });

                context.SaveChanges();
            }
        }
        public static void Seed_PY_Transactions(DbTrackerContext context)
        {
            context.TransactionTypeCategories.AddOrUpdate(a => a.Name, new TransactionTypeCategory
            {
                Name = "Payment Transactions",
                Code = "PYT",
            });

            context.SaveChanges();

            var category = context.TransactionTypeCategories.FirstOrDefault(p => p.Code == "PYT");
            if (category != null)
            {
                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.SPY,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.SPY),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.PPY,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.PPY),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true
                });

                context.TransactionTypes.AddOrUpdate(a => a.TransactionTypeId, new TransactionType
                {
                    TransactionTypeCategoryId = category.TransactionTypeCategoryId,
                    TransactionTypeId = (int)TransactionTypeEnum.DPS,
                    Description = Helpers.GetEnumDescription(TransactionTypeEnum.DPS),
                    HasApproval = false,
                    IsUsedInDocumentNumbering = true
                });


                context.SaveChanges();
            }
        }

        #endregion

        #region Address

        public static void Seed_Region(DbTrackerContext context)
        {
            if (!context.Regions.Any())
            {
                context.Regions.AddOrUpdate(a => a.Name,
                new Region
                {
                    Name = "REGION III (Central Luzon)",
                    Abbreviation = "REGION III",
                    CreatedByAppUserId = 1,
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                });

                context.SaveChanges();
            }
        }
        public static void Seed_Province(DbTrackerContext context, string province)
        {
            var region = context.Regions.FirstOrDefault(x => x.Name.Trim().ToUpper() == "REGION III (Central Luzon)".Trim().ToUpper());
            if (region != null)
            {
                context.Provinces.AddOrUpdate(a => a.Name,
                new Province
                {
                    Name = province,
                    Abbreviation = province,
                    RegionId = region.RegionId,
                    CreatedByAppUserId = 1,
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                });

                context.SaveChanges();
            }
        }
        public static void Seed_City(DbTrackerContext context, string cityTownField, string code, string provinceField)
        {
            var province = context.Provinces.FirstOrDefault(x => x.Name.Trim().ToUpper() == provinceField.Trim().ToUpper());
            if (province != null)
            {
                context.CityTowns.AddOrUpdate(a => a.CityTownCode,
                new CityTown
                {
                    Name = cityTownField,
                    Abbreviation = cityTownField.Substring(0, 3).Trim().ToUpper(),
                    CityTownCode = code,
                    ProvinceId = province.ProvinceId,
                    CreatedByAppUserId = 1,
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                });

                context.SaveChanges();
            }
        }
        public static void Seed_Barangay(DbTrackerContext context, string barangayField, string cityTownField, string code)
        {
            var cityTown = context.CityTowns.FirstOrDefault(x => x.Name.Trim().ToUpper() == cityTownField.Trim().ToUpper());
            if (cityTown != null)
            {
                context.Barangays.AddOrUpdate(a => new { a.CityTownId, a.BarangayCode },
                new Barangay
                {
                    Name = barangayField,
                    BarangayCode = code,
                    CityTownId = cityTown.CityTownId,
                    CreatedByAppUserId = 1,
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                });

                context.SaveChanges();
            }
        }
        public static void Seed_Routes(DbTrackerContext context)
        {
            if (!context.Routes.Any())
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string routesPath = "Codebiz.Domain.ERP.Context.CSVSeed.routes.csv";

                using (Stream stream = assembly.GetManifestResourceStream(routesPath))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        CsvReader csvReader = new CsvReader(reader);
                        csvReader.Configuration.HeaderValidated = null;
                        csvReader.Configuration.MissingFieldFound = null;
                        var records = new List<Region>();
                        csvReader.Read();
                        csvReader.ReadHeader();
                        while (csvReader.Read())
                        {
                            string route = csvReader.GetField("Route");
                            string province = csvReader.GetField("Province");

                            string cityCode = csvReader.GetField("CityCode").PadLeft(2, '0');
                            string cityTown = csvReader.GetField("Municipality");

                            string barangayCode = csvReader.GetField("BarangayCode").PadLeft(2, '0');
                            string barangay = csvReader.GetField("Barangay");

                            string bookNo = csvReader.GetField("BookNo");
                            string description = csvReader.GetField("Description") != "" ? csvReader.GetField("Description") : "";
                            var codeValue = route.Length <= 3 ? route.PadLeft(4, '0') : route;

                            if (!context.Provinces.Any(a => a.Name.Trim().ToUpper() == province.Trim().ToUpper()))
                            {
                                ManagementMigrateData.Seed_Province(context, province);
                            }

                            if (!context.CityTowns.Any(a => a.Name.Trim().ToUpper() == cityTown.Trim().ToUpper()))
                            {
                                ManagementMigrateData.Seed_City(context, cityTown, cityCode, province);
                            }

                            if (!context.Barangays.Any(a => a.Name.Trim().ToUpper() == barangay.Trim().ToUpper()))
                            {
                                ManagementMigrateData.Seed_Barangay(context, barangay, cityTown, barangayCode);
                            }

                            context.Routes.AddOrUpdate(a => new { a.Description, a.Code, a.BookNo },
                                  new Route
                                  {
                                      Description = description,
                                      Code = codeValue,
                                      ProvinceId = context.Provinces.Where(a => a.Name.Trim().ToUpper() == province.Trim().ToUpper()).FirstOrDefault().ProvinceId,
                                      CityTownId = context.CityTowns.Where(a => a.Name.Trim().ToUpper() == cityTown.Trim().ToUpper()).FirstOrDefault().CityTownId,
                                      BarangayId = context.Barangays.Where(a => a.Name.Trim().ToUpper() == barangay.Trim().ToUpper()).FirstOrDefault().BarangayId,
                                      BookNo = bookNo,
                                      CreatedByAppUserId = 1,
                                      CreatedOn = DateTime.Now,
                                      IsActive = true
                                  });
                            context.SaveChanges();
                        }
                    }
                }
            }
        }

        #endregion

        #region Approval Procedures

        public static void Seed_Approval_Setup(DbTrackerContext context)
        {
            //if (!context.ApprovalStages.Any())
            //{
            //    var approvals = new[]
            //   {
            //        new {
            //            Name = "Member Account Application",
            //            ApproverUsernames = context.AppUsers.Where(p => p.Position.Name.Trim() == "ISD Manager").Select(p => p.Username).ToList(),
            //            OriginatorPositions = new List<string>{ "MDT Officer" },
            //            Transactions = new List<int>{
            //                (int)TransactionTypeEnum.MemberAccountApplication
            //            }
            //        },
            //        new {
            //            Name = "ISD Manager",
            //            ApproverUsernames = context.AppUsers.Where(p => p.Position.Name.Trim() == "ISD Manager").Select(p => p.Username).ToList(),
            //            OriginatorPositions = new List<string>{ "MDT Officer" },
            //            Transactions = new List<int>{
            //                (int)TransactionTypeEnum.DiscountApplication,
            //                (int)TransactionTypeEnum.ChangeOfName,
            //                (int)TransactionTypeEnum.BurialAssistance,
            //                (int)TransactionTypeEnum.JobOrderComplaint,
            //                (int)TransactionTypeEnum.JobOrderRequest,
            //            }
            //        },
            //        new {
            //            Name = "TSD Manager",
            //            ApproverUsernames = context.AppUsers.Where(p => p.Position.Name.Trim() == "TSD Manager").Select(p => p.Username).ToList(),
            //            OriginatorPositions = new List<string>{ "Record Officer", "MDT Officer" },
            //            Transactions = new List<int>{
            //                (int)TransactionTypeEnum.JobOrderReceive,
            //                (int)TransactionTypeEnum.JobOrderRequestIndirect,
            //                (int)TransactionTypeEnum.JobOrderComplaintIndirect,
            //            }
            //        },
            //        //new {
            //        //    Name = "Job Order Request Direct - PAN AREA",
            //        //    ApproverUsernames = context.AppUsers.Where(p => p.Position.Name.Trim() == "Area Manager").Select(p => p.Username).ToList(),
            //        //    OriginatorPositions = new List<string>{ "Record Officer", "MDT Officer" },
            //        //    Transactions = new List<int>{
            //        //        (int)TransactionTypeEnum.JobOrderRequestDirect,
            //        //         (int)TransactionTypeEnum.JobOrderComplaintDirect,
            //        //    }
            //        //},
            //        //new {
            //        //    Name = "Job Order Request Indirect - PAN AREA",
            //        //    ApproverUsernames = context.AppUsers.Where(p => p.Position.Name.Trim() == "Area Manager").Select(p => p.Username).ToList(),
            //        //    OriginatorPositions = new List<string>{ "Record Officer", "MDT Officer" },
            //        //    Transactions = new List<int>{
            //        //        (int)TransactionTypeEnum.JobOrderRequestIndirect,
            //        //        (int)TransactionTypeEnum.JobOrderComplaintIndirect,
            //        //    }
            //        //},
            //        new {
            //            Name = "Job Order Request Direct - MAIN OFFICE",
            //            ApproverUsernames = context.AppUsers.Where(p => p.Position.Name.Trim() == "ISD Manager").Select(p => p.Username).ToList(),
            //            OriginatorPositions = new List<string>{ "Record Officer", "MDT Officer" },
            //            Transactions = new List<int>{
            //                (int)TransactionTypeEnum.JobOrderRequestDirect,
            //                (int)TransactionTypeEnum.JobOrderComplaintDirect,
            //            }
            //        }
            //    };

            //    foreach (var item in approvals)
            //    {
            //        #region Stage

            //        //Approval stage
            //        context.ApprovalStages.Add(new ApprovalStage
            //        {
            //            Name = item.Name,
            //            Description = item.Name,
            //            RequiredApprovals = 1,
            //            RequiredRejections = 1,
            //            LabelId = (int)ApprovalStageLabels.ApprovedBy,
            //            CreatedByAppUserId = 1,
            //            CreatedOn = DateTime.Now,
            //            IsActive = true
            //        });
            //        context.SaveChanges();

            //        //Approval stage approvers
            //        var approvalStage = context.ApprovalStages.FirstOrDefault(p => p.Name == item.Name);
            //        foreach (var approver in item.ApproverUsernames)
            //        {
            //            var user = context.AppUsers.FirstOrDefault(p => p.Username == approver);
            //            context.ApprovalStageApprovers.Add(new ApprovalStageApprover
            //            {
            //                ApprovalStageId = approvalStage.ApprovalStageId,
            //                AppUserId = user.AppUserId
            //            });
            //            context.SaveChanges();
            //        }

            //        #endregion

            //        #region Template

            //        //Approval template
            //        context.ApprovalTemplates.Add(new ApprovalTemplate
            //        {
            //            Name = item.Name,
            //            CreatedByAppUserId = 1,
            //            CreatedOn = DateTime.Now,
            //            IsActive = true
            //        });
            //        context.SaveChanges();

            //        var approvalTemplate = context.ApprovalTemplates.FirstOrDefault(p => p.Name == item.Name);

            //        //Approval template originators
            //        foreach (var originator in item.OriginatorPositions)
            //        {
            //            var users = context.AppUsers
            //                .Where(p => p.PositionId != null && p.DepartmentId != null)
            //                .Where(p => p.Position.Name == originator &&
            //                           ((p.DepartmentId != null && item.Name.Contains(p.Department.Code)) ||
            //                            (p.DepartmentId == null))).ToList();

            //            foreach (var user in users)
            //            {
            //                context.ApprovalTemplateOriginators.Add(new ApprovalTemplateOriginator
            //                {
            //                    ApprovalTemplateId = approvalTemplate.ApprovalTemplateId,
            //                    AppUserId = user.AppUserId
            //                });
            //                context.SaveChanges();
            //            }
            //        }

            //        //Approval template Transactions
            //        foreach (var transaction in item.Transactions)
            //        {
            //            context.ApprovalTemplateTransactionsData.Add(new ApprovalTemplateTransaction
            //            {
            //                ApprovalTemplateId = approvalTemplate.ApprovalTemplateId,
            //                TransactionTypeId = transaction
            //            });
            //            context.SaveChanges();
            //        }

            //        //Approval template stage
            //        context.ApprovalTemplateStageData.Add(new ApprovalTemplateStages
            //        {
            //            ApprovalTemplateId = approvalTemplate.ApprovalTemplateId,
            //            ApprovalStageId = approvalStage.ApprovalStageId,
            //            Priority = 1
            //        });

            //        context.SaveChanges();

            //        #endregion
            //    }
            //}
        }

        public static void SeedApproverLabel(DbTrackerContext context)
        {
            if (!context.ApproverLabels.Any())
            {
                context.ApproverLabels.AddOrUpdate(x => x.Name,
              new ApproverLabel() { Name = Helpers.GetEnumDescription(ApprovalStageLabels.ApprovedBy), IsActive = true, CreatedByAppUserId = 1, CreatedOn = DateTime.Now },
              new ApproverLabel() { Name = Helpers.GetEnumDescription(ApprovalStageLabels.CheckedBy), IsActive = true, CreatedByAppUserId = 1, CreatedOn = DateTime.Now },
              new ApproverLabel() { Name = Helpers.GetEnumDescription(ApprovalStageLabels.NotedBy), IsActive = true, CreatedByAppUserId = 1, CreatedOn = DateTime.Now },
              new ApproverLabel() { Name = Helpers.GetEnumDescription(ApprovalStageLabels.ReviewedBy), IsActive = true, CreatedByAppUserId = 1, CreatedOn = DateTime.Now });

                context.SaveChanges();
            }
        }

        #endregion
    }
}