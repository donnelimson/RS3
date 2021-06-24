
if exists(select 1 from sys.sysobjects where [name] = 'SP_UpdateBaseOpenQty' and xtype = 'P')
	drop procedure SP_UpdateBaseOpenQty
go 
create procedure SP_UpdateBaseOpenQty  
@docID as int = 0,  
@itemCode nvarchar(30) = '',  
@objType nvarchar(8) = ''  
as   
begin  
 if @objType = 'PQU'  
  begin   
   update   
    PurchaseQuotationDetail  set OpenQty = Quantity - T1.TargetQty   
   from   
    PurchaseQuotationDetail  PQU1  
   cross apply (  
    select   
     Sum(Quantity) TargetQty   
    from   
     PurchaseOrderDetail POR1  
    where   
     POR1.BaseEntry = PQU1.DocID   
     and POR1.BaseLine = PQU1.LineNum   
     and POR1.ItemCode = @itemCode   
    group by   
     POR1.ItemCode, POR1.BaseEntry   
    ) T1  
  
   where DocID = @docID  
  
  end  
  
 if @objType = 'POR'  
  begin   
   update   
    PurchaseOrderDetail set OpenQty = Quantity - T1.TargetQty   
   from   
    PurchaseOrderDetail  POR1  
   cross apply (  
    select   
     Sum(Quantity) TargetQty   
    from   
     PurchaseDeliveryDetail PDN1  
    where   
     PDN1.BaseEntry = POR1.DocID   
     and PDN1.BaseLine = POR1.LineNum   
     and PDN1.ItemCode = @itemCode   
    group by   
     PDN1.ItemCode, PDN1.BaseEntry   
    ) T1  
  
   where DocID = @docID  
  end  
  
 if @objType = 'PDN'  
 begin   
  update   
   PurchaseDeliveryDetail set OpenQty = Quantity - T1.TargetQty   
  from   
   PurchaseDeliveryDetail PDN1  
  cross apply (  
   select   
    Sum(Quantity) TargetQty   
   from   
    PurchaseInvoiceDetail PIN1  
   where   
    PIN1.BaseEntry = PDN1.DocID   
    and PIN1.BaseLine = PDN1.LineNum   
    and PIN1.ItemCode = @itemCode   
   group by   
    PIN1.ItemCode, PIN1.BaseEntry   
   ) T1  
  
  where DocID = @docID  
 end  

 if @objType = 'SQU'  
  begin   
   update   
    SalesQuotationDetail  set OpenQty = Quantity - T1.TargetQty   
   from   
    SalesQuotationDetail  SQU1  
   cross apply (  
    select   
     Sum(Quantity) TargetQty   
    from   
     SalesOrderDetail SOR1  
    where   
     SOR1.BaseEntry = SQU1.DocID   
     and SOR1.BaseLine = SQU1.LineNum   
     and SOR1.ItemCode = @itemCode   
    group by   
     SOR1.ItemCode, SOR1.BaseEntry   
    ) T1  
  
   where DocID = @docID  
  
  end  
  
 if @objType = 'SOR'  
  begin   
   update   
    SalesOrderDetail set OpenQty = Quantity - T1.TargetQty   
   from   
    SalesOrderDetail  SOR1  
   cross apply (  
    select   
     Sum(Quantity) TargetQty   
    from   
     SalesDeliveryDetail SDN1  
    where   
     SDN1.BaseEntry = SOR1.DocID   
     and SDN1.BaseLine = SOR1.LineNum   
     and SDN1.ItemCode = @itemCode   
    group by   
     SDN1.ItemCode, SDN1.BaseEntry   
    ) T1  
  
   where DocID = @docID  
  end  
  
 if @objType = 'SDN'  
 begin   
  update   
   SalesDeliveryDetail set OpenQty = Quantity - T1.TargetQty   
  from   
   SalesDeliveryDetail SDN1  
  cross apply (  
   select   
    Sum(Quantity) TargetQty   
   from   
    SalesInvoiceDetail SIN1  
   where   
    SIN1.BaseEntry = SDN1.DocID   
    and SIN1.BaseLine = SDN1.LineNum   
    and SIN1.ItemCode = @itemCode   
   group by   
    SIN1.ItemCode, SIN1.BaseEntry   
   ) T1  
  
  where DocID = @docID  
 end  
end
go 
