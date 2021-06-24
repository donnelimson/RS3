
if exists (select 1 from sys.sysobjects where [name] = 'SP_GetOpenInvoiceEx')
	drop procedure SP_GetOpenInvoiceEx
 go 
Create Procedure SP_GetOpenInvoiceEx
@cardCode nvarchar(30) = ''
,@docType nvarchar(3) = ''
as 
begin 
	select 
			 cast( 0 as bigint) PaymentLineID
			,cast( 0 as bigint) PaymentID
			,DocID
			,DocNum
			,DocDate
			,DocDueDate
			,TaxDate
			,DocTotal
			,Remarks
			,SumApplied
			,AppliedFC
			,AppliedSys
			,InvType
			,cast( 0 as decimal) DocRate
			,null Flags
			,null IntrsStat
			,cast( ROW_NUMBER() over(order by DocDate ) as int) DocLine
			,VatApplied
			,VatAppldFC
			,VatAppldSy
			,SelfInv
			,ObjType
			,Dscount
			,DscntSum
			,DscntSumFC
			,DscntSumSy
			,BfDcntSum
			,BfDcntSumF
			,BfDcntSumS
			,BfNetDcnt
			,BfNetDcntF
			,BfNetDcntS
			,PaidSum
			,cast( 0 as bigint) ExpAppld
			,ExpAppldFC
			,ExpAppldSC
			,Rounddiff
			,RounddifFc
			,RounddifSc
			,InstId
			,WtAppld
			,WtAppldFC
			,WtAppldSC
			,null LinkDate
			,null AmtDifPst
			,PaidDpm
			,DpmPosted
			,ExpVatSum
			,ExpVatSumF
			,ExpVatSumS
			,IsRateDiff
			,WtInvCatS
			,WtInvCatSF
			,WtInvCatSS
			,DocTransId
			,null MIEntry
			,OcrCode
			,OcrCode2
			,OcrCode3
			,OcrCode4
			,OcrCode5
			,WTOnHold
			,WTOnhldPst
			,BaseAbs
			,null MIType
			,cast(1 as bit) IsActive
			,CreatedByAppUserId
			,CreatedOn
			,ModifiedByAppUserId
			,ModifiedOn 
		from 
			(
			 select  DocID 
					,DocNum 
					,DocDate 
					,DocDueDate 
					,VatDate 
					,TaxDate 
					,0 DaysOverdue 
					,case 
						when ObjType ='PIN'  then DocTotal
						when ObjType ='PCR'  then DocTotal * -1 
					 end as DocTotal 
					,case 
						when ObjType ='PIN'  then DocTotal - Isnull(R1.ReconSum ,0)
						when ObjType ='PCR'  then DocTotal * -1 
					 end as SumApplied 
					,case 
						when ObjType ='PIN'  then DocTotal - Isnull(R1.ReconSum ,0)
						when ObjType ='PCR'  then DocTotal * -1 
					 end BalanceDue 
					,Remarks 
					,0.0 AppliedFC 
					,0.0 AppliedSys 
					,ObjType  InvType 
					,0.0 DocRate 
					,0 DocLine 
					,0.0 VatApplied 
					,0.0 VatAppldFC 
					,0.0 VatAppldSy 
					,P.IsSelfInv SelfInv 
					,@docType ObjType 
					,0.0 Dscount 
					,0.0 DscntSum 
					,0.0 DscntSumFC 
					,0.0 DscntSumSy 
					,0.0 BfDcntSum 
					,0.0 BfDcntSumF 
					,0.0 BfDcntSumS 
					,0.0 BfNetDcnt 
					,0.0 BfNetDcntF 
					,0.0 BfNetDcntS 
					,0.0 PaidSum 
					,0.0 ExpAppld 
					,0.0 ExpAppldFC 
					,0.0 ExpAppldSC 
					,0.0 Rounddiff 
					,0.0 RounddifFc 
					,0.0 RounddifSc 
					,1 InstId 
					,0.0 WtAppld 
					,0.0 WtAppldFC 
					,0.0 WtAppldSC 
					,0.0 PaidDpm 
					,'N' DpmPosted 
					,0.0 ExpVatSum 
					,0.0 ExpVatSumF 
					,0.0 ExpVatSumS 
					,'N' IsRateDiff 
					,0.0 WtInvCatS 
					,0.0 WtInvCatSF 
					,0.0 WtInvCatSS 
					,cast(0 as bigint) DocTransId 
					,'' OcrCode 
					,'' OcrCode2 
					,'' OcrCode3 
					,'' OcrCode4 
					,'' OcrCode5 
					,'' WTOnHold 
					,'' WTOnhldPst 
					,P.DocID BaseAbs 
					,P.CreatedByAppUserId
					,P.CreatedOn
					,P.ModifiedByAppUserId
					,P.ModifiedOn 
 
			from 
				PurchaseInvoice P
			outer apply(
				select Abs(Sum(ReconSum)) as ReconSum
				from 
					InternalReconcilationDetail R1 
				where R1.SrcObjAbs = P.DocID and R1.SrcObjTyp = P.ObjType 
				group by R1.SrcObjAbs , R1.SrcObjTyp 
			) R1
			
			where 
				CardCode = @cardCode and P.DocTotal - Isnull(R1.ReconSum,0) > 0
	
			union all

			select DocID 
					,DocNum 
					,DocDate 
					,DocDueDate 
					,VatDate 
					,TaxDate 
					,0 DaysOverdue 
					,case 
						when ObjType ='PIN'  then DocTotal 
						when ObjType ='PCR'  then DocTotal * -1 
					 end as DocTotal
					,case 
						when ObjType ='PIN'  then DocTotal - Isnull(R1.ReconSum,0)
						when ObjType ='PCR'  then (DocTotal - Isnull(R1.ReconSum,0) )* -1 
					 end as  SumApplied 
					,case 
						when ObjType ='PIN'  then DocTotal - Isnull(R1.ReconSum,0)
						when ObjType ='PCR'  then (DocTotal - Isnull(R1.ReconSum,0) )* -1 
					 end BalanceDue 
					,Remarks 
					,0 AppliedFC 
					,0 AppliedSys 
					,ObjType InvType 
					,0 DocRate 
					,0 DocLine 
					,0 VatApplied 
					,0 VatAppldFC 
					,0 VatAppldSy 
					,P.IsSelfInv SelfInv 
					,@docType ObjType 
					,0 Dscount 
					,0 DscntSum 
					,0 DscntSumFC 
					,0 DscntSumSy 
					,0 BfDcntSum 
					,0 BfDcntSumF 
					,0 BfDcntSumS 
					,0 BfNetDcnt 
					,0 BfNetDcntF 
					,0 BfNetDcntS 
					,0 PaidSum 
					,0 ExpAppld 
					,0 ExpAppldFC 
					,0 ExpAppldSC 
					,0 Rounddiff 
					,0 RounddifFc 
					,0 RounddifSc 
					,1 InstId 
					,0 WtAppld 
					,0 WtAppldFC 
					,0 WtAppldSC 
					,0 PaidDpm 
					,'N' DpmPosted 
					,0 ExpVatSum 
					,0 ExpVatSumF 
					,0 ExpVatSumS 
					,'N' IsRateDiff 
					,0 WtInvCatS 
					,0 WtInvCatSF 
					,0 WtInvCatSS 
					,0 DocTransId 
					,'' OcrCode 
					,'' OcrCode2 
					,'' OcrCode3 
					,'' OcrCode4 
					,'' OcrCode5 
					,'' WTOnHold 
					,'' WTOnhldPst 
					,P.DocID BaseAbs 
					,P.CreatedByAppUserId
					,P.CreatedOn
					,P.ModifiedByAppUserId
					,P.ModifiedOn 
  
				from PurchaseCreditNote P 
					outer apply(
				select Abs(Sum(ReconSum)) as ReconSum
				from 
					InternalReconcilationDetail R1 
				where R1.SrcObjAbs = P.DocID and R1.SrcObjTyp = P.ObjType 
				group by R1.SrcObjAbs , R1.SrcObjTyp 
			) R1
				where CardCode = @cardCode and P.DocTotal - Isnull(R1.ReconSum,0) > 0


			union all 
			 select  DocID 
					,DocNum 
					,DocDate 
					,DocDueDate 
					,VatDate 
					,TaxDate 
					,0 DaysOverdue 
					,case 
						when ObjType ='SIN'  then DocTotal
						when ObjType ='SCR'  then DocTotal * -1 
					 end as DocTotal 
					,case 
						when ObjType ='SIN'  then DocTotal - Isnull(R1.ReconSum ,0)
						when ObjType ='SCR'  then DocTotal * -1 
					 end as SumApplied 
					,case 
						when ObjType ='SIN'  then DocTotal - Isnull(R1.ReconSum ,0)
						when ObjType ='SCR'  then DocTotal * -1 
					 end BalanceDue 
					,Remarks 
					,0.0 AppliedFC 
					,0.0 AppliedSys 
					,ObjType  InvType 
					,0.0 DocRate 
					,0 DocLine 
					,0.0 VatApplied 
					,0.0 VatAppldFC 
					,0.0 VatAppldSy 
					,S.IsSelfInv SelfInv 
					,@docType ObjType 
					,0.0 Dscount 
					,0.0 DscntSum 
					,0.0 DscntSumFC 
					,0.0 DscntSumSy 
					,0.0 BfDcntSum 
					,0.0 BfDcntSumF 
					,0.0 BfDcntSumS 
					,0.0 BfNetDcnt 
					,0.0 BfNetDcntF 
					,0.0 BfNetDcntS 
					,0.0 PaidSum 
					,0.0 ExpAppld 
					,0.0 ExpAppldFC 
					,0.0 ExpAppldSC 
					,0.0 Rounddiff 
					,0.0 RounddifFc 
					,0.0 RounddifSc 
					,1 InstId 
					,0.0 WtAppld 
					,0.0 WtAppldFC 
					,0.0 WtAppldSC 
					,0.0 PaidDpm 
					,'N' DpmPosted 
					,0.0 ExpVatSum 
					,0.0 ExpVatSumF 
					,0.0 ExpVatSumS 
					,'N' IsRateDiff 
					,0.0 WtInvCatS 
					,0.0 WtInvCatSF 
					,0.0 WtInvCatSS 
					,cast(0 as bigint) DocTransId 
					,'' OcrCode 
					,'' OcrCode2 
					,'' OcrCode3 
					,'' OcrCode4 
					,'' OcrCode5 
					,'' WTOnHold 
					,'' WTOnhldPst 
					,S.DocID BaseAbs 
					,S.CreatedByAppUserId
					,S.CreatedOn
					,S.ModifiedByAppUserId
					,S.ModifiedOn 
 
			from 
				SalesInvoice S
			outer apply(
				select Abs(Sum(ReconSum)) as ReconSum
				from 
					InternalReconcilationDetail R1 
				where R1.SrcObjAbs = S.DocID and R1.SrcObjTyp = S.ObjType
				group by R1.SrcObjAbs , R1.SrcObjTyp 
			) R1
			
			where 
				(CardCode = @cardCode or ParentCard = @cardCode) and S.DocTotal - Isnull(R1.ReconSum,0) > 0 and S.DocStatus='O'

			

			union all 
			 select  DocID 
					,DocNum 
					,DocDate 
					,DocDueDate 
					,VatDate 
					,TaxDate 
					,0 DaysOverdue 
					,case 
						when ObjType ='SIN'  then DocTotal
						when ObjType ='SCR'  then DocTotal * -1 
					 end as DocTotal 
					,case 
						when ObjType ='SIN'  then DocTotal - Isnull(R1.ReconSum ,0)
						when ObjType ='SCR'  then DocTotal * -1 
					 end as SumApplied 
					,case 
						when ObjType ='SIN'  then DocTotal - Isnull(R1.ReconSum ,0)
						when ObjType ='SCR'  then DocTotal * -1 
					 end BalanceDue 
					,Remarks 
					,0.0 AppliedFC 
					,0.0 AppliedSys 
					,ObjType  InvType 
					,0.0 DocRate 
					,0 DocLine 
					,0.0 VatApplied 
					,0.0 VatAppldFC 
					,0.0 VatAppldSy 
					,S.IsSelfInv SelfInv 
					,@docType ObjType 
					,0.0 Dscount 
					,0.0 DscntSum 
					,0.0 DscntSumFC 
					,0.0 DscntSumSy 
					,0.0 BfDcntSum 
					,0.0 BfDcntSumF 
					,0.0 BfDcntSumS 
					,0.0 BfNetDcnt 
					,0.0 BfNetDcntF 
					,0.0 BfNetDcntS 
					,0.0 PaidSum 
					,0.0 ExpAppld 
					,0.0 ExpAppldFC 
					,0.0 ExpAppldSC 
					,0.0 Rounddiff 
					,0.0 RounddifFc 
					,0.0 RounddifSc 
					,1 InstId 
					,0.0 WtAppld 
					,0.0 WtAppldFC 
					,0.0 WtAppldSC 
					,0.0 PaidDpm 
					,'N' DpmPosted 
					,0.0 ExpVatSum 
					,0.0 ExpVatSumF 
					,0.0 ExpVatSumS 
					,'N' IsRateDiff 
					,0.0 WtInvCatS 
					,0.0 WtInvCatSF 
					,0.0 WtInvCatSS 
					,cast(0 as bigint) DocTransId 
					,'' OcrCode 
					,'' OcrCode2 
					,'' OcrCode3 
					,'' OcrCode4 
					,'' OcrCode5 
					,'' WTOnHold 
					,'' WTOnhldPst 
					,S.DocID BaseAbs 
					,S.CreatedByAppUserId
					,S.CreatedOn
					,S.ModifiedByAppUserId
					,S.ModifiedOn 
 
			from 
				SalesCreditNote S
			outer apply(
				select Abs(Sum(ReconSum)) as ReconSum
				from 
					InternalReconcilationDetail R1 
				where R1.SrcObjAbs = S.DocID and R1.SrcObjTyp = S.ObjType 
				group by R1.SrcObjAbs , R1.SrcObjTyp 
			) R1
			
			where 
				CardCode = @cardCode and S.DocTotal - Isnull(R1.ReconSum,0) > 0


			union all 
			
			 select  MIN(JEN.JournalEntryID) DocID
					,MIN(JEN.DocNum) DocNum
					,MIN(JEN.RefDate)  DocDate 
					,MIN(JEN.RefDate2) DocDueDate 
					,MIN(JEN.RefDate3) VatDate 
					,MIN(JEN.RefDate3) TaxDate 
					,0 DaysOverdue 
					,CASE when SUM(R1.Discount)!=0
					then
				   SUM((Isnull(JEN1.Debit,0) - Isnull(JEN1.Credit,0)) * 
					case 
						when @docType = 'PPY' then -1 
						when @docType = 'SPY' then 1
						else 1 end) - Isnull(SUM(R1.Discount)*-1,0) + SUM(R1.Discount)
					else
					SUM((Isnull(JEN1.Debit,0) - Isnull(JEN1.Credit,0)) * 
					case 
						when @docType = 'PPY' then -1 
						when @docType = 'SPY' then 1
						else 1 end) - Isnull(SUM(R1.Discount)*-1,0)
					end  as DocTotal
					,0 SumApplied 
					,CASE when SUM(R1.Discount)=0
					then
					SUM(((Isnull(JEN1.Debit,0) - Isnull(JEN1.Credit,0))  * 
					case 
						when @docType = 'PPY' and (Isnull(JEN1.Debit,0) - Isnull(JEN1.Credit,0)) > 0 then 1
						when @docType = 'SPY' and (Isnull(JEN1.Debit,0) - Isnull(JEN1.Credit,0)) < 0 then -1
						else 1 end) - Isnull(R1.ReconSum ,0))
					when SUM(R1.Discount)!=0  then 
					SUM(R1.Discount) else
							SUM(((Isnull(JEN1.Debit,0) - Isnull(JEN1.Credit,0))  * 
					case 
						when @docType = 'PPY' and (Isnull(JEN1.Debit,0) - Isnull(JEN1.Credit,0)) > 0 then 1
						when @docType = 'SPY' and (Isnull(JEN1.Debit,0) - Isnull(JEN1.Credit,0)) < 0 then -1
						else 1 end) - Isnull(R1.ReconSum ,0))
					end as BalanceDue 
					,JEN.Remarks  Remarks 
					,0.0 AppliedFC 
					,0.0 AppliedSys 
					,JEN1.TransType  InvType 
					,0.0 DocRate 
					,0 DocLine 
					,CASE when (select sum(VatAmt) from InternalReconcilationDetail where TransId=MAX(JEN.JournalEntryID) and ShortName!=@cardCode and IsCredit='D')!=0 then
					(select sum(VatAmt) from InternalReconcilationDetail where TransId=MAX(JEN.JournalEntryID) and ShortName!=@cardCode and IsCredit='D')
					else (select sum(vatamt) from JournalEntryDetail where JournalEntryID=MIN(JEN.JournalEntryID) and ShortName=@cardCode  and ref4 is null)
					end as VatAppld 
					,0.0 VatAppldFC 
					,0.0 VatAppldSy 
					,'N' SelfInv 
					,'PPY' ObjType 
					,0.0 Dscount 
					,0.0 DscntSum 
					,0.0 DscntSumFC 
					,0.0 DscntSumSy 
					,0.0 BfDcntSum 
					,0.0 BfDcntSumF 
					,0.0 BfDcntSumS 
					,0.0 BfNetDcnt 
					,0.0 BfNetDcntF 
					,0.0 BfNetDcntS 
					,0.0 PaidSum 
					,0.0 ExpAppld 
					,0.0 ExpAppldFC 
					,0.0 ExpAppldSC 
					,0.0 Rounddiff 
					,0.0 RounddifFc 
					,0.0 RounddifSc 
					,1 InstId 
					,CASE when (select sum(wtsum) from InternalReconcilationDetail where TransId=MAX(JEN.JournalEntryID) and ShortName!=@cardCode and IsCredit='D')!=0 then
					(select sum(wtsum) from InternalReconcilationDetail where TransId=MAX(JEN.JournalEntryID) and ShortName!=@cardCode and IsCredit='D')
					else (select sum(debit) from JournalEntryDetail where JournalEntryID=MIN(JEN.JournalEntryID) and ShortName!=@cardCode and Ref4='WT')
					end as WtAppld 
					,0.0 WtAppldFC 
					,0.0 WtAppldSC 
					,0.0 PaidDpm 
					,'N' DpmPosted 
					,0.0 ExpVatSum 
					,0.0 ExpVatSumF 
					,0.0 ExpVatSumS 
					,'N' IsRateDiff 
					,0.0 WtInvCatS 
					,0.0 WtInvCatSF 
					,0.0 WtInvCatSS 
					,MAX(JEN.Ref3) DocTransId 
					,'' OcrCode 
					,'' OcrCode2 
					,'' OcrCode3 
					,'' OcrCode4 
					,'' OcrCode5 
					,'' WTOnHold 
					,'' WTOnhldPst 
					,0 BaseAbs 
					,CASE when (select JE.CreatedByAppUserId from InternalReconcilation R inner join JournalEntry JE on R.ReconJEId=JE.JournalEntryID)!=null
					then
					 (select JE.CreatedByAppUserId from InternalReconcilation R inner join JournalEntry JE on R.ReconJEId=JE.JournalEntryID)
					else 
					 MIN(JEN.CreatedByAppUserId)
					 end CreatedByAppUserId
					,CASE when (select JE.CreatedOn from InternalReconcilation R inner join JournalEntry JE on R.ReconJEId=JE.JournalEntryID)!=null
					then
					 (select JE.CreatedOn from InternalReconcilation R inner join JournalEntry JE on R.ReconJEId=JE.JournalEntryID)
					else 
					 MIN(JEN.CreatedOn)
					 end CreatedOn
					,CASE when (select JE.ModifiedByAppUserId from InternalReconcilation R inner join JournalEntry JE on R.ReconJEId=JE.JournalEntryID)!=null
					then
					 (select JE.ModifiedByAppUserId from InternalReconcilation R inner join JournalEntry JE on R.ReconJEId=JE.JournalEntryID)
					else 
					 MIN(JEN.ModifiedByAppUserId)
					 end ModifiedByAppUserId
					,CASE when (select JE.ModifiedOn from InternalReconcilation R inner join JournalEntry JE on R.ReconJEId=JE.JournalEntryID)!=null
					then
					 (select JE.ModifiedOn from InternalReconcilation R inner join JournalEntry JE on R.ReconJEId=JE.JournalEntryID)
					else 
					 MIN(JEN.ModifiedOn)
					 end ModifiedOn
 
			from 
				JournalEntry JEN
				inner join JournalEntryDetail JEN1 on JEN1.JournalEntryID = JEN.JournalEntryID 
			outer apply(
				select Abs(Sum(ReconSum)) as ReconSum, Abs(Sum(CashDisSum)) as Discount, R.ReconJEId, JEN.DocNum, R1.TransId
				from 
					InternalReconcilationDetail R1 inner join InternalReconcilation R on R.ReconID = R1.ReconID
				--where R1.SrcObjAbs = JEN1.JournalEntryID  and R1.SrcObjTyp = JEN1.ObjType 
				where R1.TransId  = JEN1.JournalEntryID and R1.TransRowId  = JEN1.LineID  and R1.ShortName= JEN1.ShortName
				group by R1.SrcObjAbs,  R1.SrcObjTyp,R.ReconJEId, R1.TransId
			) R1

			where 
				ShortName = @cardCode 
				and ((Isnull(JEN1.Debit,0) - Isnull(JEN1.Credit,0)) * 
					case 
						when @docType = 'PPY' then -1 
						when @docType = 'SPY' then 1
						else 1 end  - Isnull(R1.ReconSum ,0)) <> 0 
				and (JEN1.TransType in ('JVO', 'JEN', 'PPY', 'SPY', 'EB')) 
				group by JEN1.RefDate,JEN1.RefDate2,JEN1.RefDate3,
				JEN.Remarks, JEN1.TransType
			) A 
			where BalanceDue  <> 0
end 
