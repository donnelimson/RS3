if exists(select 1 from sys.sysobjects where [name] = 'SP_GetTransactionJournalReport' and xtype = 'P')
	drop procedure SP_GetTransactionJournalReport
go

create procedure SP_GetTransactionJournalReport
@transType nvarchar(30) = ''
,@fromDate datetime = null
,@toDate datetime = null
,@docNum nvarchar(30)=''
as
begin 
	select 
		 JEN1.JournalEntryLineID
		,JEN1.JournalEntryID TransID
		,JEN1.ObjType
		,JEN1.BaseEntry
		,JEN1.BaseNum
		,JEN1.BaseType
		,JEN1.TargetEntry
		,JEN1.TargetNum
		,JEN1.TargetType
		,JEN1.TransType
		,JEN1.GLAcctCode
		,ACT.AcctName as GLAcctName
		,JEN1.LineID
		,JEN1.VisID
		,JEN1.FinancePeriod
		,JEN1.BatchNum
		,JEN1.ShortName
		,case when GLAcctCode = ShortName then  ACT.AcctName else CRD.CardName end as ShortNameDesc
		,JEN1.Debit
		,JEN1.Credit
		,JEN1.DebitSC
		,JEN1.CreditSC
		,JEN1.DebitFC
		,JEN1.CreditFC
		,JEN1.ContraAccount
		,JEN1.DebitCredit
		,JEN1.BaseAmt
		,JEN1.VatGroup
		,JEN1.VatPercent
		,JEN1.VatAmt
		,JEN1.RefDate
		,JEN1.RefDate2
		,JEN1.RefDate3
		,JEN1.CreatedBy
		,JEN1.BaseRef
		,JEN1.Ref1
		,JEN1.Ref2
		,JEN1.Ref3
		,JEN1.Project
		,JEN1.OcrCode
		,JEN1.OcrCode2
		,JEN1.OcrCode3
		,JEN1.OcrCode4
		,JEN1.OcrCode5
		,JEN1.LineRemarks
		,JEN1.VersionNo
		,JEN1.IsActive
		,JEN1.CreatedByAppUserId
		,JEN1.CreatedOn
		,JEN1.ModifiedByAppUserId
		,JEN1.ModifiedOn
	from JournalEntryDetail JEN1
	inner join JournalEntry JEN  on JEN1.JournalEntryID = JEN.JournalEntryID 
	outer apply (
	   select AcctCode, AcctName from GLAccount  where AcctCode = JEN1.GLAcctCode
	) ACT

	outer apply (
		select CardCode, CardName from BusinessPartner where CardCode = JEN1.ShortName
	) CRD

	where case when @fromDate is not null and @toDate is not null then 
	case when JEN.RefDate between @fromDate and @toDate then 1 else 0 end 
	else 1 end =1
	and case 
			when @transType  = 'ALL' then 1 
			when JEN.DocNum = @docNum then 1
			when JEN1.TransType = @transType then 1
			else 0
		end = 1 
end 

go 