namespace Domain.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Account", "CreatedByAppUserId", "dbo.AppUser");
            DropForeignKey("dbo.Account", "ModifiedByAppUserId", "dbo.AppUser");
            DropForeignKey("dbo.Account", "Member_MemberId", "dbo.Member");
            DropForeignKey("dbo.Member", "CreatedByAppUserId", "dbo.AppUser");
            DropForeignKey("dbo.Member", "MemberTypeId", "dbo.MemberType");
            DropForeignKey("dbo.Member", "ModifiedByAppUserId", "dbo.AppUser");
            DropForeignKey("dbo.Account", "PrimaryId", "dbo.Member");
            DropForeignKey("dbo.Account", "PrimarySecondary1Trustor2Id", "dbo.Member");
            DropForeignKey("dbo.Account", "PrimarySecondary2Id", "dbo.Member");
            DropForeignKey("dbo.Account", "PrimaryTrustor1Id", "dbo.Member");
            DropForeignKey("dbo.MonthlyAveragePerTransactionType", "MemberId", "dbo.Member");
            DropForeignKey("dbo.MonthlyAveragePerTransactionType", "TransactionTypeId", "dbo.TransactionType");
            DropForeignKey("dbo.TransactionAnalysisMember", "MemberId", "dbo.Member");
            DropForeignKey("dbo.TransactionAnalysisMember", "TransactionAnalysisId", "dbo.TransactionAnalysis");
            DropForeignKey("dbo.Transaction", "AccountId", "dbo.Account");
            DropForeignKey("dbo.Transaction", "CreatedByAppUserId", "dbo.AppUser");
            DropForeignKey("dbo.Transaction", "MemberId", "dbo.Member");
            DropForeignKey("dbo.Transaction", "ModifiedByAppUserId", "dbo.AppUser");
            DropForeignKey("dbo.Transaction", "TransactionTypeId", "dbo.TransactionType");
            DropForeignKey("dbo.TransactionAnalysisMemberDetail", "TransactionId", "dbo.Transaction");
            DropForeignKey("dbo.TransactionAnalysisMemberDetail", "TransactionAnalysisMemberId", "dbo.TransactionAnalysisMember");
            DropIndex("dbo.Account", new[] { "PrimaryId" });
            DropIndex("dbo.Account", new[] { "PrimarySecondary1Trustor2Id" });
            DropIndex("dbo.Account", new[] { "PrimarySecondary2Id" });
            DropIndex("dbo.Account", new[] { "PrimaryTrustor1Id" });
            DropIndex("dbo.Account", new[] { "CreatedByAppUserId" });
            DropIndex("dbo.Account", new[] { "ModifiedByAppUserId" });
            DropIndex("dbo.Account", new[] { "Member_MemberId" });
            DropIndex("dbo.Member", new[] { "MemberTypeId" });
            DropIndex("dbo.Member", new[] { "CreatedByAppUserId" });
            DropIndex("dbo.Member", new[] { "ModifiedByAppUserId" });
            DropIndex("dbo.MonthlyAveragePerTransactionType", new[] { "MemberId" });
            DropIndex("dbo.MonthlyAveragePerTransactionType", new[] { "TransactionTypeId" });
            DropIndex("dbo.TransactionAnalysisMember", new[] { "TransactionAnalysisId" });
            DropIndex("dbo.TransactionAnalysisMember", new[] { "MemberId" });
            DropIndex("dbo.TransactionAnalysisMemberDetail", new[] { "TransactionAnalysisMemberId" });
            DropIndex("dbo.TransactionAnalysisMemberDetail", new[] { "TransactionId" });
            DropIndex("dbo.Transaction", new[] { "TransactionTypeId" });
            DropIndex("dbo.Transaction", new[] { "MemberId" });
            DropIndex("dbo.Transaction", new[] { "AccountId" });
            DropIndex("dbo.Transaction", new[] { "CreatedByAppUserId" });
            DropIndex("dbo.Transaction", new[] { "ModifiedByAppUserId" });
            DropTable("dbo.Account");
            DropTable("dbo.Member");
            DropTable("dbo.MemberType");
            DropTable("dbo.MonthlyAveragePerTransactionType");
            DropTable("dbo.TransactionAnalysisMember");
            DropTable("dbo.TransactionAnalysisMemberDetail");
            DropTable("dbo.Transaction");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        TransactionDate = c.DateTime(nullable: false),
                        TransactionTime = c.DateTime(nullable: false),
                        TraceNo = c.Single(),
                        TransactionTypeStr = c.String(maxLength: 255),
                        TransactionTypeId = c.Int(nullable: false),
                        MemberId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                        LoanAccountNo = c.String(maxLength: 255),
                        TransactionAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentType = c.String(maxLength: 255),
                        ECFlag = c.String(maxLength: 255),
                        ImportDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByAppUserId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedByAppUserId = c.Int(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.TransactionId);
            
            CreateTable(
                "dbo.TransactionAnalysisMemberDetail",
                c => new
                    {
                        TransactionAnalysisMemberDetailId = c.Int(nullable: false, identity: true),
                        TransactionAnalysisMemberId = c.Int(nullable: false),
                        TransactionId = c.Int(nullable: false),
                        ReportingReason = c.Int(nullable: false),
                        Remarks = c.String(),
                        Exclude = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionAnalysisMemberDetailId);
            
            CreateTable(
                "dbo.TransactionAnalysisMember",
                c => new
                    {
                        TransactionAnalysisMemberId = c.Int(nullable: false, identity: true),
                        TransactionAnalysisId = c.Int(nullable: false),
                        MemberId = c.Int(nullable: false),
                        ReportingReason = c.Int(nullable: false),
                        Remarks = c.String(),
                        Exclude = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionAnalysisMemberId);
            
            CreateTable(
                "dbo.MonthlyAveragePerTransactionType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        TransactionTypeId = c.Int(nullable: false),
                        AverageAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MemberType",
                c => new
                    {
                        MemberTypeId = c.Int(nullable: false),
                        CodeName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.MemberTypeId);
            
            CreateTable(
                "dbo.Member",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        MemberNo = c.String(maxLength: 255),
                        Tin = c.String(maxLength: 255),
                        LastName = c.String(nullable: false, maxLength: 255),
                        FirstName = c.String(nullable: false, maxLength: 255),
                        MiddleName = c.String(nullable: false, maxLength: 255),
                        QualifierName = c.String(maxLength: 255),
                        BirthDate = c.DateTime(),
                        BirthPlace = c.String(maxLength: 255),
                        Address = c.String(maxLength: 255),
                        CityTownId = c.String(maxLength: 255),
                        ServiceStat = c.String(maxLength: 255),
                        MemberStat = c.String(maxLength: 255),
                        MemberTypeId = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByAppUserId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedByAppUserId = c.Int(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.MemberId);
            
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        PrimaryId = c.Int(),
                        AccountNo = c.String(maxLength: 255),
                        AccountType = c.String(maxLength: 255),
                        AccountStatus = c.String(maxLength: 255),
                        PrimarySecondary1Trustor2Id = c.Int(),
                        PrimarySecondary2Id = c.Int(),
                        IsJointAccount = c.Boolean(nullable: false),
                        JointType = c.String(maxLength: 255),
                        ITFAccount = c.String(maxLength: 255),
                        PrimaryTrustor1Id = c.Int(),
                        F11 = c.String(maxLength: 255),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByAppUserId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedByAppUserId = c.Int(),
                        ModifiedOn = c.DateTime(),
                        Member_MemberId = c.Int(),
                    })
                .PrimaryKey(t => t.AccountId);
            
            CreateIndex("dbo.Transaction", "ModifiedByAppUserId");
            CreateIndex("dbo.Transaction", "CreatedByAppUserId");
            CreateIndex("dbo.Transaction", "AccountId");
            CreateIndex("dbo.Transaction", "MemberId");
            CreateIndex("dbo.Transaction", "TransactionTypeId");
            CreateIndex("dbo.TransactionAnalysisMemberDetail", "TransactionId");
            CreateIndex("dbo.TransactionAnalysisMemberDetail", "TransactionAnalysisMemberId");
            CreateIndex("dbo.TransactionAnalysisMember", "MemberId");
            CreateIndex("dbo.TransactionAnalysisMember", "TransactionAnalysisId");
            CreateIndex("dbo.MonthlyAveragePerTransactionType", "TransactionTypeId");
            CreateIndex("dbo.MonthlyAveragePerTransactionType", "MemberId");
            CreateIndex("dbo.Member", "ModifiedByAppUserId");
            CreateIndex("dbo.Member", "CreatedByAppUserId");
            CreateIndex("dbo.Member", "MemberTypeId");
            CreateIndex("dbo.Account", "Member_MemberId");
            CreateIndex("dbo.Account", "ModifiedByAppUserId");
            CreateIndex("dbo.Account", "CreatedByAppUserId");
            CreateIndex("dbo.Account", "PrimaryTrustor1Id");
            CreateIndex("dbo.Account", "PrimarySecondary2Id");
            CreateIndex("dbo.Account", "PrimarySecondary1Trustor2Id");
            CreateIndex("dbo.Account", "PrimaryId");
            AddForeignKey("dbo.TransactionAnalysisMemberDetail", "TransactionAnalysisMemberId", "dbo.TransactionAnalysisMember", "TransactionAnalysisMemberId");
            AddForeignKey("dbo.TransactionAnalysisMemberDetail", "TransactionId", "dbo.Transaction", "TransactionId");
            AddForeignKey("dbo.Transaction", "TransactionTypeId", "dbo.TransactionType", "TransactionTypeId");
            AddForeignKey("dbo.Transaction", "ModifiedByAppUserId", "dbo.AppUser", "AppUserId");
            AddForeignKey("dbo.Transaction", "MemberId", "dbo.Member", "MemberId");
            AddForeignKey("dbo.Transaction", "CreatedByAppUserId", "dbo.AppUser", "AppUserId");
            AddForeignKey("dbo.Transaction", "AccountId", "dbo.Account", "AccountId");
            AddForeignKey("dbo.TransactionAnalysisMember", "TransactionAnalysisId", "dbo.TransactionAnalysis", "TransactionAnalysisId");
            AddForeignKey("dbo.TransactionAnalysisMember", "MemberId", "dbo.Member", "MemberId");
            AddForeignKey("dbo.MonthlyAveragePerTransactionType", "TransactionTypeId", "dbo.TransactionType", "TransactionTypeId");
            AddForeignKey("dbo.MonthlyAveragePerTransactionType", "MemberId", "dbo.Member", "MemberId");
            AddForeignKey("dbo.Account", "PrimaryTrustor1Id", "dbo.Member", "MemberId");
            AddForeignKey("dbo.Account", "PrimarySecondary2Id", "dbo.Member", "MemberId");
            AddForeignKey("dbo.Account", "PrimarySecondary1Trustor2Id", "dbo.Member", "MemberId");
            AddForeignKey("dbo.Account", "PrimaryId", "dbo.Member", "MemberId");
            AddForeignKey("dbo.Member", "ModifiedByAppUserId", "dbo.AppUser", "AppUserId");
            AddForeignKey("dbo.Member", "MemberTypeId", "dbo.MemberType", "MemberTypeId");
            AddForeignKey("dbo.Member", "CreatedByAppUserId", "dbo.AppUser", "AppUserId");
            AddForeignKey("dbo.Account", "Member_MemberId", "dbo.Member", "MemberId");
            AddForeignKey("dbo.Account", "ModifiedByAppUserId", "dbo.AppUser", "AppUserId");
            AddForeignKey("dbo.Account", "CreatedByAppUserId", "dbo.AppUser", "AppUserId");
        }
    }
}
