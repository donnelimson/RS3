if exists(select 1 from sys.sysobjects where [name] = 'SP_InventoryQtyCount' and xtype = 'P')
	drop procedure SP_InventoryQtyCount
go

Create Procedure SP_InventoryQtyCount
@itemCode nvarchar(30) = null,
@whsCode nvarchar(30)= null,
@countedQty numeric(19,6) = 0,
@isCounted nchar(1) ='Y' 
as
begin 

 declare @itemID int , 
		 @warehouseID int
 
 set @itemID = Isnull((select top 1 ItemMasterID from ItemMaster where ItemCode = @itemCode),0)
 set @warehouseID = Isnull((select top 1 WarehouseID from Warehouse where WhsCode  = @whsCode),0)

 if @itemID > 0
 begin 
	update ItemWarehouse set Counted = @countedQty , IsCounted =@IsCounted  where ItemMasterID = @itemID and WarehouseId = @warehouseID
 end 
end 
go