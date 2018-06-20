namespace Options
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class RetailHubOptions : DbContext
    {
        // Your context has been configured to use a 'RetailHubOptions' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Options.RetailHubOptions' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'RetailHubOptions' 
        // connection string in the application configuration file.
        public RetailHubOptions()
            : base("name=RetailHubOptions1")
        {
        }

        public  DbSet<GlobalOptions> GlobalOptions { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}