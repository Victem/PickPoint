using PickPoint.Data.Entities;
using PickPoint.Data.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickPoint.Data.DB
{
    public static class FakeDataCreator
    {
        public static IEnumerable<Postamate> GetPostamates()
        {
            var postamates = new Postamate[]
            {
                new Postamate {  Id = "0001-001", Status = true, Address = "Some place 1-1" },
                new Postamate {  Id = "0001-002", Status = true, Address = "Some place 1-2" },
                new Postamate {  Id = "0001-003", Status = false, Address = "Some place 1-3" },
                new Postamate {  Id = "0002-010", Status = true, Address = "Some place 2-1" },
                new Postamate {  Id = "0002-020", Status = false, Address = "Some place 2-2" },
                new Postamate {  Id = "0002-030", Status = true, Address = "Some place 2-3" },
                new Postamate {  Id = "0003-100", Status = true, Address = "Some place 3-1" },
                new Postamate {  Id = "0003-200", Status = false, Address = "Some place 3-2" },
                new Postamate {  Id = "0003-300", Status = false, Address = "Some place 3-3" },
                new Postamate {  Id = "0004-120", Status = true, Address = "Some place 4-1" },
                new Postamate {  Id = "0004-230", Status = true, Address = "Some place 4-2" },
                new Postamate {  Id = "0004-340", Status = true, Address = "Some place 4-3" },
            };
            return postamates;
        }

        public static IEnumerable<Order> GetOrders()
        {
            var orders = new Order[]
            {
                new Order
                {
                    Id = 1,
                    Customer= "Petrov",
                    Items = new string[] { "product1", "product2" },
                    Price = 2000.50m,
                    PostamateId = "0001-001",
                    Phone= "+7111-222-33-44",
                    Status = OrderStatus.Registered
                },
                new Order
                {
                    Id = 2,
                    Customer= "Ivanov",
                    Items = new string[] { "product2" },
                    Price = 300,
                    PostamateId = "0003-100",
                    Phone= "+7111-222-33-55",
                    Status = OrderStatus.Registered
                },
                new Order
                {
                    Id = 3,
                    Customer= "Sidorov",
                    Items = new string[] { "product4", "product2" },
                    Price = 499.99m,
                    PostamateId = "0002-010",
                    Phone= "+7222-222-33-44",
                    Status = OrderStatus.Registered
                },
                new Order
                {
                    Id = 4,
                    Customer= "Petrov",
                    Items = new string[] { "product1", "product2", "product6" },
                    Price = 5000,
                    PostamateId = "0001-001",
                    Phone= "+7111-222-33-44",
                    Status = OrderStatus.Registered
                },
                new Order
                {
                    Id = 5,
                    Customer= "Toporov",
                    Items = new string[] { "product5" },
                    Price = 1000,
                    PostamateId = "0004-230",
                    Phone= "+7111-222-33-66",
                    Status = OrderStatus.Registered
                },
            };
            return orders;
        }
    }
}
