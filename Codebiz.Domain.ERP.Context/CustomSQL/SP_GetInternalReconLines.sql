
if exists(select 1 from sys.sysobjects where [name] = 'SP_GetInternalReconLines' and xtype = 'P')
	drop procedure SP_GetInternalReconLines
go 

create procedure SP_GetInternalReconLines
@reconID int null = 0
as 
begin
 select
  cast(ROW_NUMBER() over( order by ITR1.ReconLineID) as int) LineNum
 ,ITR1.ReconLineID
 ,ITR1.ReconID
 ,ITR1.ShortName
 ,ITR1.TransId
 ,ITR1.TransRowId
 ,ITR1.SrcObjTyp
 ,ITR1.SrcObjAbs
 ,(
 ITR1.ReconSum * 
	case 
		when SrcObjTyp = 'PPY' then 1 
		when SrcObjTyp = 'PIN' then -1 
		when SrcObjTyp = 'PCR' then  1 
		
		when SrcObjTyp = 'SPY' then -1 
		when SrcObjTyp = 'SIN' then 1 
		when SrcObjTyp = 'SCR' then -1 

		when SrcObjTyp in ('JEN', 'JVO') and JEN.DocTotal > 0 and JEN.CardType = 'S' then 1 --PPY/PCR
		when SrcObjTyp in ('JEN', 'JVO') and JEN.DocTotal < 0 and JEN.CardType = 'S' then -1 --PIN
		when SrcObjTyp in ('JEN', 'JVO') and JEN.DocTotal > 0 and JEN.CardType = 'C' then 1 --SIN
		when SrcObjTyp in ('JEN', 'JVO') and JEN.DocTotal < 0 and JEN.CardType = 'C' then -1 --SPY/SCR
	end ) as ReconSum
 ,ITR1.ReconSumFC
 ,ITR1.ReconSumSC
 ,ITR1.FrgnCurr
 ,ITR1.SumMthCurr
 ,ITR1.IsCredit
 ,ITR1.Account
 ,ITR1.CashDisSum
 ,ITR1.WTSum
 ,ITR1.WTSumFC
 ,ITR1.WTSumSC
 ,ITR1.ExpSum
 ,ITR1.ExpSumFC
 ,ITR1.ExpSumSC
 ,ITR1.NetBefDisc
 ,ITR1.LineNum
 ,JEN.TransType 
 ,JEN.DocNum
 ,JEN.DocTotal 
 ,JEN.Ref1
 ,JEN.Ref2
 ,JEN.Ref3
 ,JEN.RefDate DocDate
 ,JEN.RefDate2 DocDueDate
 ,JEN.RefDate3 DocumentDate
 ,JEN.CardName ShortNameDesc
 
 from InternalReconcilationDetail ITR1
 cross apply (
  select 
  GLAcctCode
  ,ShortName
  ,(IsNull(JEN1.Debit, 0) - IsNull(JEN1.Credit, 0)) DocTotal
  ,JEN1.TransType
  ,DocNum
  ,JEN1.Ref1
  ,JEN1.Ref2
  ,JEN1.Ref3
  ,JEN1.RefDate
  ,JEN1.RefDate2
  ,JEN1.RefDate3
  ,CRD.CardName
  ,CRD.CardType
  from JournalEntryDetail JEN1 
  inner join JournalEntry JEN on JEN1.JournalEntryID = JEN.JournalEntryID 
  inner join BusinessPartner CRD on JEN1.ShortName = CRD.CardCode 
  where JEN1.JournalEntryID =ITR1.TransId and JEN1.ShortName = ITR1.ShortName 
 ) JEN
 where ITR1.ReconID = @reconID
 --exec GetInternalReconLines @reconID = 6
end 
go