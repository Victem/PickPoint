using Microsoft.EntityFrameworkCore;

using PickPoint.Data.DB;
using PickPoint.Infrastructure;

using PickPoint.WebApi.Controllers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickPoint.Test
{
    public class TestMemoryDataBuilder
    {
        private TestMemoryDataBuilder()
        {
            Console.WriteLine("Seed data");
            using (var pickPointContext = new PickPointContext(GetOptions()))
            {
                pickPointContext.AddRange(FakeDataCreator.GetPostamates());
                pickPointContext.AddRange(FakeDataCreator.GetOrders());
                pickPointContext.SaveChanges();
            };
        }

        private DbContextOptions<PickPointContext> GetOptions()
        {
            var builder = new DbContextOptionsBuilder<PickPointContext>();
            builder.UseInMemoryDatabase("PickPoint");
            var options = builder.Options;
            return options;
        }

        //public static PickPointContext GetContext()
        //{
        //    var builder = new DbContextOptionsBuilder<PickPointContext>();
        //    builder.UseInMemoryDatabase("PickPoint");
        //    var options = builder.Options;

        //    using (var pickPointContext = new PickPointContext(options))
        //    {
        //        pickPointContext.SaveChanges();
        //    };
        //}

        private static TestMemoryDataBuilder builder;

        public static TestMemoryDataBuilder GetInstance()
        {
            if (builder == null)
            {
                builder = new TestMemoryDataBuilder();
            }
            return builder;
        }

        public PickPointContext GetContext()
        {
            return new PickPointContext(GetOptions());
        }
    }
}
