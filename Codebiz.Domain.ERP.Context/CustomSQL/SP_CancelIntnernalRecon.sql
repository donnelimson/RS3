
if exists (select 1 from sys.sysobjects where [name] = 'SP_CancelIntnernalRecon' and xtype = 'P')
	drop procedure SP_CancelIntnernalRecon
go 

create procedure SP_CancelIntnernalRecon
@reconID int  = 0 , 
@cancelBy int null = 0,
@cancelDate datetime null
as 
begin 

set nocount on

DECLARE @ErrorMessage NVARCHAR(4000);
DECLARE @ErrorSeverity INT;
DECLARE @ErrorState INT;

declare @itr table (
	ReconID		bigint null, 
	IsCard		nchar(1) null, 
	ReconType	nvarchar(8) null, 
	ReconDate	datetime null, 
	Total		decimal(19,6) null, 
	ReconCurr	nvarchar(8) null, 
	Canceled	nchar(1) null, 
	CancelAbs	bigint null, 
	IsSystem	nchar(1) null, 
	InitObjTyp	nvarchar(8) null, 
	InitObjAbs	bigint null, 
	ReconRule1	nvarchar(8) null,
	ReconRule2	nvarchar(8) null,
	ReconRule3	nvarchar(8) null,
	IsMultiBP	nchar(1) null, 
	OldMatNum	bigint null, 
	ReconJEId	bigint null, 
	BuildDesc	nvarchar(50) null,
	BPLId		bigint null, 
	BPLName		nvarchar(80) null, 
	VATRegNum	nvarchar(50) null,
	IsActive	bit null, 
	CreatedByAppUserId	int	null,
	CreatedOn	datetime null,
	ModifiedByAppUserId	int	null,
	ModifiedOn	datetime null
)

declare @itr1 table (
		ReconLineID	bigint, 
		ReconID		bigint, 
		ShortName	nvarchar(50) null,
		TransId		bigint null,
		TransRowId	bigint null,
		SrcObjTyp	nvarchar(50) null,
		SrcObjAbs	bigint null, 
		ReconSum	decimal(19,6) null, 
		ReconSumFC	decimal(19,6) null, 
		ReconSumSC	decimal(19,6) null, 
		FrgnCurr	nvarchar(8) null,
		SumMthCurr	decimal(19,6) null, 
		IsCredit	nvarchar(50) null,
		Account		nvarchar(50) null,
		CashDisSum	decimal(19,6) null, 
		WTSum		decimal(19,6) null, 
		WTSumFC		decimal(19,6) null, 
		WTSumSC		decimal(19,6) null, 
		ExpSum		decimal(19,6) null, 
		ExpSumFC	decimal(19,6) null, 
		ExpSumSC	decimal(19,6) null, 
		NetBefDisc	decimal(19,6) null, 
		CreatedByAppUserId	int	null,
		CreatedOn	datetime null,
		ModifiedByAppUserId	int	null,
		ModifiedOn	datetime null,
		LineNum	int	null
)

insert into @itr (
		ReconID
		,IsCard
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
		,ModifiedByAppUserId
		,ModifiedOn

	)

	select 
	 ReconID
	,IsCard
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
	,ModifiedByAppUserId
	,ModifiedOn

	from InternalReconcilation where ReconID = @reconID 

	insert into @itr1 
	select
		 ReconLineID
		,ReconID
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
		,CreatedByAppUserId
		,CreatedOn
		,ModifiedByAppUserId
		,ModifiedOn
		,LineNum
	
		from InternalReconcilationDetail where ReconID = @reconID 
		 
		 begin tran tr1

		 begin try	
			declare @newReconID  bigint 
				 insert into InternalReconcilation(
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
					,ModifiedByAppUserId
					,ModifiedOn)

		 			select 
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
						,@cancelBy 
						,@cancelDate
					from @itr 

					set @newReconID = @@IDENTITY

					if @newReconID > 0 
					begin
						update @itr1  set ReconID = @newReconID , ReconSum = ReconSum * -1
						insert InternalReconcilationDetail (
							ReconID
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
							,CreatedByAppUserId
							,CreatedOn
							,ModifiedByAppUserId
							,ModifiedOn
							,LineNum
						)
						select
							 ReconID
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
							,CreatedByAppUserId
							,CreatedOn
							,ModifiedByAppUserId
							,ModifiedOn
							,LineNum
							from @itr1
				
							update InternalReconcilation set Canceled = 'Y', CancelAbs = @newReconID where ReconID = @reconID 
							update InternalReconcilation set CancelAbs = @reconID where ReconID = @newReconID 


						commit tran tr1
					end 
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
			rollback tran tr1
		 end catch 

		 set nocount off
end
go 