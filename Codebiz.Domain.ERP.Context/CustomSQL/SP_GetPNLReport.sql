
if exists(select 1 from sys.sysobjects where [name] = 'SP_GetPNLReport')
	drop procedure SP_GetPNLReport
go 

create procedure SP_GetPNLReport
	@dateType nvarchar(1) = 'P', -- Posting date, dUe date, Docdate
	@reportType nvarchar(1) = 'A',  -- Annual , Quarter, Mothly
	@fromDate as datetime null = '2021-01-01',
	@toDate as datetime  null = '2021-12-31'
as 
begin 

		if(@reportType  = 'A')
		begin 
			select 
				 ACT.AcctCode 
				,ACT.AcctName 
				,ACT.GroupMask 
				,ACT.Postable 
				,ACT.FatherCode 
				,ACT.Level 
				,cast(Sum(JEN1.Credit-JEN1.Debit) as decimal(19,6)) as CurrentPeriod
				,cast(Sum(JEN1.Credit-JEN1.Debit) as decimal(19,6)) as CurrentYear

				into #t1
			from 
				GLAccount ACT 
					outer apply(
						select Debit,Credit from JournalEntryDetail JEN1 where JEN1.GLAcctCode =  ACT.AcctCode and 
						case 
							when @dateType = 'P' and JEN1.RefDate between @fromDate and @toDate then 1 
							when @dateType = 'U' and JEN1.RefDate2  between @fromDate and @toDate then 1 
							when @dateType = 'D' and JEN1.RefDate3  between @fromDate and @toDate then 1 
						else 0 end = 1 

					) JEN1
			where ACT.GroupMask in( 4,5,6,7,8)
			group by 
				ACT.AcctCode 
				,ACT.AcctName 
				,ACT.GroupMask 
				,ACT.Postable 
				,ACT.FatherCode 
				,ACT.Level

				--exec SP_GetPNLReport
				update A set CurrentPeriod = total
				from #t1 A
				cross apply(
				  select sum(CurrentPeriod) total  from #t1 where FatherCode = A.AcctCode  
				) B
				where A.Postable = 'N'
				

				update A set CurrentPeriod = total
				from #t1 A
				cross apply(
				  select sum(CurrentPeriod) total  from #t1 where FatherCode = A.AcctCode   
				) B
				where A.Postable = 'N'

				update A set CurrentPeriod = total
				from #t1 A
				cross apply(
				  select sum(CurrentPeriod) total  from #t1 where FatherCode = A.AcctCode   
				) B
				where A.Postable = 'N'

				update A set CurrentPeriod = total
				from #t1 A
				cross apply(
				  select sum(CurrentPeriod) total  from #t1 where FatherCode = A.AcctCode   
				) B
				where A.Postable = 'N'


				update A set CurrentPeriod = total
				from #t1 A
				cross apply(
				  select sum(CurrentPeriod) total  from #t1 where FatherCode = A.AcctCode   
				) B
				where A.Postable = 'N'

				select * from #t1
				drop table #t1
		end 

		if(@reportType  = 'Q')
		begin 
			select 1 

				--update A set CurrentPeriod = total
				--from #t1 A
				--cross apply(
				--  select sum(CurrentPeriod) total  from #t1 where FatherCode = A.AcctCode   
				--) B
				--where A.Postable = 'N'

				--update A set CurrentPeriod = total
				--from #t1 A
				--cross apply(
				--  select sum(CurrentPeriod) total  from #t1 where FatherCode = A.AcctCode   
				--) B
				--where A.Postable = 'N'

				--update A set CurrentPeriod = total
				--from #t1 A
				--cross apply(
				--  select sum(CurrentPeriod) total  from #t1 where FatherCode = A.AcctCode   
				--) B
				--where A.Postable = 'N'


				--update A set CurrentPeriod = total
				--from #t1 A
				--cross apply(
				--  select sum(CurrentPeriod) total  from #t1 where FatherCode = A.AcctCode   
				--) B
				--where A.Postable = 'N'

				--select * from #t1
				--drop table #t1
		end

		if(@reportType  = 'M')
		begin 
			select 
				 ACT.AcctCode 
				,ACT.AcctName 
				,ACT.GroupMask 
				,ACT.Postable 
				,ACT.FatherCode 
				,ACT.Level 
				,cast(Sum(Jan) as decimal(19,6)) as Jan
				,cast(Sum(Feb) as decimal(19,6)) as Feb
				,cast(Sum(Mar) as decimal(19,6)) as Mar
				,cast(Sum(Apr) as decimal(19,6)) as Apr
				,cast(Sum(May) as decimal(19,6)) as May
				,cast(Sum(Jun) as decimal(19,6)) as Jun
				,cast(Sum(Jul) as decimal(19,6)) as Jul
				,cast(Sum(Aug) as decimal(19,6)) as Aug
				,cast(Sum(Sep) as decimal(19,6)) as Sep
				, cast(Sum(Oct) as decimal(19,6)) as Oct
				, cast(Sum(Nov) as decimal(19,6)) as Nov
				, cast(Sum(Dec) as decimal(19,6)) as Dec
				into #TM
			from 
				GLAccount ACT 
					outer apply(
						select 
							Debit
							,Credit
							,RefDate
							,RefDate2
							,RefDate3
							,case when Month(JEN1.RefDate) = 1 then 1 else 0 end * (Credit-Debit) as Jan
							,case when Month(JEN1.RefDate) = 2 then 1 else 0 end * (Credit-Debit) as Feb
							,case when Month(JEN1.RefDate) = 3 then 1 else 0 end * (Credit-Debit) as Mar
							,case when Month(JEN1.RefDate) = 4 then 1 else 0 end * (Credit-Debit) as Apr
							,case when Month(JEN1.RefDate) = 5 then 1 else 0 end * (Credit-Debit) as May
							,case when Month(JEN1.RefDate) = 6 then 1 else 0 end * (Credit-Debit) as Jun
							,case when Month(JEN1.RefDate) = 7 then 1 else 0 end * (Credit-Debit) as Jul
							,case when Month(JEN1.RefDate) = 8 then 1 else 0 end * (Credit-Debit) as Aug
							,case when Month(JEN1.RefDate) = 9 then 1 else 0 end * (Credit-Debit) as Sep
							,case when Month(JEN1.RefDate) = 10 then 1 else 0 end * (Credit-Debit) as Oct
							,case when Month(JEN1.RefDate) = 11 then 1 else 0 end * (Credit-Debit) as Nov
							,case when Month(JEN1.RefDate) = 12 then 1 else 0 end * (Credit-Debit) as Dec
						from 
							JournalEntryDetail JEN1 where JEN1.GLAcctCode =  ACT.AcctCode and 
						case 
							when @dateType = 'P' and JEN1.RefDate between @fromDate and @toDate then 1 
							when @dateType = 'U' and JEN1.RefDate2  between @fromDate and @toDate then 1 
							when @dateType = 'D' and JEN1.RefDate3  between @fromDate and @toDate then 1 
						else 0 end = 1 
					) JEN1
						where ACT.GroupMask in( 4,5,6,7,8)
						group by 
							ACT.AcctCode 
							,ACT.AcctName 
							,ACT.GroupMask 
							,ACT.Postable 
							,ACT.FatherCode 
							,ACT.Level
							--,JEN1.RefDate 
							--,JEN1.RefDate2 
							--,JEN1.RefDate3 

				update A set 
				 Jan = B.Jan
				,Feb = B.Feb
				,Mar = B.Mar
				,Apr = B.Apr
				,May = B.May
				,Jun = B.Jun
				,Jul = B.Jul
				,Aug = B.Aug
				,Sep = B.Sep
				,Oct = B.Oct
				,Nov = B.Nov
				,Dec = B.Dec

				from #TM A
				cross apply(
				  select 
					 cast(Sum(Jan) as decimal(19,6)) as Jan
					,cast(Sum(Feb) as decimal(19,6)) as Feb
					,cast(Sum(Mar) as decimal(19,6)) as Mar
					,cast(Sum(Apr) as decimal(19,6)) as Apr
					,cast(Sum(May) as decimal(19,6)) as May
					,cast(Sum(Jun) as decimal(19,6)) as Jun
					,cast(Sum(Jul) as decimal(19,6)) as Jul
					,cast(Sum(Aug) as decimal(19,6)) as Aug
					,cast(Sum(Sep) as decimal(19,6)) as Sep
					,cast(Sum(Oct) as decimal(19,6)) as Oct
					,cast(Sum(Nov) as decimal(19,6)) as Nov
					,cast(Sum(Dec) as decimal(19,6)) as Dec
				  
				  from #TM where FatherCode = A.AcctCode   
				) B
				where A.Postable = 'N'

				update A set 
				 Jan = B.Jan
				,Feb = B.Feb
				,Mar = B.Mar
				,Apr = B.Apr
				,May = B.May
				,Jun = B.Jun
				,Jul = B.Jul
				,Aug = B.Aug
				,Sep = B.Sep
				,Oct = B.Oct
				,Nov = B.Nov
				,Dec = B.Dec

				from #TM A
				cross apply(
				  select 
					 cast(Sum(Jan) as decimal(19,6)) as Jan
					,cast(Sum(Feb) as decimal(19,6)) as Feb
					,cast(Sum(Mar) as decimal(19,6)) as Mar
					,cast(Sum(Apr) as decimal(19,6)) as Apr
					,cast(Sum(May) as decimal(19,6)) as May
					,cast(Sum(Jun) as decimal(19,6)) as Jun
					,cast(Sum(Jul) as decimal(19,6)) as Jul
					,cast(Sum(Aug) as decimal(19,6)) as Aug
					,cast(Sum(Sep) as decimal(19,6)) as Sep
					,cast(Sum(Oct) as decimal(19,6)) as Oct
					,cast(Sum(Nov) as decimal(19,6)) as Nov
					,cast(Sum(Dec) as decimal(19,6)) as Dec
				  
				  from #TM where FatherCode = A.AcctCode   
				) B
				where A.Postable = 'N'

				update A set 
				 Jan = B.Jan
				,Feb = B.Feb
				,Mar = B.Mar
				,Apr = B.Apr
				,May = B.May
				,Jun = B.Jun
				,Jul = B.Jul
				,Aug = B.Aug
				,Sep = B.Sep
				,Oct = B.Oct
				,Nov = B.Nov
				,Dec = B.Dec

				from #TM A
				cross apply(
				  select 
					 cast(Sum(Jan) as decimal(19,6)) as Jan
					,cast(Sum(Feb) as decimal(19,6)) as Feb
					,cast(Sum(Mar) as decimal(19,6)) as Mar
					,cast(Sum(Apr) as decimal(19,6)) as Apr
					,cast(Sum(May) as decimal(19,6)) as May
					,cast(Sum(Jun) as decimal(19,6)) as Jun
					,cast(Sum(Jul) as decimal(19,6)) as Jul
					,cast(Sum(Aug) as decimal(19,6)) as Aug
					,cast(Sum(Sep) as decimal(19,6)) as Sep
					,cast(Sum(Oct) as decimal(19,6)) as Oct
					,cast(Sum(Nov) as decimal(19,6)) as Nov
					,cast(Sum(Dec) as decimal(19,6)) as Dec
				  
				  from #TM where FatherCode = A.AcctCode   
				) B
				where A.Postable = 'N'

				update A set 
				 Jan = B.Jan
				,Feb = B.Feb
				,Mar = B.Mar
				,Apr = B.Apr
				,May = B.May
				,Jun = B.Jun
				,Jul = B.Jul
				,Aug = B.Aug
				,Sep = B.Sep
				,Oct = B.Oct
				,Nov = B.Nov
				,Dec = B.Dec

				from #TM A
				cross apply(
				  select 
					 cast(Sum(Jan) as decimal(19,6)) as Jan
					,cast(Sum(Feb) as decimal(19,6)) as Feb
					,cast(Sum(Mar) as decimal(19,6)) as Mar
					,cast(Sum(Apr) as decimal(19,6)) as Apr
					,cast(Sum(May) as decimal(19,6)) as May
					,cast(Sum(Jun) as decimal(19,6)) as Jun
					,cast(Sum(Jul) as decimal(19,6)) as Jul
					,cast(Sum(Aug) as decimal(19,6)) as Aug
					,cast(Sum(Sep) as decimal(19,6)) as Sep
					,cast(Sum(Oct) as decimal(19,6)) as Oct
					,cast(Sum(Nov) as decimal(19,6)) as Nov
					,cast(Sum(Dec) as decimal(19,6)) as Dec
				  
				  from #TM where FatherCode = A.AcctCode   
				) B
				where A.Postable = 'N'


				update A set 
				 Jan = B.Jan
				,Feb = B.Feb
				,Mar = B.Mar
				,Apr = B.Apr
				,May = B.May
				,Jun = B.Jun
				,Jul = B.Jul
				,Aug = B.Aug
				,Sep = B.Sep
				,Oct = B.Oct
				,Nov = B.Nov
				,Dec = B.Dec

				from #TM A
				cross apply(
				  select 
					 cast(Sum(Jan) as decimal(19,6)) as Jan
					,cast(Sum(Feb) as decimal(19,6)) as Feb
					,cast(Sum(Mar) as decimal(19,6)) as Mar
					,cast(Sum(Apr) as decimal(19,6)) as Apr
					,cast(Sum(May) as decimal(19,6)) as May
					,cast(Sum(Jun) as decimal(19,6)) as Jun
					,cast(Sum(Jul) as decimal(19,6)) as Jul
					,cast(Sum(Aug) as decimal(19,6)) as Aug
					,cast(Sum(Sep) as decimal(19,6)) as Sep
					,cast(Sum(Oct) as decimal(19,6)) as Oct
					,cast(Sum(Nov) as decimal(19,6)) as Nov
					,cast(Sum(Dec) as decimal(19,6)) as Dec
				  
				  from #TM where FatherCode = A.AcctCode   
				) B
				where A.Postable = 'N'
				

				select * from #TM
				drop table #TM
		end
	--exec SP_GetPNLReport
end
go

