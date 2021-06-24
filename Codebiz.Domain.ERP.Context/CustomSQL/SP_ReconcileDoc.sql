/*

0=Manual(MA), 
1=Automatic(AU), 
2=Semi-Automatic (SA), 
3=Payment(PY), 
4=Credit Memo(CR), 
5=Reversal(RE), 
6=Zero Value(ZE), 
7=Cancellation(CA), 
8=BoE(BE)
9=Deposit(DE)
10=Bank Statement Processing(BS), 
11=Period Closing(PC), 
12=Correction Invoice(CI), 
13=Inventory/Expense Allocation(AL), 
14=WIP(WIP), 
15=Deferred Tax Interim Account(DI), 
16=Down Payment Allocation(DP), 
17=Auto. Conversion Difference(CD), 
18=Interim Document(ID), 
	
*/


if exists( select 1 from sys.sysobjects where [name] = 'SP_ReconcileDoc' and xtype = 'P')
	drop procedure SP_ReconcileDoc
go 

Create Procedure SP_ReconcileDoc
@docID int null,
@docType nvarchar(3) null,
@docDate datetime null,
@createdBy int null = 0
as
begin 

set nocount on;

DECLARE @ErrorMessage NVARCHAR(4000);
DECLARE @ErrorSeverity INT;
DECLARE @ErrorState INT;

declare @reconID bigint = 0,
		@reconSum decimal = 0


declare @itr table (
	 ReconID	bigint null
	,IsCard	nchar(1) null
	,ReconType	nvarchar(3)  null
	,ReconDate	datetime  null
	,Total	decimal  null
	,ReconCurr	nvarchar(3)  null
	,Canceled	nchar(1) null
	,CancelAbs	bigint null
	,IsSystem	nchar  null
	,InitObjTyp	nvarchar(3)  null
	,InitObjAbs	bigint  null
	,ReconRule1	nvarchar(50)  null
	,ReconRule2	nvarchar(50)  null
	,ReconRule3	nvarchar(50)  null
	,IsMultiBP	nchar(1)  null
	,OldMatNum	bigint null
	,ReconJEId	bigint null
	,BuildDesc	nvarchar(50) null
	,BPLId	bigint null
	,BPLName	nvarchar(50) null
	,VATRegNum	nvarchar(50) null
)

declare @itr1 table (
	 ReconLineID	bigint null
	,ReconID	bigint null
	,LineNum	int null
	,ShortName	nvarchar(30) null
	,TransId	bigint null
	,TransRowId	bigint null
	,SrcObjTyp	nvarchar(3)
	,SrcObjAbs	bigint null
	,ReconSum	decimal null
	,ReconSumFC	decimal null
	,ReconSumSC	decimal null
	,FrgnCurr	nvarchar(3) null
	,SumMthCurr	decimal null
	,IsCredit	nvarchar(1) null
	,Account	nvarchar(30) null
	,CashDisSum	decimal null
	,WTSum	decimal null
	,WTSumFC	decimal null
	,WTSumSC	decimal null
	,ExpSum	decimal null
	,ExpSumFC	decimal null
	,ExpSumSC	decimal null
	,NetBefDisc	decimal null
)

