﻿--Table Schema
CREATE TABLE [dbo].[TStudent] (
    [StudentID]   INT          IDENTITY (1, 1) NOT NULL,
    [FullName]    VARCHAR (50) NOT NULL,
    [Contact]     BIGINT       NOT NULL,
    [Country]     VARCHAR (50) NOT NULL,
    [Designation] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([StudentID] ASC)
);
---Insert some data into table
SET IDENTITY_INSERT [dbo].[TStudent] ON
INSERT INTO [dbo].[TStudent] ([StudentID], [FullName], [Contact], [Country], [Designation]) VALUES (1, N'Raviteja', 934348384, N'India', N'software trainee')
INSERT INTO [dbo].[TStudent] ([StudentID], [FullName], [Contact], [Country], [Designation]) VALUES (2, N'avinash', 903983473, N'china', N'trainee')
INSERT INTO [dbo].[TStudent] ([StudentID], [FullName], [Contact], [Country], [Designation]) VALUES (3, N'Rajk', 938483746, N'Canada', N'senior')
SET IDENTITY_INSERT [dbo].[TStudent] OFF