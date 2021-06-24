if exists(select 1 from sys.sysobjects where [name] = 'SP_GetStockPrice' and xtype = 'P')
	drop procedure SP_GetStockPrice
go 


Create Procedure SP_GetStockPrice
@itemCode as nvarchar(30) null  = ''
,@whsCode nvarchar(30) null = ''
,@valuationMethod nchar(1) null  = 'A'-- (F)ifo, (A)ve, (S)tandard
as 
begin

--set @docDate = case when @docDate is null then GETDATE() end
	if @valuationMethod = 'A' -- Average 
	begin 
			select  
			 ItemCode
			,WhsCode
			,CalcPrice
		from (
			select 
			 ItemCode
			,WhsCode
			,Avg(CalcPrice) CalcPrice
			from 
				InventoryJournal 
			where 
				ItemCode = @itemCode and WhsCode = @whsCode
			group by 
			 ItemCode
			,WhsCode

		) I
	end

	if @valuationMethod = 'F' -- FIFO
	begin 

	
		;with T1 as (
			select 
			 ROW_NUMBER() over( order by DocDate )  row_num
			,ItemCode
			,WhsCode
			,DocDate 
			,CalcPrice
			,InQty 
			,OutQty 
			,sum(InQty-OutQty) over(order by InventoryJournalID ) BalanceQty
			from 
			InventoryJournal 
			where ItemCode = @itemCode and WhsCode = @whsCode
		)

		select * into #T1 from T1 
		
		declare @row_num int =0 

		select @row_num = Isnull(max(row_num),0) from #T1  where BalanceQty = 0
		
		--select * from #T1 
		--print @row_num

		select top 1 
 		     ItemCode
			,WhsCode
			,CalcPrice
		from #t1 where row_num > @row_num

		drop table #T1
	end
end 
go