if @DocType = 'PPY' 
	begin 
	
	begin tran tr_1;

		begin try 
			insert into @itr1(
					ReconID	
					,LineNum	
					,ShortName	
					,TransId	
					,TransRowId	
					,SrcObjTyp	
					,SrcObjAbs	
					,ReconSum	
					,ReconSumFC	
					,ReconSumSC	
					,FrgnCurr	
					,SumMthCurr	
					,IsCredit	
					,Account	
					,CashDisSum	
					,WTSum	
					,WTSumFC	
					,WTSumSC	
					,ExpSum	
					,ExpSumFC	
					,ExpSumSC	
					,NetBefDisc	
				)

			select  *
			from (
				select     --Payment/Debit
					null ReconID	
				,null LineNum	
				,PPY.CardCode ShortName	
				,PPY.TransID TransId	
				,JEN.LineID TransRowId	
				,PPY.ObjType  SrcObjTyp	
				,PPY.PaymentID  SrcObjAbs	
				,(PPY.DocTotal - Isnull(PPY.NoDocSum,0))  ReconSum	
				--,(PPY.DocTotal)  ReconSum
				,0 ReconSumFC	
				,0 ReconSumSC	
				,null FrgnCurr	
				,null SumMthCurr	
				,'D' IsCredit	
				,BP.CtlDebCredPayAcct Account	
				,0 CashDisSum	
				,0 WTSum	
				,0 WTSumFC	
				,0 WTSumSC	
				,0 ExpSum	
				,0 ExpSumFC	
				,0 ExpSumSC	
				,0 NetBefDisc	 
				from 
					OutgoingPayment PPY
						cross apply ( 
							select * from BusinessPartner where CardCode  = PPY.CardCode
							) BP
						cross apply (
							select * from JournalEntryDetail JEN1 where JEN1.JournalEntryID = PPY.TransID and JEN1.ShortName = PPy.CardCode 
						) JEN
				where 
					PPY.PaymentID = @DocID 

				union all 
				select      --Invoice/Credit
				 null ReconID	
				,null LineNum	
				,P.CardCode ShortName	
				,P.TransID  TransId	
				,P.LineID  TransRowId	
				,PPY1.InvType  SrcObjTyp	
				,PPY1.DocID  SrcObjAbs	
				,case 
				  when PPY1.InvType  = 'PIN' then PPY1.SumApplied * -1
				  when PPY1.InvType  = 'PCR' then PPY1.SumApplied * -1
				  when PPY1.InvType  in ('JVO', 'JEN', 'SPY', 'PPY') then PPY1.SumApplied * 
					case 
						when @docType = 'PPY' and P.DocTotal  > 0 then -1 
						when @docType = 'PPY' and P.DocTotal  < 0 then 1 
					end 
				 end ReconSum	
				,0 ReconSumFC	
				,0 ReconSumSC	
				,null FrgnCurr	
				,null SumMthCurr	
				,case 
					when PPY1.InvType = 'PIN' then 'C' 
					when PPY1.InvType = 'PCR' then 'D' 
					when PPY1.InvType in ('JVO', 'JEN', 'SPY', 'PPY') and P.DocTotal > 0  then 'D' 
					when PPY1.InvType in ('JVO', 'JEN', 'SPY', 'PPY') and P.DocTotal < 0  then 'C' 
				 end IsCredit	
				,BP.CtlDebCredPayAcct Account	
				,0 CashDisSum	
				,0 WTSum	
				,0 WTSumFC	
				,0 WTSumSC	
				,0 ExpSum	
				,0 ExpSumFC	
				,0 ExpSumSC	
				,0 NetBefDisc	 
				from 
					OutgoingPayment PPY
					inner join OutgoingPaymentInvoice PPY1 on PPY.PaymentID = PPY1.PaymentID 
					outer apply(
						select P.CardCode ,P.TransID ,(P.DocTotal* -1) as DocTotal ,P.ObjType ,JEN1.LineID  from 
							PurchaseInvoice P 
							left join JournalEntryDetail JEN1 on P.TransID = JournalEntryID and JEN1.ShortName = P.CardCode 
						where 
							P.DocID = PPY1.DocID  and P.ObjType = PPY1.InvType 
						
						union all
						
						select P.CardCode , P.TransID,P.DocTotal,P.ObjType ,JEN1.LineID  from 
							PurchaseCreditNote P 
							left join JournalEntryDetail JEN1 on P.TransID = JournalEntryID and JEN1.ShortName = P.CardCode 
						where P.DocID = PPY1.DocID  and P.ObjType = PPY1.InvType 

						union all 
						select  
							JEN1.ShortName CardCode , JEN1.JournalEntryID  TransID,(Isnull(JEN1.Debit,0) - Isnull(JEN1.Credit,0)) DocTotal,JEN1.TransType ObjType ,JEN1.LineID  
						from 
							JournalEntryDetail JEN1
						where JEN1.TransType  in('JVO', 'JEN', 'PPY', 'SPY') and JEN1.JournalEntryID = PPY1.DocID  and JEN1.ShortName = PPY.CardCode 
					) P 

					cross apply ( 
						select * from BusinessPartner where CardCode  = PPY.CardCode
					) BP
				where 
					PPY.PaymentID = @DocID 
			) A
			
			insert into @itr(
				IsCard	
				,ReconType	
				,ReconDate
				,Total	
				,ReconCurr	
				,Canceled	
				,CancelAbs	
				,IsSystem	
				,InitObjTyp	
				,InitObjAbs	
				,ReconRule1	
				,ReconRule2	
				,ReconRule3	
				,IsMultiBP	
				,OldMatNum	
				,ReconJEId	
				,BuildDesc	
				,BPLId	
				,BPLName	
				,VATRegNum	
			)

			select 
				 'Y' IsCard	 
				,'PY' ReconType -- Payment
				,@DocDate ReconDate 
				,PPY.DocTotal Total	
				,null ReconCurr	
				,null Canceled	
				,null CancelAbs	
				,'Y' IsSystem	
				,PPY.ObjType InitObjTyp	
				,PPY.PaymentID  InitObjAbs	
				,null ReconRule1	
				,null ReconRule2	
				,null ReconRule3	
				,'N' IsMultiBP	
				,null OldMatNum	
				,null ReconJEId	
				,null BuildDesc	
				,null BPLId	
				,null BPLName	
				,null VATRegNum	

			from
				OutgoingPayment PPY where PaymentID = @DocID 

		select * from @itr1 

		set @reconSum = (select sum(ReconSum) from @itr1)
		
		if @reconSum <> 0 
				RAISERROR  ('Reconcile Error',11,1);

			insert into InternalReconcilation (
					 IsCard	
					,ReconType	
					,ReconDate
					,Total	
					,ReconCurr	
					,Canceled	
					,CancelAbs	
					,IsSystem	
					,InitObjTyp	
					,InitObjAbs	
					,ReconRule1	
					,ReconRule2	
					,ReconRule3	
					,IsMultiBP	
					,OldMatNum	
					,ReconJEId	
					,BuildDesc	
					,BPLId	
					,BPLName	
					,VATRegNum	
					,IsActive
					,CreatedByAppUserId
					,CreatedOn 
			)
			select IsCard	
					,ReconType	
					,ReconDate
					,Total	
					,ReconCurr	
					,Canceled	
					,CancelAbs	
					,IsSystem	
					,InitObjTyp	
					,InitObjAbs	
					,ReconRule1	
					,ReconRule2	
					,ReconRule3	
					,IsMultiBP	
					,OldMatNum	
					,ReconJEId	
					,BuildDesc	
					,BPLId	
					,BPLName	
					,VATRegNum
					,1 IsActive
					,@createdBy CreatedByAppUserId
					,@DocDate 
					from @itr

			set @reconID = @@IDENTITY 

			insert into InternalReconcilationDetail (
					ReconID	
					,LineNum	
					,ShortName	
					,TransId	
					,TransRowId	
					,SrcObjTyp	
					,SrcObjAbs	
					,ReconSum	
					,ReconSumFC	
					,ReconSumSC	
					,FrgnCurr	
					,SumMthCurr	
					,IsCredit	
					,Account	
					,CashDisSum	
					,WTSum	
					,WTSumFC	
					,WTSumSC	
					,ExpSum	
					,ExpSumFC	
					,ExpSumSC	
					,NetBefDisc	
					,IsActive
					,CreatedByAppUserId
					,CreatedOn 
			)

			select 
					 @reconID ReconID	
					,LineNum	
					,ShortName	
					,TransId	
					,TransRowId	
					,SrcObjTyp	
					,SrcObjAbs	
					,Abs(Isnull(ReconSum, 0))
					,ReconSumFC	
					,ReconSumSC	
					,FrgnCurr	
					,SumMthCurr	
					,IsCredit	
					,Account	
					,CashDisSum	
					,WTSum	
					,WTSumFC	
					,WTSumSC	
					,ExpSum	
					,ExpSumFC	
					,ExpSumSC	
					,NetBefDisc
					,1 IsActive
					,@createdBy CreatedByAppUserId
					,@DocDate 
					 
			from 
				@itr1

				commit tran tr_1;
		end try
		
		begin catch 
			SELECT 
				@ErrorMessage = ERROR_MESSAGE(),
				@ErrorSeverity = ERROR_SEVERITY(),
				@ErrorState = ERROR_STATE();

			RAISERROR (@ErrorMessage, -- Message text.
					   @ErrorSeverity, -- Severity.
					   @ErrorState -- State.
					   );

				rollback tran tr_1;
		end catch
			--select * from @itr1 
	end

