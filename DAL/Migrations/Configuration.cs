namespace DAL.Migrations
{
	using System.Data.Entity.Migrations;
	using DAL.DataLayer;

	internal sealed class Configuration : DbMigrationsConfiguration<Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.DataLayer.Context context) {}
    }
}
