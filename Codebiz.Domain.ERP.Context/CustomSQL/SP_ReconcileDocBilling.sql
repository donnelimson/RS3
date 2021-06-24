USE [CodebizERPDEV2]
GO
/****** Object:  StoredProcedure [dbo].[SP_ReconcileDocBilling]    Script Date: 5/11/2021 11:23:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER Procedure [dbo].[SP_ReconcileDocBilling]
@docNum1 nvarchar(20)null,
@docNum2 nvarchar(20)null,
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
	,Total	decimal(16,9)  null
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
	,ReconSum	decimal(16,9) null
	,ReconSumFC	decimal null
	,ReconSumSC	decimal null
	,FrgnCurr	nvarchar(3) null
	,SumMthCurr	decimal null
	,IsCredit	nvarchar(1) null
	,Account	nvarchar(30) null
	,CashDisSum	decimal(16,9) null
	,WTSum	decimal(16,9) null
	,WTSumFC	decimal null
	,WTSumSC	decimal null
	,ExpSum	decimal null
	,ExpSumFC	decimal null
	,ExpSumSC	decimal null
	,NetBefDisc	decimal null
)

if @DocType = 'JE' 
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
					,JEN.ShortName ShortName	
					,JE.JournalEntryID TransId	
					,JEN.LineID TransRowId	
					,JE.ObjType  SrcObjTyp	
					,(select JournalEntryID from JournalEntry where DocNum=@docNum1) SrcObjAbs	
					,CASE when JEN.Ref4='WT' then 0 
					else Isnull(JEN.Credit *-1,0) end as ReconSum	
					,null ReconSumFC	
					,null ReconSumSC	
					,null FrgnCurr	
					,null SumMthCurr	
					,'C' IsCredit	
					,BP.CtlDebCredPayAcct Account	
					,CASE when JEN.Ref4 ='DSC' 
					then JEN.Credit 
					when JEN.Ref4 = 'WT'
					then JEN.Debit
					else 0 end as CashDisSum	
					,CASE when JEN.REf4='WT' then
			     	(select sum(credit) from JournalEntryDetail where ref4='WT' and journalentryid =(select JournalEntryID from JournalEntry where DocNum=@docNum1))-JEN.Debit else 0 end as WTSum						,null WTSumFC	
					,null WTSumSC	
					,null ExpSum	
					,null ExpSumFC	
					,null ExpSumSC	
					,null NetBefDisc	 
			    from 
					JournalEntry JE
						cross apply ( 
							select * from BusinessPartner where CardCode  = JE.Ref1
							) BP
						cross apply (
							select * from JournalEntryDetail JEN1 where JEN1.JournalEntryID = JE.JournalEntryID and JEN1.ShortName = JE.Ref1
						) JEN
				where 
					JE.DocNum = @docNum2 

				union all 
				select      --SIN/Debit
					null ReconID	
					,null LineNum	
					,JEN.ShortName ShortName	
					,JE.JournalEntryID  TransId	
					,JEN.LineID TransRowId	
					,JE.ObjType  SrcObjTyp	
					,(select JournalEntryID from JournalEntry where DocNum=@docNum1)  SrcObjAbs	
					,CASE when JEN.Ref4='WT' then Isnull(JEN.Credit*-1,0)
					else Isnull(JEN.Debit,0) end as ReconSum	
					,null ReconSumFC	
					,null ReconSumSC	
					,null FrgnCurr	
					,null SumMthCurr	
					,'D' IsCredit	
					,BP.CtlDebCredPayAcct Account	
					,CASE when JEN.Ref4 ='DSC' 
					then JEN.Debit 
					when JEN.Ref4 = 'WT'
					then JEN.Credit
					else 0 end as CashDisSum		
					,CASE when JEN.REf4='WT' then
					(select sum(debit) from JournalEntryDetail where ref4='WT' and journalentryid =(select JournalEntryID from JournalEntry where DocNum=@docNum1))-Jen.Credit else 0 end as WTSum	
					,null WTSumFC	
					,null WTSumSC	
					,null ExpSum	
					,null ExpSumFC	
					,null ExpSumSC	
					,null NetBefDisc	 
			    from 
					JournalEntry JE
						cross apply ( 
							select * from BusinessPartner where CardCode  = JE.Ref1
							) BP
						cross apply (
							select * from JournalEntryDetail JEN1 where JEN1.JournalEntryID = JE.JournalEntryID and JEN1.ShortName != JE.Ref1
						) JEN
				where 
					JE.DocNum = @docNum2
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
				,JE.SysTotal Total	
				,null ReconCurr	
				,null Canceled	
				,null CancelAbs	
				,'Y' IsSystem	
				,JE.ObjType InitObjTyp	
			    ,(select JournalEntryID from JournalEntry where DocNum=@docNum1)
				,null ReconRule1	
				,null ReconRule2	
				,null ReconRule3	
				,'N' IsMultiBP	
				,null OldMatNum	
				,(select JournalEntryID from JournalEntry where DocNum=@docNum1)ReconJEId	
				,null BuildDesc	
				,null BPLId	
				,null BPLName	
				,null VATRegNum	

			from
				JournalEntry  JE where JE.DocNum = @docNum2 


			 select * from @itr1 
			-- check  if balance 
		
			set  @reconSum = (select sum(ReconSum) from @itr1)
			
			if @reconSum <> 0 
				RAISERROR('Reconcile Error. The reconciled amounts are not equal',11,1);
				--select * from @itr1
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
            Update JournalEntry set DocStatus='C' where DocNum=@docNum2
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