if @DocType = 'SPY'
	begin
		begin tran tr_1;

		begin try 
			insert into @itr1(
					ReconID	
					,LineNum	
					,ShortName	
					,TransId	
					,TransRowId	
					,SrcObjTyp	
					,SrcObjAbs	
					,ReconSum	
					,ReconSumFC	
					,ReconSumSC	
					,FrgnCurr	
					,SumMthCurr	
					,IsCredit	
					,Account	
					,CashDisSum	
					,WTSum	
					,WTSumFC	
					,WTSumSC	
					,ExpSum	
					,ExpSumFC	
					,ExpSumSC	
					,NetBefDisc	
				)

			select  *
			from (
				select     --Payment/Credit
					null ReconID	
					,null LineNum	
					,SPY.CardCode ShortName	
					,SPY.TransID TransId	
					,JEN.LineID TransRowId	
					,SPY.ObjType  SrcObjTyp	
					,SPY.PaymentID  SrcObjAbs	
					,((SPY.DocTotal - Isnull(SPY.NoDocSum,0)) * -1) ReconSum	
					,0 ReconSumFC	
					,0 ReconSumSC	
					,null FrgnCurr	
					,null SumMthCurr	
					,'C' IsCredit	
					,BP.CtlDebCredPayAcct Account	
					,0 CashDisSum	
					,0 WTSum	
					,0 WTSumFC	
					,0 WTSumSC	
					,0 ExpSum	
					,0 ExpSumFC	
					,0 ExpSumSC	
					,0 NetBefDisc	 
				from 
					IncomingPayment SPY
						cross apply ( 
							select * from BusinessPartner where CardCode  = SPY.CardCode
							) BP
						cross apply (
							select * from JournalEntryDetail JEN1 where JEN1.JournalEntryID = SPY.TransID and JEN1.ShortName = SPY.CardCode 
						) JEN
				where 
					SPY.PaymentID = @DocID 

				union all 
				select      --Invoice/Debit
					null ReconID	
					,null LineNum	
					,P.CardCode ShortName	
					,P.TransID  TransId	
					,P.LineID  TransRowId	
					,SPY1.InvType  SrcObjTyp	
					--,P.ObjType  SrcObjTyp	
					,SPY1.DocID  SrcObjAbs	
					--,case 
					--  when P.ObjType = 'SIN' then SPY1.SumApplied 
					--  when P.ObjType = 'SCR' then SPY1.SumApplied * -1
					-- end ReconSum	

					--,SPY1.SumApplied ReconSum	
					,case 
						when SPY1.InvType  = 'SIN' then SPY1.SumApplied 
						when SPY1.InvType  = 'SCR' then SPY1.SumApplied * -1
						when SPY1.InvType  in ('JVO', 'JEN','PPY', 'SPY') then SPY1.SumApplied * 
								case 
									when @docType = 'SPY' and P.DocTotal  < 0 then -1 
									when @docType = 'SPY' and P.DocTotal  > 0 then 1 
								end 
					end ReconSum	
					,0 ReconSumFC	
					,0 ReconSumSC	
					,null FrgnCurr	
					,null SumMthCurr	
					,case 
						when SPY1.InvType = 'SIN' then 'D' 
						when SPY1.InvType = 'SCR' then 'C' 
						when SPY1.InvType in ('JVO', 'JEN','PPY', 'SPY') and P.DocTotal < 0   then 'D' 
						when SPY1.InvType in ('JVO', 'JEN','PPY', 'SPY') and P.DocTotal > 0  then 'C' 
					 end IsCredit	
					,BP.CtlDebCredPayAcct Account	
					,0 CashDisSum	
					,0 WTSum	
					,0 WTSumFC	
					,0 WTSumSC	
					,0 ExpSum	
					,0 ExpSumFC	
					,0 ExpSumSC	
					,0 NetBefDisc	 
				from 
					IncomingPayment SPY
					inner join IncomingPaymentInvoice SPY1 on SPY.PaymentID = SPY1.PaymentID 
					outer apply(
						select SIN.CardCode ,SIN.TransID ,(SIN.DocTotal) as DocTotal ,SIN.ObjType ,JEN1.LineID  from 
							SalesInvoice SIN 
							left join JournalEntryDetail JEN1 on SIN.TransID = JournalEntryID and JEN1.ShortName = SIN.CardCode 
						where 
							SIN.DocID = SPY1.DocID  and SIN.ObjType = SPY1.InvType 
						
						union all
						
						select SCR.CardCode , SCR.TransID,(SCR.DocTotal) DocTotal ,SCR.ObjType ,JEN1.LineID  from 
							SalesCreditNote SCR 
							left join JournalEntryDetail JEN1 on SCR.TransID = JournalEntryID and JEN1.ShortName = SCR.CardCode 
						where SCR.DocID = SPY1.DocID  and SCR.ObjType = SPY1.InvType 

						union all 
						select  
							JEN1.ShortName CardCode , JEN1.JournalEntryID  TransID,(Isnull(JEN1.Debit,0) - Isnull(JEN1.Credit,0)) DocTotal,JEN1.TransType ObjType ,JEN1.LineID  
						from 
							JournalEntryDetail JEN1
						where JEN1.TransType  in('JVO', 'JEN','PPY', 'SPY') and JEN1.JournalEntryID = SPY1.DocID  and JEN1.ShortName = SPY.CardCode 
					) P 

					cross apply ( 
						select * from BusinessPartner where CardCode  = SPY.CardCode
					) BP
				where 
					SPY.PaymentID = @DocID 
			) A
			
			insert into @itr(
				IsCard	
				,ReconType	
				,ReconDate
				,Total	
				,ReconCurr	
				,Canceled	
				,CancelAbs	
				,IsSystem	
				,InitObjTyp	
				,InitObjAbs	
				,ReconRule1	
				,ReconRule2	
				,ReconRule3	
				,IsMultiBP	
				,OldMatNum	
				,ReconJEId	
				,BuildDesc	
				,BPLId	
				,BPLName	
				,VATRegNum	
			)

			select 
				 'Y' IsCard	 
				,'PY' ReconType -- Payment
				,@DocDate ReconDate 
				,SPY.DocTotal Total	
				,null ReconCurr	
				,null Canceled	
				,null CancelAbs	
				,'Y' IsSystem	
				,SPY.ObjType InitObjTyp	
				,SPY.PaymentID  InitObjAbs	
				,null ReconRule1	
				,null ReconRule2	
				,null ReconRule3	
				,'N' IsMultiBP	
				,null OldMatNum	
				,null ReconJEId	
				,null BuildDesc	
				,null BPLId	
				,null BPLName	
				,null VATRegNum	

			from
				IncomingPayment SPY where PaymentID = @DocID 

		--select * from @itr1 

		set @reconSum = (select sum(ReconSum) from @itr1)
		print @reconsum
		
		if @reconSum <> 0 
				RAISERROR  ('Reconcile Error',11,1);

			insert into InternalReconcilation (
					 IsCard	
					,ReconType	
					,ReconDate
					,Total	
					,ReconCurr	
					,Canceled	
					,CancelAbs	
					,IsSystem	
					,InitObjTyp	
					,InitObjAbs	
					,ReconRule1	
					,ReconRule2	
					,ReconRule3	
					,IsMultiBP	
					,OldMatNum	
					,ReconJEId	
					,BuildDesc	
					,BPLId	
					,BPLName	
					,VATRegNum	
					,IsActive
					,CreatedByAppUserId
					,CreatedOn 

			)
			select IsCard	
					,ReconType	
					,ReconDate
					,Total	
					,ReconCurr	
					,Canceled	
					,CancelAbs	
					,IsSystem	
					,InitObjTyp	
					,InitObjAbs	
					,ReconRule1	
					,ReconRule2	
					,ReconRule3	
					,IsMultiBP	
					,OldMatNum	
					,ReconJEId	
					,BuildDesc	
					,BPLId	
					,BPLName	
					,VATRegNum
					,1 IsActive
					,@createdBy CreatedByAppUserId
					,@DocDate 
					from @itr

			set @reconID = @@IDENTITY 

			insert into InternalReconcilationDetail 
			(
					ReconID	
					,LineNum	
					,ShortName	
					,TransId	
					,TransRowId	
					,SrcObjTyp	
					,SrcObjAbs	
					,ReconSum	
					,ReconSumFC	
					,ReconSumSC	
					,FrgnCurr	
					,SumMthCurr	
					,IsCredit	
					,Account	
					,CashDisSum	
					,WTSum	
					,WTSumFC	
					,WTSumSC	
					,ExpSum	
					,ExpSumFC	
					,ExpSumSC	
					,NetBefDisc	
					,IsActive
					,CreatedByAppUserId
					,CreatedOn 
					 
			)

			select 
					 @reconID ReconID	
					,LineNum	
					,ShortName	
					,TransId	
					,TransRowId	
					,SrcObjTyp	
					,SrcObjAbs	
					,Abs(Isnull(ReconSum, 0))
					,ReconSumFC	
					,ReconSumSC	
					,FrgnCurr	
					,SumMthCurr	
					,IsCredit	
					,Account	
					,CashDisSum	
					,WTSum	
					,WTSumFC	
					,WTSumSC	
					,ExpSum	
					,ExpSumFC	
					,ExpSumSC	
					,NetBefDisc
					,1 IsActive
					,@createdBy CreatedByAppUserId
					,@DocDate 
					 
			from 
				@itr1

				commit tran tr_1;
		end try
		
		begin catch 
			SELECT 
				@ErrorMessage = ERROR_MESSAGE(),
				@ErrorSeverity = ERROR_SEVERITY(),
				@ErrorState = ERROR_STATE();

			RAISERROR (@ErrorMessage, -- Message text.
					   @ErrorSeverity, -- Severity.
					   @ErrorState -- State.
					   );

				rollback tran tr_1;
		end catch
			--select * from @itr1 
	end 

