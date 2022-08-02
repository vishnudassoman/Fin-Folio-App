/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [AMC]
      ,[Code]
      ,[Scheme_Name]
      ,[Scheme_Type]
      ,[Scheme_Category]
      ,[Scheme_NAV_Name]
      ,[Scheme_Minimum_Amount]
      ,[Launch_Date]
      ,[Closure_Date]
      ,[ISIN_Div_Payout_ISIN_GrowthISIN_Div_Reinvestment]
  FROM [Portfolio].[dbo].[SchemeData1307222335SS]
  where Launch_Date is  null

  --update [dbo].[SchemeData1307222335SS]
  --set [Launch_Date] = DATEADD(YYYY, -10, GETDATE())
  --where
  -- Launch_Date is  nul
  
  
  select * from
  [dbo].[Schemes]
  where
  NAVName like 'TATA Digi%'