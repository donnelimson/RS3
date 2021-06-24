if exists (select 1 from sys.sysobjects where [name] = 'SP_UpdateInventoryOnHand' and xtype = 'P')
	drop procedure SP_UpdateInventoryOnHand
go 

create procedure SP_UpdateInventoryOnHand
@itemCode nvarchar(30) = '',
@whsCode nvarchar(30) = '',
@objType nvarchar(8) = ''
as
begin 
declare @res int = 0
	Update ItemWarehouse 
		set OnHand  = TotalOnhand, 
		Counted = case when @objType = 'IQP' then 0 end,
		IsCounted = case when @objType = 'IQP' then 'N' end 
	from ItemWarehouse IW
	inner join ItemMaster IM on IM.ItemMasterID = IW.ItemMasterID 
	inner join Warehouse WHS on WHS.WarehouseID = IW.WarehouseId 
	cross apply(
	 select  Sum(InQty - OutQty) as TotalOnHand
	 from InventoryJournal IJ
	 where ItemCode  = @itemCode and WhsCode = @whsCode
	 group by ItemCode, WhsCode 
	) I

	where IM.ItemCode = @itemCode and WHS.WhsCode = @whsCode 
	set @res = @@ERROR
	return @res
end 
go 