if @DocType = 'PCR' 
	begin 
	
	begin tran tr_1;

		begin try 
			insert into @itr1(
					ReconID	
					,LineNum	
					,ShortName	
					,TransId	
					,TransRowId	
					,SrcObjTyp	
					,SrcObjAbs	
					,ReconSum	
					,ReconSumFC	
					,ReconSumSC	
					,FrgnCurr	
					,SumMthCurr	
					,IsCredit	
					,Account	
					,CashDisSum	
					,WTSum	
					,WTSumFC	
					,WTSumSC	
					,ExpSum	
					,ExpSumFC	
					,ExpSumSC	
					,NetBefDisc	
				)

			select  *
			from (
				select     --PCR/Debit
					null ReconID	
					,null LineNum	
					,PCR.CardCode ShortName	
					,PCR.TransID TransId	
					,JEN.LineID TransRowId	
					,PCR.ObjType  SrcObjTyp	
					,PCR.DocID  SrcObjAbs	
					,PCR.DocTotal ReconSum	
					,null ReconSumFC	
					,null ReconSumSC	
					,null FrgnCurr	
					,null SumMthCurr	
					,'D' IsCredit	
					,BP.CtlDebCredPayAcct Account	
					,null CashDisSum	
					,null WTSum	
					,null WTSumFC	
					,null WTSumSC	
					,null ExpSum	
					,null ExpSumFC	
					,null ExpSumSC	
					,null NetBefDisc	 
				from 
					PurchaseCreditNote PCR
						cross apply ( 
							select * from BusinessPartner where CardCode  = PCR.CardCode
							) BP
						cross apply (
							select * from JournalEntryDetail JEN1 where JEN1.JournalEntryID = PCR.TransID and JEN1.ShortName = PCR.CardCode 
						) JEN
				where 
					PCR.DocID = @DocID 

				union all 
				select      --PIN/Credit
					null ReconID	
					,null LineNum	
					,P.CardCode ShortName	
					,P.TransID  TransId	
					,0 TransRowId	
					,P.ObjType  SrcObjTyp	
					,P.DocID  SrcObjAbs	
					,Isnull(P.SumApplied,0) ReconSum	
					,null ReconSumFC	
					,null ReconSumSC	
					,null FrgnCurr	
					,null SumMthCurr	
					,'C' IsCredit	
					,BP.CtlDebCredPayAcct Account	
					,null CashDisSum	
					,null WTSum	
					,null WTSumFC	
					,null WTSumSC	
					,null ExpSum	
					,null ExpSumFC	
					,null ExpSumSC	
					,null NetBefDisc	 
				from 
					PurchaseCreditNote  PCR
					inner join PurchaseCreditNoteDetail PCR1 on PCR.DocID = PCR1.DocID 
					outer apply(
						--select Sum(PIN1.GTotal) * -1 as SumApplied, PIN.DocID ,PIN.ObjType , PIN.TransID, PIN.CardCode   
						select (PCR1.Quantity * PIN1.PriceAfVat) * -1 as SumApplied, PIN.DocID ,PIN.ObjType , PIN.TransID, PIN.CardCode   
						from 
							PurchaseInvoice PIN
							inner join PurchaseInvoiceDetail PIN1 on PIN.DocID = PIN1.DocID 
						where 
							PIN.DocID = PCR1.BaseEntry and PIN1.LineNum = PCR1.BaseLine and PIN.CardCode = PCR.CardCode
						group by 
							PIN.DocID , PIN.ObjType , PIN.TransID  , PIN.CardCode, PIN1.PriceAfVat
					) P 

					cross apply ( 
						select * from BusinessPartner where CardCode  = PCR.CardCode
					) BP
				where 
					PCR.DocID = @DocID 
			) A
			
			insert into @itr(
				IsCard	
				,ReconType	
				,ReconDate
				,Total	
				,ReconCurr	
				,Canceled	
				,CancelAbs	
				,IsSystem	
				,InitObjTyp	
				,InitObjAbs	
				,ReconRule1	
				,ReconRule2	
				,ReconRule3	
				,IsMultiBP	
				,OldMatNum	
				,ReconJEId	
				,BuildDesc	
				,BPLId	
				,BPLName	
				,VATRegNum	
			)

			select 
				 'Y' IsCard	 
				,'CR' ReconType
				,@DocDate ReconDate 
				,PCR.DocTotal Total	
				,null ReconCurr	
				,null Canceled	
				,null CancelAbs	
				,'Y' IsSystem	
				,PCR.ObjType InitObjTyp	
				,PCR.DocID  InitObjAbs	
				,null ReconRule1	
				,null ReconRule2	
				,null ReconRule3	
				,'N' IsMultiBP	
				,null OldMatNum	
				,null ReconJEId	
				,null BuildDesc	
				,null BPLId	
				,null BPLName	
				,null VATRegNum	

			from
				PurchaseCreditNote  PCR where DocID = @DocID 


			-- select * from @itr1 
			-- check  if balance 
			
			set  @reconSum = (select sum(ReconSum) from @itr1)
			
			if @reconSum <> 0 
				RAISERROR('Reconcile Error. The reconciled amounts are not equal',11,1);

			insert into InternalReconcilation (
					 IsCard	
					,ReconType	
					,ReconDate
					,Total	
					,ReconCurr	
					,Canceled	
					,CancelAbs	
					,IsSystem	
					,InitObjTyp	
					,InitObjAbs	
					,ReconRule1	
					,ReconRule2	
					,ReconRule3	
					,IsMultiBP	
					,OldMatNum	
					,ReconJEId	
					,BuildDesc	
					,BPLId	
					,BPLName	
					,VATRegNum	
					,IsActive
					,CreatedByAppUserId
					,CreatedOn 

			)
			select IsCard	
					,ReconType	
					,ReconDate
					,Total	
					,ReconCurr	
					,Canceled	
					,CancelAbs	
					,IsSystem	
					,InitObjTyp	
					,InitObjAbs	
					,ReconRule1	
					,ReconRule2	
					,ReconRule3	
					,IsMultiBP	
					,OldMatNum	
					,ReconJEId	
					,BuildDesc	
					,BPLId	
					,BPLName	
					,VATRegNum
					,1 IsActive
					,@createdBy CreatedByAppUserId
					,@DocDate 
					from @itr

			set @reconID = @@IDENTITY 

			insert into InternalReconcilationDetail 
			(
					ReconID	
					,LineNum	
					,ShortName	
					,TransId	
					,TransRowId	
					,SrcObjTyp	
					,SrcObjAbs	
					,ReconSum	
					,ReconSumFC	
					,ReconSumSC	
					,FrgnCurr	
					,SumMthCurr	
					,IsCredit	
					,Account	
					,CashDisSum	
					,WTSum	
					,WTSumFC	
					,WTSumSC	
					,ExpSum	
					,ExpSumFC	
					,ExpSumSC	
					,NetBefDisc	
					,IsActive
					,CreatedByAppUserId
					,CreatedOn 
					 
			)

			select 
					 @reconID ReconID	
					,LineNum	
					,ShortName	
					,TransId	
					,TransRowId	
					,SrcObjTyp	
					,SrcObjAbs	
					,Abs(Isnull(ReconSum, 0))
					,ReconSumFC	
					,ReconSumSC	
					,FrgnCurr	
					,SumMthCurr	
					,IsCredit	
					,Account	
					,CashDisSum	
					,WTSum	
					,WTSumFC	
					,WTSumSC	
					,ExpSum	
					,ExpSumFC	
					,ExpSumSC	
					,NetBefDisc
					,1 IsActive
					,@createdBy CreatedByAppUserId
					,@DocDate 
					 
			from 
				@itr1

				commit tran tr_1;
		end try
		
		begin catch 
		 

			SELECT 
				@ErrorMessage = ERROR_MESSAGE(),
				@ErrorSeverity = ERROR_SEVERITY(),
				@ErrorState = ERROR_STATE();

			RAISERROR (@ErrorMessage, -- Message text.
					   @ErrorSeverity, -- Severity.
					   @ErrorState -- State.
					   );

				rollback tran tr_1;
		end catch

			--select * from @itr1 
		
	end

