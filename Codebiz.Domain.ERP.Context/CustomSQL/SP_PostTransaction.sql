if exists (select 1 from sys.sysobjects where [name] = 'SP_PostTransaction' and xtype = 'P')
	drop procedure SP_PostTransaction  
go 

Create Procedure SP_PostTransaction  
 @docID bigint = 0
,@docType nvarchar(5) = ''
,@createdBy  int = 1 
,@error_code int = 0 output 
,@error_message nvarchar(1024) = '' output
as   
begin   
  
SET NOCOUNT ON   
  
 declare @jdtID bigint = 0  
 declare @jdt table (  
   DocNum nvarchar(30) null  
  ,Series nvarchar(30) null  
  ,ObjType nvarchar(5)  
  ,BaseEntry bigint  
  ,BaseNum bigint  
  ,BaseType nvarchar(30) null  
  ,TargetEntry bigint  
  ,TargetNum bigint  
  ,FinancePeriod int  
  ,BatchNum bigint  
  ,TargetType nvarchar(30) null  
  ,TransType nvarchar(30) null  
  ,RefDate datetime null  
  ,RefDate2 datetime null  
  ,RefDate3 datetime null  
  ,Ref1 nvarchar(30) null  
  ,Ref2 nvarchar(30) null  
  ,Ref3 nvarchar(30) null  
  ,Project nvarchar(30) null  
  ,Remarks nvarchar(255) null  
  ,SysTotal decimal(19,6) null  
  ,SysTotalLC decimal(19,6) null  
  ,SysTotalFC decimal(19,6) null  
  ,VersionNo nvarchar(30) null  
  ,IsActive bit null  
  ,CreatedByAppUserId int null  
  ,CreatedOn datetime null  
  ,ModifiedByAppUserId int null   
  ,ModifiedOn datetime null  
  )  
 declare @jdtline table (  
   JournalEntryID bigint null  
  ,ObjType nvarchar(5) null  
  ,BaseEntry bigint null  
  ,BaseNum bigint null  
  ,BaseType nvarchar(30) null  
  ,TargetEntry bigint null  
  ,TargetNum bigint null  
  ,TargetType nvarchar(30) null  
  ,TransType nvarchar(30) null  
  ,GLAcctCode nvarchar(30) null  
  ,LineID int null  
  ,VisID int null  
  ,FinancePeriod int null  
  ,BatchNum bigint null  
  ,ShortName nvarchar(30) null  
  ,Debit decimal(19,6) null  
  ,Credit decimal(19,6) null  
  ,DebitSC decimal(19,6) null  
  ,CreditSC decimal(19,6) null  
  ,DebitFC decimal(19,6) null  
  ,CreditFC decimal(19,6) null  
  ,ContraAccount nvarchar(30) null  
  ,DebitCredit nvarchar(1) null  
  ,BaseAmt decimal(19,6) null  
  ,VatGroup nvarchar(30) null  
  ,VatPercent decimal(19,6) null  
  ,VatAmt decimal(19,6) null  
  ,RefDate datetime null  
  ,RefDate2 datetime null  
  ,RefDate3 datetime null  
  ,CreatedBy bigint null  
  ,BaseRef nvarchar(30) null  
  ,Ref1 nvarchar(30) null   
  ,Ref2 nvarchar(30) null   
  ,Ref3 nvarchar(30) null
  ,Project nvarchar(30) null  
  ,OcrCode nvarchar(30) null  
  ,OcrCode2 nvarchar(30) null   
  ,OcrCode3 nvarchar(30) null   
  ,OcrCode4 nvarchar(30) null   
  ,OcrCode5 nvarchar(30) null   
  ,LineRemarks nvarchar(255) null  
  ,VersionNo nvarchar(30) null  
  ,IsActive bit null  
  ,CreatedByAppUserId int null  
  ,CreatedOn datetime null  
  ,ModifiedByAppUserId int null   
  ,ModifiedOn datetime null 
  ,Ref4 nvarchar(30) null   
  ,Ref5 nvarchar(30) null   
  )  
  

  /*Sales Delivery*/
 if @docType = 'SDN'  
 begin   
  insert into @jdt  
   select   
    null DocNum  
   ,null Series  
   ,'JEN' ObjType  
   ,null BaseEntry  
   ,null BaseNum  
   ,null BaseType  
   ,null TargetEntry  
   ,null TargetNum  
   ,FPR.PeriodDetailID FinancePeriod  
   ,null BatchNum  
   ,null TargetType  
   ,'SDN' TransType  
   ,SDN.DocDate  RefDate   
   ,SDN.DocDueDate RefDate2   
   ,SDN.TaxDate RefDate3   
   ,SDN.DocID Ref1  
   ,null Ref2  
   ,null Ref3  
   ,SDN.Project Project  
   ,'Sales DeliveryNote - ' + SDN.CardCode    Remarks  
   ,null SysTotal  
   ,null SysTotalLC  
   ,null SysTotalFC  
   ,null VersionNo   
   ,1 IsActive   
   ,@createdBy CreatedByAppUserId   
   ,Getdate() CreatedOn   
   ,null ModifiedByAppUserId   
   ,null ModifiedOn   
    from   
    SalesDelivery SDN  
    outer apply (  
     select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= SDN.DocDate and ToRefDate >= SDN.DocDate   
    ) FPR   
  
  where SDN.DocID = @docID   
   
  insert into @jdtline  (
		JournalEntryID, ObjType ,BaseEntry ,BaseNum ,BaseType ,TargetEntry ,TargetNum   ,TargetType ,TransType ,GLAcctCode ,LineID ,VisID ,FinancePeriod ,BatchNum ,ShortName ,Debit ,Credit ,DebitSC ,CreditSC ,DebitFC ,CreditFC 
		,ContraAccount ,DebitCredit ,BaseAmt ,VatGroup ,VatPercent ,VatAmt ,RefDate ,RefDate2 ,RefDate3 ,CreatedBy ,BaseRef ,Ref1 ,Ref2 ,Ref3 ,Project ,OcrCode ,OcrCode2 ,OcrCode3 ,OcrCode4 ,OcrCode5 ,LineRemarks ,VersionNo ,IsActive ,CreatedByAppUserId ,CreatedOn ,ModifiedByAppUserId ,ModifiedOn   
	  )
    select  @jdtID 
	JournalEntryID  
     ,ObjType   
     ,BaseEntry   
     ,BaseNum   
     ,BaseType   
     ,TargetEntry   
     ,TargetNum   
     ,TargetType   
     ,TransType   
     ,GLAcctCode   
     ,ROW_NUMBER() over( order by GLAcctCode) LineID   
     ,VisID   
     ,FinancePeriod   
     ,BatchNum   
     ,ShortName   
     ,Debit   
     ,Credit   
     ,DebitSC   
     ,CreditSC   
     ,DebitFC   
     ,CreditFC   
     ,ContraAccount   
     ,DebitCredit   
     ,BaseAmt   
     ,VatGroup   
     ,VatPercent   
     ,VatAmt   
     ,RefDate   
     ,RefDate2   
     ,RefDate3   
     ,CreatedBy   
     ,BaseRef   
     ,Ref1   
     ,Ref2   
     ,Ref3   
     ,Project   
     ,OcrCode   
     ,OcrCode2   
     ,OcrCode3   
     ,OcrCode4   
     ,OcrCode5  
     ,LineRemarks  
     ,null VersionNo   
     ,1 IsActive   
     ,@createdBy CreatedByAppUserId   
     ,Getdate() CreatedOn   
     ,null ModifiedByAppUserId   
     ,null ModifiedOn   
    from (  
     select   --Credit Invntry  
     'JEN' ObjType   
     ,null BaseEntry   
     ,null BaseNum   
     ,null BaseType   
     ,null TargetEntry   
     ,null TargetNum   
     ,null TargetType   
     ,'SDN' TransType   
     ,ITG.InvntryAcct GLAcctCode   
     ,null LineID   
     ,null VisID   
     ,FPR.PeriodDetailID FinancePeriod   
     ,null BatchNum   
     ,ITG.InvntryAcct ShortName   
     ,0 Debit   
     ,Sum(SDN1.StockValue) Credit   
     ,null DebitSC   
     ,null CreditSC   
     ,null DebitFC   
     ,null CreditFC   
     ,null ContraAccount   
     ,null DebitCredit   
     ,null BaseAmt   
     ,null VatGroup   
     ,null VatPercent   
     ,null VatAmt   
     ,SDN.DocDate  RefDate   
     ,SDN.DocDueDate RefDate2   
     ,SDN.TaxDate RefDate3   
     ,SDN.DocID CreatedBy   
     ,SDN.DocNum BaseRef   
     ,SDN.DocID  Ref1   
     ,null Ref2   
     ,null Ref3   
     ,SDN1.ProjectCode Project   
     ,SDN1.OcrCode OcrCode    
     ,SDN1.OcrCode2 OcrCode2   
     ,SDN1.OcrCode3 OcrCode3   
     ,SDN1.OcrCode4 OcrCode4   
     ,SDN1.OcrCode5 OcrCode5   
     ,'Sales DeliveryNote - ' + SDN.CardCode   LineRemarks   
        from   
      SalesDeliveryDetail SDN1  
      inner join SalesDelivery SDN on SDN1.DocID = SDN.DocID  
      inner join ItemMaster ITM on SDN1.ItemCode = ITM.ItemCode   
      inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
      outer apply (  
       select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= SDN.DocDate and ToRefDate >= SDN.DocDate   
      ) FPR   
      where SDN.DocID = @docID   
      group by   
        ITG.InvntryAcct  
       ,SDN.CardCode   
       ,FPR.PeriodDetailID   
       ,SDN.DocDate   
       ,SDN.DocDueDate   
       ,SDN.TaxDate   
       ,SDN.DocID   
       ,SDN.DocNum  
       ,SDN1.ProjectCode   
       ,SDN1.OcrCode  
       ,SDN1.OcrCode2  
       ,SDN1.OcrCode3   
       ,SDN1.OcrCode4   
       ,SDN1.OcrCode5   
        
     union ALL  
  
     select  --Debit COSGS  
     'JEN' ObjType   
     ,null BaseEntry   
     ,null BaseNum   
     ,null BaseType   
     ,null TargetEntry   
     ,null TargetNum   
     ,null TargetType   
     ,'SDN' TransType   
     ,ITG.COGSAcct GLAcctCode   
     ,null LineID   
     ,null VisID   
     ,FPR.PeriodDetailID FinancePeriod   
     ,null BatchNum   
     ,ITG.COGSAcct ShortName   
     ,Sum(SDN1.StockValue) Debit   
     ,0 Credit   
     ,null DebitSC   
     ,null CreditSC   
     ,null DebitFC   
     ,null CreditFC   
     ,null ContraAccount   
     ,null DebitCredit   
     ,null BaseAmt   
     ,null VatGroup   
     ,null VatPercent   
     ,null VatAmt   
     ,SDN.DocDate  RefDate   
     ,SDN.DocDueDate RefDate2   
     ,SDN.TaxDate RefDate3   
     ,SDN.DocID CreatedBy   
     ,SDN.DocNum BaseRef   
     ,SDN.DocID  Ref1   
     ,null Ref2   
     ,null Ref3   
     ,SDN1.ProjectCode Project   
     ,SDN1.OcrCode OcrCode    
     ,SDN1.OcrCode2 OcrCode2   
     ,SDN1.OcrCode3 OcrCode3   
     ,SDN1.OcrCode4 OcrCode4   
     ,SDN1.OcrCode5 OcrCode5   
     ,'Sales DeliveryNote - ' + SDN.CardCode   LineRemarks   
      from   
      SalesDeliveryDetail SDN1  
      inner join SalesDelivery SDN on SDN1.DocID = SDN.DocID  
      inner join BusinessPartner  CRD on SDN.CardCode  = CRD.CardCode  
      inner join ItemMaster ITM on SDN1.ItemCode = ITM.ItemCode   
      inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
      outer apply (  
       select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= SDN.DocDate and ToRefDate >= SDN.DocDate   
      ) FPR   
       where SDN.DocID = @docID   
       group by   
        ITG.InvntryAcct  
       ,ITG.COGSAcct   
       ,SDN.CardCode          ,CRD.CtlDebCredPayAcct   
       ,FPR.PeriodDetailID   
       ,SDN.DocDate   
       ,SDN.DocDueDate   
       ,SDN.TaxDate   
       ,SDN.DocID   
       ,SDN.DocNum   
       ,SDN1.ProjectCode   
       ,SDN1.OcrCode  
       ,SDN1.OcrCode2  
       ,SDN1.OcrCode3   
       ,SDN1.OcrCode4   
       ,SDN1.OcrCode5   
    ) A  
  
  update @jdt set SysTotal = (select sum(debit) from @jdtline)  
  insert into  JournalEntry ( 
  DocNum 
  ,Series 
  ,ObjType
  ,BaseEntry
  ,BaseNum
  ,BaseType
  ,TargetEntry
  ,TargetNum
  ,FinancePeriod
  ,BatchNum 
  ,TargetType
  ,TransType
  ,RefDate
  ,RefDate2
  ,RefDate3
  ,Ref1
  ,Ref2
  ,Ref3
  ,Project
  ,Remarks
  ,SysTotal
  ,SysTotalLC
  ,SysTotalFC
  ,VersionNo
  ,IsActive
  ,CreatedByAppUserId
  ,CreatedOn
  ,ModifiedByAppUserId
  ,ModifiedOn 
  ) select * from @jdt  
  set @jdtID = @@IDENTITY  
  if @jdtID > 0  
  begin   
    update @jdtline set JournalEntryID = @jdtID   
    insert into JournalEntryDetail select * from @jdtline  
    update PurchaseDelivery set TransID = @jdtID ,JournalRemarks = (select top 1 Remarks from @jdt) where DocID = @docID  
  end   
 end  
  
  /*Sales Return*/
 if @docType = 'SRD'  
 begin   
  insert into @jdt  
    select   
     null DocNum  
    ,null Series  
    ,'JEN' ObjType  
    ,null BaseEntry  
    ,null BaseNum  
    ,null BaseType  
    ,null TargetEntry  
    ,null TargetNum  
    ,FPR.PeriodDetailID FinancePeriod  
    ,null BatchNum  
    ,null TargetType  
    ,'SRD' TransType  
    ,SRD.DocDate  RefDate   
    ,SRD.DocDueDate RefDate2   
    ,SRD.TaxDate RefDate3   
    ,SRD.DocID Ref1  
    ,null Ref2  
    ,null Ref3  
    ,SRD.Project Project  
    ,'Sales Return - ' + SRD.CardCode    Remarks  
    ,null SysTotal  
    ,null SysTotalLC  
    ,null SysTotalFC  
    ,null VersionNo   
    ,1 IsActive   
    ,@createdBy CreatedByAppUserId   
    ,Getdate() CreatedOn   
    ,null ModifiedByAppUserId   
    ,null ModifiedOn   
     from   
     SalesReturn SRD  
     outer apply (  
      select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= SRD.DocDate and ToRefDate >= SRD.DocDate   
     ) FPR   
  
   where SRD.DocID = @docID   
   
  insert into @jdtline  (
	JournalEntryID, ObjType ,BaseEntry ,BaseNum ,BaseType ,TargetEntry ,TargetNum   ,TargetType ,TransType ,GLAcctCode ,LineID ,VisID ,FinancePeriod ,BatchNum ,ShortName ,Debit ,Credit ,DebitSC ,CreditSC ,DebitFC ,CreditFC 
	,ContraAccount ,DebitCredit ,BaseAmt ,VatGroup ,VatPercent ,VatAmt ,RefDate ,RefDate2 ,RefDate3 ,CreatedBy ,BaseRef ,Ref1 ,Ref2 ,Ref3 ,Project ,OcrCode ,OcrCode2 ,OcrCode3 ,OcrCode4 ,OcrCode5 ,LineRemarks ,VersionNo ,IsActive ,CreatedByAppUserId ,CreatedOn ,ModifiedByAppUserId ,ModifiedOn   
	)
    select  @jdtID JournalEntryID  
     ,ObjType   
     ,BaseEntry   
     ,BaseNum   
     ,BaseType   
     ,TargetEntry   
     ,TargetNum   
     ,TargetType   
     ,TransType   
     ,GLAcctCode   
     ,ROW_NUMBER() over( order by GLAcctCode) LineID   
     ,VisID   
     ,FinancePeriod   
     ,BatchNum   
     ,ShortName   
     ,Debit   
     ,Credit   
     ,DebitSC   
     ,CreditSC   
     ,DebitFC   
     ,CreditFC   
     ,ContraAccount   
     ,DebitCredit   
     ,BaseAmt   
     ,VatGroup   
     ,VatPercent   
     ,VatAmt   
     ,RefDate   
     ,RefDate2   
     ,RefDate3   
     ,CreatedBy   
     ,BaseRef   
     ,Ref1   
     ,Ref2   
     ,Ref3   
     ,Project   
     ,OcrCode   
     ,OcrCode2   
     ,OcrCode3   
     ,OcrCode4   
     ,OcrCode5  
     ,LineRemarks  
     ,null VersionNo   
     ,1 IsActive   
     ,@createdBy CreatedByAppUserId   
     ,Getdate() CreatedOn   
     ,null ModifiedByAppUserId   
     ,null ModifiedOn   
    from (  
     select   --Debit Invntry  
     'JEN' ObjType   
     ,null BaseEntry   
     ,null BaseNum   
     ,null BaseType   
     ,null TargetEntry   
     ,null TargetNum   
     ,null TargetType   
     ,'SRD' TransType   
     ,ITG.InvntryAcct GLAcctCode   
     ,null LineID   
     ,null VisID   
     ,FPR.PeriodDetailID FinancePeriod   
     ,null BatchNum   
     ,ITG.InvntryAcct ShortName   
     ,Sum(SRD1.LineTotal) Debit   
     ,0 Credit   
     ,null DebitSC   
     ,null CreditSC   
     ,null DebitFC   
     ,null CreditFC   
     ,null ContraAccount   
     ,null DebitCredit   
     ,null BaseAmt   
     ,null VatGroup   
     ,null VatPercent   
     ,null VatAmt   
     ,SRD.DocDate  RefDate   
     ,SRD.DocDueDate RefDate2   
     ,SRD.TaxDate RefDate3   
     ,SRD.DocID CreatedBy   
     ,SRD.DocNum BaseRef   
     ,SRD.DocID  Ref1   
     ,null Ref2   
     ,null Ref3   
     ,SRD1.ProjectCode Project   
     ,SRD1.OcrCode OcrCode    
     ,SRD1.OcrCode2 OcrCode2   
     ,SRD1.OcrCode3 OcrCode3   
     ,SRD1.OcrCode4 OcrCode4   
     ,SRD1.OcrCode5 OcrCode5   
     ,'Sales Return - ' + SRD.CardCode   LineRemarks   
        from   
      SalesReturnDetail SRD1  
      inner join SalesReturn SRD on SRD1.DocID = SRD.DocID  
      inner join ItemMaster ITM on SRD1.ItemCode = ITM.ItemCode   
      inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
      outer apply (  
       select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= SRD.DocDate and ToRefDate >= SRD.DocDate   
      ) FPR   
      where SRD.DocID = @docID   
      group by   
        ITG.InvntryAcct  
       ,SRD.CardCode   
       ,FPR.PeriodDetailID   
       ,SRD.DocDate   
       ,SRD.DocDueDate   
       ,SRD.TaxDate   
       ,SRD.DocID   
       ,SRD.DocNum  
       ,SRD1.ProjectCode   
       ,SRD1.OcrCode  
       ,SRD1.OcrCode2  
       ,SRD1.OcrCode3   
       ,SRD1.OcrCode4   
       ,SRD1.OcrCode5   
        
     union ALL  
  
     select  --Credi COSGS  
     'JEN' ObjType   
     ,null BaseEntry   
     ,null BaseNum   
     ,null BaseType   
     ,null TargetEntry   
     ,null TargetNum   
     ,null TargetType   
     ,'SRD' TransType   
     ,ITG.COGSAcct GLAcctCode   
     ,null LineID   
     ,null VisID   
     ,FPR.PeriodDetailID FinancePeriod   
     ,null BatchNum   
     ,ITG.COGSAcct ShortName   
     ,0 Debit   
     ,Sum(SRD1.LineTotal) Credit   
     ,null DebitSC   
     ,null CreditSC   
     ,null DebitFC   
     ,null CreditFC   
     ,null ContraAccount   
     ,null DebitCredit   
     ,null BaseAmt   
     ,null VatGroup   
     ,null VatPercent   
     ,null VatAmt   
     ,SRD.DocDate  RefDate   
     ,SRD.DocDueDate RefDate2   
     ,SRD.TaxDate RefDate3   
     ,SRD.DocID CreatedBy   
     ,SRD.DocNum BaseRef   
     ,SRD.DocID  Ref1   
     ,null Ref2   
     ,null Ref3   
     ,SRD1.ProjectCode Project   
     ,SRD1.OcrCode OcrCode    
     ,SRD1.OcrCode2 OcrCode2   
     ,SRD1.OcrCode3 OcrCode3   
     ,SRD1.OcrCode4 OcrCode4   
     ,SRD1.OcrCode5 OcrCode5   
     ,'Sales Return - ' + SRD.CardCode   LineRemarks   
      from   
      SalesReturnDetail SRD1  
      inner join SalesReturn SRD on SRD1.DocID = SRD.DocID  
      inner join BusinessPartner  CRD on SRD.CardCode  = CRD.CardCode  
      inner join ItemMaster ITM on SRD1.ItemCode = ITM.ItemCode   
      inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
      outer apply (  
       select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= SRD.DocDate and ToRefDate >= SRD.DocDate   
      ) FPR   
       where SRD.DocID = @docID   
       group by   
        ITG.InvntryAcct  
       ,ITG.COGSAcct   
       ,SRD.CardCode   
       ,CRD.CtlDebCredPayAcct   
       ,FPR.PeriodDetailID   
       ,SRD.DocDate   
       ,SRD.DocDueDate   
       ,SRD.TaxDate   
       ,SRD.DocID   
       ,SRD.DocNum   
       ,SRD1.ProjectCode   
       ,SRD1.OcrCode  
       ,SRD1.OcrCode2  
       ,SRD1.OcrCode3   
       ,SRD1.OcrCode4   
       ,SRD1.OcrCode5   
    ) A  
  
  update @jdt set SysTotal = (select sum(debit) from @jdtline)  
  insert into JournalEntry ( 
  DocNum 
  ,Series 
  ,ObjType
  ,BaseEntry
  ,BaseNum
  ,BaseType
  ,TargetEntry
  ,TargetNum
  ,FinancePeriod
  ,BatchNum 
  ,TargetType
  ,TransType
  ,RefDate
  ,RefDate2
  ,RefDate3
  ,Ref1
  ,Ref2
  ,Ref3
  ,Project
  ,Remarks
  ,SysTotal
  ,SysTotalLC
  ,SysTotalFC
  ,VersionNo
  ,IsActive
  ,CreatedByAppUserId
  ,CreatedOn
  ,ModifiedByAppUserId
  ,ModifiedOn 
  ) select * from @jdt  
  set @jdtID = @@IDENTITY  
  if @jdtID > 0  
  begin   
    update @jdtline set JournalEntryID = @jdtID   
    insert into JournalEntryDetail 
	select * from @jdtline  
    update PurchaseDelivery set 
		TransID = @jdtID 
		,JournalRemarks = (select top 1 Remarks from @jdt)
	where DocID = @docID  
  end   
 end  
  
  /*Sales Invoice*/
 if @docType = 'SIN'  
 begin   
  insert into @jdt  
   select   
    null DocNum  
   ,null Series  
   ,'JEN' ObjType  
   ,null BaseEntry  
   ,null BaseNum  
   ,null BaseType  
   ,null TargetEntry  
   ,null TargetNum  
   ,FPR.PeriodDetailID FinancePeriod  
   ,null BatchNum  
   ,null TargetType  
   ,'SIN' TransType  
   ,SIN.DocDate  RefDate   
   ,SIN.DocDueDate RefDate2   
   ,SIN.TaxDate RefDate3   
   ,SIN.DocID Ref1  
   ,null Ref2  
   ,null Ref3  
   ,SIN.Project Project  
   ,'Sales Invoice - ' + SIN.CardCode    Remarks  
   ,null SysTotal  
   ,null SysTotalLC  
   ,null SysTotalFC  
   ,null VersionNo   
   ,1 IsActive   
   ,@createdBy CreatedByAppUserId   
   ,Getdate() CreatedOn   
   ,null ModifiedByAppUserId   
   ,null ModifiedOn   
    from   
    SalesInvoice SIN   
    outer apply (  
     select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= SIN.DocDate and ToRefDate >= SIN.DocDate   
    ) FPR   
  
  where SIN.DocID = @docID   
   
  insert into @jdtline  
  (
	JournalEntryID, ObjType ,BaseEntry ,BaseNum ,BaseType ,TargetEntry ,TargetNum   ,TargetType ,TransType ,GLAcctCode ,LineID ,VisID ,FinancePeriod ,BatchNum ,ShortName ,Debit ,Credit ,DebitSC ,CreditSC ,DebitFC ,CreditFC 
	,ContraAccount ,DebitCredit ,BaseAmt ,VatGroup ,VatPercent ,VatAmt ,RefDate ,RefDate2 ,RefDate3 ,CreatedBy ,BaseRef ,Ref1 ,Ref2 ,Ref3 ,Project ,OcrCode ,OcrCode2 ,OcrCode3 ,OcrCode4 ,OcrCode5 ,LineRemarks ,VersionNo ,IsActive ,CreatedByAppUserId ,CreatedOn ,ModifiedByAppUserId ,ModifiedOn   
  )
    select  @jdtID JournalEntryID  
     ,ObjType   
     ,BaseEntry   
     ,BaseNum   
     ,BaseType   
     ,TargetEntry   
     ,TargetNum   
     ,TargetType   
     ,TransType   
     ,GLAcctCode   
     ,ROW_NUMBER() over( order by GLAcctCode) LineID   
     ,VisID   
     ,FinancePeriod   
     ,BatchNum   
     ,ShortName   
     ,Debit   
     ,Credit   
     ,DebitSC   
     ,CreditSC   
     ,DebitFC   
     ,CreditFC   
     ,ContraAccount   
     ,DebitCredit   
     ,BaseAmt   
     ,VatGroup   
     ,VatPercent   
     ,VatAmt   
     ,RefDate   
     ,RefDate2   
     ,RefDate3   
     ,CreatedBy   
     ,BaseRef   
     ,Ref1   
     ,Ref2   
     ,Ref3   
     ,Project   
     ,OcrCode   
     ,OcrCode2   
     ,OcrCode3   
     ,OcrCode4   
     ,OcrCode5  
     ,LineRemarks  
     ,null VersionNo   
     ,1 IsActive   
     ,@createdBy CreatedByAppUserId   
     ,Getdate() CreatedOn   
     ,null ModifiedByAppUserId   
     ,null ModifiedOn   
    from (  
     select   --Debit A/R  
     'JEN' ObjType   
     ,null BaseEntry   
     ,null BaseNum   
     ,null BaseType   
     ,null TargetEntry   
     ,null TargetNum   
     ,null TargetType   
     ,'SIN' TransType   
     ,CRD.CtlDebCredPayAcct GLAcctCode   
     ,null LineID   
     ,null VisID   
     ,FPR.PeriodDetailID FinancePeriod   
     ,null BatchNum   
     ,CRD.CardCode ShortName   
     ,Sum(SIN.DocTotal)  Debit   
     ,0 Credit   
     ,null DebitSC   
     ,null CreditSC   
     ,null DebitFC   
     ,null CreditFC   
     ,null ContraAccount   
     ,null DebitCredit   
     ,null BaseAmt   
     ,null VatGroup   
     ,null VatPercent   
     ,null VatAmt   
     ,SIN.DocDate  RefDate   
     ,SIN.DocDueDate RefDate2   
     ,SIN.TaxDate RefDate3   
     ,SIN.DocID CreatedBy   
     ,SIN.DocNum BaseRef   
     ,SIN.DocID  Ref1   
     ,null Ref2   
     ,null Ref3   
     ,SIN1.ProjectCode Project   
     ,SIN1.OcrCode OcrCode    
     ,SIN1.OcrCode2 OcrCode2   
     ,SIN1.OcrCode3 OcrCode3   
     ,SIN1.OcrCode4 OcrCode4   
     ,SIN1.OcrCode5 OcrCode5   
     ,'Sales Invoice - ' + SIN.CardCode   LineRemarks   
        from   
      SalesInvoiceDetail SIN1  
      inner join SalesInvoice SIN on SIN1.DocID = SIN.DocID  
      inner join BusinessPartner  CRD on SIN.CardCode  = CRD.CardCode  
      inner join ItemMaster ITM on SIN1.ItemCode = ITM.ItemCode   
      inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
      outer apply (  
       select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= SIN.DocDate and ToRefDate >= SIN.DocDate   
      ) FPR   
      where SIN.DocID = @docID   
      group by   
        SIN.CardCode   
       ,CRD.CtlDebCredPayAcct  
       ,CRD.CardCode  
       ,FPR.PeriodDetailID   
       ,SIN.DocDate   
       ,SIN.DocDueDate   
       ,SIN.TaxDate   
       ,SIN.DocID   
       ,SIN.DocNum   
       ,SIN1.ProjectCode   
       ,SIN1.OcrCode  
       ,SIN1.OcrCode2  
       ,SIN1.OcrCode3   
       ,SIN1.OcrCode4   
       ,SIN1.OcrCode5   
  
  union ALL  
     select   --Credit Output Tax  
     'JEN' ObjType   
     ,null BaseEntry   
     ,null BaseNum   
     ,null BaseType   
     ,null TargetEntry   
     ,null TargetNum   
     ,null TargetType   
     ,'SIN' TransType   
     ,TG.Account GLAcctCode   
     ,null LineID   
     ,null VisID   
     ,FPR.PeriodDetailID FinancePeriod   
     ,null BatchNum   
     ,TG.Account ShortName   
     ,0  Debit   
     ,Sum(SIN1.VatSum) Credit   
     ,null DebitSC   
     ,null CreditSC   
     ,null DebitFC   
     ,null CreditFC   
     ,null ContraAccount   
     ,null DebitCredit   
     ,null BaseAmt   
     ,SIN1.VatGroup VatGroup   
     ,min(SIN1.VatPercent) VatPercent   
     ,min(SIN1.VatSum) VatAmt   
     ,SIN.DocDate  RefDate   
     ,SIN.DocDueDate RefDate2   
     ,SIN.TaxDate RefDate3   
     ,SIN.DocID CreatedBy   
     ,SIN.DocNum BaseRef   
     ,SIN.DocID  Ref1   
     ,null Ref2   
     ,null Ref3   
     ,SIN1.ProjectCode Project   
     ,SIN1.OcrCode OcrCode    
     ,SIN1.OcrCode2 OcrCode2   
     ,SIN1.OcrCode3 OcrCode3   
     ,SIN1.OcrCode4 OcrCode4   
     ,SIN1.OcrCode5 OcrCode5   
     ,'Sales Invoice - ' + SIN.CardCode   LineRemarks   
        from   
      SalesInvoiceDetail SIN1  
      inner join SalesInvoice SIN on SIN1.DocID = SIN.DocID  
      inner join ItemMaster ITM on SIN1.ItemCode = ITM.ItemCode   
      inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
      outer apply (  
       select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= SIN.DocDate and ToRefDate >= SIN.DocDate   
      ) FPR   
  
      outer apply(  
        select Rate,VatCode,Account  From TaxGroup where VatCode = SIN1.VatGroup and Category = 'O'  
      ) TG  
      where SIN.DocID = @docID   
      group by   
       ITG.GoodClearingAcct   
       ,SIN.CardCode   
       ,FPR.PeriodDetailID   
       ,SIN.DocDate   
       ,SIN.DocDueDate   
       ,SIN.TaxDate   
       ,SIN.DocID   
       ,SIN.DocNum   
       ,SIN1.VatGroup   
       ,TG.Account  
       ,SIN1.ProjectCode   
       ,SIN1.OcrCode  
       ,SIN1.OcrCode2  
       ,SIN1.OcrCode3   
       ,SIN1.OcrCode4   
       ,SIN1.OcrCode5   

	   union ALL  
  
		select  -- Debit Wtax Acct  
		 'JEN' ObjType   
		 ,null BaseEntry   
		 ,null BaseNum   
		 ,null BaseType   
		 ,null TargetEntry   
		 ,null TargetNum   
		 ,null TargetType   
		 ,'SIN' TransType   
		 ,SINW.AcctCode GLAcctCode   
		 ,null LineID   
		 ,null VisID   
		 ,FPR.PeriodDetailID FinancePeriod   
		 ,null BatchNum   
		 ,SINW.AcctCode ShortName   
		 ,Sum(SINW.WTAmnt)   Debit   
		 ,0 Credit   
		 ,null DebitSC   
		 ,null CreditSC   
		 ,null DebitFC   
		 ,null CreditFC   
		 ,null ContraAccount   
		 ,null DebitCredit   
		 ,null BaseAmt   
		 ,null VatGroup   
		 ,null VatPercent   
		 ,null VatAmt   
		 ,SIN.DocDate  RefDate   
		 ,SIN.DocDueDate RefDate2   
		 ,SIN.TaxDate RefDate3   
		 ,SIN.DocID CreatedBy   
		 ,SIN.DocNum BaseRef   
		 ,SIN.DocID  Ref1   
		 ,null Ref2   
		 ,null Ref3   
		 ,'' Project   
		 ,'' OcrCode    
		 ,'' OcrCode2   
		 ,'' OcrCode3   
		 ,'' OcrCode4   
		 ,'' OcrCode5   
		 ,'Sales Invoice - ' + SIN.CardCode   LineRemarks   
		  from   
		  SalesInvoiceWtax SINW  
		  inner join SalesInvoice SIN on SINW  .DocID = SIN.DocID  
		  inner join BusinessPartner  CRD on SIN.CardCode  = CRD.CardCode  
		  inner join GLAccount  ACT on SINW  .AcctCode = ACT.AcctCode   
		  outer apply (  
		   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= SIN.DocDate and ToRefDate >= SIN.DocDate   
		  ) FPR   
		   where SIN.DocID = @docID  and CRD.WTLiable = 'Y'
		   group by   
			SIN.CardCode   
		   ,SINW.AcctCode
		   ,CRD.CtlDebCredPayAcct   
		   ,CRD.CardCode  
		   ,FPR.PeriodDetailID   
		   ,SIN.DocDate   
		   ,SIN.DocDueDate   
		   ,SIN.TaxDate   
		   ,SIN.DocID   
		   ,SIN.DocNum   


     union ALL  
  
     select  -- Debit Revenue Acct  
     'JEN' ObjType   
     ,null BaseEntry   
     ,null BaseNum   
     ,null BaseType   
     ,null TargetEntry   
     ,null TargetNum   
     ,null TargetType   
     ,'SIN' TransType   
     ,ITG.RevenueAcct GLAcctCode   
     ,null LineID   
     ,null VisID   
     ,FPR.PeriodDetailID FinancePeriod   
     ,null BatchNum   
     ,ITG.RevenueAcct ShortName   
     ,0 Debit   
     ,Sum(SIN1.LineTotal)  Credit   
     ,null DebitSC   
     ,null CreditSC   
     ,null DebitFC   
     ,null CreditFC   
     ,null ContraAccount   
     ,null DebitCredit   
     ,null BaseAmt   
     ,null VatGroup   
     ,null VatPercent   
     ,null VatAmt   
     ,SIN.DocDate  RefDate   
     ,SIN.DocDueDate RefDate2   
     ,SIN.TaxDate RefDate3   
     ,SIN.DocID CreatedBy   
     ,SIN.DocNum BaseRef   
     ,SIN.DocID  Ref1   
     ,null Ref2   
     ,null Ref3   
     ,SIN1.ProjectCode Project   
     ,SIN1.OcrCode OcrCode    
     ,SIN1.OcrCode2 OcrCode2   
     ,SIN1.OcrCode3 OcrCode3   
     ,SIN1.OcrCode4 OcrCode4   
     ,SIN1.OcrCode5 OcrCode5   
     ,'Sales Invoice - ' + SIN.CardCode   LineRemarks   
      from   
      SalesInvoiceDetail SIN1  
      inner join SalesInvoice SIN on SIN1.DocID = SIN.DocID  
      inner join BusinessPartner  CRD on SIN.CardCode  = CRD.CardCode  
      inner join ItemMaster ITM on SIN1.ItemCode = ITM.ItemCode   
      inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
      outer apply (  
       select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= SIN.DocDate and ToRefDate >= SIN.DocDate   
      ) FPR   
       where SIN.DocID = @docID   
       group by   
       SIN.CardCode   
       ,CRD.CtlDebCredPayAcct   
       ,CRD.CardCode  
       ,ITG.RevenueAcct   
       ,FPR.PeriodDetailID   
       ,SIN.DocDate   
       ,SIN.DocDueDate   
       ,SIN.TaxDate   
       ,SIN.DocID   
       ,SIN.DocNum   
       ,SIN1.ProjectCode   
       ,SIN1.OcrCode  
       ,SIN1.OcrCode2  
       ,SIN1.OcrCode3   
       ,SIN1.OcrCode4   
       ,SIN1.OcrCode5   
  
    ) A  
  
  update @jdt set SysTotal = (select sum(debit) from @jdtline)  
  insert into JournalEntry ( 
  DocNum 
  ,Series 
  ,ObjType
  ,BaseEntry
  ,BaseNum
  ,BaseType
  ,TargetEntry
  ,TargetNum
  ,FinancePeriod
  ,BatchNum 
  ,TargetType
  ,TransType
  ,RefDate
  ,RefDate2
  ,RefDate3
  ,Ref1
  ,Ref2
  ,Ref3
  ,Project
  ,Remarks
  ,SysTotal
  ,SysTotalLC
  ,SysTotalFC
  ,VersionNo
  ,IsActive
  ,CreatedByAppUserId
  ,CreatedOn
  ,ModifiedByAppUserId
  ,ModifiedOn 
  ) select * from @jdt  
  set @jdtID = @@IDENTITY  
  
  if @jdtID > 0  
  begin   
    update @jdtline set JournalEntryID = @jdtID   
    insert into JournalEntryDetail select * from @jdtline  
  
    update SalesInvoice set TransID = @jdtID ,JournalRemarks = (select top 1 Remarks from @jdt)  where DocID = @docID  
  end   
 end  
  
  /*Sales CreditNote*/
 if @docType = 'SCR'  
 begin   
  insert into @jdt  
   select   
    null DocNum  
   ,null Series  
   ,'JEN' ObjType  
   ,null BaseEntry  
   ,null BaseNum  
   ,null BaseType  
   ,null TargetEntry  
   ,null TargetNum  
   ,FPR.PeriodDetailID FinancePeriod  
   ,null BatchNum  
   ,null TargetType  
   ,'SCR' TransType  
   ,SCR.DocDate  RefDate   
   ,SCR.DocDueDate RefDate2   
   ,SCR.TaxDate RefDate3   
   ,SCR.DocID Ref1  
   ,null Ref2  
   ,null Ref3  
   ,SCR.Project Project  
   ,'Sales CreditNote - ' + SCR.CardCode    Remarks  
   ,null SysTotal  
   ,null SysTotalLC  
   ,null SysTotalFC  
   ,null VersionNo   
   ,1 IsActive   
   ,@createdBy CreatedByAppUserId   
   ,Getdate() CreatedOn   
   ,null ModifiedByAppUserId   
   ,null ModifiedOn   
    from   
    SalesCreditNote SCR   
    outer apply (  
     select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= SCR.DocDate and ToRefDate >= SCR.DocDate   
    ) FPR   
  
  where SCR.DocID = @docID   
   
  insert into @jdtline  (
	JournalEntryID, ObjType ,BaseEntry ,BaseNum ,BaseType ,TargetEntry ,TargetNum   ,TargetType ,TransType ,GLAcctCode ,LineID ,VisID ,FinancePeriod ,BatchNum ,ShortName ,Debit ,Credit ,DebitSC ,CreditSC ,DebitFC ,CreditFC 
	,ContraAccount ,DebitCredit ,BaseAmt ,VatGroup ,VatPercent ,VatAmt ,RefDate ,RefDate2 ,RefDate3 ,CreatedBy ,BaseRef ,Ref1 ,Ref2 ,Ref3 ,Project ,OcrCode ,OcrCode2 ,OcrCode3 ,OcrCode4 ,OcrCode5 ,LineRemarks ,VersionNo ,IsActive ,CreatedByAppUserId ,CreatedOn ,ModifiedByAppUserId ,ModifiedOn   
  )
    select  @jdtID JournalEntryID  
     ,ObjType   
     ,BaseEntry   
     ,BaseNum   
     ,BaseType   
     ,TargetEntry   
     ,TargetNum   
     ,TargetType   
     ,TransType   
     ,GLAcctCode   
     ,ROW_NUMBER() over( order by GLAcctCode) LineID   
     ,VisID   
     ,FinancePeriod   
     ,BatchNum   
     ,ShortName   
     ,Debit   
     ,Credit   
     ,DebitSC   
     ,CreditSC   
     ,DebitFC   
     ,CreditFC   
     ,ContraAccount   
     ,DebitCredit   
     ,BaseAmt   
     ,VatGroup   
     ,VatPercent   
     ,VatAmt   
     ,RefDate   
     ,RefDate2   
     ,RefDate3   
     ,CreatedBy   
     ,BaseRef   
     ,Ref1   
     ,Ref2   
     ,Ref3   
     ,Project   
     ,OcrCode   
     ,OcrCode2   
     ,OcrCode3   
     ,OcrCode4   
     ,OcrCode5  
     ,LineRemarks  
     ,null VersionNo   
     ,1 IsActive   
     ,@createdBy CreatedByAppUserId   
     ,Getdate() CreatedOn   
     ,null ModifiedByAppUserId   
     ,null ModifiedOn   
    from (  
     select   --Credit A/R  
     'JEN' ObjType   
     ,null BaseEntry   
     ,null BaseNum   
     ,null BaseType   
     ,null TargetEntry   
     ,null TargetNum   
     ,null TargetType   
     ,'SCR' TransType   
     ,CRD.CtlDebCredPayAcct GLAcctCode   
     ,null LineID   
     ,null VisID   
     ,FPR.PeriodDetailID FinancePeriod   
     ,null BatchNum   
     ,CRD.CardCode ShortName   
     ,0  Debit   
     ,Sum(SCR.DocTotal) Credit   
     ,null DebitSC   
     ,null CreditSC   
     ,null DebitFC   
     ,null CreditFC   
     ,null ContraAccount   
     ,null DebitCredit   
     ,null BaseAmt   
     ,null VatGroup   
     ,null VatPercent   
     ,null VatAmt   
     ,SCR.DocDate  RefDate   
     ,SCR.DocDueDate RefDate2   
     ,SCR.TaxDate RefDate3   
     ,SCR.DocID CreatedBy   
     ,SCR.DocNum BaseRef   
     ,SCR.DocID  Ref1   
     ,null Ref2   
     ,null Ref3   
     ,SCR1.ProjectCode Project   
     ,SCR1.OcrCode OcrCode    
     ,SCR1.OcrCode2 OcrCode2   
     ,SCR1.OcrCode3 OcrCode3   
     ,SCR1.OcrCode4 OcrCode4   
     ,SCR1.OcrCode5 OcrCode5   
     ,'Sales CreditNote - ' + SCR.CardCode   LineRemarks   
        from   
      SalesCreditNoteDetail SCR1  
      inner join SalesCreditNote SCR on SCR1.DocID = SCR.DocID  
      inner join BusinessPartner CRD on SCR.CardCode  = CRD.CardCode  
      inner join ItemMaster ITM on SCR1.ItemCode = ITM.ItemCode   
      inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
      outer apply (  
       select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= SCR.DocDate and ToRefDate >= SCR.DocDate   
      ) FPR   
      where SCR.DocID = @docID   
      group by   
        SCR.CardCode   
       ,CRD.CtlDebCredPayAcct  
       ,CRD.CardCode  
       ,FPR.PeriodDetailID   
       ,SCR.DocDate   
       ,SCR.DocDueDate   
       ,SCR.TaxDate   
       ,SCR.DocID   
       ,SCR.DocNum   
       ,SCR1.ProjectCode   
       ,SCR1.OcrCode  
       ,SCR1.OcrCode2  
       ,SCR1.OcrCode3   
       ,SCR1.OcrCode4   
       ,SCR1.OcrCode5   



     union ALL  
  
     select   --Debit Output Tax  
     'JEN' ObjType   
     ,null BaseEntry   
     ,null BaseNum   
     ,null BaseType   
     ,null TargetEntry   
     ,null TargetNum   
     ,null TargetType   
     ,'SCR' TransType   
     ,TG.Account GLAcctCode   
     ,null LineID   
     ,null VisID   
     ,FPR.PeriodDetailID FinancePeriod   
     ,null BatchNum   
     ,TG.Account ShortName   
     ,Sum(SCR1.VatSum)  Debit   
     ,0 Credit   
     ,null DebitSC   
     ,null CreditSC   
     ,null DebitFC   
     ,null CreditFC   
     ,null ContraAccount   
     ,null DebitCredit   
     ,null BaseAmt   
     ,SCR1.VatGroup VatGroup   
     ,min(SCR1.VatPercent) VatPercent   
     ,min(SCR1.VatSum) VatAmt   
     ,SCR.DocDate  RefDate   
     ,SCR.DocDueDate RefDate2   
     ,SCR.TaxDate RefDate3   
     ,SCR.DocID CreatedBy   
     ,SCR.DocNum BaseRef   
     ,SCR.DocID  Ref1   
     ,null Ref2   
     ,null Ref3   
     ,SCR1.ProjectCode Project   
     ,SCR1.OcrCode OcrCode    
     ,SCR1.OcrCode2 OcrCode2   
     ,SCR1.OcrCode3 OcrCode3   
     ,SCR1.OcrCode4 OcrCode4   
     ,SCR1.OcrCode5 OcrCode5   
     ,'Sales CreditNote - ' + SCR.CardCode   LineRemarks   
        from   
      SalesCreditNoteDetail SCR1  
      inner join SalesCreditNote SCR on SCR1.DocID = SCR.DocID  
      inner join ItemMaster ITM on SCR1.ItemCode = ITM.ItemCode   
      inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
      outer apply (  
       select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= SCR.DocDate and ToRefDate >= SCR.DocDate   
      ) FPR   
  
      outer apply(  
        select Rate,VatCode,Account  From TaxGroup where VatCode = SCR1.VatGroup and Category = 'O'  
      ) TG  
      where SCR.DocID = @docID   
      group by   
       ITG.GoodClearingAcct   
       ,SCR.CardCode   
       ,FPR.PeriodDetailID   
       ,SCR.DocDate   
       ,SCR.DocDueDate   
       ,SCR.TaxDate   
       ,SCR.DocID   
       ,SCR.DocNum   
       ,SCR1.VatGroup   
       ,TG.Account  
       ,SCR1.ProjectCode   
       ,SCR1.OcrCode  
       ,SCR1.OcrCode2  
       ,SCR1.OcrCode3   
       ,SCR1.OcrCode4   
       ,SCR1.OcrCode5   


	   union ALL  
  
		select  -- Credit Wtax Acct  
		 'JEN' ObjType   
		 ,null BaseEntry   
		 ,null BaseNum   
		 ,null BaseType   
		 ,null TargetEntry   
		 ,null TargetNum   
		 ,null TargetType   
		 ,'SIN' TransType   
		 ,SCRW.AcctCode GLAcctCode   
		 ,null LineID   
		 ,null VisID   
		 ,FPR.PeriodDetailID FinancePeriod   
		 ,null BatchNum   
		 ,SCRW  .AcctCode ShortName   
		 ,0  Debit   
		 ,Sum(SCRW.WTAmnt) Credit   
		 ,null DebitSC   
		 ,null CreditSC   
		 ,null DebitFC   
		 ,null CreditFC   
		 ,null ContraAccount   
		 ,null DebitCredit   
		 ,null BaseAmt   
		 ,null VatGroup   
		 ,null VatPercent   
		 ,null VatAmt   
		 ,SCR.DocDate  RefDate   
		 ,SCR.DocDueDate RefDate2   
		 ,SCR.TaxDate RefDate3   
		 ,SCR.DocID CreatedBy   
		 ,SCR.DocNum BaseRef   
		 ,SCR.DocID  Ref1   
		 ,null Ref2   
		 ,null Ref3   
		 ,'' Project   
		 ,'' OcrCode    
		 ,'' OcrCode2   
		 ,'' OcrCode3   
		 ,'' OcrCode4   
		 ,'' OcrCode5   
		 ,'Sales CreditNote - ' + SCR.CardCode   LineRemarks   
		  from   
		  SalesCreditNoteWtax SCRW  
		  inner join SalesCreditNote SCR on SCRW.DocID = SCR.DocID  
		  inner join BusinessPartner  CRD on SCR.CardCode  = CRD.CardCode  
		  inner join GLAccount  ACT on SCRW    .AcctCode = ACT.AcctCode   
		  outer apply (  
		   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= SCR.DocDate and ToRefDate >= SCR.DocDate   
		  ) FPR   
		   where SCR.DocID = @docID  and CRD.WTLiable = 'Y'
		   group by   
			SCR.CardCode   
		   ,SCRW  .AcctCode
		   ,CRD.CtlDebCredPayAcct   
		   ,CRD.CardCode  
		   ,FPR.PeriodDetailID   
		   ,SCR.DocDate   
		   ,SCR.DocDueDate   
		   ,SCR.TaxDate   
		   ,SCR.DocID   
		   ,SCR.DocNum   


     union ALL  
  
     select  -- Credit Revenue Acct  
     'JEN' ObjType   
     ,null BaseEntry   
     ,null BaseNum   
     ,null BaseType   
     ,null TargetEntry   
     ,null TargetNum   
     ,null TargetType   
     ,'SCR' TransType   
     ,ITG.RevenueAcct GLAcctCode   
     ,null LineID   
     ,null VisID   
     ,FPR.PeriodDetailID FinancePeriod   
     ,null BatchNum   
     ,ITG.RevenueAcct ShortName   
     ,Sum(SCR1.LineTotal) Debit   
     ,0 Credit   
     ,null DebitSC   
     ,null CreditSC   
     ,null DebitFC   
     ,null CreditFC   
     ,null ContraAccount   
     ,null DebitCredit   
     ,null BaseAmt   
     ,null VatGroup   
     ,null VatPercent   
     ,null VatAmt   
     ,SCR.DocDate  RefDate   
     ,SCR.DocDueDate RefDate2   
     ,SCR.TaxDate RefDate3   
     ,SCR.DocID CreatedBy   
     ,SCR.DocNum BaseRef   
     ,SCR.DocID  Ref1   
     ,null Ref2   
     ,null Ref3   
     ,SCR1.ProjectCode Project   
     ,SCR1.OcrCode OcrCode    
     ,SCR1.OcrCode2 OcrCode2   
     ,SCR1.OcrCode3 OcrCode3   
     ,SCR1.OcrCode4 OcrCode4   
     ,SCR1.OcrCode5 OcrCode5   
     ,'Sales CreditNote - ' + SCR.CardCode   LineRemarks   
      from   
      SalesCreditNoteDetail SCR1  
      inner join SalesCreditNote SCR on SCR1.DocID = SCR.DocID  
      inner join BusinessPartner  CRD on SCR.CardCode  = CRD.CardCode  
      inner join ItemMaster ITM on SCR1.ItemCode = ITM.ItemCode   
      inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
      outer apply (  
       select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= SCR.DocDate and ToRefDate >= SCR.DocDate   
      ) FPR   
       where SCR.DocID = @docID   
       group by   
       SCR.CardCode   
       ,CRD.CtlDebCredPayAcct   
       ,CRD.CardCode  
       ,ITG.RevenueAcct   
       ,FPR.PeriodDetailID   
       ,SCR.DocDate   
       ,SCR.DocDueDate   
       ,SCR.TaxDate   
       ,SCR.DocID   
       ,SCR.DocNum   
       ,SCR1.ProjectCode   
       ,SCR1.OcrCode  
       ,SCR1.OcrCode2  
       ,SCR1.OcrCode3   
       ,SCR1.OcrCode4   
       ,SCR1.OcrCode5   
  
    ) A  
  
  update @jdt set SysTotal = (select sum(debit) from @jdtline)  
  insert into JournalEntry ( 
  DocNum 
  ,Series 
  ,ObjType
  ,BaseEntry
  ,BaseNum
  ,BaseType
  ,TargetEntry
  ,TargetNum
  ,FinancePeriod
  ,BatchNum 
  ,TargetType
  ,TransType
  ,RefDate
  ,RefDate2
  ,RefDate3
  ,Ref1
  ,Ref2
  ,Ref3
  ,Project
  ,Remarks
  ,SysTotal
  ,SysTotalLC
  ,SysTotalFC
  ,VersionNo
  ,IsActive
  ,CreatedByAppUserId
  ,CreatedOn
  ,ModifiedByAppUserId
  ,ModifiedOn 
  ) select * from @jdt  
  set @jdtID = @@IDENTITY  
  
  if @jdtID > 0  
  begin   
    update @jdtline set JournalEntryID = @jdtID   
    insert into JournalEntryDetail select * from @jdtline  
  
    update SalesCreditNote set TransID = @jdtID ,JournalRemarks = (select top 1 Remarks from @jdt)  where DocID = @docID  
  end   
 end  
  
  /*Purchase Delivery*/
 if @docType = 'PDN'  
 begin   
   
  insert into @jdt  
   select   
    null DocNum  
   ,null Series  
   ,'JEN' ObjType  
   ,null BaseEntry  
   ,null BaseNum  
   ,null BaseType  
   ,null TargetEntry  
   ,null TargetNum  
   ,FPR.PeriodDetailID FinancePeriod  
   ,null BatchNum  
   ,null TargetType  
   ,'PDN' TransType  
   ,PDN.DocDate  RefDate   
   ,PDN.DocDueDate RefDate2   
   ,PDN.TaxDate RefDate3   
   ,PDN.DocID Ref1  
   ,null Ref2  
   ,null Ref3  
   ,PDN.Project Project  
   ,'Goods Receipt PO - ' + PDN.CardCode    Remarks  
   ,null SysTotal  
   ,null SysTotalLC  
   ,null SysTotalFC  
   ,null VersionNo   
   ,1 IsActive   
   ,@createdBy CreatedByAppUserId   
   ,Getdate() CreatedOn   
   ,null ModifiedByAppUserId   
   ,null ModifiedOn   
    from   
    PurchaseDelivery PDN  
    outer apply (  
     select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= PDN.DocDate and ToRefDate >= PDN.DocDate   
    ) FPR   
  
  where PDN.DocID = @docID   
   
  insert into @jdtline  (
	JournalEntryID, ObjType ,BaseEntry ,BaseNum ,BaseType ,TargetEntry ,TargetNum   ,TargetType ,TransType ,GLAcctCode ,LineID ,VisID ,FinancePeriod ,BatchNum ,ShortName ,Debit ,Credit ,DebitSC ,CreditSC ,DebitFC ,CreditFC 
	,ContraAccount ,DebitCredit ,BaseAmt ,VatGroup ,VatPercent ,VatAmt ,RefDate ,RefDate2 ,RefDate3 ,CreatedBy ,BaseRef ,Ref1 ,Ref2 ,Ref3 ,Project ,OcrCode ,OcrCode2 ,OcrCode3 ,OcrCode4 ,OcrCode5 ,LineRemarks ,VersionNo ,IsActive ,CreatedByAppUserId ,CreatedOn ,ModifiedByAppUserId ,ModifiedOn   
  )
    select  @jdtID JournalEntryID  
     ,ObjType   
     ,BaseEntry   
     ,BaseNum   
     ,BaseType   
     ,TargetEntry   
     ,TargetNum   
     ,TargetType   
     ,TransType   
     ,GLAcctCode   
     ,ROW_NUMBER() over( order by GLAcctCode) LineID   
     ,VisID   
     ,FinancePeriod   
     ,BatchNum   
     ,ShortName   
     ,Debit   
     ,Credit   
     ,DebitSC   
     ,CreditSC   
     ,DebitFC   
     ,CreditFC   
     ,ContraAccount   
     ,DebitCredit   
     ,BaseAmt   
     ,VatGroup   
     ,VatPercent   
     ,VatAmt   
     ,RefDate   
     ,RefDate2   
     ,RefDate3   
     ,CreatedBy   
     ,BaseRef   
     ,Ref1   
     ,Ref2   
     ,Ref3   
     ,Project   
     ,OcrCode   
     ,OcrCode2   
     ,OcrCode3   
     ,OcrCode4   
     ,OcrCode5  
     ,LineRemarks  
     ,null VersionNo   
     ,1 IsActive   
     ,@createdBy CreatedByAppUserId   
     ,Getdate() CreatedOn   
     ,null ModifiedByAppUserId   
     ,null ModifiedOn   
    from (  
     select   --Credit Goods Clearing  
     'JEN' ObjType   
     ,null BaseEntry   
     ,null BaseNum   
     ,null BaseType   
     ,null TargetEntry   
     ,null TargetNum   
     ,null TargetType   
     ,'PDN' TransType   
     ,ITG.GoodClearingAcct GLAcctCode   
     ,null LineID   
     ,null VisID   
     ,FPR.PeriodDetailID FinancePeriod   
     ,null BatchNum   
     ,ITG.GoodClearingAcct ShortName   
     ,0 Debit   
     ,Sum(PDN1.LineTotal) Credit   
     ,null DebitSC   
     ,null CreditSC   
     ,null DebitFC   
     ,null CreditFC   
     ,null ContraAccount   
     ,null DebitCredit   
     ,null BaseAmt   
     ,null VatGroup   
     ,null VatPercent   
     ,null VatAmt   
     ,PDN.DocDate  RefDate   
     ,PDN.DocDueDate RefDate2   
     ,PDN.TaxDate RefDate3   
     ,PDN.DocID CreatedBy   
     ,PDN.DocNum BaseRef   
     ,PDN.DocID  Ref1   
     ,null Ref2   
     ,null Ref3   
     ,PDN1.ProjectCode Project   
     ,PDN1.OcrCode OcrCode    
     ,PDN1.OcrCode2 OcrCode2   
     ,PDN1.OcrCode3 OcrCode3   
     ,PDN1.OcrCode4 OcrCode4   
     ,PDN1.OcrCode5 OcrCode5   
     ,'Goods Receipt PO - ' + PDN.CardCode   LineRemarks   
        from   
      PurchaseDeliveryDetail PDN1  
      inner join PurchaseDelivery PDN on PDN1.DocID = PDN.DocID  
      inner join ItemMaster ITM on PDN1.ItemCode = ITM.ItemCode   
      inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
      outer apply (  
       select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= PDN.DocDate and ToRefDate >= PDN.DocDate   
      ) FPR   
      where PDN.DocID = @docID   
      group by   
       ITG.GoodClearingAcct   
       ,PDN.CardCode   
       ,FPR.PeriodDetailID   
       ,PDN.DocDate   
       ,PDN.DocDueDate   
       ,PDN.TaxDate   
       ,PDN.DocID   
       ,PDN.DocNum   
       ,PDN1.ProjectCode   
       ,PDN1.OcrCode  
       ,PDN1.OcrCode2  
       ,PDN1.OcrCode3   
       ,PDN1.OcrCode4   
       ,PDN1.OcrCode5   
     union ALL  
  
     select  --Debit Invntry Acct  
     'JEN' ObjType   
     ,null BaseEntry   
     ,null BaseNum   
     ,null BaseType   
     ,null TargetEntry   
     ,null TargetNum   
     ,null TargetType   
     ,'PDN' TransType   
     ,ITG.InvntryAcct  GLAcctCode   
     ,null LineID   
     ,null VisID   
     ,FPR.PeriodDetailID FinancePeriod   
     ,null BatchNum   
     ,ITG.InvntryAcct ShortName   
     ,Sum(PDN1.LineTotal) Debit   
     ,0 Credit   
     ,null DebitSC   
     ,null CreditSC   
     ,null DebitFC   
     ,null CreditFC   
     ,null ContraAccount   
     ,null DebitCredit   
     ,null BaseAmt   
     ,null VatGroup   
     ,null VatPercent   
     ,null VatAmt   
     ,PDN.DocDate  RefDate   
     ,PDN.DocDueDate RefDate2   
     ,PDN.TaxDate RefDate3   
     ,PDN.DocID CreatedBy   
     ,PDN.DocNum BaseRef   
     ,PDN.DocID  Ref1   
     ,null Ref2   
     ,null Ref3   
     ,PDN1.ProjectCode Project   
     ,PDN1.OcrCode OcrCode    
     ,PDN1.OcrCode2 OcrCode2   
     ,PDN1.OcrCode3 OcrCode3   
     ,PDN1.OcrCode4 OcrCode4   
     ,PDN1.OcrCode5 OcrCode5   
     ,'Goods Receipt PO - ' + PDN.CardCode   LineRemarks   
      from   
      PurchaseDeliveryDetail PDN1  
      inner join PurchaseDelivery PDN on PDN1.DocID = PDN.DocID  
      inner join BusinessPartner  CRD on PDN.CardCode  = CRD.CardCode  
      inner join ItemMaster ITM on PDN1.ItemCode = ITM.ItemCode   
      inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
      outer apply (  
       select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= PDN.DocDate and ToRefDate >= PDN.DocDate   
      ) FPR   
       where PDN.DocID = @docID   
       group by   
       ITG.InvntryAcct   
       ,PDN.CardCode   
       ,CRD.CtlDebCredPayAcct   
       ,FPR.PeriodDetailID   
       ,PDN.DocDate   
       ,PDN.DocDueDate   
       ,PDN.TaxDate   
       ,PDN.DocID   
       ,PDN.DocNum   
       ,PDN1.ProjectCode   
       ,PDN1.OcrCode  
       ,PDN1.OcrCode2  
       ,PDN1.OcrCode3   
       ,PDN1.OcrCode4   
       ,PDN1.OcrCode5   
  
    ) A  
  
  update @jdt set SysTotal = (select sum(debit) from @jdtline)  
  insert into JournalEntry ( 
  DocNum 
  ,Series 
  ,ObjType
  ,BaseEntry
  ,BaseNum
  ,BaseType
  ,TargetEntry
  ,TargetNum
  ,FinancePeriod
  ,BatchNum 
  ,TargetType
  ,TransType
  ,RefDate
  ,RefDate2
  ,RefDate3
  ,Ref1
  ,Ref2
  ,Ref3
  ,Project
  ,Remarks
  ,SysTotal
  ,SysTotalLC
  ,SysTotalFC
  ,VersionNo
  ,IsActive
  ,CreatedByAppUserId
  ,CreatedOn
  ,ModifiedByAppUserId
  ,ModifiedOn 
  ) select * from @jdt  
  set @jdtID = @@IDENTITY  
  if @jdtID > 0  
  begin   
    update @jdtline set JournalEntryID = @jdtID   
    insert into JournalEntryDetail select * from @jdtline  
    update PurchaseDelivery set TransID = @jdtID ,JournalRemarks = (select top 1 Remarks from @jdt) where DocID = @docID  
  end   
 end   
  
  /*Purchase Return*/
 if @docType = 'PRD'  
 begin   
  insert into @jdt  
   select   
    null DocNum  
   ,null Series  
   ,'JEN' ObjType  
   ,null BaseEntry  
   ,null BaseNum  
   ,null BaseType  
   ,null TargetEntry  
   ,null TargetNum  
   ,FPR.PeriodDetailID FinancePeriod  
   ,null BatchNum  
   ,null TargetType  
   ,'PRD' TransType  
   ,PRD.DocDate  RefDate   
   ,PRD.DocDueDate RefDate2   
   ,PRD.TaxDate RefDate3   
   ,PRD.DocID Ref1  
   ,null Ref2  
   ,null Ref3  
   ,PRD.Project Project  
   ,'Purchase Return - ' + PRD.CardCode    Remarks  
   ,null SysTotal  
   ,null SysTotalLC  
   ,null SysTotalFC  
   ,null VersionNo   
   ,1 IsActive   
   ,@createdBy CreatedByAppUserId   
   ,Getdate() CreatedOn   
   ,null ModifiedByAppUserId   
   ,null ModifiedOn   
    from   
    PurchaseReturn PRD  
    outer apply (  
     select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= PRD.DocDate and ToRefDate >= PRD.DocDate   
    ) FPR   
  
  where PRD.DocID = @docID   
   
  insert into @jdtline  (
	JournalEntryID, ObjType ,BaseEntry ,BaseNum ,BaseType ,TargetEntry ,TargetNum   ,TargetType ,TransType ,GLAcctCode ,LineID ,VisID ,FinancePeriod ,BatchNum ,ShortName ,Debit ,Credit ,DebitSC ,CreditSC ,DebitFC ,CreditFC 
	,ContraAccount ,DebitCredit ,BaseAmt ,VatGroup ,VatPercent ,VatAmt ,RefDate ,RefDate2 ,RefDate3 ,CreatedBy ,BaseRef ,Ref1 ,Ref2 ,Ref3 ,Project ,OcrCode ,OcrCode2 ,OcrCode3 ,OcrCode4 ,OcrCode5 ,LineRemarks ,VersionNo ,IsActive ,CreatedByAppUserId ,CreatedOn ,ModifiedByAppUserId ,ModifiedOn   
  )
    select  @jdtID JournalEntryID  
     ,ObjType   
     ,BaseEntry   
     ,BaseNum   
     ,BaseType   
     ,TargetEntry   
     ,TargetNum   
     ,TargetType   
     ,TransType   
     ,GLAcctCode   
     ,ROW_NUMBER() over( order by GLAcctCode) LineID   
     ,VisID   
     ,FinancePeriod   
     ,BatchNum   
     ,ShortName   
     ,Debit   
     ,Credit   
     ,DebitSC   
     ,CreditSC   
     ,DebitFC   
     ,CreditFC   
     ,ContraAccount   
     ,DebitCredit   
     ,BaseAmt   
     ,VatGroup   
     ,VatPercent   
     ,VatAmt   
     ,RefDate   
     ,RefDate2   
     ,RefDate3   
     ,CreatedBy   
     ,BaseRef   
     ,Ref1   
     ,Ref2   
     ,Ref3   
     ,Project   
     ,OcrCode   
     ,OcrCode2   
     ,OcrCode3   
     ,OcrCode4   
     ,OcrCode5  
     ,LineRemarks  
     ,null VersionNo   
     ,1 IsActive   
     ,@createdBy CreatedByAppUserId   
     ,Getdate() CreatedOn   
     ,null ModifiedByAppUserId   
     ,null ModifiedOn   
    from (  
     select   --Credit Goods Clearing  
     'JEN' ObjType   
     ,null BaseEntry   
     ,null BaseNum   
     ,null BaseType   
     ,null TargetEntry   
     ,null TargetNum   
     ,null TargetType   
     ,'PRD' TransType   
     ,ITG.GoodClearingAcct GLAcctCode   
     ,null LineID   
     ,null VisID   
     ,FPR.PeriodDetailID FinancePeriod   
     ,null BatchNum   
     ,ITG.GoodClearingAcct ShortName   
     ,0 Debit   
     ,Sum(PRD1.LineTotal) Credit   
     ,null DebitSC   
     ,null CreditSC   
     ,null DebitFC   
     ,null CreditFC   
     ,null ContraAccount   
     ,null DebitCredit   
     ,null BaseAmt   
     ,null VatGroup   
     ,null VatPercent   
     ,null VatAmt   
     ,PRD.DocDate  RefDate   
     ,PRD.DocDueDate RefDate2   
     ,PRD.TaxDate RefDate3   
     ,PRD.DocID CreatedBy   
     ,PRD.DocNum BaseRef   
     ,PRD.DocID  Ref1   
     ,null Ref2   
     ,null Ref3   
     ,PRD1.ProjectCode Project   
     ,PRD1.OcrCode OcrCode    
     ,PRD1.OcrCode2 OcrCode2   
     ,PRD1.OcrCode3 OcrCode3   
     ,PRD1.OcrCode4 OcrCode4   
     ,PRD1.OcrCode5 OcrCode5   
     ,'Purchase Return - ' + PRD.CardCode   LineRemarks   
        from   
      PurchaseReturnDetail PRD1  
      inner join PurchaseReturn PRD on PRD1.DocID = PRD.DocID  
      inner join ItemMaster ITM on PRD1.ItemCode = ITM.ItemCode   
      inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
      outer apply (  
       select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= PRD.DocDate and ToRefDate >= PRD.DocDate   
      ) FPR   
      where PRD.DocID = @docID   
      group by   
       ITG.GoodClearingAcct   
       ,PRD.CardCode   
       ,FPR.PeriodDetailID   
       ,PRD.DocDate   
       ,PRD.DocDueDate   
       ,PRD.TaxDate   
       ,PRD.DocID   
       ,PRD.DocNum   
       ,PRD1.ProjectCode   
       ,PRD1.OcrCode  
       ,PRD1.OcrCode2  
       ,PRD1.OcrCode3   
       ,PRD1.OcrCode4   
       ,PRD1.OcrCode5   
     union ALL  
  
     select  --Debit Invntry Acct  
     'JEN' ObjType   
     ,null BaseEntry   
     ,null BaseNum   
     ,null BaseType   
     ,null TargetEntry   
     ,null TargetNum   
     ,null TargetType   
     ,'PRD' TransType   
     ,ITG.InvntryAcct  GLAcctCode   
     ,null LineID   
     ,null VisID   
     ,FPR.PeriodDetailID FinancePeriod   
     ,null BatchNum   
     ,ITG.InvntryAcct ShortName   
     ,Sum(PRD1.LineTotal) Debit   
     ,0 Credit   
     ,null DebitSC   
     ,null CreditSC   
     ,null DebitFC   
     ,null CreditFC   
     ,null ContraAccount   
     ,null DebitCredit   
     ,null BaseAmt   
     ,null VatGroup   
     ,null VatPercent   
     ,null VatAmt   
     ,PRD.DocDate  RefDate   
     ,PRD.DocDueDate RefDate2   
     ,PRD.TaxDate RefDate3   
     ,PRD.DocID CreatedBy   
     ,PRD.DocNum BaseRef   
     ,PRD.DocID  Ref1   
     ,null Ref2   
     ,null Ref3   
     ,PRD1.ProjectCode Project   
     ,PRD1.OcrCode OcrCode    
     ,PRD1.OcrCode2 OcrCode2   
     ,PRD1.OcrCode3 OcrCode3   
     ,PRD1.OcrCode4 OcrCode4   
     ,PRD1.OcrCode5 OcrCode5   
     ,'Purchase Return - ' + PRD.CardCode   LineRemarks   
      from   
      PurchaseReturnDetail PRD1  
      inner join PurchaseReturn PRD on PRD1.DocID = PRD.DocID  
      inner join BusinessPartner  CRD on PRD.CardCode  = CRD.CardCode  
      inner join ItemMaster ITM on PRD1.ItemCode = ITM.ItemCode   
      inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
      outer apply (  
       select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= PRD.DocDate and ToRefDate >= PRD.DocDate   
      ) FPR   
       where PRD.DocID = @docID   
       group by   
       ITG.InvntryAcct   
       ,PRD.CardCode   
       ,CRD.CtlDebCredPayAcct   
       ,FPR.PeriodDetailID   
       ,PRD.DocDate   
       ,PRD.DocDueDate   
       ,PRD.TaxDate   
       ,PRD.DocID   
       ,PRD.DocNum   
       ,PRD1.ProjectCode   
       ,PRD1.OcrCode  
       ,PRD1.OcrCode2  
       ,PRD1.OcrCode3   
       ,PRD1.OcrCode4   
       ,PRD1.OcrCode5   
  
    ) A  
  
  update @jdt set SysTotal = (select sum(debit) from @jdtline)  
  insert into JournalEntry ( 
  DocNum 
  ,Series 
  ,ObjType
  ,BaseEntry
  ,BaseNum
  ,BaseType
  ,TargetEntry
  ,TargetNum
  ,FinancePeriod
  ,BatchNum 
  ,TargetType
  ,TransType
  ,RefDate
  ,RefDate2
  ,RefDate3
  ,Ref1
  ,Ref2
  ,Ref3
  ,Project
  ,Remarks
  ,SysTotal
  ,SysTotalLC
  ,SysTotalFC
  ,VersionNo
  ,IsActive
  ,CreatedByAppUserId
  ,CreatedOn
  ,ModifiedByAppUserId
  ,ModifiedOn 
  ) select * from @jdt  
  set @jdtID = @@IDENTITY  
  if @jdtID > 0  
  begin   
    update @jdtline set JournalEntryID = @jdtID   
    insert into JournalEntryDetail select * from @jdtline  
    update PurchaseReturn set TransID = @jdtID ,JournalRemarks = (select top 1 Remarks from @jdt) where DocID = @docID  
  end   
 end   
  
  /*Purchase Invoice*/
 if @docType = 'PIN'  
 begin   
  insert into @jdt  
   select   
    null DocNum  
   ,null Series  
   ,'JEN' ObjType  
   ,null BaseEntry  
   ,null BaseNum  
   ,null BaseType  
   ,null TargetEntry  
   ,null TargetNum  
   ,FPR.PeriodDetailID FinancePeriod  
   ,null BatchNum  
   ,null TargetType  
   ,'PIN' TransType  
   ,PIN.DocDate  RefDate   
   ,PIN.DocDueDate RefDate2   
   ,PIN.TaxDate RefDate3   
   ,PIN.DocID Ref1  
   ,null Ref2  
   ,null Ref3  
   ,PIN.Project Project  
   ,'Purchase Invoice - ' + PIN.CardCode    Remarks  
   ,null SysTotal  
   ,null SysTotalLC  
   ,null SysTotalFC  
   ,null VersionNo   
   ,1 IsActive   
   ,@createdBy CreatedByAppUserId   
   ,Getdate() CreatedOn   
   ,null ModifiedByAppUserId   
   ,null ModifiedOn   
    from   
    PurchaseInvoice PIN   
    outer apply (  
     select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= PIN.DocDate and ToRefDate >= PIN.DocDate   
    ) FPR   
  
  where PIN.DocID = @docID   
   
  insert into @jdtline  (
	JournalEntryID, ObjType ,BaseEntry ,BaseNum ,BaseType ,TargetEntry ,TargetNum   ,TargetType ,TransType ,GLAcctCode ,LineID ,VisID ,FinancePeriod ,BatchNum ,ShortName ,Debit ,Credit ,DebitSC ,CreditSC ,DebitFC ,CreditFC 
	,ContraAccount ,DebitCredit ,BaseAmt ,VatGroup ,VatPercent ,VatAmt ,RefDate ,RefDate2 ,RefDate3 ,CreatedBy ,BaseRef ,Ref1 ,Ref2 ,Ref3 ,Project ,OcrCode ,OcrCode2 ,OcrCode3 ,OcrCode4 ,OcrCode5 ,LineRemarks ,VersionNo ,IsActive ,CreatedByAppUserId ,CreatedOn ,ModifiedByAppUserId ,ModifiedOn   
  )
    select  @jdtID JournalEntryID  
     ,ObjType   
     ,BaseEntry   
     ,BaseNum   
     ,BaseType   
     ,TargetEntry   
     ,TargetNum   
     ,TargetType   
     ,TransType   
     ,GLAcctCode   
     ,ROW_NUMBER() over( order by GLAcctCode) LineID   
     ,VisID   
     ,FinancePeriod   
     ,BatchNum   
     ,ShortName   
     ,Debit   
     ,Credit   
     ,DebitSC   
     ,CreditSC   
     ,DebitFC   
     ,CreditFC   
     ,ContraAccount   
     ,DebitCredit   
     ,BaseAmt   
     ,VatGroup   
     ,VatPercent   
     ,VatAmt   
     ,RefDate   
     ,RefDate2   
     ,RefDate3   
     ,CreatedBy   
     ,BaseRef   
     ,Ref1   
     ,Ref2   
     ,Ref3   
     ,Project   
     ,OcrCode   
     ,OcrCode2   
     ,OcrCode3   
     ,OcrCode4   
     ,OcrCode5  
     ,LineRemarks  
     ,null VersionNo   
     ,1 IsActive   
     ,@createdBy CreatedByAppUserId   
     ,Getdate() CreatedOn   
     ,null ModifiedByAppUserId   
     ,null ModifiedOn   
    from (  
     select   --Debit Goods Clearing Acct  
     'JEN' ObjType   
     ,null BaseEntry   
     ,null BaseNum   
     ,null BaseType   
     ,null TargetEntry   
     ,null TargetNum   
     ,null TargetType   
     ,'PIN' TransType   
     ,ITG.GoodClearingAcct GLAcctCode   
     ,null LineID   
     ,null VisID   
     ,FPR.PeriodDetailID FinancePeriod   
     ,null BatchNum   
     ,ITG.GoodClearingAcct ShortName   
     ,Sum(PIN1.LineTotal)  Debit   
     ,0 Credit   
     ,null DebitSC   
     ,null CreditSC   
     ,null DebitFC   
     ,null CreditFC   
     ,null ContraAccount   
     ,null DebitCredit   
     ,null BaseAmt   
     ,null VatGroup   
     ,null VatPercent   
     ,null VatAmt   
     ,PIN.DocDate  RefDate   
     ,PIN.DocDueDate RefDate2   
     ,PIN.TaxDate RefDate3   
     ,PIN.DocID CreatedBy   
     ,PIN.DocNum BaseRef   
     ,PIN.DocID  Ref1   
     ,null Ref2   
     ,null Ref3   
     ,PIN1.ProjectCode Project   
     ,PIN1.OcrCode OcrCode    
     ,PIN1.OcrCode2 OcrCode2   
     ,PIN1.OcrCode3 OcrCode3   
     ,PIN1.OcrCode4 OcrCode4   
     ,PIN1.OcrCode5 OcrCode5   
     ,'Purchase Invoice - ' + PIN.CardCode   LineRemarks   
        from   
      PurchaseInvoiceDetail PIN1  
      inner join PurchaseInvoice PIN on PIN1.DocID = PIN.DocID  
      inner join ItemMaster ITM on PIN1.ItemCode = ITM.ItemCode   
      inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
      outer apply (  
       select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= PIN.DocDate and ToRefDate >= PIN.DocDate   
      ) FPR   
      where PIN.DocID = @docID   
      group by   
       ITG.GoodClearingAcct   
       ,PIN.CardCode   
       ,FPR.PeriodDetailID   
       ,PIN.DocDate   
       ,PIN.DocDueDate   
       ,PIN.TaxDate   
       ,PIN.DocID   
       ,PIN.DocNum   
       ,PIN1.ProjectCode   
       ,PIN1.OcrCode  
       ,PIN1.OcrCode2  
       ,PIN1.OcrCode3   
       ,PIN1.OcrCode4   
       ,PIN1.OcrCode5   
     union ALL  
  
  
     select   --Debit Input Tax  
     'JEN' ObjType   
     ,null BaseEntry   
     ,null BaseNum   
     ,null BaseType   
     ,null TargetEntry   
     ,null TargetNum   
     ,null TargetType   
     ,'PIN' TransType   
     ,TG.Account GLAcctCode   
     ,null LineID   
     ,null VisID   
     ,FPR.PeriodDetailID FinancePeriod   
     ,null BatchNum   
     ,TG.Account ShortName   
     ,Sum(PIN1.VatSum)  Debit   
     ,0 Credit   
     ,null DebitSC   
     ,null CreditSC   
     ,null DebitFC   
     ,null CreditFC   
     ,null ContraAccount   
     ,null DebitCredit   
     ,null BaseAmt   
     ,PIN1.VatGroup VatGroup   
     ,min(PIN1.VatPercent) VatPercent   
     ,min(PIN1.VatSum) VatAmt   
     ,PIN.DocDate  RefDate   
     ,PIN.DocDueDate RefDate2   
     ,PIN.TaxDate RefDate3   
     ,PIN.DocID CreatedBy   
     ,PIN.DocNum BaseRef   
     ,PIN.DocID  Ref1   
     ,null Ref2   
     ,null Ref3   
     ,PIN1.ProjectCode Project   
     ,PIN1.OcrCode OcrCode    
     ,PIN1.OcrCode2 OcrCode2   
     ,PIN1.OcrCode3 OcrCode3   
     ,PIN1.OcrCode4 OcrCode4   
     ,PIN1.OcrCode5 OcrCode5   
     ,'Purchase Invoice - ' + PIN.CardCode   LineRemarks   
        from   
      PurchaseInvoiceDetail PIN1  
      inner join PurchaseInvoice PIN on PIN1.DocID = PIN.DocID  
      inner join ItemMaster ITM on PIN1.ItemCode = ITM.ItemCode   
      inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
      outer apply (  
       select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= PIN.DocDate and ToRefDate >= PIN.DocDate   
      ) FPR   
  
      outer apply(  
        select Rate,VatCode,Account  From TaxGroup where VatCode = PIN1.VatGroup and Category = 'I'  
      ) TG  
      where PIN.DocID = @docID   
      group by   
       ITG.GoodClearingAcct   
       ,PIN.CardCode   
       ,FPR.PeriodDetailID   
       ,PIN.DocDate   
       ,PIN.DocDueDate   
       ,PIN.TaxDate   
       ,PIN.DocID   
       ,PIN.DocNum   
       ,PIN1.VatGroup   
       ,TG.Account  
       ,PIN1.ProjectCode   
       ,PIN1.OcrCode  
       ,PIN1.OcrCode2  
       ,PIN1.OcrCode3   
       ,PIN1.OcrCode4   
       ,PIN1.OcrCode5   
	 
	 union ALL  
  
     select  -- Credit Wtax Acct  
     'JEN' ObjType   
     ,null BaseEntry   
     ,null BaseNum   
     ,null BaseType   
     ,null TargetEntry   
     ,null TargetNum   
     ,null TargetType   
     ,'PIN' TransType   
     ,PINW.AcctCode GLAcctCode   
     ,null LineID   
     ,null VisID   
     ,FPR.PeriodDetailID FinancePeriod   
     ,null BatchNum   
     ,PINW.AcctCode ShortName   
     ,0 Debit   
     ,Sum(PINW.WTAmnt)  Credit   
     ,null DebitSC   
     ,null CreditSC   
     ,null DebitFC   
     ,null CreditFC   
     ,null ContraAccount   
     ,null DebitCredit   
     ,null BaseAmt   
     ,null VatGroup   
     ,null VatPercent   
     ,null VatAmt   
     ,PIN.DocDate  RefDate   
     ,PIN.DocDueDate RefDate2   
     ,PIN.TaxDate RefDate3   
     ,PIN.DocID CreatedBy   
     ,PIN.DocNum BaseRef   
     ,PIN.DocID  Ref1   
     ,null Ref2   
     ,null Ref3   
     ,'' Project   
     ,'' OcrCode    
     ,'' OcrCode2   
     ,'' OcrCode3   
     ,'' OcrCode4   
     ,'' OcrCode5   
     ,'Purchase Invoice - ' + PIN.CardCode   LineRemarks   
      from   
      PurchaseInvoiceWtax PINW  
      inner join PurchaseInvoice PIN on PINW.DocID = PIN.DocID  
      inner join BusinessPartner  CRD on PIN.CardCode  = CRD.CardCode  
      inner join GLAccount  ACT on PINW.AcctCode = ACT.AcctCode   
      outer apply (  
       select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= PIN.DocDate and ToRefDate >= PIN.DocDate   
      ) FPR   
       where PIN.DocID = @docID  and CRD.WTLiable = 'Y'
       group by   
        PIN.CardCode   
	   ,PINW.AcctCode
       ,CRD.CtlDebCredPayAcct   
       ,CRD.CardCode  
       ,FPR.PeriodDetailID   
       ,PIN.DocDate   
       ,PIN.DocDueDate   
       ,PIN.TaxDate   
       ,PIN.DocID   
       ,PIN.DocNum   

     union ALL  
  
     select  -- Credit Payable Acct  
     'JEN' ObjType   
     ,null BaseEntry   
     ,null BaseNum   
     ,null BaseType   
     ,null TargetEntry   
     ,null TargetNum   
     ,null TargetType   
     ,'PIN' TransType   
     ,CRD.CtlDebCredPayAcct GLAcctCode   
     ,null LineID   
     ,null VisID   
     ,FPR.PeriodDetailID FinancePeriod   
     ,null BatchNum   
     ,CRD.CardCode ShortName   
     ,0 Debit   
     ,Sum(PIN.DocTotal)  Credit   
     ,null DebitSC   
     ,null CreditSC   
     ,null DebitFC   
     ,null CreditFC   
     ,null ContraAccount   
     ,null DebitCredit   
     ,null BaseAmt   
     ,null VatGroup   
     ,null VatPercent   
     ,null VatAmt   
     ,PIN.DocDate  RefDate   
     ,PIN.DocDueDate RefDate2   
     ,PIN.TaxDate RefDate3   
     ,PIN.DocID CreatedBy   
     ,PIN.DocNum BaseRef   
     ,PIN.DocID  Ref1   
     ,null Ref2   
     ,null Ref3   
     ,'' Project   
     ,'' OcrCode    
     ,'' OcrCode2   
     ,'' OcrCode3   
     ,'' OcrCode4   
     ,'' OcrCode5   
     ,'Purchase Invoice - ' + PIN.CardCode   LineRemarks   
      from   
      PurchaseInvoice PIN 
      inner join BusinessPartner  CRD on PIN.CardCode  = CRD.CardCode  
      outer apply (  
       select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= PIN.DocDate and ToRefDate >= PIN.DocDate   
      ) FPR   
       where PIN.DocID = @docID   
       group by   
        PIN.CardCode   
       ,CRD.CtlDebCredPayAcct   
       ,CRD.CardCode  
       ,FPR.PeriodDetailID   
       ,PIN.DocDate   
       ,PIN.DocDueDate   
       ,PIN.TaxDate   
       ,PIN.DocID   
       ,PIN.DocNum   
    ) A  
  
  update @jdt set SysTotal = (select sum(debit) from @jdtline)  
  insert into JournalEntry ( 
  DocNum 
  ,Series 
  ,ObjType
  ,BaseEntry
  ,BaseNum
  ,BaseType
  ,TargetEntry
  ,TargetNum
  ,FinancePeriod
  ,BatchNum 
  ,TargetType
  ,TransType
  ,RefDate
  ,RefDate2
  ,RefDate3
  ,Ref1
  ,Ref2
  ,Ref3
  ,Project
  ,Remarks
  ,SysTotal
  ,SysTotalLC
  ,SysTotalFC
  ,VersionNo
  ,IsActive
  ,CreatedByAppUserId
  ,CreatedOn
  ,ModifiedByAppUserId
  ,ModifiedOn 
  ) select * from @jdt  
  set @jdtID = @@IDENTITY  
  
  if @jdtID > 0  
  begin   
    update @jdtline set JournalEntryID = @jdtID   
    insert into JournalEntryDetail select * from @jdtline  
  
    update PurchaseInvoice set TransID = @jdtID ,JournalRemarks = (select top 1 Remarks from @jdt) where DocID = @docID  
  end   
 end   
  
  /*Purchase DownPayment*/
 if @docType = 'PDP'  
 begin   
  select 1   
 end   
  
  /*Purchase CreditNote*/
 if @docType = 'PCR'  
 begin   
  insert into @jdt  
   select   
    null DocNum  
   ,null Series  
   ,'JEN' ObjType  
   ,null BaseEntry  
   ,null BaseNum  
   ,null BaseType  
   ,null TargetEntry  
   ,null TargetNum  
   ,FPR.PeriodDetailID FinancePeriod  
   ,null BatchNum  
   ,null TargetType  
   ,'PCR' TransType  
   ,PCR.DocDate  RefDate   
   ,PCR.DocDueDate RefDate2   
   ,PCR.TaxDate RefDate3   
   ,PCR.DocID Ref1  
   ,null Ref2  
   ,null Ref3  
   ,PCR.Project Project  
   ,'Purchase CreditNote - ' + PCR.CardCode    Remarks  
   ,PCR.DocTotal SysTotal  
   ,null SysTotalLC  
   ,null SysTotalFC  
   ,null VersionNo   
   ,1 IsActive   
   ,@createdBy CreatedByAppUserId   
   ,Getdate() CreatedOn   
   ,null ModifiedByAppUserId   
   ,null ModifiedOn   
    from   
    PurchaseCreditNote PCR   
    outer apply (  
     select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= PCR.DocDate and ToRefDate >= PCR.DocDate   
    ) FPR   
  
  where PCR.DocID = @docID   
   
  insert into @jdtline  (
	JournalEntryID, ObjType ,BaseEntry ,BaseNum ,BaseType ,TargetEntry ,TargetNum   ,TargetType ,TransType ,GLAcctCode ,LineID ,VisID ,FinancePeriod ,BatchNum ,ShortName ,Debit ,Credit ,DebitSC ,CreditSC ,DebitFC ,CreditFC 
	,ContraAccount ,DebitCredit ,BaseAmt ,VatGroup ,VatPercent ,VatAmt ,RefDate ,RefDate2 ,RefDate3 ,CreatedBy ,BaseRef ,Ref1 ,Ref2 ,Ref3 ,Project ,OcrCode ,OcrCode2 ,OcrCode3 ,OcrCode4 ,OcrCode5 ,LineRemarks ,VersionNo ,IsActive ,CreatedByAppUserId ,CreatedOn ,ModifiedByAppUserId ,ModifiedOn   
  )
    select  @jdtID JournalEntryID  
     ,ObjType   
     ,BaseEntry   
     ,BaseNum   
     ,BaseType   
     ,TargetEntry   
     ,TargetNum   
     ,TargetType   
     ,TransType   
     ,GLAcctCode   
     ,ROW_NUMBER() over( order by GLAcctCode) LineID   
     ,VisID   
     ,FinancePeriod   
     ,BatchNum   
     ,ShortName   
     ,Debit   
     ,Credit   
     ,DebitSC   
     ,CreditSC   
     ,DebitFC   
     ,CreditFC   
     ,ContraAccount   
     ,DebitCredit   
     ,BaseAmt   
     ,VatGroup   
     ,VatPercent   
     ,VatAmt   
     ,RefDate   
     ,RefDate2   
     ,RefDate3   
     ,CreatedBy   
     ,BaseRef   
     ,Ref1   
     ,Ref2   
     ,Ref3   
     ,Project   
     ,OcrCode   
     ,OcrCode2   
     ,OcrCode3   
     ,OcrCode4   
     ,OcrCode5  
     ,LineRemarks  
     ,null VersionNo   
     ,1 IsActive   
     ,@createdBy CreatedByAppUserId   
     ,Getdate() CreatedOn   
     ,null ModifiedByAppUserId   
     ,null ModifiedOn   
    from (  
     select   --Debit Goods Clearing Acct  
     'JEN' ObjType   
     ,null BaseEntry   
     ,null BaseNum   
     ,null BaseType   
     ,null TargetEntry   
     ,null TargetNum   
     ,null TargetType   
     ,'PCR' TransType   
     ,ITG.GoodClearingAcct GLAcctCode   
     ,null LineID   
     ,null VisID   
     ,FPR.PeriodDetailID FinancePeriod   
     ,null BatchNum   
     ,ITG.GoodClearingAcct ShortName   
     ,0  Debit   
     ,Sum(PCR1.LineTotal) Credit   
     ,null DebitSC   
     ,null CreditSC   
     ,null DebitFC   
     ,null CreditFC   
     ,null ContraAccount   
     ,null DebitCredit   
     ,null BaseAmt   
     ,null VatGroup   
     ,null VatPercent   
     ,null VatAmt   
     ,PCR.DocDate  RefDate   
     ,PCR.DocDueDate RefDate2   
     ,PCR.TaxDate RefDate3   
     ,PCR.DocID CreatedBy   
     ,PCR.DocNum BaseRef   
     ,PCR.DocID  Ref1   
     ,null Ref2   
     ,null Ref3   
     ,PCR1.ProjectCode Project   
     ,PCR1.OcrCode OcrCode    
     ,PCR1.OcrCode2 OcrCode2   
     ,PCR1.OcrCode3 OcrCode3   
     ,PCR1.OcrCode4 OcrCode4   
     ,PCR1.OcrCode5 OcrCode5   
     ,'Purchase CreditNote - ' + PCR.CardCode   LineRemarks   
        from   
      PurchaseCreditNoteDetail PCR1  
      inner join PurchaseCreditNote PCR on PCR1.DocID = PCR.DocID  
      inner join ItemMaster ITM on PCR1.ItemCode = ITM.ItemCode   
      inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
      outer apply (  
       select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= PCR.DocDate and ToRefDate >= PCR.DocDate   
      ) FPR   
      where PCR.DocID = @docID   
      group by   
       ITG.GoodClearingAcct   
       ,PCR.CardCode   
       ,FPR.PeriodDetailID   
       ,PCR.DocDate   
       ,PCR.DocDueDate   
       ,PCR.TaxDate   
       ,PCR.DocID   
       ,PCR.DocNum   
       ,PCR1.ProjectCode   
       ,PCR1.OcrCode  
       ,PCR1.OcrCode2  
       ,PCR1.OcrCode3   
       ,PCR1.OcrCode4   
       ,PCR1.OcrCode5   
     union ALL  
  
  
     select   --Debit Input Tax  
     'JEN' ObjType   
     ,null BaseEntry   
     ,null BaseNum   
     ,null BaseType   
     ,null TargetEntry   
     ,null TargetNum   
     ,null TargetType   
     ,'PCR' TransType   
     ,TG.Account GLAcctCode   
     ,null LineID   
     ,null VisID   
     ,FPR.PeriodDetailID FinancePeriod   
     ,null BatchNum   
     ,TG.Account ShortName   
     ,0  Debit   
     ,Sum(PCR1.VatSum) Credit   
     ,null DebitSC   
     ,null CreditSC   
     ,null DebitFC   
     ,null CreditFC   
     ,null ContraAccount   
     ,null DebitCredit   
     ,null BaseAmt   
     ,PCR1.VatGroup VatGroup   
     ,min(PCR1.VatPercent) VatPercent   
     ,min(PCR1.VatSum) VatAmt   
     ,PCR.DocDate  RefDate   
     ,PCR.DocDueDate RefDate2   
     ,PCR.TaxDate RefDate3   
     ,PCR.DocID CreatedBy   
     ,PCR.DocNum BaseRef   
     ,PCR.DocID  Ref1   
     ,null Ref2   
     ,null Ref3   
     ,null Project   
     ,null OcrCode   
     ,null OcrCode2   
     ,null OcrCode3   
     ,null OcrCode4   
     ,null OcrCode5   
     ,'Purchase CreditNote - ' + PCR.CardCode   LineRemarks   
        from   
      PurchaseCreditNoteDetail PCR1  
      inner join PurchaseCreditNote PCR on PCR1.DocID = PCR.DocID  
      inner join ItemMaster ITM on PCR1.ItemCode = ITM.ItemCode   
      inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
      outer apply (  
       select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= PCR.DocDate and ToRefDate >= PCR.DocDate   
      ) FPR   
  
      outer apply(  
        select Rate,VatCode,Account  From TaxGroup where VatCode = PCR1.VatGroup  and Category = 'I'  
      ) TG  
      where PCR.DocID = @docID   
      group by   
       ITG.GoodClearingAcct   
       ,PCR.CardCode   
       ,FPR.PeriodDetailID   
       ,PCR.DocDate   
       ,PCR.DocDueDate   
       ,PCR.TaxDate   
       ,PCR.DocID   
       ,PCR.DocNum   
       ,PCR1.VatGroup   
       ,TG.Account  
       ,PCR1.ProjectCode   
       ,PCR1.OcrCode  
       ,PCR1.OcrCode2  
       ,PCR1.OcrCode3   
       ,PCR1.OcrCode4   
       ,PCR1.OcrCode5  


	 union ALL  
  
     select  -- Credit Wtax Acct  
     'JEN' ObjType   
     ,null BaseEntry   
     ,null BaseNum   
     ,null BaseType   
     ,null TargetEntry   
     ,null TargetNum   
     ,null TargetType   
     ,'PIN' TransType   
     ,PCRW.AcctCode GLAcctCode   
     ,null LineID   
     ,null VisID   
     ,FPR.PeriodDetailID FinancePeriod   
     ,null BatchNum   
     ,PCRW.AcctCode ShortName   
     ,Sum(PCRW.WTAmnt)   Debit   
     ,0 Credit   
     ,null DebitSC   
     ,null CreditSC   
     ,null DebitFC   
     ,null CreditFC   
     ,null ContraAccount   
     ,null DebitCredit   
     ,null BaseAmt   
     ,null VatGroup   
     ,null VatPercent   
     ,null VatAmt   
     ,PCR.DocDate  RefDate   
     ,PCR.DocDueDate RefDate2   
     ,PCR.TaxDate RefDate3   
     ,PCR.DocID CreatedBy   
     ,PCR.DocNum BaseRef   
     ,PCR.DocID  Ref1   
     ,null Ref2   
     ,null Ref3   
     ,'' Project   
     ,'' OcrCode    
     ,'' OcrCode2   
     ,'' OcrCode3   
     ,'' OcrCode4   
     ,'' OcrCode5   
     ,'Purchase CreditNote - ' + PCR.CardCode   LineRemarks   
      from   
      PurchaseCreditNoteWtax PCRW  
      inner join PurchaseCreditNote PCR on PCRW.DocID = PCR.DocID  
      inner join BusinessPartner  CRD on PCR.CardCode  = CRD.CardCode  
      inner join GLAccount  ACT on PCRW.AcctCode = ACT.AcctCode   
      outer apply (  
       select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= PCR.DocDate and ToRefDate >= PCR.DocDate   
      ) FPR   
       where PCR.DocID = @docID  and CRD.WTLiable = 'Y'
       group by   
        PCR.CardCode   
	   ,PCRW.AcctCode
       ,CRD.CtlDebCredPayAcct   
       ,CRD.CardCode  
       ,FPR.PeriodDetailID   
       ,PCR.DocDate   
       ,PCR.DocDueDate   
       ,PCR.TaxDate   
       ,PCR.DocID   
       ,PCR.DocNum   
	   


     union ALL  
  
     select  -- Credit Payable Acct  
     'JEN' ObjType   
     ,null BaseEntry   
     ,null BaseNum   
     ,null BaseType   
     ,null TargetEntry   
     ,null TargetNum   
     ,null TargetType   
     ,'PCR' TransType   
     ,CRD.CtlDebCredPayAcct GLAcctCode   
     ,null LineID   
     ,null VisID   
     ,FPR.PeriodDetailID FinancePeriod   
     ,null BatchNum   
     ,CRD.CardCode ShortName   
     ,Sum(PCR1.GTotal) Debit   
     ,0  Credit   
     ,null DebitSC   
     ,null CreditSC   
     ,null DebitFC   
     ,null CreditFC   
     ,null ContraAccount   
     ,null DebitCredit   
     ,null BaseAmt   
     ,null VatGroup   
     ,null VatPercent   
     ,null VatAmt   
     ,PCR.DocDate  RefDate   
     ,PCR.DocDueDate RefDate2   
     ,PCR.TaxDate RefDate3   
     ,PCR.DocID CreatedBy   
     ,PCR.DocNum BaseRef   
     ,PCR.DocID  Ref1   
     ,null Ref2   
     ,null Ref3   
     ,PCR1.ProjectCode Project   
     ,PCR1.OcrCode OcrCode    
     ,PCR1.OcrCode2 OcrCode2   
     ,PCR1.OcrCode3 OcrCode3   
     ,PCR1.OcrCode4 OcrCode4   
     ,PCR1.OcrCode5 OcrCode5   
     ,'Purchase CreditNote - ' + PCR.CardCode   LineRemarks   
      from   
      PurchaseCreditNoteDetail PCR1  
      inner join PurchaseCreditNote PCR on PCR1.DocID = PCR.DocID  
      inner join BusinessPartner  CRD on PCR.CardCode  = CRD.CardCode  
      inner join ItemMaster ITM on PCR1.ItemCode = ITM.ItemCode   
      inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
      outer apply (  
       select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= PCR.DocDate and ToRefDate >= PCR.DocDate   
      ) FPR   
       where PCR.DocID = @docID   
       group by   
       ITG.InvntryAcct   
       ,PCR.CardCode   
       ,CRD.CtlDebCredPayAcct   
       ,CRD.CardCode  
       ,FPR.PeriodDetailID   
       ,PCR.DocDate   
       ,PCR.DocDueDate   
       ,PCR.TaxDate   
       ,PCR.DocID   
       ,PCR.DocNum   
       ,PCR1.ProjectCode   
       ,PCR1.OcrCode  
       ,PCR1.OcrCode2  
       ,PCR1.OcrCode3   
       ,PCR1.OcrCode4   
       ,PCR1.OcrCode5   
  
    ) A  
  
  update @jdt set SysTotal = (select sum(debit) from @jdtline)  
  insert into JournalEntry ( 
  DocNum 
  ,Series 
  ,ObjType
  ,BaseEntry
  ,BaseNum
  ,BaseType
  ,TargetEntry
  ,TargetNum
  ,FinancePeriod
  ,BatchNum 
  ,TargetType
  ,TransType
  ,RefDate
  ,RefDate2
  ,RefDate3
  ,Ref1
  ,Ref2
  ,Ref3
  ,Project
  ,Remarks
  ,SysTotal
  ,SysTotalLC
  ,SysTotalFC
  ,VersionNo
  ,IsActive
  ,CreatedByAppUserId
  ,CreatedOn
  ,ModifiedByAppUserId
  ,ModifiedOn 
  ) select * from @jdt  
  set @jdtID = @@IDENTITY  
  
  if @jdtID > 0  
  begin   
    update @jdtline set JournalEntryID = @jdtID   
    insert into JournalEntryDetail select * from @jdtline  
  
    update PurchaseCreditNote set TransID = @jdtID ,JournalRemarks = (select top 1 Remarks from @jdt) where DocID = @docID  
  end   
 end   

 /*Purchase Payment*/
 if @docType = 'PPY'  
	begin
		insert into @jdt  
			   select   
				null DocNum  
			   ,null Series  
			   ,'JEN' ObjType  
			   ,null BaseEntry  
			   ,null BaseNum  
			   ,null BaseType  
			   ,null TargetEntry  
			   ,null TargetNum  
			   ,FPR.PeriodDetailID FinancePeriod  
			   ,null BatchNum  
			   ,null TargetType  
			   ,'PPY' TransType  
			   ,PPY.DocDate  RefDate   
			   ,PPY.DocDueDate RefDate2   
			   ,PPY.TaxDate RefDate3   
			   ,PPY.PaymentID Ref1  
			   ,null Ref2  
			   ,null Ref3  
			   ,null Project  
			   ,'Vendor Payment- ' + PPY.CardCode    Remarks  
			   ,null SysTotal  
			   ,null SysTotalLC  
			   ,null SysTotalFC  
			   ,null VersionNo   
			   ,1 IsActive   
			   ,@createdBy CreatedByAppUserId   
			   ,Getdate() CreatedOn   
			   ,null ModifiedByAppUserId   
			   ,null ModifiedOn   
				from   
				OutgoingPayment PPY   
				outer apply (  
				 select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= PPY.DocDate and ToRefDate >= PPY.DocDate   
				) FPR   
  
			  where PPY.PaymentID = @docID   
		  
		insert into @jdtline  (
	JournalEntryID, ObjType ,BaseEntry ,BaseNum ,BaseType ,TargetEntry ,TargetNum   ,TargetType ,TransType ,GLAcctCode ,LineID ,VisID ,FinancePeriod ,BatchNum ,ShortName ,Debit ,Credit ,DebitSC ,CreditSC ,DebitFC ,CreditFC 
	,ContraAccount ,DebitCredit ,BaseAmt ,VatGroup ,VatPercent ,VatAmt ,RefDate ,RefDate2 ,RefDate3 ,CreatedBy ,BaseRef ,Ref1 ,Ref2 ,Ref3 ,Project ,OcrCode ,OcrCode2 ,OcrCode3 ,OcrCode4 ,OcrCode5 ,LineRemarks ,VersionNo ,IsActive ,CreatedByAppUserId ,CreatedOn ,ModifiedByAppUserId ,ModifiedOn   
  )
			select  @jdtID JournalEntryID  
			 ,ObjType   
			 ,BaseEntry   
			 ,BaseNum   
			 ,BaseType   
			 ,TargetEntry   
			 ,TargetNum   
			 ,TargetType   
			 ,TransType   
			 ,GLAcctCode   
			 ,ROW_NUMBER() over( order by GLAcctCode) LineID   
			 ,VisID   
			 ,FinancePeriod   
			 ,BatchNum   
			 ,ShortName   
			 ,Debit   
			 ,Credit   
			 ,DebitSC   
			 ,CreditSC   
			 ,DebitFC   
			 ,CreditFC   
			 ,ContraAccount   
			 ,DebitCredit   
			 ,BaseAmt   
			 ,VatGroup   
			 ,VatPercent   
			 ,VatAmt   
			 ,RefDate   
			 ,RefDate2   
			 ,RefDate3   
			 ,CreatedBy   
			 ,BaseRef   
			 ,Ref1   
			 ,Ref2   
			 ,Ref3   
			 ,Project   
			 ,OcrCode   
			 ,OcrCode2   
			 ,OcrCode3   
			 ,OcrCode4   
			 ,OcrCode5  
			 ,LineRemarks  
			 ,null VersionNo   
			 ,1 IsActive   
			 ,@createdBy CreatedByAppUserId   
			 ,Getdate() CreatedOn   
			 ,null ModifiedByAppUserId   
			 ,null ModifiedOn   
			from (  
			 select   --Credit Cash/Bank Acct   
				 'JEN' ObjType   
				 ,null BaseEntry   
				 ,null BaseNum   
				 ,null BaseType   
				 ,null TargetEntry   
				 ,null TargetNum   
				 ,null TargetType   
				 ,'PPY' TransType   
				 ,PPY.CashAcct GLAcctCode   
				 ,null LineID   
				 ,null VisID   
				 ,FPR.PeriodDetailID FinancePeriod   
				 ,null BatchNum   
				 ,PPY.CashAcct ShortName   
				 ,0  Debit   
				 ,Sum(PPY.CashSum) Credit   
				 ,null DebitSC   
				 ,null CreditSC   
				 ,null DebitFC   
				 ,null CreditFC   
				 ,null ContraAccount   
				 ,null DebitCredit   
				 ,null BaseAmt   
				 ,null VatGroup   
				 ,null VatPercent   
				 ,null VatAmt   
				 ,PPY.DocDate  RefDate   
				 ,PPY.DocDueDate RefDate2   
				 ,PPY.TaxDate RefDate3   
				 ,PPY.PaymentID CreatedBy   
				 ,PPY.DocNum BaseRef   
				 ,PPY.PaymentID  Ref1   
				 ,null Ref2   
				 ,null Ref3   
				 ,null Project   
				 ,null OcrCode    
				 ,null OcrCode2   
				 ,null OcrCode3   
				 ,null OcrCode4   
				 ,null OcrCode5   
				 ,'VENDOR PAYMENT - ' + PPY.CardCode   LineRemarks   
			 from   
				  OutgoingPayment PPY  
				  outer apply (  
				   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= PPY.DocDate and ToRefDate >= PPY.DocDate   
				  ) FPR   
				  where PPY.PaymentID = @docID   and Isnull(PPY.CashSum, 0) > 0

			  group by 
				  PPY.PaymentID
				  ,PPY.DocNum 
				  ,PPY.CardCode 
				  ,PPY.CashAcct
				  ,FPR.PeriodDetailID
				  ,PPY.DocDate
				  ,PPY.DocDueDate
				  ,PPY.TaxDate

				  union all 
				    select   --Credit Chk
					 'JEN' ObjType   
					 ,null BaseEntry   
					 ,null BaseNum   
					 ,null BaseType   
					 ,null TargetEntry   
					 ,null TargetNum   
					 ,null TargetType   
					 ,'PPY' TransType   
					 ,CHK.CheckAcct  GLAcctCode   
					 ,null LineID   
					 ,null VisID   
					 ,FPR.PeriodDetailID FinancePeriod   
					 ,null BatchNum   
					 ,CHK.CheckAcct  ShortName   
					 ,0  Debit   
					 ,Sum(CHK.CheckSum) Credit   
					 ,null DebitSC   
					 ,null CreditSC   
					 ,null DebitFC   
					 ,null CreditFC   
					 ,null ContraAccount   
					 ,null DebitCredit   
					 ,null BaseAmt   
					 ,null VatGroup   
					 ,null VatPercent   
					 ,null VatAmt   
					 ,PPY.DocDate  RefDate   
					 ,PPY.DocDueDate RefDate2   
					 ,PPY.TaxDate RefDate3   
					 ,PPY.PaymentID CreatedBy   
					 ,PPY.DocNum BaseRef   
					 ,PPY.PaymentID  Ref1   
					 ,null Ref2   
					 ,null Ref3   
					 ,null Project   
					 ,null OcrCode    
					 ,null OcrCode2   
					 ,null OcrCode3   
					 ,null OcrCode4   
					 ,null OcrCode5   
					 ,'VENDOR PAYMENT - ' + PPY.CardCode   LineRemarks   
				 from   
				  OutgoingPayment PPY  
					  outer apply (  
					   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= PPY.DocDate and ToRefDate >= PPY.DocDate   
					  ) FPR   

					  cross apply(
						select * from OutgoingPaymentCheque   where PaymentID  = PPY.PaymentID 
					  ) CHK
				  where 
					   PPY.PaymentID = @docID   and Isnull(PPY.CreditSum, 0) > 0
				  group by 
					   PPY.PaymentID
					  ,PPY.DocNum 
					  ,PPY.CardCode 
					  ,CHK.CheckAcct 
					  ,FPR.PeriodDetailID
					  ,PPY.DocDate
					  ,PPY.DocDueDate
					  ,PPY.TaxDate

			
				  union all 
				  select   --Credit Bank Transfer
				 'JEN' ObjType   
				 ,null BaseEntry   
				 ,null BaseNum   
				 ,null BaseType   
				 ,null TargetEntry   
				 ,null TargetNum   
				 ,null TargetType   
				 ,'PPY' TransType   
				 ,PPY.TrsfrAcct GLAcctCode   
				 ,null LineID   
				 ,null VisID   
				 ,FPR.PeriodDetailID FinancePeriod   
				 ,null BatchNum   
				 ,PPY.TrsfrAcct ShortName   
				 ,0  Debit   
				 ,Sum(PPY.TrsfrSum) Credit   
				 ,null DebitSC   
				 ,null CreditSC   
				 ,null DebitFC   
				 ,null CreditFC   
				 ,null ContraAccount   
				 ,null DebitCredit   
				 ,null BaseAmt   
				 ,null VatGroup   
				 ,null VatPercent   
				 ,null VatAmt   
				 ,PPY.DocDate  RefDate   
				 ,PPY.DocDueDate RefDate2   
				 ,PPY.TaxDate RefDate3   
				 ,PPY.PaymentID CreatedBy   
				 ,PPY.DocNum BaseRef   
				 ,PPY.PaymentID  Ref1   
				 ,null Ref2   
				 ,null Ref3   
				 ,null Project   
				 ,null OcrCode    
				 ,null OcrCode2   
				 ,null OcrCode3   
				 ,null OcrCode4   
				 ,null OcrCode5   
				 ,'VENDOR PAYMENT - ' + PPY.CardCode   LineRemarks   
				 from   
				  OutgoingPayment PPY  
				  outer apply (  
				   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= PPY.DocDate and ToRefDate >= PPY.DocDate   
				  ) FPR   
				  where PPY.PaymentID = @docID   and Isnull(PPY.TrsfrSum, 0) > 0

				  group by 
				  PPY.PaymentID
				  ,PPY.DocNum 
				  ,PPY.CardCode 
				  ,PPY.TrsfrAcct 
				  ,FPR.PeriodDetailID
				  ,PPY.DocDate
				  ,PPY.DocDueDate
				  ,PPY.TaxDate

				  union all 
				    select   --Credit Credit Card
					 'JEN' ObjType   
					 ,null BaseEntry   
					 ,null BaseNum   
					 ,null BaseType   
					 ,null TargetEntry   
					 ,null TargetNum   
					 ,null TargetType   
					 ,'PPY' TransType   
					 ,CC.CreditAcct GLAcctCode   
					 ,null LineID   
					 ,null VisID   
					 ,FPR.PeriodDetailID FinancePeriod   
					 ,null BatchNum   
					 ,CC.CreditAcct ShortName   
					 ,0  Debit   
					 ,Sum(CC.CreditSum) Credit   
					 ,null DebitSC   
					 ,null CreditSC   
					 ,null DebitFC   
					 ,null CreditFC   
					 ,null ContraAccount   
					 ,null DebitCredit   
					 ,null BaseAmt   
					 ,null VatGroup   
					 ,null VatPercent   
					 ,null VatAmt   
					 ,PPY.DocDate  RefDate   
					 ,PPY.DocDueDate RefDate2   
					 ,PPY.TaxDate RefDate3   
					 ,PPY.PaymentID CreatedBy   
					 ,PPY.DocNum BaseRef   
					 ,PPY.PaymentID  Ref1   
					 ,null Ref2   
					 ,null Ref3   
					 ,null Project   
					 ,null OcrCode    
					 ,null OcrCode2   
					 ,null OcrCode3   
					 ,null OcrCode4   
					 ,null OcrCode5   
					 ,'VENDOR PAYMENT - ' + PPY.CardCode   LineRemarks   
				 from   
				  OutgoingPayment PPY  
					  outer apply (  
					   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= PPY.DocDate and ToRefDate >= PPY.DocDate   
					  ) FPR   

					  cross apply(
						select * from OutgoingPaymentCreditCard  where PaymentID  = PPY.PaymentID 
					  ) CC
				  where 
					   PPY.PaymentID = @docID   and Isnull(PPY.CreditSum, 0) > 0
				  group by 
					   PPY.PaymentID
					  ,PPY.DocNum 
					  ,PPY.CardCode 
					  ,CC.CreditAcct 
					  ,FPR.PeriodDetailID
					  ,PPY.DocDate
					  ,PPY.DocDueDate
					  ,PPY.TaxDate


					  union all
				select  -- Debit Payable Acct  
				 'JEN' ObjType   
				 ,null BaseEntry   
				 ,null BaseNum   
				 ,null BaseType   
				 ,null TargetEntry   
				 ,null TargetNum   
				 ,null TargetType   
				 ,'PPY' TransType   
				 ,CRD.CtlDebCredPayAcct GLAcctCode   
				 ,null LineID   
				 ,null VisID   
				 ,FPR.PeriodDetailID FinancePeriod   
				 ,null BatchNum   
				 ,CRD.CardCode ShortName   
				 ,Sum(PPY.DocTotal) Debit   
				 ,0  Credit   
				 ,null DebitSC   
				 ,null CreditSC   
				 ,null DebitFC   
				 ,null CreditFC   
				 ,null ContraAccount   
				 ,null DebitCredit   
				 ,null BaseAmt   
				 ,null VatGroup   
				 ,null VatPercent   
				 ,null VatAmt   
				 ,PPY.DocDate  RefDate   
				 ,PPY.DocDueDate RefDate2   
				 ,PPY.TaxDate RefDate3   
				 ,PPY.PaymentID CreatedBy   
				 ,PPY.DocNum BaseRef   
				 ,PPY.PaymentID  Ref1   
				 ,null Ref2   
				 ,null Ref3   
				 ,null Project   
				 ,null OcrCode    
				 ,null OcrCode2   
				 ,null OcrCode3   
				 ,null OcrCode4   
				 ,null OcrCode5   
				 ,'VENDOR PAYMENT - ' + PPY.CardCode   LineRemarks   
				  from   
				  OutgoingPayment PPY 
				  inner join BusinessPartner  CRD on PPY.CardCode  = CRD.CardCode  
				  outer apply (  
				   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= PPY.DocDate and ToRefDate >= PPY.DocDate   
				  ) FPR   
				   where PPY.PaymentID = @docID   and Isnull(PPY.DocTotal, 0) > 0
				   group by   
				   PPY.CardCode   
				   ,CRD.CtlDebCredPayAcct   
				   ,CRD.CardCode  
				   ,FPR.PeriodDetailID   
				   ,PPY.DocDate   
				   ,PPY.DocDueDate   
				   ,PPY.TaxDate   
				   ,PPY.PaymentID
				   ,PPY.DocNum   
				  				    
			) A  

		update @jdt set SysTotal = (select sum(debit) from @jdtline)  
		insert into JournalEntry ( 
			DocNum 
			,Series 
			,ObjType
			,BaseEntry
			,BaseNum
			,BaseType
			,TargetEntry
			,TargetNum
			,FinancePeriod
			,BatchNum 
			,TargetType
			,TransType
			,RefDate
			,RefDate2
			,RefDate3
			,Ref1
			,Ref2
			,Ref3
			,Project
			,Remarks
			,SysTotal
			,SysTotalLC
			,SysTotalFC
			,VersionNo
			,IsActive
			,CreatedByAppUserId
			,CreatedOn
			,ModifiedByAppUserId
			,ModifiedOn 
			) select * from @jdt  
		set @jdtID = @@IDENTITY  
  
		if @jdtID > 0  
			begin   
			
				update @jdtline set JournalEntryID = @jdtID   
			
				insert into JournalEntryDetail select * from @jdtline  
  
				update OutgoingPayment set TransID = @jdtID ,JournalRemarks = (select top 1 Remarks from @jdt)  where PaymentID = @docID  
			
			end   
	end 

 /*Sales Payment*/
 if @docType = 'SPY'  
	begin
			insert into @jdt  
			   select   
				null DocNum  
			   ,null Series  
			   ,'JEN' ObjType  
			   ,null BaseEntry  
			   ,null BaseNum  
			   ,null BaseType  
			   ,null TargetEntry  
			   ,null TargetNum  
			   ,FPR.PeriodDetailID FinancePeriod  
			   ,null BatchNum  
			   ,null TargetType  
			   ,'SPY' TransType  
			   ,SPY.DocDate  RefDate   
			   ,SPY.DocDueDate RefDate2   
			   ,SPY.TaxDate RefDate3   
			   ,SPY.PaymentID Ref1  
			   ,null Ref2  
			   ,null Ref3  
			   ,null Project  
			   ,'Customer Payment- ' + SPY.CardCode    Remarks  
			   ,SPY.DocTotal SysTotal  
			   ,null SysTotalLC  
			   ,null SysTotalFC  
			   ,null VersionNo   
			   ,1 IsActive   
			   ,@createdBy CreatedByAppUserId   
			   ,Getdate() CreatedOn   
			   ,null ModifiedByAppUserId   
			   ,null ModifiedOn   
				from   
				IncomingPayment SPY   
				outer apply (  
				 select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= SPY.DocDate and ToRefDate >= SPY.DocDate   
				) FPR   
  
			  where SPY.PaymentID = @docID   
		  
			insert into @jdtline  (
				JournalEntryID, ObjType ,BaseEntry ,BaseNum ,BaseType ,TargetEntry ,TargetNum   ,TargetType ,TransType ,GLAcctCode ,LineID ,VisID ,FinancePeriod ,BatchNum ,ShortName ,Debit ,Credit ,DebitSC ,CreditSC ,DebitFC ,CreditFC 
				,ContraAccount ,DebitCredit ,BaseAmt ,VatGroup ,VatPercent ,VatAmt ,RefDate ,RefDate2 ,RefDate3 ,CreatedBy ,BaseRef ,Ref1 ,Ref2 ,Ref3 ,Project ,OcrCode ,OcrCode2 ,OcrCode3 ,OcrCode4 ,OcrCode5 ,LineRemarks ,VersionNo ,IsActive ,CreatedByAppUserId ,CreatedOn ,ModifiedByAppUserId ,ModifiedOn   
			)
				select  @jdtID JournalEntryID  
				 ,ObjType   
				 ,BaseEntry   
				 ,BaseNum   
				 ,BaseType   
				 ,TargetEntry   
				 ,TargetNum   
				 ,TargetType   
				 ,TransType   
				 ,GLAcctCode   
				 ,ROW_NUMBER() over( order by GLAcctCode) LineID   
				 ,VisID   
				 ,FinancePeriod   
				 ,BatchNum   
				 ,ShortName   
				 ,Debit   
				 ,Credit   
				 ,DebitSC   
				 ,CreditSC   
				 ,DebitFC   
				 ,CreditFC   
				 ,ContraAccount   
				 ,DebitCredit   
				 ,BaseAmt   
				 ,VatGroup   
				 ,VatPercent   
				 ,VatAmt   
				 ,RefDate   
				 ,RefDate2   
				 ,RefDate3   
				 ,CreatedBy   
				 ,BaseRef   
				 ,Ref1   
				 ,Ref2   
				 ,Ref3   
				 ,Project   
				 ,OcrCode   
				 ,OcrCode2   
				 ,OcrCode3   
				 ,OcrCode4   
				 ,OcrCode5  
				 ,LineRemarks  
				 ,null VersionNo   
				 ,1 IsActive   
				 ,@createdBy CreatedByAppUserId   
				 ,Getdate() CreatedOn   
				 ,null ModifiedByAppUserId   
				 ,null ModifiedOn   
				from (  
				 select   --Credit Cash/Bank Acct   
					 'JEN' ObjType   
					 ,null BaseEntry   
					 ,null BaseNum   
					 ,null BaseType   
					 ,null TargetEntry   
					 ,null TargetNum   
					 ,null TargetType   
					 ,'SPY' TransType   
					 ,SPY.CashAcct GLAcctCode   
					 ,null LineID   
					 ,null VisID   
					 ,FPR.PeriodDetailID FinancePeriod   
					 ,null BatchNum   
					 ,SPY.CashAcct ShortName   
					 ,Sum(SPY.CashSum)  Debit   
					 ,0 Credit   
					 ,null DebitSC   
					 ,null CreditSC   
					 ,null DebitFC   
					 ,null CreditFC   
					 ,null ContraAccount   
					 ,null DebitCredit   
					 ,null BaseAmt   
					 ,null VatGroup   
					 ,null VatPercent   
					 ,null VatAmt   
					 ,SPY.DocDate  RefDate   
					 ,SPY.DocDueDate RefDate2   
					 ,SPY.TaxDate RefDate3   
					 ,SPY.PaymentID CreatedBy   
					 ,SPY.DocNum BaseRef   
					 ,SPY.PaymentID  Ref1   
					 ,null Ref2   
					 ,null Ref3   
					 ,null Project   
					 ,null OcrCode    
					 ,null OcrCode2   
					 ,null OcrCode3   
					 ,null OcrCode4   
					 ,null OcrCode5   
					 ,'CUSTOMER PAYMENT - ' + SPY.CardCode   LineRemarks   
				 from   
					  IncomingPayment SPY  
					  outer apply (  
					   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= SPY.DocDate and ToRefDate >= SPY.DocDate   
					  ) FPR   
					  where SPY.PaymentID = @docID   and Isnull(SPY.CashSum, 0) > 0

				  group by 
					  SPY.PaymentID
					  ,SPY.DocNum 
					  ,SPY.CardCode 
					  ,SPY.CashAcct
					  ,FPR.PeriodDetailID
					  ,SPY.DocDate
					  ,SPY.DocDueDate
					  ,SPY.TaxDate


					  union all 
					  select   --Debit Chk Acct 
					 'JEN' ObjType   
					 ,null BaseEntry   
					 ,null BaseNum   
					 ,null BaseType   
					 ,null TargetEntry   
					 ,null TargetNum   
					 ,null TargetType   
					 ,'SPY' TransType   
					 ,CHK.CheckAcct GLAcctCode   
					 ,null LineID   
					 ,null VisID   
					 ,FPR.PeriodDetailID FinancePeriod   
					 ,null BatchNum   
					 ,CHK.CheckAcct ShortName   
					 ,Sum(CHK.CheckSum)  Debit   
					 ,0 Credit   
					 ,null DebitSC   
					 ,null CreditSC   
					 ,null DebitFC   
					 ,null CreditFC   
					 ,null ContraAccount   
					 ,null DebitCredit   
					 ,null BaseAmt   
					 ,null VatGroup   
					 ,null VatPercent   
					 ,null VatAmt   
					 ,SPY.DocDate  RefDate   
					 ,SPY.DocDueDate RefDate2   
					 ,SPY.TaxDate RefDate3   
					 ,SPY.PaymentID CreatedBy   
					 ,SPY.DocNum BaseRef   
					 ,SPY.PaymentID  Ref1   
					 ,null Ref2   
					 ,null Ref3   
					 ,null Project   
					 ,null OcrCode    
					 ,null OcrCode2   
					 ,null OcrCode3   
					 ,null OcrCode4   
					 ,null OcrCode5   
					 ,'CUSTOMER PAYMENT - ' + SPY.CardCode   LineRemarks   
				 from   
					  IncomingPayment SPY  
					  outer apply (  
					   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= SPY.DocDate and ToRefDate >= SPY.DocDate   
					  ) FPR   

					  outer apply(

					  select C.* from IncomingPaymentCheque C where PaymentID = SPY.PaymentID 
					  
					  ) CHK
					  where SPY.PaymentID = @docID   and Isnull(SPY.CheckSum, 0) > 0

				  group by 
					  SPY.PaymentID
					  ,SPY.DocNum 
					  ,SPY.CardCode 
					  ,SPY.CashAcct
					  ,FPR.PeriodDetailID
					  ,SPY.DocDate
					  ,SPY.DocDueDate
					  ,SPY.TaxDate
					  ,CHK.CheckAcct 

			
					  union all 
					  select   --Credit Bank Transfer
					 'JEN' ObjType   
					 ,null BaseEntry   
					 ,null BaseNum   
					 ,null BaseType   
					 ,null TargetEntry   
					 ,null TargetNum   
					 ,null TargetType   
					 ,'SPY' TransType   
					 ,SPY.TrsfrAcct GLAcctCode   
					 ,null LineID   
					 ,null VisID   
					 ,FPR.PeriodDetailID FinancePeriod   
					 ,null BatchNum   
					 ,SPY.TrsfrAcct ShortName   
					 ,Sum(SPY.TrsfrSum)  Debit   
					 ,0 Credit   
					 ,null DebitSC   
					 ,null CreditSC   
					 ,null DebitFC   
					 ,null CreditFC   
					 ,null ContraAccount   
					 ,null DebitCredit   
					 ,null BaseAmt   
					 ,null VatGroup   
					 ,null VatPercent   
					 ,null VatAmt   
					 ,SPY.DocDate  RefDate   
					 ,SPY.DocDueDate RefDate2   
					 ,SPY.TaxDate RefDate3   
					 ,SPY.PaymentID CreatedBy   
					 ,SPY.DocNum BaseRef   
					 ,SPY.PaymentID  Ref1   
					 ,null Ref2   
					 ,null Ref3   
					 ,null Project   
					 ,null OcrCode    
					 ,null OcrCode2   
					 ,null OcrCode3   
					 ,null OcrCode4   
					 ,null OcrCode5   
					 ,'CUSTOMER PAYMENT - ' + SPY.CardCode   LineRemarks   
					 from   
					  IncomingPayment SPY  
					  outer apply (  
					   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= SPY  .DocDate and ToRefDate >= SPY  .DocDate   
					  ) FPR   
					  where SPY.PaymentID = @docID   and Isnull(SPY.TrsfrSum, 0) > 0

					  group by 
					  SPY.PaymentID
					  ,SPY.DocNum 
					  ,SPY.CardCode 
					  ,SPY.TrsfrAcct 
					  ,FPR.PeriodDetailID
					  ,SPY.DocDate
					  ,SPY.DocDueDate
					  ,SPY.TaxDate

					  union all 
						select   --Credit Credit Card
						 'JEN' ObjType   
						 ,null BaseEntry   
						 ,null BaseNum   
						 ,null BaseType   
						 ,null TargetEntry   
						 ,null TargetNum   
						 ,null TargetType   
						 ,'SPY' TransType   
						 ,CC.CreditAcct GLAcctCode   
						 ,null LineID   
						 ,null VisID   
						 ,FPR.PeriodDetailID FinancePeriod   
						 ,null BatchNum   
						 ,CC.CreditAcct ShortName   
						 ,Sum(CC.CreditSum)  Debit   
						 ,0 Credit   
						 ,null DebitSC   
						 ,null CreditSC   
						 ,null DebitFC   
						 ,null CreditFC   
						 ,null ContraAccount   
						 ,null DebitCredit   
						 ,null BaseAmt   
						 ,null VatGroup   
						 ,null VatPercent   
						 ,null VatAmt   
						 ,SPY.DocDate  RefDate   
						 ,SPY.DocDueDate RefDate2   
						 ,SPY.TaxDate RefDate3   
						 ,SPY.PaymentID CreatedBy   
						 ,SPY.DocNum BaseRef   
						 ,SPY.PaymentID  Ref1   
						 ,null Ref2   
						 ,null Ref3   
						 ,null Project   
						 ,null OcrCode    
						 ,null OcrCode2   
						 ,null OcrCode3   
						 ,null OcrCode4   
						 ,null OcrCode5   
						 ,'CUSTOMER PAYMENT - ' + SPY.CardCode   LineRemarks   
					 from   
					  IncomingPayment SPY  
						  outer apply (  
						   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= SPY.DocDate and ToRefDate >= SPY.DocDate   
						  ) FPR   

						  cross apply(
							select * from IncomingPaymentCreditCard  where PaymentID  = SPY.PaymentID 
						  ) CC
					  where 
						   SPY.PaymentID = @docID   and Isnull(SPY.CreditSum, 0) > 0
					  group by 
						   SPY.PaymentID
						  ,SPY.DocNum 
						  ,SPY.CardCode 
						  ,CC.CreditAcct 
						  ,FPR.PeriodDetailID
						  ,SPY.DocDate
						  ,SPY.DocDueDate
						  ,SPY.TaxDate


						  union all
					select  -- Credit Recievable Acct  
					 'JEN' ObjType   
					 ,null BaseEntry   
					 ,null BaseNum   
					 ,null BaseType   
					 ,null TargetEntry   
					 ,null TargetNum   
					 ,null TargetType   
					 ,'SPY' TransType   
					 ,CRD.CtlDebCredPayAcct GLAcctCode   
					 ,null LineID   
					 ,null VisID   
					 ,FPR.PeriodDetailID FinancePeriod   
					 ,null BatchNum   
					 ,CRD.CardCode ShortName   
					 ,0 Debit   
					 ,Sum(SPY.DocTotal) Credit   
					 ,null DebitSC   
					 ,null CreditSC   
					 ,null DebitFC   
					 ,null CreditFC   
					 ,null ContraAccount   
					 ,null DebitCredit   
					 ,null BaseAmt   
					 ,null VatGroup   
					 ,null VatPercent   
					 ,null VatAmt   
					 ,SPY.DocDate  RefDate   
					 ,SPY.DocDueDate RefDate2   
					 ,SPY.TaxDate RefDate3   
					 ,SPY.PaymentID CreatedBy   
					 ,SPY.DocNum BaseRef   
					 ,SPY.PaymentID  Ref1   
					 ,null Ref2   
					 ,null Ref3   
					 ,null Project   
					 ,null OcrCode    
					 ,null OcrCode2   
					 ,null OcrCode3   
					 ,null OcrCode4   
					 ,null OcrCode5   
					 ,'CUSTOMER PAYMENT - ' + SPY.CardCode   LineRemarks   
					  from   
					  IncomingPayment SPY 
					  inner join BusinessPartner  CRD on SPY.CardCode  = CRD.CardCode  
					  outer apply (  
					   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= SPY.DocDate and ToRefDate >= SPY.DocDate   
					  ) FPR   
					   where SPY.PaymentID = @docID   and Isnull(SPY.DocTotal, 0) > 0
					   group by   
					   SPY.CardCode   
					   ,CRD.CtlDebCredPayAcct   
					   ,CRD.CardCode  
					   ,FPR.PeriodDetailID   
					   ,SPY.DocDate   
					   ,SPY.DocDueDate   
					   ,SPY.TaxDate   
					   ,SPY.PaymentID
					   ,SPY.DocNum   
				  				    
				) A  

			update @jdt set SysTotal = (select sum(debit) from @jdtline)  
			insert into JournalEntry ( 
				DocNum 
				,Series 
				,ObjType
				,BaseEntry
				,BaseNum
				,BaseType
				,TargetEntry
				,TargetNum
				,FinancePeriod
				,BatchNum 
				,TargetType
				,TransType
				,RefDate
				,RefDate2
				,RefDate3
				,Ref1
				,Ref2
				,Ref3
				,Project
				,Remarks
				,SysTotal
				,SysTotalLC
				,SysTotalFC
				,VersionNo
				,IsActive
				,CreatedByAppUserId
				,CreatedOn
				,ModifiedByAppUserId
				,ModifiedOn 
				) 
				select * from @jdt  
			set @jdtID = @@IDENTITY  
  
			if @jdtID > 0  
				begin   
			
					update @jdtline set JournalEntryID = @jdtID   
			
					insert into JournalEntryDetail select * from @jdtline  
  
					update IncomingPayment  set TransID = @jdtID ,JournalRemarks = (select top 1 Remarks from @jdt)  where PaymentID = @docID  
			
				end   
	end 

 /*Goods Receipt*/
 if @docType = 'IGN'
	begin
	 insert into @jdt  
		   select   
			null DocNum  
		   ,null Series  
		   ,'JEN' ObjType  
		   ,null BaseEntry  
		   ,null BaseNum  
		   ,null BaseType  
		   ,null TargetEntry  
		   ,null TargetNum  
		   ,FPR.PeriodDetailID FinancePeriod  
		   ,null BatchNum  
		   ,null TargetType  
		   ,'IGN' TransType  
		   ,IGN.DocDate  RefDate   
		   ,IGN.DocDueDate RefDate2   
		   ,IGN.TaxDate RefDate3   
		   ,IGN.DocID Ref1  
		   ,null Ref2  
		   ,null Ref3  
		   ,IGN.Project Project  
		   ,'Goods Receipt - ' + IGN.CardCode    Remarks  
		   ,null SysTotal  
		   ,null SysTotalLC  
		   ,null SysTotalFC  
		   ,null VersionNo   
		   ,1 IsActive   
		   ,@createdBy CreatedByAppUserId   
		   ,Getdate() CreatedOn   
		   ,null ModifiedByAppUserId   
		   ,null ModifiedOn   
			from   
			GoodsReceipt IGN  
			outer apply (  
			 select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= IGN.DocDate and ToRefDate >= IGN.DocDate   
			) FPR   
  
		  where IGN.DocID = @docID   
   
		  insert into @jdtline  (
	JournalEntryID, ObjType ,BaseEntry ,BaseNum ,BaseType ,TargetEntry ,TargetNum   ,TargetType ,TransType ,GLAcctCode ,LineID ,VisID ,FinancePeriod ,BatchNum ,ShortName ,Debit ,Credit ,DebitSC ,CreditSC ,DebitFC ,CreditFC 
	,ContraAccount ,DebitCredit ,BaseAmt ,VatGroup ,VatPercent ,VatAmt ,RefDate ,RefDate2 ,RefDate3 ,CreatedBy ,BaseRef ,Ref1 ,Ref2 ,Ref3 ,Project ,OcrCode ,OcrCode2 ,OcrCode3 ,OcrCode4 ,OcrCode5 ,LineRemarks ,VersionNo ,IsActive ,CreatedByAppUserId ,CreatedOn ,ModifiedByAppUserId ,ModifiedOn   
  )
			select  @jdtID JournalEntryID  
			 ,ObjType   
			 ,BaseEntry   
			 ,BaseNum   
			 ,BaseType   
			 ,TargetEntry   
			 ,TargetNum   
			 ,TargetType   
			 ,TransType   
			 ,GLAcctCode   
			 ,ROW_NUMBER() over( order by GLAcctCode) LineID   
			 ,VisID   
			 ,FinancePeriod   
			 ,BatchNum   
			 ,ShortName   
			 ,Debit   
			 ,Credit   
			 ,DebitSC   
			 ,CreditSC   
			 ,DebitFC   
			 ,CreditFC   
			 ,ContraAccount   
			 ,DebitCredit   
			 ,BaseAmt   
			 ,VatGroup   
			 ,VatPercent   
			 ,VatAmt   
			 ,RefDate   
			 ,RefDate2   
			 ,RefDate3   
			 ,CreatedBy   
			 ,BaseRef   
			 ,Ref1   
			 ,Ref2   
			 ,Ref3   
			 ,Project   
			 ,OcrCode   
			 ,OcrCode2   
			 ,OcrCode3   
			 ,OcrCode4   
			 ,OcrCode5  
			 ,LineRemarks  
			 ,null VersionNo   
			 ,1 IsActive   
			 ,@createdBy CreatedByAppUserId   
			 ,Getdate() CreatedOn   
			 ,null ModifiedByAppUserId   
			 ,null ModifiedOn   
			from (  
			 select   --Credit Invntry Offset Acct  
			 'JEN' ObjType   
			 ,null BaseEntry   
			 ,null BaseNum   
			 ,null BaseType   
			 ,null TargetEntry   
			 ,null TargetNum   
			 ,null TargetType   
			 ,'IGN' TransType   
			 ,IGN1.AcctCode  GLAcctCode   
			 ,null LineID   
			 ,null VisID   
			 ,FPR.PeriodDetailID FinancePeriod   
			 ,null BatchNum   
			 ,IGN1.AcctCode  ShortName   
			 ,0 Debit   
			 ,Sum(IGN1.LineTotal) Credit   
			 ,null DebitSC   
			 ,null CreditSC   
			 ,null DebitFC   
			 ,null CreditFC   
			 ,null ContraAccount   
			 ,null DebitCredit   
			 ,null BaseAmt   
			 ,null VatGroup   
			 ,null VatPercent   
			 ,null VatAmt   
			 ,IGN.DocDate  RefDate   
			 ,IGN.DocDueDate RefDate2   
			 ,IGN.TaxDate RefDate3   
			 ,IGN.DocID CreatedBy   
			 ,IGN.DocNum BaseRef   
			 ,IGN.DocID  Ref1   
			 ,null Ref2   
			 ,null Ref3   
			 ,IGN1.ProjectCode Project   
			 ,IGN1.OcrCode OcrCode    
			 ,IGN1.OcrCode2 OcrCode2   
			 ,IGN1.OcrCode3 OcrCode3   
			 ,IGN1.OcrCode4 OcrCode4   
			 ,IGN1.OcrCode5 OcrCode5   
			 ,'Goods Receipt - ' + IGN.CardCode   LineRemarks   
				from   
			  GoodsReceiptDetail IGN1  
			  inner join GoodsReceipt IGN on IGN1.DocID = IGN.DocID  
			  inner join ItemMaster ITM on IGN1.ItemCode = ITM.ItemCode   
			  inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
			  outer apply (  
			   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= IGN.DocDate and ToRefDate >= IGN.DocDate   
			  ) FPR   
			  where IGN.DocID = @docID   
			  group by   
			    ITG.GoodClearingAcct   
			   ,IGN1.AcctCode
			   ,IGN.CardCode   
			   ,FPR.PeriodDetailID   
			   ,IGN.DocDate   
			   ,IGN.DocDueDate   
			   ,IGN.TaxDate   
			   ,IGN.DocID   
			   ,IGN.DocNum   
			   ,IGN1.ProjectCode   
			   ,IGN1.OcrCode  
			   ,IGN1.OcrCode2  
			   ,IGN1.OcrCode3   
			   ,IGN1.OcrCode4   
			   ,IGN1.OcrCode5   
			 union ALL  
  
			 select  --Debit Invntry Acct  
			 'JEN' ObjType   
			 ,null BaseEntry   
			 ,null BaseNum   
			 ,null BaseType   
			 ,null TargetEntry   
			 ,null TargetNum   
			 ,null TargetType   
			 ,'IGN' TransType   
			 ,ITG.InvntryAcct  GLAcctCode   
			 ,null LineID   
			 ,null VisID   
			 ,FPR.PeriodDetailID FinancePeriod   
			 ,null BatchNum   
			 ,ITG.InvntryAcct ShortName   
			 ,Sum(IGN1.LineTotal) Debit   
			 ,0 Credit   
			 ,null DebitSC   
			 ,null CreditSC   
			 ,null DebitFC   
			 ,null CreditFC   
			 ,null ContraAccount   
			 ,null DebitCredit   
			 ,null BaseAmt   
			 ,null VatGroup   
			 ,null VatPercent   
			 ,null VatAmt   
			 ,IGN.DocDate  RefDate   
			 ,IGN.DocDueDate RefDate2   
			 ,IGN.TaxDate RefDate3   
			 ,IGN.DocID CreatedBy   
			 ,IGN.DocNum BaseRef   
			 ,IGN.DocID  Ref1   
			 ,null Ref2   
			 ,null Ref3   
			 ,IGN1.ProjectCode Project   
			 ,IGN1.OcrCode OcrCode    
			 ,IGN1.OcrCode2 OcrCode2   
			 ,IGN1.OcrCode3 OcrCode3   
			 ,IGN1.OcrCode4 OcrCode4   
			 ,IGN1.OcrCode5 OcrCode5   
			 ,'Goods Receipt - ' + IGN.CardCode   LineRemarks   
			  from   
			  GoodsReceiptDetail IGN1  
			  inner join GoodsReceipt IGN on IGN1.DocID = IGN.DocID  
			  inner join BusinessPartner  CRD on IGN.CardCode  = CRD.CardCode  
			  inner join ItemMaster ITM on IGN1.ItemCode = ITM.ItemCode   
			  inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
			  outer apply (  
			   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= IGN.DocDate and ToRefDate >= IGN.DocDate   
			  ) FPR   
			   where IGN.DocID = @docID   
			   group by   
			   ITG.InvntryAcct   
			   ,IGN.CardCode   
			   ,CRD.CtlDebCredPayAcct   
			   ,FPR.PeriodDetailID   
			   ,IGN.DocDate   
			   ,IGN.DocDueDate   
			   ,IGN.TaxDate   
			   ,IGN.DocID   
			   ,IGN.DocNum   
			   ,IGN1.ProjectCode   
			   ,IGN1.OcrCode  
			   ,IGN1.OcrCode2  
			   ,IGN1.OcrCode3   
			   ,IGN1.OcrCode4   
			   ,IGN1.OcrCode5   
  
			) A  
  
		  update @jdt set SysTotal = (select sum(debit) from @jdtline)  
		  insert into JournalEntry ( 
		  DocNum 
		  ,Series 
		  ,ObjType
		  ,BaseEntry
		  ,BaseNum
		  ,BaseType
		  ,TargetEntry
		  ,TargetNum
		  ,FinancePeriod
		  ,BatchNum 
		  ,TargetType
		  ,TransType
		  ,RefDate
		  ,RefDate2
		  ,RefDate3
		  ,Ref1
		  ,Ref2
		  ,Ref3
		  ,Project
		  ,Remarks
		  ,SysTotal
		  ,SysTotalLC
		  ,SysTotalFC
		  ,VersionNo
		  ,IsActive
		  ,CreatedByAppUserId
		  ,CreatedOn
		  ,ModifiedByAppUserId
		  ,ModifiedOn 
		  ) select * from @jdt  
		  set @jdtID = @@IDENTITY  
		  if @jdtID > 0  
		  begin   
				update @jdtline set JournalEntryID = @jdtID   
				insert into JournalEntryDetail select * from @jdtline  
				update GoodsReceipt  set TransID = @jdtID ,JournalRemarks = (select top 1 Remarks from @jdt) where DocID = @docID  
		  end   
	 end   
  
 /*Goods Issue*/
 if @docType = 'IGE'
	 begin   
		  insert into @jdt  
		   select   
			null DocNum  
		   ,null Series  
		   ,'JEN' ObjType  
		   ,null BaseEntry  
		   ,null BaseNum  
		   ,null BaseType  
		   ,null TargetEntry  
		   ,null TargetNum  
		   ,FPR.PeriodDetailID FinancePeriod  
		   ,null BatchNum  
		   ,null TargetType  
		   ,'IGE' TransType  
		   ,IGE.DocDate  RefDate   
		   ,IGE.DocDueDate RefDate2   
		   ,IGE.TaxDate RefDate3   
		   ,IGE.DocID Ref1  
		   ,null Ref2  
		   ,null Ref3  
		   ,IGE.Project Project  
		   ,'Goods Issue - ' + IGE.CardCode    Remarks  
		   ,null SysTotal  
		   ,null SysTotalLC  
		   ,null SysTotalFC  
		   ,null VersionNo   
		   ,1 IsActive   
		   ,@createdBy CreatedByAppUserId   
		   ,Getdate() CreatedOn   
		   ,null ModifiedByAppUserId   
		   ,null ModifiedOn   
			from   
			GoodsIssue IGE  
			outer apply (  
			 select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= IGE.DocDate and ToRefDate >= IGE.DocDate   
			) FPR   
  
		  where IGE.DocID = @docID   
   
		  insert into @jdtline  (
	JournalEntryID, ObjType ,BaseEntry ,BaseNum ,BaseType ,TargetEntry ,TargetNum   ,TargetType ,TransType ,GLAcctCode ,LineID ,VisID ,FinancePeriod ,BatchNum ,ShortName ,Debit ,Credit ,DebitSC ,CreditSC ,DebitFC ,CreditFC 
	,ContraAccount ,DebitCredit ,BaseAmt ,VatGroup ,VatPercent ,VatAmt ,RefDate ,RefDate2 ,RefDate3 ,CreatedBy ,BaseRef ,Ref1 ,Ref2 ,Ref3 ,Project ,OcrCode ,OcrCode2 ,OcrCode3 ,OcrCode4 ,OcrCode5 ,LineRemarks ,VersionNo ,IsActive ,CreatedByAppUserId ,CreatedOn ,ModifiedByAppUserId ,ModifiedOn   
  )
			select  @jdtID JournalEntryID  
			 ,ObjType   
			 ,BaseEntry   
			 ,BaseNum   
			 ,BaseType   
			 ,TargetEntry   
			 ,TargetNum   
			 ,TargetType   
			 ,TransType   
			 ,GLAcctCode   
			 ,ROW_NUMBER() over( order by GLAcctCode) LineID   
			 ,VisID   
			 ,FinancePeriod   
			 ,BatchNum   
			 ,ShortName   
			 ,Debit   
			 ,Credit   
			 ,DebitSC   
			 ,CreditSC   
			 ,DebitFC   
			 ,CreditFC   
			 ,ContraAccount   
			 ,DebitCredit   
			 ,BaseAmt   
			 ,VatGroup   
			 ,VatPercent   
			 ,VatAmt   
			 ,RefDate   
			 ,RefDate2   
			 ,RefDate3   
			 ,CreatedBy   
			 ,BaseRef   
			 ,Ref1   
			 ,Ref2   
			 ,Ref3   
			 ,Project   
			 ,OcrCode   
			 ,OcrCode2   
			 ,OcrCode3   
			 ,OcrCode4   
			 ,OcrCode5  
			 ,LineRemarks  
			 ,null VersionNo   
			 ,1 IsActive   
			 ,@createdBy CreatedByAppUserId   
			 ,Getdate() CreatedOn   
			 ,null ModifiedByAppUserId   
			 ,null ModifiedOn   
			from (  
			 select   --Debit Invntry Offset Acct  
			 'JEN' ObjType   
			 ,null BaseEntry   
			 ,null BaseNum   
			 ,null BaseType   
			 ,null TargetEntry   
			 ,null TargetNum   
			 ,null TargetType   
			 ,'IGE' TransType   
			 ,IGE1.AcctCode  GLAcctCode   
			 ,null LineID   
			 ,null VisID   
			 ,FPR.PeriodDetailID FinancePeriod   
			 ,null BatchNum   
			 ,IGE1.AcctCode ShortName   
			 ,Sum(IGE1.LineTotal)  Debit   
			 ,0 Credit   
			 ,null DebitSC   
			 ,null CreditSC   
			 ,null DebitFC   
			 ,null CreditFC   
			 ,null ContraAccount   
			 ,null DebitCredit   
			 ,null BaseAmt   
			 ,null VatGroup   
			 ,null VatPercent   
			 ,null VatAmt   
			 ,IGE.DocDate  RefDate   
			 ,IGE.DocDueDate RefDate2   
			 ,IGE.TaxDate RefDate3   
			 ,IGE.DocID CreatedBy   
			 ,IGE.DocNum BaseRef   
			 ,IGE.DocID  Ref1   
			 ,null Ref2   
			 ,null Ref3   
			 ,IGE1.ProjectCode Project   
			 ,IGE1.OcrCode OcrCode    
			 ,IGE1.OcrCode2 OcrCode2   
			 ,IGE1.OcrCode3 OcrCode3   
			 ,IGE1.OcrCode4 OcrCode4   
			 ,IGE1.OcrCode5 OcrCode5   
			 ,'Goods Issue - ' + IGE.CardCode   LineRemarks   
				from   
			  GoodsIssueDetail IGE1  
			  inner join GoodsIssue IGE on IGE1.DocID = IGE.DocID  
			  inner join ItemMaster ITM on IGE1.ItemCode = ITM.ItemCode   
			  inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
			  outer apply (  
			   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= IGE.DocDate and ToRefDate >= IGE.DocDate   
			  ) FPR   
			  where IGE.DocID = @docID   
			  group by   
			    ITG.GoodClearingAcct   
			   ,IGE1.AcctCode  
			   ,IGE.CardCode   
			   ,FPR.PeriodDetailID   
			   ,IGE.DocDate   
			   ,IGE.DocDueDate   
			   ,IGE.TaxDate   
			   ,IGE.DocID   
			   ,IGE.DocNum   
			   ,IGE1.ProjectCode   
			   ,IGE1.OcrCode  
			   ,IGE1.OcrCode2  
			   ,IGE1.OcrCode3   
			   ,IGE1.OcrCode4   
			   ,IGE1.OcrCode5   
			 union ALL  
  
			 select  --Credit Invntry Acct  
			 'JEN' ObjType   
			 ,null BaseEntry   
			 ,null BaseNum   
			 ,null BaseType   
			 ,null TargetEntry   
			 ,null TargetNum   
			 ,null TargetType   
			 ,'IGE' TransType   
			 ,ITG.InvntryAcct  GLAcctCode   
			 ,null LineID   
			 ,null VisID   
			 ,FPR.PeriodDetailID FinancePeriod   
			 ,null BatchNum   
			 ,ITG.InvntryAcct ShortName   
			 ,0 Debit   
			 ,Sum(IGE1.LineTotal)  Credit   
			 ,null DebitSC   
			 ,null CreditSC   
			 ,null DebitFC   
			 ,null CreditFC   
			 ,null ContraAccount   
			 ,null DebitCredit   
			 ,null BaseAmt   
			 ,null VatGroup   
			 ,null VatPercent   
			 ,null VatAmt   
			 ,IGE.DocDate  RefDate   
			 ,IGE.DocDueDate RefDate2   
			 ,IGE.TaxDate RefDate3   
			 ,IGE.DocID CreatedBy   
			 ,IGE.DocNum BaseRef   
			 ,IGE.DocID  Ref1   
			 ,null Ref2   
			 ,null Ref3   
			 ,IGE1.ProjectCode Project   
			 ,IGE1.OcrCode OcrCode    
			 ,IGE1.OcrCode2 OcrCode2   
			 ,IGE1.OcrCode3 OcrCode3   
			 ,IGE1.OcrCode4 OcrCode4   
			 ,IGE1.OcrCode5 OcrCode5   
			 ,'Goods Issue - ' + IGE.CardCode   LineRemarks   
			  from   
			  GoodsIssueDetail IGE1  
			  inner join GoodsIssue IGE on IGE1.DocID = IGE.DocID  
			  inner join BusinessPartner  CRD on IGE.CardCode  = CRD.CardCode  
			  inner join ItemMaster ITM on IGE1.ItemCode = ITM.ItemCode   
			  inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
			  outer apply (  
			   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= IGE.DocDate and ToRefDate >= IGE.DocDate   
			  ) FPR   
			   where IGE.DocID = @docID   
			   group by   
			   ITG.InvntryAcct   
			   ,IGE.CardCode   
			   ,CRD.CtlDebCredPayAcct   
			   ,FPR.PeriodDetailID   
			   ,IGE.DocDate   
			   ,IGE.DocDueDate   
			   ,IGE.TaxDate   
			   ,IGE.DocID   
			   ,IGE.DocNum   
			   ,IGE1.ProjectCode   
			   ,IGE1.OcrCode  
			   ,IGE1.OcrCode2  
			   ,IGE1.OcrCode3   
			   ,IGE1.OcrCode4   
			   ,IGE1.OcrCode5   
  
			) A  
  
		  update @jdt set SysTotal = (select sum(debit) from @jdtline)  
		  insert into JournalEntry ( 
		  DocNum 
		  ,Series 
		  ,ObjType
		  ,BaseEntry
		  ,BaseNum
		  ,BaseType
		  ,TargetEntry
		  ,TargetNum
		  ,FinancePeriod
		  ,BatchNum 
		  ,TargetType
		  ,TransType
		  ,RefDate
		  ,RefDate2
		  ,RefDate3
		  ,Ref1
		  ,Ref2
		  ,Ref3
		  ,Project
		  ,Remarks
		  ,SysTotal
		  ,SysTotalLC
		  ,SysTotalFC
		  ,VersionNo
		  ,IsActive
		  ,CreatedByAppUserId
		  ,CreatedOn
		  ,ModifiedByAppUserId
		  ,ModifiedOn 
		  ) select * from @jdt  
		  set @jdtID = @@IDENTITY  
		  if @jdtID > 0  
		  begin   
				update @jdtline set JournalEntryID = @jdtID   
				insert into JournalEntryDetail select * from @jdtline  
				update GoodsIssue  set TransID = @jdtID ,JournalRemarks = (select top 1 Remarks from @jdt)  where DocID = @docID  
		  end   
	 end   

 /*Invntry Qty OB*/
 if @docType = 'IQI'
	 begin   
		  insert into @jdt  
		   select   
			null DocNum  
		   ,null Series  
		   ,'JEN' ObjType  
		   ,null BaseEntry  
		   ,null BaseNum  
		   ,null BaseType  
		   ,null TargetEntry  
		   ,null TargetNum  
		   ,FPR.PeriodDetailID FinancePeriod  
		   ,null BatchNum  
		   ,null TargetType  
		   ,'IQI' TransType  
		   ,IQI.DocDate  RefDate   
		   ,IQI.DocDueDate RefDate2   
		   ,null RefDate3   
		   ,IQI.DocID Ref1  
		   ,null Ref2  
		   ,null Ref3  
		   ,null Project  
		   ,'Inventory OB' Remarks  
		   ,null SysTotal  
		   ,null SysTotalLC  
		   ,null SysTotalFC  
		   ,null VersionNo   
		   ,1 IsActive   
		   ,@createdBy CreatedByAppUserId   
		   ,Getdate() CreatedOn   
		   ,null ModifiedByAppUserId   
		   ,null ModifiedOn   
			from   
			InventoryQtyTracking  IQI  
			outer apply (  
			 select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= IQI.DocDate and ToRefDate >= IQI.DocDate   
			) FPR   
  
		  where IQI.DocID = @docID   
   
		  insert into @jdtline  (
	JournalEntryID, ObjType ,BaseEntry ,BaseNum ,BaseType ,TargetEntry ,TargetNum   ,TargetType ,TransType ,GLAcctCode ,LineID ,VisID ,FinancePeriod ,BatchNum ,ShortName ,Debit ,Credit ,DebitSC ,CreditSC ,DebitFC ,CreditFC 
	,ContraAccount ,DebitCredit ,BaseAmt ,VatGroup ,VatPercent ,VatAmt ,RefDate ,RefDate2 ,RefDate3 ,CreatedBy ,BaseRef ,Ref1 ,Ref2 ,Ref3 ,Project ,OcrCode ,OcrCode2 ,OcrCode3 ,OcrCode4 ,OcrCode5 ,LineRemarks ,VersionNo ,IsActive ,CreatedByAppUserId ,CreatedOn ,ModifiedByAppUserId ,ModifiedOn   
  )
			select  @jdtID JournalEntryID  
			 ,ObjType   
			 ,BaseEntry   
			 ,BaseNum   
			 ,BaseType   
			 ,TargetEntry   
			 ,TargetNum   
			 ,TargetType   
			 ,TransType   
			 ,GLAcctCode   
			 ,ROW_NUMBER() over( order by GLAcctCode) LineID   
			 ,VisID   
			 ,FinancePeriod   
			 ,BatchNum   
			 ,ShortName   
			 ,Debit   
			 ,Credit   
			 ,DebitSC   
			 ,CreditSC   
			 ,DebitFC   
			 ,CreditFC   
			 ,ContraAccount   
			 ,DebitCredit   
			 ,BaseAmt   
			 ,VatGroup   
			 ,VatPercent   
			 ,VatAmt   
			 ,RefDate   
			 ,RefDate2   
			 ,RefDate3   
			 ,CreatedBy   
			 ,BaseRef   
			 ,Ref1   
			 ,Ref2   
			 ,Ref3   
			 ,Project   
			 ,OcrCode   
			 ,OcrCode2   
			 ,OcrCode3   
			 ,OcrCode4   
			 ,OcrCode5  
			 ,LineRemarks  
			 ,null VersionNo   
			 ,1 IsActive   
			 ,@createdBy CreatedByAppUserId   
			 ,Getdate() CreatedOn   
			 ,null ModifiedByAppUserId   
			 ,null ModifiedOn   
			from (  
			 select   --Debit Invntry Account
			 'JEN' ObjType   
			 ,null BaseEntry   
			 ,null BaseNum   
			 ,null BaseType   
			 ,null TargetEntry   
			 ,null TargetNum   
			 ,null TargetType   
			 ,'IQI' TransType   
			 ,ITG.InvntryAcct GLAcctCode   
			 ,null LineID   
			 ,null VisID   
			 ,FPR.PeriodDetailID FinancePeriod   
			 ,null BatchNum   
			 ,ITG.InvntryAcct ShortName   
			 ,Sum(IQI1.DocTotal)  Debit   
			 ,0.0 Credit   
			 ,null DebitSC   
			 ,null CreditSC   
			 ,null DebitFC   
			 ,null CreditFC   
			 ,null ContraAccount   
			 ,null DebitCredit   
			 ,null BaseAmt   
			 ,null VatGroup   
			 ,null VatPercent   
			 ,null VatAmt   
			 ,IQI.DocDate  RefDate   
			 ,IQI.DocDueDate RefDate2   
			 ,null RefDate3   
			 ,IQI.DocID CreatedBy   
			 ,IQI.DocID BaseRef   
			 ,IQI.DocID  Ref1   
			 ,null Ref2   
			 ,null Ref3   
			 ,null Project   
			 ,IQI1.OcrCode OcrCode    
			 ,IQI1.OcrCode2 OcrCode2   
			 ,IQI1.OcrCode3 OcrCode3   
			 ,IQI1.OcrCode4 OcrCode4   
			 ,null OcrCode5   
			 ,'Inventory OB ' LineRemarks   
				from   
			  InventoryQtyTrackingDetail  IQI1  
			  inner join InventoryQtyTracking IQI on IQI1.DocID = IQI.DocID  
			  inner join ItemMaster ITM on IQI1.ItemCode = ITM.ItemCode   
			  inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
			  outer apply (  
			   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= IQI.DocDate and ToRefDate >= IQI.DocDate   
			  ) FPR   
			  where IQI.DocID = @docID   
			  group by   
			    ITG.InvntryAcct   
			   ,FPR.PeriodDetailID   
			   ,IQI.DocDate   
			   ,IQI.DocDueDate   
			   --,IQI.TaxDate   
			   ,IQI.DocID   
			   --,IQI.DocNum   
			   --,IQI1.ProjectCode   
			   ,IQI1.OcrCode  
			   ,IQI1.OcrCode2  
			   ,IQI1.OcrCode3   
			   ,IQI1.OcrCode4   
			   --,IQI1.OcrCode5   
			 union ALL  
  
			 select  --Credit Invntry Offset Acct  
			 'JEN' ObjType   
			 ,null BaseEntry   
			 ,null BaseNum   
			 ,null BaseType   
			 ,null TargetEntry   
			 ,null TargetNum   
			 ,null TargetType   
			 ,'IQI' TransType   
			 ,IQI1.InvntryOffsetIncAcct GLAcctCode   
			 ,null LineID   
			 ,null VisID   
			 ,FPR.PeriodDetailID FinancePeriod   
			 ,null BatchNum   
			 ,IQI1.InvntryOffsetIncAcct ShortName   
			 ,0 Debit   
			 ,Sum(IQI1.DocTotal)  Credit   
			 ,null DebitSC   
			 ,null CreditSC   
			 ,null DebitFC   
			 ,null CreditFC   
			 ,null ContraAccount   
			 ,null DebitCredit   
			 ,null BaseAmt   
			 ,null VatGroup   
			 ,null VatPercent   
			 ,null VatAmt   
			 ,IQI.DocDate  RefDate   
			 ,IQI.DocDueDate RefDate2   
			 ,null RefDate3   
			 ,IQI.DocID CreatedBy   
			 ,null BaseRef   
			 ,IQI.DocID  Ref1   
			 ,null Ref2   
			 ,null Ref3   
			 ,null Project   
			 ,IQI1.OcrCode OcrCode    
			 ,IQI1.OcrCode2 OcrCode2   
			 ,IQI1.OcrCode3 OcrCode3   
			 ,IQI1.OcrCode4 OcrCode4   
			 ,null OcrCode5   
			 ,'Inventory OB' LineRemarks   
			  from   
			  InventoryQtyTrackingDetail  IQI1  
			  inner join InventoryQtyTracking IQI on IQI1.DocID = IQI.DocID  
			  --inner join BusinessPartner  CRD on IQI.CardCode  = CRD.CardCode  
			  inner join ItemMaster ITM on IQI1.ItemCode = ITM.ItemCode   
			  inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
			  outer apply (  
			   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= IQI.DocDate and ToRefDate >= IQI.DocDate   
			  ) FPR   
			   where IQI.DocID = @docID   
			   group by   
			    ITG.InvntryAcct
			   ,IQI1.InvntryOffsetIncAcct
			   --,IQI.CardCode   
			   --,CRD.CtlDebCredPayAcct   
			   ,FPR.PeriodDetailID   
			   ,IQI.DocDate   
			   ,IQI.DocDueDate   
			   --,IQI.TaxDate   
			   ,IQI.DocID   
			   --,IQI.DocNum   
			   --,IQI1.ProjectCode   
			   ,IQI1.OcrCode  
			   ,IQI1.OcrCode2  
			   ,IQI1.OcrCode3   
			   ,IQI1.OcrCode4   
			   --,IQI1.OcrCode5   
  
			) A  
  
		  update @jdt set SysTotal = (select sum(debit) from @jdtline)  
		  insert into JournalEntry ( 
		  DocNum 
		  ,Series 
		  ,ObjType
		  ,BaseEntry
		  ,BaseNum
		  ,BaseType
		  ,TargetEntry
		  ,TargetNum
		  ,FinancePeriod
		  ,BatchNum 
		  ,TargetType
		  ,TransType
		  ,RefDate
		  ,RefDate2
		  ,RefDate3
		  ,Ref1
		  ,Ref2
		  ,Ref3
		  ,Project
		  ,Remarks
		  ,SysTotal
		  ,SysTotalLC
		  ,SysTotalFC
		  ,VersionNo
		  ,IsActive
		  ,CreatedByAppUserId
		  ,CreatedOn
		  ,ModifiedByAppUserId
		  ,ModifiedOn 
		  ) 
		  
		  select * from @jdt  
		  set @jdtID = @@IDENTITY  

		  if @jdtID > 0  
		  begin   
			update @jdtline set JournalEntryID = @jdtID   
			insert into JournalEntryDetail select * from @jdtline  
			update InventoryQtyTracking  set TransID = @jdtID where DocID = @docID  
		  end   
	end   

 /*Invntry Qty Posting*/
 if @docType = 'IQP'
	 begin   
		  insert into @jdt  
		   select   
			null DocNum  
		   ,null Series  
		   ,'JEN' ObjType  
		   ,null BaseEntry  
		   ,null BaseNum  
		   ,null BaseType  
		   ,null TargetEntry  
		   ,null TargetNum  
		   ,FPR.PeriodDetailID FinancePeriod  
		   ,null BatchNum  
		   ,null TargetType  
		   ,'IQP' TransType  
		   ,IQP.DocDate  RefDate   
		   ,IQP.DocDueDate RefDate2   
		   ,IQP.DocDate  RefDate3   
		   ,IQP.DocID Ref1  
		   ,null Ref2  
		   ,null Ref3  
		   ,null Project  
		   ,'Inventory Qty Posting' Remarks  
		   ,null SysTotal  
		   ,null SysTotalLC  
		   ,null SysTotalFC  
		   ,null VersionNo   
		   ,1 IsActive   
		   ,@createdBy CreatedByAppUserId   
		   ,Getdate() CreatedOn   
		   ,null ModifiedByAppUserId   
		   ,null ModifiedOn   
			from   
			InventoryQtyPosting IQP
			outer apply (  
			 select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= IQP.DocDate and ToRefDate >= IQP.DocDate   
			) FPR   
  
		  where IQP.DocID = @docID   
   
		  insert into @jdtline  (
	JournalEntryID, ObjType ,BaseEntry ,BaseNum ,BaseType ,TargetEntry ,TargetNum   ,TargetType ,TransType ,GLAcctCode ,LineID ,VisID ,FinancePeriod ,BatchNum ,ShortName ,Debit ,Credit ,DebitSC ,CreditSC ,DebitFC ,CreditFC 
	,ContraAccount ,DebitCredit ,BaseAmt ,VatGroup ,VatPercent ,VatAmt ,RefDate ,RefDate2 ,RefDate3 ,CreatedBy ,BaseRef ,Ref1 ,Ref2 ,Ref3 ,Project ,OcrCode ,OcrCode2 ,OcrCode3 ,OcrCode4 ,OcrCode5 ,LineRemarks ,VersionNo ,IsActive ,CreatedByAppUserId ,CreatedOn ,ModifiedByAppUserId ,ModifiedOn   
  )
			select  @jdtID JournalEntryID  
			 ,ObjType   
			 ,BaseEntry   
			 ,BaseNum   
			 ,BaseType   
			 ,TargetEntry   
			 ,TargetNum   
			 ,TargetType   
			 ,TransType   
			 ,GLAcctCode   
			 ,ROW_NUMBER() over( order by GLAcctCode) LineID   
			 ,VisID   
			 ,FinancePeriod   
			 ,BatchNum   
			 ,ShortName   
			 ,Debit   
			 ,Credit   
			 ,DebitSC   
			 ,CreditSC   
			 ,DebitFC   
			 ,CreditFC   
			 ,ContraAccount   
			 ,DebitCredit   
			 ,BaseAmt   
			 ,VatGroup   
			 ,VatPercent   
			 ,VatAmt   
			 ,RefDate   
			 ,RefDate2   
			 ,RefDate3   
			 ,CreatedBy   
			 ,BaseRef   
			 ,Ref1   
			 ,Ref2   
			 ,Ref3   
			 ,Project   
			 ,OcrCode   
			 ,OcrCode2   
			 ,OcrCode3   
			 ,OcrCode4   
			 ,OcrCode5  
			 ,LineRemarks  
			 ,null VersionNo   
			 ,1 IsActive   
			 ,@createdBy CreatedByAppUserId   
			 ,Getdate() CreatedOn   
			 ,null ModifiedByAppUserId   
			 ,null ModifiedOn   
			from (  
			 select   --Debit/Credit Invntry Offset
			 'JEN' ObjType   
			 ,null BaseEntry   
			 ,null BaseNum   
			 ,null BaseType   
			 ,null TargetEntry   
			 ,null TargetNum   
			 ,null TargetType   
			 ,'IQP' TransType   
			 ,case when Sum(IQP1.DocTotal) > 0 then IQP1.InvntryOffsetIncAcct  else IQP1.InvntryOffsetDecAcct  end   GLAcctCode   
			 ,null LineID   
			 ,null VisID   
			 ,FPR.PeriodDetailID FinancePeriod   
			 ,null BatchNum   
			 ,case when Sum(IQP1.DocTotal) > 0 then IQP1.InvntryOffsetIncAcct  else IQP1.InvntryOffsetDecAcct end  ShortName   
			 ,case when Sum(IQP1.DocTotal) > 0 then 0 else Abs(Sum(IQP1.DocTotal)) end  Debit   
			 ,case when Sum(IQP1.DocTotal) > 0 then Abs(Sum(IQP1.DocTotal)) else 0 end Credit   
			 ,null DebitSC   
			 ,null CreditSC   
			 ,null DebitFC   
			 ,null CreditFC   
			 ,null ContraAccount   
			 ,null DebitCredit   
			 ,null BaseAmt   
			 ,null VatGroup   
			 ,null VatPercent   
			 ,null VatAmt   
			 ,IQP.DocDate  RefDate   
			 ,IQP.DocDueDate RefDate2   
			 ,IQP.DocDate   RefDate3   
			 ,IQP.DocID CreatedBy   
			 ,null BaseRef   
			 ,IQP.DocID  Ref1   
			 ,null Ref2   
			 ,null Ref3   
			 ,null Project   
			 ,IQP1.OcrCode OcrCode    
			 ,IQP1.OcrCode2 OcrCode2   
			 ,IQP1.OcrCode3 OcrCode3   
			 ,IQP1.OcrCode4 OcrCode4   
			 ,null OcrCode5   
			 ,'Inventory Qty Posting' LineRemarks   
				from   
			  InventoryQtyPostingDetail IQP1  
			  inner join InventoryQtyPosting IQP on IQP1.DocID = IQP.DocID  
			  inner join ItemMaster ITM on IQP1.ItemCode = ITM.ItemCode   
			  inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
			  outer apply (  
			   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= IQP.DocDate and ToRefDate >= IQP.DocDate   
			  ) FPR   
			  where IQP.DocID = @docID   
			  group by   
			    --ITG.GoodClearingAcct   
			   IQP1.InvntryOffsetIncAcct  
			   ,IQP1.InvntryOffsetDecAcct 
			   --,IQP.CardCode   
			   ,FPR.PeriodDetailID   
			   ,IQP.DocDate   
			   ,IQP.DocDueDate   
			   --,IQP.TaxDate   
			   ,IQP.DocID   
			   --,IQP.DocNum   
			   --,IQP1.ProjectCode   
			   ,IQP1.OcrCode  
			   ,IQP1.OcrCode2  
			   ,IQP1.OcrCode3   
			   ,IQP1.OcrCode4   
			   --,IQP1.OcrCode5   
			 union ALL  
  
			 select  --Debit/Credit Invntry Acct  
			 'JEN' ObjType   
			 ,null BaseEntry   
			 ,null BaseNum   
			 ,null BaseType   
			 ,null TargetEntry   
			 ,null TargetNum   
			 ,null TargetType   
			 ,'IQP' TransType   
			 ,ITG.InvntryAcct  GLAcctCode   
			 ,null LineID   
			 ,null VisID   
			 ,FPR.PeriodDetailID FinancePeriod   
			 ,null BatchNum   
			 ,ITG.InvntryAcct ShortName   
			 ,case when Sum(IQP1.DocTotal) > 0 then Abs(Sum(IQP1.DocTotal)) else 0 end  Debit   
			 ,case when Sum(IQP1.DocTotal) > 0 then 0 else Abs(Sum(IQP1.DocTotal)) end Credit   
			 ,null DebitSC   
			 ,null CreditSC   
			 ,null DebitFC   
			 ,null CreditFC   
			 ,null ContraAccount   
			 ,null DebitCredit   
			 ,null BaseAmt   
			 ,null VatGroup   
			 ,null VatPercent   
			 ,null VatAmt   
			 ,IQP.DocDate  RefDate   
			 ,IQP.DocDueDate RefDate2   
			 ,IQP.DocDate  RefDate3   
			 ,IQP.DocID CreatedBy   
			 ,null BaseRef   
			 ,IQP.DocID  Ref1   
			 ,null Ref2   
			 ,null Ref3   
			 ,null Project   
			 ,IQP1.OcrCode OcrCode    
			 ,IQP1.OcrCode2 OcrCode2   
			 ,IQP1.OcrCode3 OcrCode3   
			 ,IQP1.OcrCode4 OcrCode4   
			 ,null OcrCode5   
			 ,'Inventory Qty Posting' LineRemarks   
			  from   
			  InventoryQtyPostingDetail IQP1  
			  inner join InventoryQtyPosting IQP on IQP1.DocID = IQP.DocID  
			  --inner join BusinessPartner  CRD on IQP.CardCode  = CRD.CardCode  
			  inner join ItemMaster ITM on IQP1.ItemCode = ITM.ItemCode   
			  inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
			  outer apply (  
			   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= IQP.DocDate and ToRefDate >= IQP.DocDate   
			  ) FPR   
			   where IQP.DocID = @docID   
			   group by   
			   ITG.InvntryAcct   
			   --,IQP.CardCode   
			   --,CRD.CtlDebCredPayAcct   
			   ,FPR.PeriodDetailID   
			   ,IQP.DocDate   
			   ,IQP.DocDueDate   
			   --,IQP.TaxDate   
			   ,IQP.DocID   
			   --,IQP.DocNum   
			   --,IQP1.ProjectCode   
			   ,IQP1.OcrCode  
			   ,IQP1.OcrCode2  
			   ,IQP1.OcrCode3   
			   ,IQP1.OcrCode4   
			   --,IQP1.OcrCode5   
  
			) A  
  
		  update @jdt set SysTotal = (select sum(debit) from @jdtline)  
		  insert into JournalEntry ( 
		  DocNum 
		  ,Series 
		  ,ObjType
		  ,BaseEntry
		  ,BaseNum
		  ,BaseType
		  ,TargetEntry
		  ,TargetNum
		  ,FinancePeriod
		  ,BatchNum 
		  ,TargetType
		  ,TransType
		  ,RefDate
		  ,RefDate2
		  ,RefDate3
		  ,Ref1
		  ,Ref2
		  ,Ref3
		  ,Project
		  ,Remarks
		  ,SysTotal
		  ,SysTotalLC
		  ,SysTotalFC
		  ,VersionNo
		  ,IsActive
		  ,CreatedByAppUserId
		  ,CreatedOn
		  ,ModifiedByAppUserId
		  ,ModifiedOn 
		  ) 
		  
		  select * from @jdt  
		  set @jdtID = @@IDENTITY  
		  if @jdtID > 0  
		  begin   
			update @jdtline set JournalEntryID = @jdtID   
			insert into JournalEntryDetail 
			select * from @jdtline  
			update InventoryQtyPosting set TransID = @jdtID where DocID = @docID  
		  end   
	  end

