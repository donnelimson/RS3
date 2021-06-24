
if exists (select 1 from sys.sysobjects where [name] = 'SP_GetAvailableSerialNo' and xtype = 'P')
	drop procedure SP_GetAvailableSerialNo
go 


Create Procedure SP_GetAvailableSerialNo
@itemCode as nvarchar(30)
as
begin
  select * from ItemSerialNumberTransaction A
  where 
	ItemCode = @itemCode
	and Direction = 0
	and not exists ( select 1 from ItemSerialNumberTransaction where ItemCode = @itemCode and Direction = 1 and InternalSerialNo = A.InternalSerialNo)
end 
go 
--exec SP_GetAvailableSerialNo @itemCode = 'I00001'