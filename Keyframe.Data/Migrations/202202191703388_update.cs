namespace Keyframe.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AnimRequest", "UserProfile_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.AnimRequest", "UserProfile_UserId1", "dbo.UserProfile");
            DropForeignKey("dbo.UserProfile", "AnimRequest_RequestId", "dbo.AnimRequest");
            DropIndex("dbo.AnimRequest", new[] { "UserProfile_UserId" });
            DropIndex("dbo.AnimRequest", new[] { "UserProfile_UserId1" });
            DropIndex("dbo.UserProfile", new[] { "AnimRequest_RequestId" });
            CreateTable(
                "dbo.UserProfileAnimRequest",
                c => new
                    {
                        UserProfile_UserId = c.Int(nullable: false),
                        AnimRequest_RequestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserProfile_UserId, t.AnimRequest_RequestId })
                .ForeignKey("dbo.UserProfile", t => t.UserProfile_UserId, cascadeDelete: true)
                .ForeignKey("dbo.AnimRequest", t => t.AnimRequest_RequestId, cascadeDelete: true)
                .Index(t => t.UserProfile_UserId)
                .Index(t => t.AnimRequest_RequestId);
            
            DropColumn("dbo.AnimRequest", "UserProfile_UserId");
            DropColumn("dbo.AnimRequest", "UserProfile_UserId1");
            DropColumn("dbo.UserProfile", "AnimRequest_RequestId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfile", "AnimRequest_RequestId", c => c.Int());
            AddColumn("dbo.AnimRequest", "UserProfile_UserId1", c => c.Int());
            AddColumn("dbo.AnimRequest", "UserProfile_UserId", c => c.Int());
            DropForeignKey("dbo.UserProfileAnimRequest", "AnimRequest_RequestId", "dbo.AnimRequest");
            DropForeignKey("dbo.UserProfileAnimRequest", "UserProfile_UserId", "dbo.UserProfile");
            DropIndex("dbo.UserProfileAnimRequest", new[] { "AnimRequest_RequestId" });
            DropIndex("dbo.UserProfileAnimRequest", new[] { "UserProfile_UserId" });
            DropTable("dbo.UserProfileAnimRequest");
            CreateIndex("dbo.UserProfile", "AnimRequest_RequestId");
            CreateIndex("dbo.AnimRequest", "UserProfile_UserId1");
            CreateIndex("dbo.AnimRequest", "UserProfile_UserId");
            AddForeignKey("dbo.UserProfile", "AnimRequest_RequestId", "dbo.AnimRequest", "RequestId");
            AddForeignKey("dbo.AnimRequest", "UserProfile_UserId1", "dbo.UserProfile", "UserId");
            AddForeignKey("dbo.AnimRequest", "UserProfile_UserId", "dbo.UserProfile", "UserId");
        }
    }
}
