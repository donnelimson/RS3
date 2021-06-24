if exists( select 1 from sys.sysobjects where [name] = 'SP_UpdateOrderQty' and xtype = 'P')
	drop procedure SP_UpdateOrderQty
go 

create Procedure SP_UpdateOrderQty
 @itemCode nvarchar(30) = '',
 @objType nvarchar(5) = ''
 as
begin

	if @objType   = 'POR' 
	begin
		update ItemWarehouse  set Ordered = POR.TotalOrderQty , Available = (OnHand  - [Committed] + POR.TotalOrderQty)
		from 
		ItemWarehouse ITW
		inner join ItemMaster ITM on ITW.ItemMasterID = ITM.ItemMasterID 
		inner join Warehouse WHS on WHS.WarehouseID  = ITW.WarehouseId 
		cross apply 
		(
			select ItemCode ,WhsCode , Sum(OpenQty) TotalOrderQty 
			from 
				PurchaseOrderDetail 
			where 
				ItemCode = ITM.ItemCode 
				and WhsCode = WHS.WhsCode 
				and LineStatus = 'O'

			group by ItemCode,WhsCode 
		) POR

		where ITM.ItemCode = @itemCode 
		
	end

	if @objType = 'SOR'
	begin
	update ItemWarehouse  set [Committed] = SOR.TotalOrderQty , Available = (OnHand  - SOR.TotalOrderQty + Ordered )
		from 
		ItemWarehouse ITW
		inner join ItemMaster ITM on ITW.ItemMasterID = ITM.ItemMasterID 
		inner join Warehouse WHS on WHS.WarehouseID  = ITW.WarehouseId 
		cross apply 
		(
			select ItemCode ,WhsCode , Sum(OpenQty) TotalOrderQty 
			from 
				SalesOrderDetail  
			where 
				ItemCode = ITM.ItemCode 
				and WhsCode = WHS.WhsCode 
				and LineStatus = 'O'

			group by ItemCode,WhsCode 
		) SOR

		where ITM.ItemCode = @itemCode 
	end
	--exec SP_UpdateOrderQty 'I53601261', 'SOR'
end
go