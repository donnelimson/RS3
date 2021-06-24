namespace Domain.Context.Migrations
{
    using Codebiz.Domain.Common.Model;
    using System.Data.Entity.Migrations;


    public sealed class Configuration : DbMigrationsConfiguration<AppCommonContext>
    {

        public AppUser AdminUser { get; set; }
        public AppUser AdminUnitUser { get; set; }
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppCommonContext context)
        {
        }
    }

    public static class StringExtension
    {
        public static string ToSentenceCase(this string str)
        {
            return System.Text.RegularExpressions.Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]));
        }
    }
}
