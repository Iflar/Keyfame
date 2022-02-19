namespace Keyframe.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAcceptedRequestsToUserProfile : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserProfileAnimRequest", "UserProfile_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.UserProfileAnimRequest", "AnimRequest_RequestId", "dbo.AnimRequest");
            DropIndex("dbo.UserProfileAnimRequest", new[] { "UserProfile_UserId" });
            DropIndex("dbo.UserProfileAnimRequest", new[] { "AnimRequest_RequestId" });
            AddColumn("dbo.AnimRequest", "UserProfile_UserId", c => c.Int());
            AddColumn("dbo.AnimRequest", "UserProfile_UserId1", c => c.Int());
            AddColumn("dbo.UserProfile", "AnimRequest_RequestId", c => c.Int());
            CreateIndex("dbo.AnimRequest", "UserProfile_UserId");
            CreateIndex("dbo.AnimRequest", "UserProfile_UserId1");
            CreateIndex("dbo.UserProfile", "AnimRequest_RequestId");
            AddForeignKey("dbo.AnimRequest", "UserProfile_UserId", "dbo.UserProfile", "UserId");
            AddForeignKey("dbo.AnimRequest", "UserProfile_UserId1", "dbo.UserProfile", "UserId");
            AddForeignKey("dbo.UserProfile", "AnimRequest_RequestId", "dbo.AnimRequest", "RequestId");
            DropTable("dbo.UserProfileAnimRequest");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserProfileAnimRequest",
                c => new
                    {
                        UserProfile_UserId = c.Int(nullable: false),
                        AnimRequest_RequestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserProfile_UserId, t.AnimRequest_RequestId });
            
            DropForeignKey("dbo.UserProfile", "AnimRequest_RequestId", "dbo.AnimRequest");
            DropForeignKey("dbo.AnimRequest", "UserProfile_UserId1", "dbo.UserProfile");
            DropForeignKey("dbo.AnimRequest", "UserProfile_UserId", "dbo.UserProfile");
            DropIndex("dbo.UserProfile", new[] { "AnimRequest_RequestId" });
            DropIndex("dbo.AnimRequest", new[] { "UserProfile_UserId1" });
            DropIndex("dbo.AnimRequest", new[] { "UserProfile_UserId" });
            DropColumn("dbo.UserProfile", "AnimRequest_RequestId");
            DropColumn("dbo.AnimRequest", "UserProfile_UserId1");
            DropColumn("dbo.AnimRequest", "UserProfile_UserId");
            CreateIndex("dbo.UserProfileAnimRequest", "AnimRequest_RequestId");
            CreateIndex("dbo.UserProfileAnimRequest", "UserProfile_UserId");
            AddForeignKey("dbo.UserProfileAnimRequest", "AnimRequest_RequestId", "dbo.AnimRequest", "RequestId", cascadeDelete: true);
            AddForeignKey("dbo.UserProfileAnimRequest", "UserProfile_UserId", "dbo.UserProfile", "UserId", cascadeDelete: true);
        }
    }
}