if @DocType = 'SCR' 
	begin 
		begin tran tr_1;

		begin try 
			insert into @itr1(
					ReconID	
					,LineNum	
					,ShortName	
					,TransId	
					,TransRowId	
					,SrcObjTyp	
					,SrcObjAbs	
					,ReconSum	
					,ReconSumFC	
					,ReconSumSC	
					,FrgnCurr	
					,SumMthCurr	
					,IsCredit	
					,Account	
					,CashDisSum	
					,WTSum	
					,WTSumFC	
					,WTSumSC	
					,ExpSum	
					,ExpSumFC	
					,ExpSumSC	
					,NetBefDisc	
				)

			select  *
			from (
				select     --SCR/Credit
					null ReconID	
					,null LineNum	
					,SCR.CardCode ShortName	
					,SCR.TransID TransId	
					,JEN.LineID TransRowId	
					,SCR.ObjType  SrcObjTyp	
					,SCR.DocID  SrcObjAbs	
					,(SCR.DocTotal * -1)  ReconSum	
					,null ReconSumFC	
					,null ReconSumSC	
					,null FrgnCurr	
					,null SumMthCurr	
					,'C' IsCredit	
					,BP.CtlDebCredPayAcct Account	
					,null CashDisSum	
					,null WTSum	
					,null WTSumFC	
					,null WTSumSC	
					,null ExpSum	
					,null ExpSumFC	
					,null ExpSumSC	
					,null NetBefDisc	 
				from 
					SalesCreditNote SCR
						cross apply ( 
							select * from BusinessPartner where CardCode  = SCR.CardCode
							) BP
						cross apply (
							select * from JournalEntryDetail JEN1 where JEN1.JournalEntryID = SCR.TransID and JEN1.ShortName = SCR.CardCode 
						) JEN
				where 
					SCR.DocID = @DocID 

				union all 
				select      --SIN/Debit
					null ReconID	
					,null LineNum	
					,P.CardCode ShortName	
					,P.TransID  TransId	
					,0 TransRowId	
					,P.ObjType  SrcObjTyp	
					,P.DocID  SrcObjAbs	
					,Isnull(P.SumApplied,0) ReconSum	
					,null ReconSumFC	
					,null ReconSumSC	
					,null FrgnCurr	
					,null SumMthCurr	
					,'D' IsCredit	
					,BP.CtlDebCredPayAcct Account	
					,null CashDisSum	
					,null WTSum	
					,null WTSumFC	
					,null WTSumSC	
					,null ExpSum	
					,null ExpSumFC	
					,null ExpSumSC	
					,null NetBefDisc	 
				from 
					SalesCreditNote  SCR
					inner join SalesCreditNoteDetail SCR1 on SCR.DocID = SCR1.DocID 
					outer apply(
						select (SCR1.Quantity * SIN1.PriceAfVat) as SumApplied, SIN.DocID ,SIN.ObjType , SIN.TransID, SIN.CardCode   from 
							SalesInvoice SIN
							inner join SalesInvoiceDetail SIN1 on SIN.DocID = SIN1.DocID 
						where 
							SIN.DocID = SCR1.BaseEntry and SIN1.LineNum = SCR1.BaseLine and SIN.CardCode = SCR.CardCode
						group by 
							SIN.DocID , SIN.ObjType , SIN.TransID  , SIN.CardCode  , SIN1.PriceAfVat , SIN1.Quantity , SIN1.OpenQty
					) P 

					cross apply ( 
						select * from BusinessPartner where CardCode  = SCR.CardCode
					) BP
				where 
					SCR.DocID = @DocID 
			) A
			
			insert into @itr(
				IsCard	
				,ReconType	
				,ReconDate
				,Total	
				,ReconCurr	
				,Canceled	
				,CancelAbs	
				,IsSystem	
				,InitObjTyp	
				,InitObjAbs	
				,ReconRule1	
				,ReconRule2	
				,ReconRule3	
				,IsMultiBP	
				,OldMatNum	
				,ReconJEId	
				,BuildDesc	
				,BPLId	
				,BPLName	
				,VATRegNum	
			)

			select 
				 'Y' IsCard	 
				,'CR' ReconType
				,@DocDate ReconDate 
				,SCR.DocTotal Total	
				,null ReconCurr	
				,null Canceled	
				,null CancelAbs	
				,'Y' IsSystem	
				,SCR.ObjType InitObjTyp	
				,SCR.DocID  InitObjAbs	
				,null ReconRule1	
				,null ReconRule2	
				,null ReconRule3	
				,'N' IsMultiBP	
				,null OldMatNum	
				,null ReconJEId	
				,null BuildDesc	
				,null BPLId	
				,null BPLName	
				,null VATRegNum	

			from
				SalesCreditNote  SCR where DocID = @DocID 


			 --select * from @itr1 
			-- check  if balance 
			
			set  @reconSum = (select sum(ReconSum) from @itr1)
			
			if @reconSum <> 0 
				RAISERROR('Reconcile Error. The reconciled amounts are not equal',11,1);

			insert into InternalReconcilation (
					 IsCard	
					,ReconType	
					,ReconDate
					,Total	
					,ReconCurr	
					,Canceled	
					,CancelAbs	
					,IsSystem	
					,InitObjTyp	
					,InitObjAbs	
					,ReconRule1	
					,ReconRule2	
					,ReconRule3	
					,IsMultiBP	
					,OldMatNum	
					,ReconJEId	
					,BuildDesc	
					,BPLId	
					,BPLName	
					,VATRegNum	
					,IsActive
					,CreatedByAppUserId
					,CreatedOn 

			)
			select IsCard	
					,ReconType	
					,ReconDate
					,Total	
					,ReconCurr	
					,Canceled	
					,CancelAbs	
					,IsSystem	
					,InitObjTyp	
					,InitObjAbs	
					,ReconRule1	
					,ReconRule2	
					,ReconRule3	
					,IsMultiBP	
					,OldMatNum	
					,ReconJEId	
					,BuildDesc	
					,BPLId	
					,BPLName	
					,VATRegNum
					,1 IsActive
					,@createdBy CreatedByAppUserId
					,@DocDate 
					from @itr

			set @reconID = @@IDENTITY 

			insert into InternalReconcilationDetail 
			(
					ReconID	
					,LineNum	
					,ShortName	
					,TransId	
					,TransRowId	
					,SrcObjTyp	
					,SrcObjAbs	
					,ReconSum	
					,ReconSumFC	
					,ReconSumSC	
					,FrgnCurr	
					,SumMthCurr	
					,IsCredit	
					,Account	
					,CashDisSum	
					,WTSum	
					,WTSumFC	
					,WTSumSC	
					,ExpSum	
					,ExpSumFC	
					,ExpSumSC	
					,NetBefDisc	
					,IsActive
					,CreatedByAppUserId
					,CreatedOn 
					 
			)

			select 
					 @reconID ReconID	
					,LineNum	
					,ShortName	
					,TransId	
					,TransRowId	
					,SrcObjTyp	
					,SrcObjAbs	
					,Abs(Isnull(ReconSum, 0))
					,ReconSumFC	
					,ReconSumSC	
					,FrgnCurr	
					,SumMthCurr	
					,IsCredit	
					,Account	
					,CashDisSum	
					,WTSum	
					,WTSumFC	
					,WTSumSC	
					,ExpSum	
					,ExpSumFC	
					,ExpSumSC	
					,NetBefDisc
					,1 IsActive
					,@createdBy CreatedByAppUserId
					,@DocDate 
					 
			from 
				@itr1

				commit tran tr_1;
		end try
		
		begin catch 
		 

			SELECT 
				@ErrorMessage = ERROR_MESSAGE(),
				@ErrorSeverity = ERROR_SEVERITY(),
				@ErrorState = ERROR_STATE();

			RAISERROR (@ErrorMessage, -- Message text.
					   @ErrorSeverity, -- Severity.
					   @ErrorState -- State.
					   );

				rollback tran tr_1;
		end catch

			--select * from @itr1 
	end


