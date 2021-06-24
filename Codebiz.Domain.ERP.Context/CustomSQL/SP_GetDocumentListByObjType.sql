

if exists ( select 1 from sys.sysobjects where [name] = 'SP_GetDocumentListByObjType' and xtype = 'P')
	drop procedure SP_GetDocumentListByObjType
go

create procedure SP_GetDocumentListByObjType
@objType nvarchar(8) = '',
@predicate nvarchar(120) = ''
as 
begin 

 if @objType  =  'SQU'
	 select * from SalesQuotation where 
	 case 
		when @predicate = '' then 1 
		when @predicate <> ''  and DocNum like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardCode like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardName like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and NumAtCard like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and Remarks like '%'  + @predicate  + '%' then 1 
		else  0
	 end = 1 
 
 else if @objType  =  'SOR'
	 select * from SalesOrder where 
	 case 
		when @predicate = '' then 1 
		when @predicate <> ''  and DocNum like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardCode like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardName like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and NumAtCard like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and Remarks like '%'  + @predicate  + '%' then 1 
		else  0
	 end = 1 

 else if @objType  =  'SDN'
	 select * from SalesDelivery where 
	 case 
		when @predicate = '' then 1 
		when @predicate <> ''  and DocNum like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardCode like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardName like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and NumAtCard like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and Remarks like '%'  + @predicate  + '%' then 1 
		else  0
	 end = 1 


else if @objType  =  'SRD'
	 select * from SalesReturn where 
	 case 
		when @predicate = '' then 1 
		when @predicate <> ''  and DocNum like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardCode like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardName like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and NumAtCard like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and Remarks like '%'  + @predicate  + '%' then 1 
		else  0
	 end = 1 
 
 else if @objType  =  'SIN'
	 select * from SalesInvoice where 
	 case 
		when @predicate = '' then 1 
		when @predicate <> ''  and DocNum like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardCode like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardName like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and NumAtCard like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and Remarks like '%'  + @predicate  + '%' then 1 
		else  0
	 end = 1 

 else if @objType  =  'SCR'
	 select * from SalesCreditNote where 
	 case 
		when @predicate = '' then 1 
		when @predicate <> ''  and DocNum like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardCode like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardName like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and NumAtCard like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and Remarks like '%'  + @predicate  + '%' then 1 
		else  0
	 end = 1 


 if @objType  =  'PQU'
	 select * from PurchaseQuotation where 
	 case 
		when @predicate = '' then 1 
		when @predicate <> ''  and DocNum like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardCode like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardName like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and NumAtCard like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and Remarks like '%'  + @predicate  + '%' then 1 
		else  0
	 end = 1 
 
 else if @objType  =  'POR'
    begin
	 select * from PurchaseOrder where 
	 case 
		when @predicate = '' then 1 
		when @predicate <> ''  and DocNum like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardCode like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardName like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and NumAtCard like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and Remarks like '%'  + @predicate  + '%' then 1 
		else  0
	 end = 1 
	 end 
 else if @objType  =  'PDN'
	 select * from PurchaseDelivery where 
	 case 
		when @predicate = '' then 1 
		when @predicate <> ''  and DocNum like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardCode like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardName like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and NumAtCard like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and Remarks like '%'  + @predicate  + '%' then 1 
		else  0
	 end = 1 

else if @objType  =  'PRD'
	 select * from PurchaseReturn where 
	 case 
		when @predicate = '' then 1 
		when @predicate <> ''  and DocNum like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardCode like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardName like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and NumAtCard like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and Remarks like '%'  + @predicate  + '%' then 1 
		else  0
	 end = 1 
 
 else if @objType  =  'PIN'
	 select * from PurchaseInvoice where 
	 case 
		when @predicate = '' then 1 
		when @predicate <> ''  and DocNum like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardCode like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardName like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and NumAtCard like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and Remarks like '%'  + @predicate  + '%' then 1 
		else  0
	 end = 1 

 else if @objType  =  'PCR'
	 select * from PurchaseCreditNote where 
	 case 
		when @predicate = '' then 1 
		when @predicate <> ''  and DocNum like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardCode like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and CardName like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and NumAtCard like '%'  + @predicate  + '%' then 1 
		when @predicate <> ''  and Remarks like '%'  + @predicate  + '%' then 1 
		else  0
	 end = 1 
end
go