/*Invntry Revaluation*/
 if @docType = 'IMV'
	 begin   
		  insert into @jdt  
		   select   
			null DocNum  
		   ,null Series  
		   ,'JEN' ObjType  
		   ,null BaseEntry  
		   ,null BaseNum  
		   ,null BaseType  
		   ,null TargetEntry  
		   ,null TargetNum  
		   ,FPR.PeriodDetailID FinancePeriod  
		   ,null BatchNum  
		   ,null TargetType  
		   ,'IMV' TransType  
		   ,IMV.DocDate  RefDate   
		   ,IMV.DocDueDate RefDate2   
		   ,IMV.DocDate  RefDate3   
		   ,IMV.DocID Ref1  
		   ,null Ref2  
		   ,null Ref3  
		   ,null Project  
		   ,'Inventory Revaluation' Remarks  
		   ,null SysTotal  
		   ,null SysTotalLC  
		   ,null SysTotalFC  
		   ,null VersionNo   
		   ,1 IsActive   
		   ,@createdBy CreatedByAppUserId   
		   ,Getdate() CreatedOn   
		   ,null ModifiedByAppUserId   
		   ,null ModifiedOn   
			from   
			InventoryRevaluation IMV
			outer apply (  
			 select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= IMV.DocDate and ToRefDate >= IMV.DocDate   
			) FPR   
  
		  where IMV.DocID = @docID   
   
		  insert into @jdtline  ( 
		  JournalEntryID, ObjType ,BaseEntry ,BaseNum ,BaseType ,TargetEntry ,TargetNum   ,TargetType ,TransType ,GLAcctCode ,LineID ,VisID ,FinancePeriod ,BatchNum ,ShortName ,Debit ,Credit ,DebitSC ,CreditSC ,DebitFC ,CreditFC 
		,ContraAccount ,DebitCredit ,BaseAmt ,VatGroup ,VatPercent ,VatAmt ,RefDate ,RefDate2 ,RefDate3 ,CreatedBy ,BaseRef ,Ref1 ,Ref2 ,Ref3 ,Project ,OcrCode ,OcrCode2 ,OcrCode3 ,OcrCode4 ,OcrCode5 ,LineRemarks ,VersionNo ,IsActive ,CreatedByAppUserId ,CreatedOn ,ModifiedByAppUserId ,ModifiedOn   
		)
			select  @jdtID JournalEntryID  
			 ,ObjType   
			 ,BaseEntry   
			 ,BaseNum   
			 ,BaseType   
			 ,TargetEntry   
			 ,TargetNum   
			 ,TargetType   
			 ,TransType   
			 ,GLAcctCode   
			 ,ROW_NUMBER() over( order by GLAcctCode) LineID   
			 ,VisID   
			 ,FinancePeriod   
			 ,BatchNum   
			 ,ShortName   
			 ,Debit   
			 ,Credit   
			 ,DebitSC   
			 ,CreditSC   
			 ,DebitFC   
			 ,CreditFC   
			 ,ContraAccount   
			 ,DebitCredit   
			 ,BaseAmt   
			 ,VatGroup   
			 ,VatPercent   
			 ,VatAmt   
			 ,RefDate   
			 ,RefDate2   
			 ,RefDate3   
			 ,CreatedBy   
			 ,BaseRef   
			 ,Ref1   
			 ,Ref2   
			 ,Ref3   
			 ,Project   
			 ,OcrCode   
			 ,OcrCode2   
			 ,OcrCode3   
			 ,OcrCode4   
			 ,OcrCode5  
			 ,LineRemarks  
			 ,null VersionNo   
			 ,1 IsActive   
			 ,@createdBy CreatedByAppUserId   
			 ,Getdate() CreatedOn   
			 ,null ModifiedByAppUserId   
			 ,null ModifiedOn   
			from (  
			 select   --Debit/Credit Invntry Offset
			 'JEN' ObjType   
			 ,null BaseEntry   
			 ,null BaseNum   
			 ,null BaseType   
			 ,null TargetEntry   
			 ,null TargetNum   
			 ,null TargetType   
			 ,'IMV' TransType   
			 ,case when Sum(IMV1.LineTotal) > 0 then IMV1.InvntryRevalIncAcct else IMV1.InvntryRevalDecAcct  end   GLAcctCode   
			 ,null LineID   
			 ,null VisID   
			 ,FPR.PeriodDetailID FinancePeriod   
			 ,null BatchNum   
			 ,case when Sum(IMV1.LineTotal) > 0 then IMV1.InvntryRevalIncAcct  else IMV1.InvntryRevalDecAcct end  ShortName   
			 ,case when Sum(IMV1.LineTotal) > 0 then 0 else Abs(Sum(IMV1.LineTotal)) end  Debit   
			 ,case when Sum(IMV1.LineTotal) > 0 then Abs(Sum(IMV1.LineTotal)) else 0 end Credit   
			 ,null DebitSC   
			 ,null CreditSC   
			 ,null DebitFC   
			 ,null CreditFC   
			 ,null ContraAccount   
			 ,null DebitCredit   
			 ,null BaseAmt   
			 ,null VatGroup   
			 ,null VatPercent   
			 ,null VatAmt   
			 ,IMV.DocDate  RefDate   
			 ,IMV.DocDueDate RefDate2   
			 ,IMV.DocDate   RefDate3   
			 ,IMV.DocID CreatedBy   
			 ,null BaseRef   
			 ,IMV.DocID  Ref1   
			 ,null Ref2   
			 ,null Ref3   
			 ,null Project   
			 ,IMV1.OcrCode OcrCode    
			 ,IMV1.OcrCode2 OcrCode2   
			 ,IMV1.OcrCode3 OcrCode3   
			 ,IMV1.OcrCode4 OcrCode4   
			 ,null OcrCode5   
			 ,'Inventory Revaluation' LineRemarks   
				from   
			  InventoryRevaluationDetail IMV1  
			  inner join InventoryRevaluation IMV on IMV1.DocID = IMV.DocID  
			  inner join ItemMaster ITM on IMV1.ItemCode = ITM.ItemCode   
			  inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
			  outer apply (  
			   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= IMV.DocDate and ToRefDate >= IMV.DocDate   
			  ) FPR   
			  where IMV.DocID = @docID   
			  group by   
			    --ITG.GoodClearingAcct   
			   IMV1.InvntryRevalIncAcct  
			   ,IMV1.InvntryRevalDecAcct
			   --,IMV.CardCode   
			   ,FPR.PeriodDetailID   
			   ,IMV.DocDate   
			   ,IMV.DocDueDate   
			   --,IMV.TaxDate   
			   ,IMV.DocID   
			   --,IMV.DocNum   
			   --,IMV1.ProjectCode   
			   ,IMV1.OcrCode  
			   ,IMV1.OcrCode2  
			   ,IMV1.OcrCode3   
			   ,IMV1.OcrCode4   
			   --,IMV1.OcrCode5   
			 union ALL  
  
			 select  --Debit/Credit Invntry Acct  
			 'JEN' ObjType   
			 ,null BaseEntry   
			 ,null BaseNum   
			 ,null BaseType   
			 ,null TargetEntry   
			 ,null TargetNum   
			 ,null TargetType   
			 ,'IMV' TransType   
			 ,ITG.InvntryAcct  GLAcctCode   
			 ,null LineID   
			 ,null VisID   
			 ,FPR.PeriodDetailID FinancePeriod   
			 ,null BatchNum   
			 ,ITG.InvntryAcct ShortName   
			 ,case when Sum(IMV1.LineTotal) > 0 then Abs(Sum(IMV1.LineTotal)) else 0 end  Debit   
			 ,case when Sum(IMV1.LineTotal) > 0 then 0 else Abs(Sum(IMV1.LineTotal)) end Credit   
			 ,null DebitSC   
			 ,null CreditSC   
			 ,null DebitFC   
			 ,null CreditFC   
			 ,null ContraAccount   
			 ,null DebitCredit   
			 ,null BaseAmt   
			 ,null VatGroup   
			 ,null VatPercent   
			 ,null VatAmt   
			 ,IMV.DocDate  RefDate   
			 ,IMV.DocDueDate RefDate2   
			 ,IMV.DocDate  RefDate3   
			 ,IMV.DocID CreatedBy   
			 ,null BaseRef   
			 ,IMV.DocID  Ref1   
			 ,null Ref2   
			 ,null Ref3   
			 ,null Project   
			 ,IMV1.OcrCode OcrCode    
			 ,IMV1.OcrCode2 OcrCode2   
			 ,IMV1.OcrCode3 OcrCode3   
			 ,IMV1.OcrCode4 OcrCode4   
			 ,null OcrCode5   
			 ,'Inventory Revaluation' LineRemarks   
			  from   
			  InventoryRevaluationDetail IMV1  
			  inner join InventoryRevaluation IMV on IMV1.DocID = IMV.DocID  
			  --inner join BusinessPartner  CRD on IMV.CardCode  = CRD.CardCode  
			  inner join ItemMaster ITM on IMV1.ItemCode = ITM.ItemCode   
			  inner join ItemGroup  ITG on ITM.ItemGroupId = ITG.ItemGroupID   
			  outer apply (  
			   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= IMV.DocDate and ToRefDate >= IMV.DocDate   
			  ) FPR   
			   where IMV.DocID = @docID   
			   group by   
			   ITG.InvntryAcct   
			   --,IMV.CardCode   
			   --,CRD.CtlDebCredPayAcct   
			   ,FPR.PeriodDetailID   
			   ,IMV.DocDate   
			   ,IMV.DocDueDate   
			   --,IMV.TaxDate   
			   ,IMV.DocID   
			   --,IMV.DocNum   
			   --,IMV1.ProjectCode   
			   ,IMV1.OcrCode  
			   ,IMV1.OcrCode2  
			   ,IMV1.OcrCode3   
			   ,IMV1.OcrCode4   
			   --,IMV1.OcrCode5   
  
			) A  
  
		  update @jdt set SysTotal = (select sum(debit) from @jdtline)  
		  insert into JournalEntry ( 
		  DocNum 
		  ,Series 
		  ,ObjType
		  ,BaseEntry
		  ,BaseNum
		  ,BaseType
		  ,TargetEntry
		  ,TargetNum
		  ,FinancePeriod
		  ,BatchNum 
		  ,TargetType
		  ,TransType
		  ,RefDate
		  ,RefDate2
		  ,RefDate3
		  ,Ref1
		  ,Ref2
		  ,Ref3
		  ,Project
		  ,Remarks
		  ,SysTotal
		  ,SysTotalLC
		  ,SysTotalFC
		  ,VersionNo
		  ,IsActive
		  ,CreatedByAppUserId
		  ,CreatedOn
		  ,ModifiedByAppUserId
		  ,ModifiedOn 
		  ) 
		  
		  select * from @jdt  
		  set @jdtID = @@IDENTITY  
		  if @jdtID > 0  
		  begin   
			update @jdtline set JournalEntryID = @jdtID   
			insert into JournalEntryDetail 
			select * from @jdtline  
			update InventoryRevaluation set TransID = @jdtID where DocID = @docID  
		  end   
	  end


