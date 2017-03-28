CREATE PROCEDURE [dbo].[prcGetCollectionData]
	
AS
BEGIN
	Select 
		p.*,
		pi.binImage
	from dbo.Products p 
		join dbo.ProductImages pi on 
		pi.intImageID = p.intImageID
	order by intProductID desc
END