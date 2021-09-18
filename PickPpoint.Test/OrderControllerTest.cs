using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using PickPoint.Data.DTO;
using PickPoint.Data.DTOMappings;
using PickPoint.Infrastructure;

using PIckPopint.WebApi.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PickPpoint.Test
{
    [TestClass]
    public class OrderControllerTest
    {
        private OrderCreateDto GetNewOrderDto() 
        {
            return new OrderCreateDto
            {
                Items = new string[] {
                    "product1 ",
                    "product2",
                    "product3",
                    "product4 ",
                    "product5",
                    "product6",
                    "product7 ",
                    "product8",
                    "product9"
                },
                Customer = "Customer",
                Phone = "+7111-222-33-44",
                Price = 10000,
                PostamateId = "0001-001"
            };
        }

        private OrderUpdateDto GetUpdateOrderDto()
        {
            return new OrderUpdateDto
            {
                Items = new string[] {
                    "product1 ",
                    "product2",
                    "product3",
                    "product4 ",
                    "product5",
                    "product6",
                    "product7 ",
                    "product8",
                    "product9"
                },
                Customer = "Customer",
                Phone = "+7111-222-33-44",
                Price = 10000,
            };
        }
        [TestMethod]
        public void OrderController_Get_ById_Ok()
        {
            // Arrange
            var expectedId = 3;
            using var pickPointContext = TestMemoryDataBuilder.GetInstance().GetContext();
            var postamatesRepository = new PostamateDbRepository(pickPointContext);
            var ordersRepository = new OrderDbRepository(pickPointContext);
            var orderController = new OrderController(postamatesRepository , ordersRepository);

            // Action
            var result = (OkObjectResult)orderController.Get(expectedId).Result;
            var orderDto = (result.Value as OrderDto);
            var actualId = orderDto.Id;

            // Assert
            Assert.AreEqual(result.StatusCode, StatusCodes.Status200OK);
            Assert.AreEqual(expectedId, actualId);
        }

        [TestMethod]
        public void OrderController_Get_ById_NotFound()
        {
            // Arrange
            var expectedId = 999;
            using var pickPointContext = TestMemoryDataBuilder.GetInstance().GetContext();
            var postamatesRepository = new PostamateDbRepository(pickPointContext);
            var ordersRepository = new OrderDbRepository(pickPointContext);
            var orderController = new OrderController(postamatesRepository, ordersRepository);

            // Action
            var result = (NotFoundResult)orderController.Get(expectedId).Result;
            
            // Assert
            Assert.AreEqual(result.StatusCode, StatusCodes.Status404NotFound);
        }

        [TestMethod]
        public void OrderController_Post_InvalidModel_TooMany_Products()
        {
            // Arrange
            var newOrder = GetNewOrderDto();
            newOrder.Items = Enumerable.Range(1, 12)
                .Select((number, index) => $"product{index}")
                .ToArray();
            using var pickPointContext = TestMemoryDataBuilder.GetInstance().GetContext();
            var postamatesRepository = new PostamateDbRepository(pickPointContext);
            var ordersRepository = new OrderDbRepository(pickPointContext);
            var orderController = new OrderController(postamatesRepository, ordersRepository);
            
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(newOrder);

            // There is no request context no bind model
            orderController.ModelState.AddModelError(nameof(newOrder.Items), $"{newOrder.Items.Length}");
            
            // Action
            var result = (BadRequestObjectResult)orderController.Post(newOrder).Result;
            // Validate model separatelty
            var isCorrectItemsCount = Validator.TryValidateObject(newOrder, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isCorrectItemsCount);
            Assert.AreEqual(result.StatusCode, StatusCodes.Status400BadRequest);
        }

        [TestMethod]
        public void OrderController_Post_InvalidModel_Whong_Phone_Format()
        {
            // Arrange
            var newOrder = GetNewOrderDto();
            newOrder.Phone = "+7111222-33-44";
            using var pickPointContext = TestMemoryDataBuilder.GetInstance().GetContext();
            var postamatesRepository = new PostamateDbRepository(pickPointContext);
            var ordersRepository = new OrderDbRepository(pickPointContext);
            var orderController = new OrderController(postamatesRepository, ordersRepository);

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(newOrder);

            // There is no request context no bind model
            orderController.ModelState.AddModelError(nameof(newOrder.Items), $"{newOrder.Items.Length}");

            // Action
            var result = (BadRequestObjectResult)orderController.Post(newOrder).Result;
            // Validate model separatelty
            var isCorrectItemsCount = Validator.TryValidateObject(newOrder, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isCorrectItemsCount);
            Assert.AreEqual(result.StatusCode, StatusCodes.Status400BadRequest);
        }

        [TestMethod]
        public void OrderController_Post_InvalidModel_Whong_Postamate_Format()
        {
            // Arrange
            var newOrder = GetNewOrderDto();
            newOrder.PostamateId = "0001001";

            using var pickPointContext = TestMemoryDataBuilder.GetInstance().GetContext();
            var postamatesRepository = new PostamateDbRepository(pickPointContext);
            var ordersRepository = new OrderDbRepository(pickPointContext);
            var orderController = new OrderController(postamatesRepository, ordersRepository);

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(newOrder);

            // There is no request context no bind model
            orderController.ModelState.AddModelError(nameof(newOrder.Items), $"{newOrder.Items.Length}");

            // Action
            var result = (BadRequestObjectResult)orderController.Post(newOrder).Result;
            // Validate model separatelty
            var isCorrectItemsCount = Validator.TryValidateObject(newOrder, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isCorrectItemsCount);
            Assert.AreEqual(result.StatusCode, StatusCodes.Status400BadRequest);
        }

        [TestMethod]
        public void OrderController_Post_Unexisted_Postamate()
        {
            // Arrange
            var newOrder = GetNewOrderDto();
            newOrder.PostamateId = "9999-999";

            using var pickPointContext = TestMemoryDataBuilder.GetInstance().GetContext();
            var postamatesRepository = new PostamateDbRepository(pickPointContext);
            var ordersRepository = new OrderDbRepository(pickPointContext);
            var orderController = new OrderController(postamatesRepository, ordersRepository);

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(newOrder);


            // Action
            var result = (NotFoundResult)orderController.Post(newOrder).Result;
            // Validate model separatelty
            var isCorrectItemsCount = Validator.TryValidateObject(newOrder, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isCorrectItemsCount);
            Assert.AreEqual(result.StatusCode, StatusCodes.Status404NotFound);
        }

        [TestMethod]
        public void OrderController_Post_Inactive_Postamate()
        {// Arrange
            var newOrder = GetNewOrderDto();
            newOrder.PostamateId = "0001-003";
            
            using var pickPointContext = TestMemoryDataBuilder.GetInstance().GetContext();
            var postamatesRepository = new PostamateDbRepository(pickPointContext);
            var ordersRepository = new OrderDbRepository(pickPointContext);
            var orderController = new OrderController(postamatesRepository, ordersRepository);

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(newOrder);


            // Action
            var result = (StatusCodeResult)orderController.Post(newOrder).Result;
            // Validate model separatelty
            var isCorrectItemsCount = Validator.TryValidateObject(newOrder, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isCorrectItemsCount);
            Assert.AreEqual(result.StatusCode, StatusCodes.Status403Forbidden);
        }

        [TestMethod]
        public void OrderController_Post_Ok()
        {
            // Arrange
            var newOrder = GetNewOrderDto();
            using var pickPointContext = TestMemoryDataBuilder.GetInstance().GetContext();
            var postamatesRepository = new PostamateDbRepository(pickPointContext);
            var ordersRepository = new OrderDbRepository(pickPointContext);
            var orderController = new OrderController(postamatesRepository, ordersRepository);

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(newOrder);


            // Action
            var result = (CreatedAtActionResult)orderController.Post(newOrder).Result;
            // Validate model separatelty
            var isCorrectItemsCount = Validator.TryValidateObject(newOrder, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isCorrectItemsCount);
            Assert.IsInstanceOfType(result.Value, typeof(OrderDto));
            Assert.AreEqual(result.StatusCode, StatusCodes.Status201Created);
        }



        [TestMethod]
        public void OrderController_Put_InvalidModel_Whong_Phone_Format()
        {
            // Arrange
            var newOrder = GetNewOrderDto();
            using var pickPointContext = TestMemoryDataBuilder.GetInstance().GetContext();
            var postamatesRepository = new PostamateDbRepository(pickPointContext);
            var ordersRepository = new OrderDbRepository(pickPointContext);
            var orderController = new OrderController(postamatesRepository, ordersRepository);

            // Action
            var createResult = (CreatedAtActionResult)orderController.Post(newOrder).Result;
            var order = (createResult.Value as OrderDto);
            var updateDto = order.ToOrderUpdateDto();
            updateDto.Phone = "+7111222-33-44";
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(updateDto);

            // There is no request context no bind model
            orderController.ModelState.AddModelError(nameof(updateDto.Items), $"{updateDto.Items.Length}");
            var updateResult = (BadRequestResult)orderController.Put(updateDto);
            
            // Validate model separatelty
            var isCorrectItemsCount = Validator.TryValidateObject(updateDto, validationContext, validationResults, true);

            // Assert
            Assert.AreEqual(updateResult.StatusCode, StatusCodes.Status400BadRequest);
            Assert.IsFalse(isCorrectItemsCount);
        }

        [TestMethod]
        public void OrderController_Put_InvalidModel_TooMany_Productst()
        {
            // Arrange
            var newOrder = GetNewOrderDto();
            using var pickPointContext = TestMemoryDataBuilder.GetInstance().GetContext();
            var postamatesRepository = new PostamateDbRepository(pickPointContext);
            var ordersRepository = new OrderDbRepository(pickPointContext);
            var orderController = new OrderController(postamatesRepository, ordersRepository);

            // Action
            var createResult = (CreatedAtActionResult)orderController.Post(newOrder).Result;
            var order = (createResult.Value as OrderDto);
            var updateDto = order.ToOrderUpdateDto();
            updateDto.Items = Enumerable.Range(1, 12)
                .Select((number, index) => $"product{index}")
                .ToArray();
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(updateDto);

            // There is no request context no bind model
            orderController.ModelState.AddModelError(nameof(updateDto.Items), $"{updateDto.Items.Length}");
            var updateResult = (BadRequestResult)orderController.Put(updateDto);

            // Validate model separatelty
            var isCorrectItemsCount = Validator.TryValidateObject(updateDto, validationContext, validationResults, true);

            // Assert
            Assert.AreEqual(updateResult.StatusCode, StatusCodes.Status400BadRequest);
            Assert.IsFalse(isCorrectItemsCount);
        }


        [TestMethod]
        public void OrderController_Put_Unexisted()
        {
            // Arrange
            var updateOrder = GetUpdateOrderDto();
            updateOrder.Id = 999;
            using var pickPointContext = TestMemoryDataBuilder.GetInstance().GetContext();
            var postamatesRepository = new PostamateDbRepository(pickPointContext);
            var ordersRepository = new OrderDbRepository(pickPointContext);
            var orderController = new OrderController(postamatesRepository, ordersRepository);

            // Action
            var updateResult = (NotFoundResult)orderController.Put(updateOrder);
            
            // Assert
            Assert.AreEqual(updateResult.StatusCode, StatusCodes.Status404NotFound);
        }

        [TestMethod]
        public void OrderController_Put_OK()
        {
            // Arrange
            var newOrder = GetNewOrderDto();
            using var pickPointContext = TestMemoryDataBuilder.GetInstance().GetContext();
            var postamatesRepository = new PostamateDbRepository(pickPointContext);
            var ordersRepository = new OrderDbRepository(pickPointContext);
            var orderController = new OrderController(postamatesRepository, ordersRepository);

            // Action
            var createResult = (CreatedAtActionResult)orderController.Post(newOrder).Result;
            var order = (createResult.Value as OrderDto);
            var updateDto = order.ToOrderUpdateDto();
            updateDto.Price = 1000;
            updateDto.Items = new string[] { "product update" };
            var updateResult = (OkResult)orderController.Put(updateDto);
            var result = (OkObjectResult)orderController.Get(order.Id).Result;
            var orderDto = (result.Value as OrderDto);


            // Assert
            Assert.AreEqual(updateResult.StatusCode, StatusCodes.Status200OK);
            Assert.AreNotEqual(newOrder.Price, orderDto.Price);
            Assert.AreNotEqual(newOrder.Items.Length, orderDto.Items.Length);
        }



        [TestMethod]
        public void OrderController_Delete_Unexisted()
        {
            // Arrange
            var expectedId = 10;
            using var pickPointContext = TestMemoryDataBuilder.GetInstance().GetContext();
            var postamatesRepository = new PostamateDbRepository(pickPointContext);
            var ordersRepository = new OrderDbRepository(pickPointContext);
            var orderController = new OrderController(postamatesRepository, ordersRepository);

            // Action
            var result = (NotFoundResult)orderController.Delete(expectedId);
            
            // Assert
            Assert.AreEqual(result.StatusCode, StatusCodes.Status404NotFound);
        }

        [TestMethod]
        public void OrderController_Delete_Ok()
        {
            // Arrange
            var newOrder = GetNewOrderDto();
            using var pickPointContext = TestMemoryDataBuilder.GetInstance().GetContext();
            var postamatesRepository = new PostamateDbRepository(pickPointContext);
            var ordersRepository = new OrderDbRepository(pickPointContext);
            var orderController = new OrderController(postamatesRepository, ordersRepository);

            // Action
            var createResult = (CreatedAtActionResult)orderController.Post(newOrder).Result;
            var order = (createResult.Value as OrderDto);
            var actualId = order.Id;
            var deleteResult = (NoContentResult)orderController.Delete(order.Id);

            // Assert
            Assert.AreEqual(deleteResult.StatusCode, StatusCodes.Status204NoContent);
            
        }
    }
}