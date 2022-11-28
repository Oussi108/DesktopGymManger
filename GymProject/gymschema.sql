


-- Script Date: 3/5/2021 4:01   - 
CREATE TABLE [Trainers] (
  [Id] int IDENTITY (1,1) NOT NULL
, [LastName] nvarchar(50) NULL
, [FirstName] nvarchar(50) NULL
, [Birthday] datetime NULL
, [Gender] nvarchar(6) NULL
, [Phone] nvarchar(50) NULL
, [Email] nvarchar(50) NULL
, [Description] nvarchar(200) NULL
);
GO
CREATE TABLE [MembershipType] (
  [Id] int IDENTITY (1,1) NOT NULL
, [Name] nvarchar(100) NULL
, [Price] money NULL
, [Description] nvarchar(255) NULL
);
GO
CREATE TABLE [Members] (
  [Id] int IDENTITY (1,1) NOT NULL
, [CIN] Varchar(10) 
, [LastName] nvarchar(50) NULL
, [FirstName] nvarchar(50) NULL
, [Birthday] datetime NULL
, [Gender] nvarchar(6) NULL
, [Adress] nvarchar(100) NULL
, [Phone] nvarchar(100) NULL
, [Email] nvarchar(100) NULL
, [Image] image NULL
, [Description] nvarchar(200) NULL
, [GroupID] int
);
GO
CREATE TABLE [Memberships] (
  [Id] int IDENTITY (1,1) NOT NULL
, [MemberId] int NOT NULL
, [TrainerId] int NOT NULL
, [MembershipType] int NOT NULL
, [StartDate] datetime NOT NULL
, [EndDate] datetime NULL

);
GO
CREATE TABLE [HistoMember] (
  [Id] int IDENTITY (1,1) NOT NULL
, [MemberId] int NULL
, [Enterdate] date NULL
, [Leavedate] date NULL
);
GO
CREATE TABLE [Groups] (
  [Id] int IDENTITY (1,1) NOT NULL
, [MemberID] int NULL
,[NameGroupe] Varchar(55)
);
CREATE TABLE [Session] (
  [Id] int IDENTITY (1,1) NOT NULL
, [GroupID] int NULL
,[Day] date
);



GO
ALTER TABLE [Trainers] ADD CONSTRAINT [PK_Trainers] PRIMARY KEY ([Id]);
GO
ALTER TABLE [MembershipType] ADD CONSTRAINT [PK_MembershipType] PRIMARY KEY ([Id]);
GO
ALTER TABLE [Members] ADD CONSTRAINT [PK_Members] PRIMARY KEY ([Id]);
GO
ALTER TABLE [Memberships] ADD CONSTRAINT [PK_Memberships] PRIMARY KEY ([Id]);
GO
ALTER TABLE [Groups] ADD CONSTRAINT [PK_Groups] PRIMARY KEY ([Id]);
GO
ALTER TABLE [Session] ADD CONSTRAINT [PK_Session] PRIMARY KEY ([Id]);
GO
ALTER TABLE [HistoMember] ADD CONSTRAINT [PK_Checkin] PRIMARY KEY ([Id]);
GO
ALTER TABLE [Trainers] ADD CONSTRAINT [UQ__Trainers__00000000000000C4] UNIQUE ([Id]);
GO
ALTER TABLE [MembershipType] ADD CONSTRAINT [UQ__Plans__000000000000008C] UNIQUE ([Id]);
GO
ALTER TABLE [Members] ADD CONSTRAINT [UQ__Members__0000000000000114] UNIQUE ([Id]);
GO

ALTER TABLE [Memberships] ADD CONSTRAINT [UQ__Memberships__00000000000001D2] UNIQUE ([Id]);
GO
ALTER TABLE [HistoMember] ADD CONSTRAINT [UQ__Checkin__00000000000001E2] UNIQUE ([Id]);
GO


ALTER TABLE [Members] ADD CONSTRAINT [FK_Group_Members] FOREIGN KEY ([GroupID]) REFERENCES [Groups]([Id]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [Session] ADD CONSTRAINT [FK_Group_Session] FOREIGN KEY ([GroupID]) REFERENCES [Groups]([Id]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [HistoMember] ADD CONSTRAINT [FK_Member_histo] FOREIGN KEY ([MemberId]) REFERENCES [Members]([Id]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [Memberships] ADD CONSTRAINT [FK_Member_Memberships] FOREIGN KEY ([MemberId]) REFERENCES [Members]([Id]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [Memberships] ADD CONSTRAINT [FK_type_Memberships] FOREIGN KEY ([MembershipType]) REFERENCES [MembershipType]([Id]) ON DELETE CASCADE ON UPDATE CASCADE;
GO
ALTER TABLE [Memberships] ADD CONSTRAINT [FK_Trainer_Memberships] FOREIGN KEY ([TrainerId]) REFERENCES [Trainers]([Id]) ON DELETE CASCADE ON UPDATE CASCADE;
GO


