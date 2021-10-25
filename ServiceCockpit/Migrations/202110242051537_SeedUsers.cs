namespace ServiceCockpit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'4344ad9b-43ff-4597-9c69-9a54e66dbec6', N'Gfeller2.Admin@Gfeller.ch', 0, N'AChMguH3K5kxY79AVZFCbkABm9J7OsDe+ukg3yNMxPegihki6+T1vnyLNAWe7ZMS4A==', N'7bb8d1b8-30a8-40dd-9cd4-12c0ad09807d', NULL, 0, 0, NULL, 1, 0, N'Gfeller2.Admin@Gfeller.ch')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'5f53d5c4-8936-495c-8b96-0c2ac45fcf52', N'Gfeller1.Admin@Gfeller.ch', 0, N'AAIIU3g13bRJKE+HR1VPNqYE6Wkv4Hb812vIW0gowwBGaCMzWe87B3FLJhVjBGTQiQ==', N'88d5f4b5-46d7-434a-9ed2-e081b6dd17d9', NULL, 0, 0, NULL, 1, 0, N'Gfeller1.Admin@Gfeller.ch')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'91d4f7fa-029a-4cbe-80bc-c7ebd131ef91', N'Gfeller3.Admin@Gfeller.ch', 0, N'APqwPCPqB3kxoaTxX1n5eyIBELX5JzpsOr0xIQ6XaebWMPF7JlPOYcRGkdzVtIe3HQ==', N'3fa4e509-626c-461b-a0ca-ade81b4c3f48', NULL, 0, 0, NULL, 1, 0, N'Gfeller3.Admin@Gfeller.ch')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ad35f5e7-105a-4fc4-8e15-af1f7ca41629', N'guest@gfeller.ch', 0, N'AOb2BUquFdR4imHkVnLQESI5YYDLoCCrjlKWcL5Ae1WGs76/PzOwy0yPYL/uKtdAXA==', N'227fc2b7-bf92-4a3c-af6e-144156e7a889', NULL, 0, 0, NULL, 1, 0, N'guest@gfeller.ch')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'd65e2963-2903-4a2c-bb5f-ce0a68f7a741', N'CanManageAll')


INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4344ad9b-43ff-4597-9c69-9a54e66dbec6', N'd65e2963-2903-4a2c-bb5f-ce0a68f7a741')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5f53d5c4-8936-495c-8b96-0c2ac45fcf52', N'd65e2963-2903-4a2c-bb5f-ce0a68f7a741')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'91d4f7fa-029a-4cbe-80bc-c7ebd131ef91', N'd65e2963-2903-4a2c-bb5f-ce0a68f7a741')


");
        }
        
        public override void Down()
        {
        }
    }
}