if @DocType = 'DPS' 
	begin 
		begin tran tr_1;

		begin try 
			insert into @itr1(
					ReconID	
					,LineNum	
					,ShortName	
					,TransId	
					,TransRowId	
					,SrcObjTyp	
					,SrcObjAbs	
					,ReconSum	
					,ReconSumFC	
					,ReconSumSC	
					,FrgnCurr	
					,SumMthCurr	
					,IsCredit	
					,Account	
					,CashDisSum	
					,WTSum	
					,WTSumFC	
					,WTSumSC	
					,ExpSum	
					,ExpSumFC	
					,ExpSumSC	
					,NetBefDisc	
				)

			select  *
			from (
				select     --DPS /CASH Credit
					null ReconID	
					,null LineNum	
					,DPS.AllocAcct ShortName	
					,DPS.TransAbs  TransId	
					,JEN1.LineID TransRowId	
					,DPS.ObjType  SrcObjTyp	
					,DPS.DepositID SrcObjAbs	
					,DPS.CashSum * -1   ReconSum	
					,null ReconSumFC	
					,null ReconSumSC	
					,null FrgnCurr	
					,null SumMthCurr	
					,'C' IsCredit	
					,DPS.AllocAcct Account	
					,null CashDisSum	
					,null WTSum	
					,null WTSumFC	
					,null WTSumSC	
					,null ExpSum	
					,null ExpSumFC	
					,null ExpSumSC	
					,null NetBefDisc	 
				from 
					Deposit  DPS
					outer apply(
						select * from JournalEntryDetail where JournalEntryID = DPS.TransAbs and ShortName = DPS.AllocAcct 
					) JEN1

				where 
					DPS.DepositID = @DocID  and IsNull(DPS.CashSum ,0)  <> 0


					union all 
					select     --DPS /CHECK Credit
					null ReconID	
					,null LineNum	
					,DPS1.CashCheckAcct  ShortName	
					,DPS.TransAbs  TransId	
					,JEN1.LineID TransRowId	
					,DPS.ObjType  SrcObjTyp	
					,DPS.DepositID SrcObjAbs	
					,sum(Isnull(DPS1.CheckSum,0)) * -1    ReconSum	
					,null ReconSumFC	
					,null ReconSumSC	
					,null FrgnCurr	
					,null SumMthCurr	
					,'C' IsCredit	
					,DPS1.CashCheckAcct  Account	
					,null CashDisSum	
					,null WTSum	
					,null WTSumFC	
					,null WTSumSC	
					,null ExpSum	
					,null ExpSumFC	
					,null ExpSumSC	
					,null NetBefDisc	 
				from 
					Deposit  DPS
					cross apply(
					 select CR.* 
					 from 
					 DepositCheck DC
					 inner join CheckRegister CR on DC.CheckID = CR.CheckID 
					 
					 where DC.DepositID = DPS.DepositID 
					 
					) DPS1
					outer apply(
						select * from JournalEntryDetail where JournalEntryID = DPS.TransAbs and ShortName = DPS1.CashCheckAcct and ref2 = DPS1.CheckID 
					) JEN1

				where 
					DPS.DepositID = @DocID 
				group by 
					DPS.DepositID ,
					DPS1.CashCheckAcct,
					DPS.TransAbs,
					JEN1.LineID,
					DPS.ObjType 



				union all 
				select      --DPS/Debit
					null ReconID	
					,null LineNum	
					,DPS.BankAcct ShortName	
					,DPS.TransAbs TransId	
					,0 TransRowId	
					,DPS.ObjType  SrcObjTyp	
					,DPS.DepositID  SrcObjAbs	
					,Isnull(DPS.DocTotal ,0) ReconSum	
					,null ReconSumFC	
					,null ReconSumSC	
					,null FrgnCurr	
					,null SumMthCurr	
					,'D' IsCredit	
					,DPS.BankAcct Account	
					,null CashDisSum	
					,null WTSum	
					,null WTSumFC	
					,null WTSumSC	
					,null ExpSum	
					,null ExpSumFC	
					,null ExpSumSC	
					,null NetBefDisc	 
				from 
					Deposit  DPS
					outer apply(
						select *  from DepositCheck where DepositID = DPS.DepositID
					) DPS1
					
				where 
					DPS.DepositID  = @DocID 
			) A
			
			insert into @itr(
				IsCard	
				,ReconType	
				,ReconDate
				,Total	
				,ReconCurr	
				,Canceled	
				,CancelAbs	
				,IsSystem	
				,InitObjTyp	
				,InitObjAbs	
				,ReconRule1	
				,ReconRule2	
				,ReconRule3	
				,IsMultiBP	
				,OldMatNum	
				,ReconJEId	
				,BuildDesc	
				,BPLId	
				,BPLName	
				,VATRegNum	
			)

			select 
				 'Y' IsCard	 
				,'DE' ReconType
				,@DocDate ReconDate 
				,DPS.DocTotal Total	
				,null ReconCurr	
				,null Canceled	
				,null CancelAbs	
				,'Y' IsSystem	
				,DPS.ObjType InitObjTyp	
				,DPS.DepositID InitObjAbs	
				,null ReconRule1	
				,null ReconRule2	
				,null ReconRule3	
				,'N' IsMultiBP	
				,null OldMatNum	
				,null ReconJEId	
				,null BuildDesc	
				,null BPLId	
				,null BPLName	
				,null VATRegNum	

			from
				Deposit  DPS where DepositID = @DocID 

			 --select * from @itr
			 --select * from @itr1 
			-- check  if balance 
			
			set  @reconSum = (select sum(ReconSum) from @itr1)
			
			if @reconSum <> 0 
				RAISERROR('Reconcile Error. The reconciled amounts are not equal',11,1);

			insert into InternalReconcilation (
					 IsCard	
					,ReconType	
					,ReconDate
					,Total	
					,ReconCurr	
					,Canceled	
					,CancelAbs	
					,IsSystem	
					,InitObjTyp	
					,InitObjAbs	
					,ReconRule1	
					,ReconRule2	
					,ReconRule3	
					,IsMultiBP	
					,OldMatNum	
					,ReconJEId	
					,BuildDesc	
					,BPLId	
					,BPLName	
					,VATRegNum	
					,IsActive
					,CreatedByAppUserId
					,CreatedOn 

			)
			select IsCard	
					,ReconType	
					,ReconDate
					,Total	
					,ReconCurr	
					,Canceled	
					,CancelAbs	
					,IsSystem	
					,InitObjTyp	
					,InitObjAbs	
					,ReconRule1	
					,ReconRule2	
					,ReconRule3	
					,IsMultiBP	
					,OldMatNum	
					,ReconJEId	
					,BuildDesc	
					,BPLId	
					,BPLName	
					,VATRegNum
					,1 IsActive
					,@createdBy CreatedByAppUserId
					,@DocDate 
					from @itr

			set @reconID = @@IDENTITY 

			insert into InternalReconcilationDetail 
			(
					ReconID	
					,LineNum	
					,ShortName	
					,TransId	
					,TransRowId	
					,SrcObjTyp	
					,SrcObjAbs	
					,ReconSum	
					,ReconSumFC	
					,ReconSumSC	
					,FrgnCurr	
					,SumMthCurr	
					,IsCredit	
					,Account	
					,CashDisSum	
					,WTSum	
					,WTSumFC	
					,WTSumSC	
					,ExpSum	
					,ExpSumFC	
					,ExpSumSC	
					,NetBefDisc	
					,IsActive
					,CreatedByAppUserId
					,CreatedOn 
					 
			)

			select 
					 @reconID ReconID	
					,LineNum	
					,ShortName	
					,TransId	
					,TransRowId	
					,SrcObjTyp	
					,SrcObjAbs	
					,Abs(Isnull(ReconSum, 0))
					,ReconSumFC	
					,ReconSumSC	
					,FrgnCurr	
					,SumMthCurr	
					,IsCredit	
					,Account	
					,CashDisSum	
					,WTSum	
					,WTSumFC	
					,WTSumSC	
					,ExpSum	
					,ExpSumFC	
					,ExpSumSC	
					,NetBefDisc
					,1 IsActive
					,@createdBy CreatedByAppUserId
					,@DocDate 
					 
			from 
				@itr1

				commit tran tr_1;
		end try
		
		begin catch 
		 

			SELECT 
				@ErrorMessage = ERROR_MESSAGE(),
				@ErrorSeverity = ERROR_SEVERITY(),
				@ErrorState = ERROR_STATE();

			RAISERROR (@ErrorMessage, -- Message text.
					   @ErrorSeverity, -- Severity.
					   @ErrorState -- State.
					   );

				rollback tran tr_1;
		end catch

			--select * from @itr1 
	end
set nocount off;
end 
go 

--declare @docDate datetime= getdate()
--exec SP_ReconcileDoc @docID=13,@docType='PPY',@docDate=@docDate , @createdBy=1