
if exists(select 1 from sys.sysobjects where [name] = 'SP_GetOpenTransactionForRecon' and xtype = 'P')
	drop procedure SP_GetOpenTransactionForRecon
go

create procedure SP_GetOpenTransactionForRecon
	@acctCode nvarchar(30) = '',
	@dateType nchar  = null,
	@fromDate datetime = null,
	@toDate datetime = null
as
begin 
--exec SP_GetOpenTransactionForRecon @acctCode = '12110310000', @dateType = 'P' , @fromDate = '2021-01-01', @toDate = '2021-12-31'
	select  
		 cast(A.RowNum as int)   LineNum
		,A.ShortName   ShortName
		,A.ShortNameDesc  ShortNameDesc
		,cast(A.JournalEntryID as bigint) TransId
		,cast(A.LineID as bigint)  TransRowId
		,A.TransType    SrcObjTyp
		,cast(A.CreatedBy as bigint) SrcObjAbs
		,Amount DocTotal
		,Balance BalanceDue
		,ReconSum  ReconSum
		,RefDate DocDate 
		,RefDate2 DocDueDate 
		,RefDate2  DocumentDate 
		,Ref1 Ref1
		,Ref2 Ref2
		,Ref3 Ref3
		,LineRemarks  Details 
		,0.0  ReconSumFC
		,0.0  ReconSumSC
		,''  FrgnCurr
		,0.0  SumMthCurr
		,case when ReconSum > 0 then 'D' else 'C' end   IsCredit
		,A.GLAcctCode   Account
		,0.0  CashDisSum
		,0.0  WTSum
		,0.0  WTSumFC
		,0.0  WTSumSC
		,0.0  ExpSum
		,0.0  ExpSumFC
		,0.0  ExpSumSC
		,0.0  NetBefDisc
		

	from (
			select
				 ROW_NUMBER() over( order by JournalEntryLineID) RowNum
				,JournalEntryLineID
				,JEN1.JournalEntryID
				,JEN1.ObjType
				,JEN1.BaseEntry
				,JEN1.BaseNum
				,JEN1.BaseType
				,JEN1.TargetEntry
				,JEN1.TargetNum
				,JEN1.TargetType
				,JEN1.TransType
				,JEN1.GLAcctCode
				,JEN1.LineID
				,JEN1.VisID
				,JEN1.FinancePeriod
				,JEN1.BatchNum
				,JEN1.ShortName
				,ACT.AcctName  ShortNameDesc
				,(Isnull(JEN1.Debit, 0)-Isnull(JEN1.Credit,0)) as  Amount
				,((Isnull(JEN1.Debit, 0)-Isnull(JEN1.Credit,0)) - Isnull(R1.ReconSum , 0) * 
					case  
						when transtype = 'PIN' then -1 
						when transtype = 'PPY' then 1 
						when transtype = 'PCR' then 1 

						when transtype = 'SIN' then 1 
						when transtype = 'SCR' then -1 
						when transtype = 'SPY' then -1 
						
						when transtype in ('JEN', 'JVO', 'SPY', 'PPY') then 1
						else 0
					end
				) as  Balance
				,((Isnull(JEN1.Debit, 0)-Isnull(JEN1.Credit,0)) - Isnull(R1.ReconSum , 0) * 
					case  
						when transtype = 'PIN' then -1 
						when transtype = 'PPY' then 1 
						when transtype = 'PCR' then 1 
						when transtype = 'SCR' then -1 
						when transtype = 'SPY' then -1 
						when transtype in ('JEN', 'JVO', 'SPY', 'PPY') then 1
						else 0
					end
				) as  ReconSum
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
			from 
				JournalEntryDetail JEN1
				cross apply(
					select AcctCode , AcctName from GLAccount  where AcctCode  = JEN1.ShortName 
				) ACT
				outer apply(
						select Abs(Sum(ReconSum)) as ReconSum
						from 
							InternalReconcilationDetail R1 
						where 
							R1.TransId = JEN1.JournalEntryID and R1.TransRowId = JEN1.LineID 
						group by 
							R1.SrcObjAbs , R1.SrcObjTyp 
				) R1

			where 
			case 
				when @acctCode = '' then 1 
				when @acctCode <> ''  and JEN1.ShortName = @acctCode then 1 
				else 0 
			end = 1
			and 
				case 
					when @dateType is null or @dateType = '' then 1 
					when @dateType = 'P' and RefDate between @fromDate and @toDate  then 1  -- posting date 
					when @dateType = 'D' and RefDate2 between @fromDate and @toDate  then 1 -- due date 
					when @dateType = 'O' and RefDate2 between @fromDate and @toDate  then 1 -- doc date 
					else 0
				end = 1 
) A

where Balance  <> 0
end 
go 


