if exists(select 1 from sys.sysobjects where [name] = 'SP_GetItemsForInventoryQtyPosting' and xtype = 'P')
	drop procedure SP_GetItemsForInventoryQtyPosting
go 

create procedure SP_GetItemsForInventoryQtyPosting
@whsCode nvarchar(30)
as begin 
	select 
		0 DocLineID
		,0 DocID
		,cast(ROW_NUMBER() over (order by ItemCode,WhsCode)  as int) LineNum
		,'IQP' ObjType
		,ITM.ItemCode 
		,ITM.ItemName 
		,ITW.OnHand  OnHandQtyBef 
		,(ITW.Counted - ITW.OnHand) QtyDiff
		,ITW.Counted Quantity 
		,0.0 Price 
		,0.0 Rate 
		,WHS.WhsCode  WhsCode 
		,ITM.InvntryUOM InvntryUOM 
		,null InvntryOffsetIncAcct
		,null InvntryOffsetDecAcct
		,null DocTotal
		,null DocTotalFC
		,null DocTotalSC 
		,null OcrCode
		,null OcrCode1 
		,null OcrCode2 
		,null OcrCode3
		,null OcrCode4
		,null OcrCode5
		,null Project
		,cast(1 as bit) IsActive
		,1 CreatedByAppUserId
		,Getdate() CreatedOn
		,null ModifiedByAppUserId
		,null ModifiedOn

	from 
		ItemMaster ITM
		inner join ItemWarehouse ITW on ITM.ItemMasterID = ITW.ItemMasterID 
		inner join Warehouse  WHS on WHS.WarehouseID = ITW.WarehouseId 
	where 
		WHS.WhsCode = @whsCode
		and ITW.IsCounted = 'Y'
end 
go 