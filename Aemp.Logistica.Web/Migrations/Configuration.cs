using System.Web.Security;
using WebMatrix.WebData;

namespace Aemp.Logistica.Web.Migrations
{
  using System;
  using System.Data.Entity;
  using System.Data.Entity.Migrations;
  using System.Linq;

  internal sealed class Configuration : DbMigrationsConfiguration<Aemp.Logistica.Web.Models.UsersContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = true;
    }

    protected override void Seed(Aemp.Logistica.Web.Models.UsersContext context)
    {
      WebSecurity.InitializeDatabaseConnection(
        "SecurityDb",
        "UserProfile",
        "UserId",
        "UserName", autoCreateTables: true);

      if (!Roles.RoleExists("Administrator")) Roles.CreateRole("Administrator");

      if (!WebSecurity.UserExists("lelong37"))
      {
        WebSecurity.CreateUserAndAccount(
          "lelong37",
          "password",
          new { EmailAddress = "enrique.albert.gleiser@gmail.com" });
      }

      if (!Roles.GetRolesForUser("lelong37").Contains("Administrator"))
      {
        Roles.AddUsersToRoles(new[] { "lelong37" }, new[] { "Administrator" });
      }
    }
  }
}
