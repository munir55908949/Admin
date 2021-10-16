CREATE VIEW dbo.OrderDataView
AS
SELECT dbo.OrderData.Id, dbo.OrderData.Price, dbo.OrderData.InvoiceId, dbo.OrderData.IsDeleted, dbo.CategoryService.PriceUnit, dbo.ServiceType.Name AS ServiceTypeName, dbo.UnitType.Name AS UnitTypeName, 
                  dbo.Category.Name AS CategoryName, dbo.OrderData.UnitNo
FROM     dbo.OrderData INNER JOIN
                  dbo.CategoryService ON dbo.OrderData.CategoryServiceId = dbo.CategoryService.Id INNER JOIN
                  dbo.ServiceType ON dbo.CategoryService.ServiceTypeId = dbo.ServiceType.Id INNER JOIN
                  dbo.UnitType ON dbo.CategoryService.UnitTypeId = dbo.UnitType.Id INNER JOIN
                  dbo.Category ON dbo.CategoryService.CategoryId = dbo.Category.Id
WHERE  (dbo.OrderData.IsDeleted = 0) OR
                  (dbo.OrderData.IsDeleted IS NULL)
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'OrderDataView';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'd
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2892
         Alias = 3636
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'OrderDataView';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[62] 4[3] 2[19] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[50] 4[25] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1[50] 2[25] 3) )"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4[30] 2[40] 3) )"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1[56] 3) )"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2[66] 3) )"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1[75] 4) )"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "OrderData"
            Begin Extent = 
               Top = 66
               Left = 323
               Bottom = 316
               Right = 579
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "CategoryService"
            Begin Extent = 
               Top = 112
               Left = 703
               Bottom = 345
               Right = 930
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "ServiceType"
            Begin Extent = 
               Top = 177
               Left = 1122
               Bottom = 318
               Right = 1316
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "UnitType"
            Begin Extent = 
               Top = 340
               Left = 1122
               Bottom = 481
               Right = 1316
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Category"
            Begin Extent = 
               Top = 4
               Left = 1122
               Bottom = 145
               Right = 1316
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 13
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   En', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'OrderDataView';

