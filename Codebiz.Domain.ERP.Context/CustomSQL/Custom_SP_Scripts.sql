/*update GL */
update GLAccount  set Postable  =  'Y' where Postable = 'N' and Level > 2 and AcctCode not in (select FatherCode from GLAccount group by FatherCode)
go 







