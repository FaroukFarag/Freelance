namespace Freelance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostAndProposal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientName = c.String(nullable: false),
                        JobType = c.Int(nullable: false),
                        JobBudget = c.Double(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        JobDescription = c.String(nullable: false, maxLength: 255),
                        IsAccepted = c.Boolean(nullable: false),
                        ClientId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ClientId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Proposals",
                c => new
                    {
                        FreelancerId = c.String(nullable: false, maxLength: 128),
                        PostId = c.Int(nullable: false),
                        IsAccepted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.FreelancerId, t.PostId })
                .ForeignKey("dbo.AspNetUsers", t => t.FreelancerId)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .Index(t => t.FreelancerId)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Proposals", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Proposals", "FreelancerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "ClientId", "dbo.AspNetUsers");
            DropIndex("dbo.Proposals", new[] { "PostId" });
            DropIndex("dbo.Proposals", new[] { "FreelancerId" });
            DropIndex("dbo.Posts", new[] { "ClientId" });
            DropTable("dbo.Proposals");
            DropTable("dbo.Posts");
        }
    }
}
