
if exists(select 1 from sys.sysobjects where [name] = 'SP_GetPaymentLines' and xtype = 'P')
	drop procedure SP_GetPaymentLines
go 

create Procedure SP_GetPaymentLines
	@paymentID int null,
	@paymentType nvarchar(3)  null = ''
as
begin 
	
	if(@paymentType = 'PPY')
	begin 
		select * from
		( 
			select 
			 PYLine.OutgoingPaymentLineID
			,PYLine.PaymentID
			,PYLine.DocID
			,PYLine.DocNum
			,PYLine.SumApplied
			,PYLine.AppliedFC
			,PYLine.AppliedSys
			,PYLine.InvType
			,PYLine.DocRate
			,PYLine.Flags
			,PYLine.IntrsStat
			,PYLine.DocLine
			,PYLine.VatApplied
			,PYLine.VatAppldFC
			,PYLine.VatAppldSy
			,PYLine.SelfInv
			,PYLine.ObjType
			,PYLine.Dscount
			,PYLine.DscntSum
			,PYLine.DscntSumFC
			,PYLine.DscntSumSy
			,PYLine.BfDcntSum
			,PYLine.BfDcntSumF
			,PYLine.BfDcntSumS
			,PYLine.BfNetDcnt
			,PYLine.BfNetDcntF
			,PYLine.BfNetDcntS
			,PYLine.PaidSum
			,PYLine.ExpAppld
			,PYLine.ExpAppldFC
			,PYLine.ExpAppldSC
			,PYLine.Rounddiff
			,PYLine.RounddifFc
			,PYLine.RounddifSc
			,PYLine.InstId
			,PYLine.WtAppld
			,PYLine.WtAppldFC
			,PYLine.WtAppldSC
			,PYLine.LinkDate
			,PYLine.AmtDifPst
			,PYLine.PaidDpm
			,PYLine.DpmPosted
			,PYLine.ExpVatSum
			,PYLine.ExpVatSumF
			,PYLine.ExpVatSumS
			,PYLine.IsRateDiff
			,PYLine.WtInvCatS
			,PYLine.WtInvCatSF
			,PYLine.WtInvCatSS
			,PYLine.DocTransId
			,PYLine.MIEntry
			,PYLine.OcrCode
			,PYLine.OcrCode2
			,PYLine.OcrCode3
			,PYLine.OcrCode4
			,PYLine.OcrCode5
			,PYLine.WTOnHold
			,PYLine.WTOnhldPst
			,PYLine.BaseAbs
			,PYLine.MIType
			,PYLine.IsActive
			,PYLine.CreatedByAppUserId
			,PYLine.CreatedOn
			,PYLine.ModifiedByAppUserId
			,PYLine.ModifiedOn
			,A.DocDate
			,A.DocDueDate
			,A.DocTotal
			,A.PaidToDate
			,A.Remarks
			from OutgoingPaymentInvoice PYLine
			outer apply (
				select DocDate,DocDueDate, TaxDate , VatDate , VatSum ,DocTotal,PaidToDate , Remarks from  PurchaseInvoice where DocID = PYLine.DocID and PYLine.InvType = 'PIN'
				union all 
				select DocDate,DocDueDate, TaxDate , VatDate , VatSum ,DocTotal,PaidToDate , Remarks from  SalesCreditNote where DocID = PYLine.DocID and PYLine.InvType = 'SCR'
			) A
			where PYLine.PaymentID = @paymentID


		) PYLines
	end

	if(@paymentType = 'SPY')
	begin 
		select * from
		( 
			select 
			 PYLine.IncomingPaymentLineID
			,PYLine.PaymentID
			,PYLine.DocID
			,PYLine.DocNum
			,PYLine.SumApplied
			,PYLine.AppliedFC
			,PYLine.AppliedSys
			,PYLine.InvType
			,PYLine.DocRate
			,PYLine.Flags
			,PYLine.IntrsStat
			,PYLine.DocLine
			,PYLine.VatApplied
			,PYLine.VatAppldFC
			,PYLine.VatAppldSy
			,PYLine.SelfInv
			,PYLine.ObjType
			,PYLine.Dscount
			,PYLine.DscntSum
			,PYLine.DscntSumFC
			,PYLine.DscntSumSy
			,PYLine.BfDcntSum
			,PYLine.BfDcntSumF
			,PYLine.BfDcntSumS
			,PYLine.BfNetDcnt
			,PYLine.BfNetDcntF
			,PYLine.BfNetDcntS
			,PYLine.PaidSum
			,PYLine.ExpAppld
			,PYLine.ExpAppldFC
			,PYLine.ExpAppldSC
			,PYLine.Rounddiff
			,PYLine.RounddifFc
			,PYLine.RounddifSc
			,PYLine.InstId
			,PYLine.WtAppld
			,PYLine.WtAppldFC
			,PYLine.WtAppldSC
			,PYLine.LinkDate
			,PYLine.AmtDifPst
			,PYLine.PaidDpm
			,PYLine.DpmPosted
			,PYLine.ExpVatSum
			,PYLine.ExpVatSumF
			,PYLine.ExpVatSumS
			,PYLine.IsRateDiff
			,PYLine.WtInvCatS
			,PYLine.WtInvCatSF
			,PYLine.WtInvCatSS
			,PYLine.DocTransId
			,PYLine.MIEntry
			,PYLine.OcrCode
			,PYLine.OcrCode2
			,PYLine.OcrCode3
			,PYLine.OcrCode4
			,PYLine.OcrCode5
			,PYLine.WTOnHold
			,PYLine.WTOnhldPst
			,PYLine.BaseAbs
			,PYLine.MIType
			,PYLine.IsActive
			,PYLine.CreatedByAppUserId
			,PYLine.CreatedOn
			,PYLine.ModifiedByAppUserId
			,PYLine.ModifiedOn
			,A.DocDate
			,A.DocDueDate
			,A.DocTotal
			,A.PaidToDate
			,A.Remarks
			from IncomingPaymentInvoice PYLine
			outer apply (
				select DocDate,DocDueDate, TaxDate , VatDate , VatSum ,DocTotal,PaidToDate , Remarks from  SalesInvoice where DocID = PYLine.DocID and PYLine.InvType = 'SIN'
				union all 
				select DocDate,DocDueDate, TaxDate , VatDate , VatSum ,DocTotal,PaidToDate , Remarks from  PurchaseCreditNote where DocID = PYLine.DocID and PYLine.InvType = 'PCR'
			) A
			where PYLine.PaymentID = @paymentID


		) PYLines
	end

end
go 
--exec SP_GetPaymentLines 1, 'PPY'
