using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace YarnEye.WebApi.Context
{
    public static class NodeFactory
    {
        public static string ConnectionString { get; set; }
        public static YarnEyeContext CreateNodes()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseNpgsql(ConnectionString);
            YarnEyeContext nodeContext = new YarnEyeContext(optionsBuilder.Options);
            return nodeContext;
        }

        public static void ApplyMigrations()
        {
            var nodeContext = CreateNodes();
            if (nodeContext != null)
            {
                try
                {
                    nodeContext.Database.Migrate();
                    nodeContext.Dispose();

                    Console.WriteLine("Migration Succeeded");
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Migration Error: " + ex.Message);
                }
            }
        }
    }
}