/*Deposits*/
 if @docType = 'DPS'
	 begin   
		  insert into @jdt  
		   select   
			null DocNum  
		   ,null Series  
		   ,'JEN' ObjType  
		   ,null BaseEntry  
		   ,null BaseNum  
		   ,null BaseType  
		   ,null TargetEntry  
		   ,null TargetNum  
		   ,FPR.PeriodDetailID FinancePeriod  
		   ,null BatchNum  
		   ,null TargetType  
		   ,'DPS' TransType  
		   ,DPS.DepositDate RefDate   
		   ,DPS.DepositDate RefDate2   
		   ,DPS.TaxDate RefDate3   
		   ,DPS.DepositID Ref1  
		   ,null Ref2  
		   ,null Ref3  
		   ,null Project  
		   ,'' Remarks  
		   ,null SysTotal  
		   ,null SysTotalLC  
		   ,null SysTotalFC  
		   ,null VersionNo   
		   ,1 IsActive   
		   ,@createdBy CreatedByAppUserId   
		   ,Getdate() CreatedOn   
		   ,null ModifiedByAppUserId   
		   ,null ModifiedOn   
			from   
			Deposit DPS
			outer apply (  
			 select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= DPS.DepositDate and ToRefDate >= DPS.DepositDate
			) FPR   
  
		  where DPS.DepositID = @docID   
   
		  insert into @jdtline  ( 
		  JournalEntryID, ObjType ,BaseEntry ,BaseNum ,BaseType ,TargetEntry ,TargetNum   ,TargetType ,TransType ,GLAcctCode ,LineID ,VisID ,FinancePeriod ,BatchNum ,ShortName ,Debit ,Credit ,DebitSC ,CreditSC ,DebitFC ,CreditFC 
		,ContraAccount ,DebitCredit ,BaseAmt ,VatGroup ,VatPercent ,VatAmt ,RefDate ,RefDate2 ,RefDate3 ,CreatedBy ,BaseRef ,Ref1 ,Ref2 ,Ref3 ,Project ,OcrCode ,OcrCode2 ,OcrCode3 ,OcrCode4 ,OcrCode5 ,LineRemarks ,VersionNo ,IsActive ,CreatedByAppUserId ,CreatedOn ,ModifiedByAppUserId ,ModifiedOn   
		)
			select  @jdtID JournalEntryID  
			 ,ObjType   
			 ,BaseEntry   
			 ,BaseNum   
			 ,BaseType   
			 ,TargetEntry   
			 ,TargetNum   
			 ,TargetType   
			 ,TransType   
			 ,GLAcctCode   
			 ,ROW_NUMBER() over( order by GLAcctCode) LineID   
			 ,VisID   
			 ,FinancePeriod   
			 ,BatchNum   
			 ,ShortName   
			 ,Debit   
			 ,Credit   
			 ,DebitSC   
			 ,CreditSC   
			 ,DebitFC   
			 ,CreditFC   
			 ,ContraAccount   
			 ,DebitCredit   
			 ,BaseAmt   
			 ,VatGroup   
			 ,VatPercent   
			 ,VatAmt   
			 ,RefDate   
			 ,RefDate2   
			 ,RefDate3   
			 ,CreatedBy   
			 ,BaseRef   
			 ,Ref1   
			 ,Ref2   
			 ,Ref3   
			 ,Project   
			 ,OcrCode   
			 ,OcrCode2   
			 ,OcrCode3   
			 ,OcrCode4   
			 ,OcrCode5  
			 ,LineRemarks  
			 ,null VersionNo   
			 ,1 IsActive   
			 ,@createdBy CreatedByAppUserId   
			 ,Getdate() CreatedOn   
			 ,null ModifiedByAppUserId   
			 ,null ModifiedOn   
			from (  
			 select   --Debit Bank
			 'JEN' ObjType   
			 ,null BaseEntry   
			 ,null BaseNum   
			 ,null BaseType   
			 ,null TargetEntry   
			 ,null TargetNum   
			 ,null TargetType   
			 ,'DPS' TransType   
			 ,DPS.BankAcct GLAcctCode   
			 ,null LineID   
			 ,null VisID   
			 ,FPR.PeriodDetailID FinancePeriod   
			 ,null BatchNum   
			 ,DPS.BankAcct ShortName   
			 ,DPS.DocTotal Debit   
			 ,0  Credit   
			 ,null DebitSC   
			 ,null CreditSC   
			 ,null DebitFC   
			 ,null CreditFC   
			 ,null ContraAccount   
			 ,null DebitCredit   
			 ,null BaseAmt   
			 ,null VatGroup   
			 ,null VatPercent   
			 ,null VatAmt   
			 ,DPS.DepositDate  RefDate   
			 ,DPS.DepositDate    RefDate2   
			 ,DPS.TaxDate RefDate3   
			 ,DPS.DepositID CreatedBy   
			 ,null BaseRef   
			 ,DPS.DepositID Ref1   
			 ,null Ref2   
			 ,null Ref3   
			 ,null Project   
			 ,DPS.OcrCode OcrCode    
			 ,DPS.OcrCode2 OcrCode2   
			 ,DPS.OcrCode3 OcrCode3   
			 ,DPS.OcrCode4 OcrCode4   
			 ,null OcrCode5   
			 ,'Deposits' LineRemarks   
				from   
			  Deposit DPS
			  outer apply (  
			   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= DPS.DepositDate  and ToRefDate >= DPS.DepositDate 
			  ) FPR   
			  where DPS.DepositID = @docID   
			 
			  union ALL  
  
			 select  --Credit Interim Acc/CASH
			 'JEN' ObjType   
			 ,null BaseEntry   
			 ,null BaseNum   
			 ,null BaseType   
			 ,null TargetEntry   
			 ,null TargetNum   
			 ,null TargetType   
			 ,'DPS' TransType   
			 ,DPS.AllocAcct  GLAcctCode   
			 ,null LineID   
			 ,null VisID   
			 ,FPR.PeriodDetailID FinancePeriod   
			 ,null BatchNum   
			 ,DPS.AllocAcct  ShortName   
			 ,0  Debit   
			 ,DPS.CashSum Credit   
			 ,null DebitSC   
			 ,null CreditSC   
			 ,null DebitFC   
			 ,null CreditFC   
			 ,null ContraAccount   
			 ,null DebitCredit   
			 ,null BaseAmt   
			 ,null VatGroup   
			 ,null VatPercent   
			 ,null VatAmt   
			 ,DPS.DepositDate   RefDate   
			 ,DPS.DepositDate  RefDate2   
			 ,DPS.TaxDate  RefDate3   
			 ,DPS.DepositID CreatedBy   
			 ,null BaseRef   
			 ,DPS.DepositID   Ref1   
			 ,null Ref2   
			 ,'CSH' Ref3   
			 ,null Project   
			 ,DPS.OcrCode OcrCode    
			 ,DPS.OcrCode2 OcrCode2   
			 ,DPS.OcrCode3 OcrCode3   
			 ,DPS.OcrCode4 OcrCode4   
			 ,null OcrCode5   
			 ,'CASH DEPOSIT' LineRemarks   
			  from   
			  Deposit  DPS  
			  outer apply (  
			   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= DPS.DepositDate  and ToRefDate >= DPS.DepositDate 
			  ) FPR   
			   where DPS.DepositID  = @docID   and IsNull(DPS.CashSum , 0) <> 0



			   union ALL  
  
			 select  --Credit Interim Acc /CHECK
			 'JEN' ObjType   
			 ,null BaseEntry   
			 ,null BaseNum   
			 ,null BaseType   
			 ,null TargetEntry   
			 ,null TargetNum   
			 ,null TargetType   
			 ,'DPS' TransType   
			 ,CHK.CashCheckAcct GLAcctCode   
			 ,null LineID   
			 ,null VisID   
			 ,FPR.PeriodDetailID FinancePeriod   
			 ,null BatchNum   
			 ,CHK.CashCheckAcct ShortName   
			 ,0  Debit   
			 ,Sum(CHK.CheckSum) Credit   
			 ,null DebitSC   
			 ,null CreditSC   
			 ,null DebitFC   
			 ,null CreditFC   
			 ,null ContraAccount   
			 ,null DebitCredit   
			 ,null BaseAmt   
			 ,null VatGroup   
			 ,null VatPercent   
			 ,null VatAmt   
			 ,DPS.DepositDate   RefDate   
			 ,DPS.DepositDate  RefDate2   
			 ,DPS.TaxDate  RefDate3   
			 ,DPS.DepositID CreatedBy   
			 ,null BaseRef   
			 ,DPS.DepositID Ref1   
			 ,DPS1.CheckID Ref2   
			 ,'CHK' Ref3   
			 ,null Project   
			 ,DPS.OcrCode OcrCode    
			 ,DPS.OcrCode2 OcrCode2   
			 ,DPS.OcrCode3 OcrCode3   
			 ,DPS.OcrCode4 OcrCode4   
			 ,null OcrCode5   
			 ,'CHECK DEPOSIT' LineRemarks   
			  from   
			  Deposit  DPS  
			  inner join DepositCheck DPS1 on DPS.DepositID = DPS1.DepositID 
			  cross apply (
			   select * from CheckRegister C where C.CheckID = DPS1.CheckID 
			  ) CHK
			  
			  outer apply (  
			   select PeriodDetailID  from FinancialPeriodDetail where FromRefDate <= DPS.DepositDate  and ToRefDate >= DPS.DepositDate 
			  ) FPR   
			   where DPS.DepositID  = @docID   
			   group by 
			   FPR.PeriodDetailID
			   ,DPS.DepositID 
			   ,DPS1.CheckID 
			   ,DPS.DepositDate
			   ,DPS.TaxDate 
			   ,CHK.CashCheckAcct
			   ,DPS.OcrCode 
			   ,DPS.OcrCode2 
			   ,DPS.OcrCode3 
			   ,DPS.OcrCode4 
  
			) A  
  
		  update @jdt set SysTotal = (select sum(debit) from @jdtline)  
		  insert into JournalEntry ( 
		  DocNum 
		  ,Series 
		  ,ObjType
		  ,BaseEntry
		  ,BaseNum
		  ,BaseType
		  ,TargetEntry
		  ,TargetNum
		  ,FinancePeriod
		  ,BatchNum 
		  ,TargetType
		  ,TransType
		  ,RefDate
		  ,RefDate2
		  ,RefDate3
		  ,Ref1
		  ,Ref2
		  ,Ref3
		  ,Project
		  ,Remarks
		  ,SysTotal
		  ,SysTotalLC
		  ,SysTotalFC
		  ,VersionNo
		  ,IsActive
		  ,CreatedByAppUserId
		  ,CreatedOn
		  ,ModifiedByAppUserId
		  ,ModifiedOn 
		  ) 
		  
		  select * from @jdt  
		  set @jdtID = @@IDENTITY  
		  if @jdtID > 0  
		  begin   
			update @jdtline set JournalEntryID = @jdtID   
			insert into JournalEntryDetail 
			select * from @jdtline  
			update Deposit  set TransAbs = @jdtID where DepositID = @docID  
		  end   
	  end

 set @error_code = -1  
 set @error_message = 'Please specify doctype'
end
go 
