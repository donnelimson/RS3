

if exists( select 1 from sys.sysobjects  where [name] = 'SP_GetPostingPeriod' and xtype = 'P')
	drop procedure SP_GetPostingPeriod
go

Create Procedure SP_GetPostingPeriod
@postingDate date ,
@dueDate date = null,
@docDate date = null
as 
begin 
  select * from 
	FinancialPeriodDetail A
  where 
	A.FromRefDate <= convert(date,@postingDate) and A.ToRefDate >= convert(date,@postingDate) 
	and A.FromDueDate <= Isnull(@dueDate, GetDate()) and A.ToDueDate >= ISNULL(@dueDate, GetDate()) 
	and A.FromTaxDate <= Isnull(@docDate, GetDate()) and A.ToTaxDate >= ISNULL(@docDate, GetDate()) 
end
go 