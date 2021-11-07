using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using PickPoint.Data.DB;
using PickPoint.Data.DTO;
using PickPoint.Data.Entities;
using PickPoint.Infrastructure;

using PickPoint.WebApi.Controllers;

using System.Collections.Generic;
using System.Linq;

namespace PickPpoint.Test
{
    [TestClass]
    public class PostamateControllerTest
    {
        [TestMethod]
        public void PostamateController_Get()
        {
            var expectedCount = 8;
            using var pickPointContext = TestMemoryDataBuilder.GetInstance().GetContext();
            var postamatesRepository = new PostamateDbRepository(pickPointContext);
            var postamateController = new PostamateController(postamatesRepository);

            var result = (OkObjectResult)postamateController.Get().Result;
            var postamates = (result.Value as IEnumerable<PostamateDto>);
            var actualCount = postamates.Count();

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void PostamateController_Get_ById_Ok()
        {
            // Arrange
            var expectedId = "0003-300";
            using var pickPointContext = TestMemoryDataBuilder.GetInstance().GetContext();
            var postamatesRepository = new PostamateDbRepository(pickPointContext);
            var postamateController = new PostamateController(postamatesRepository);
           
            // Action
            var result = (OkObjectResult)postamateController.Get(expectedId).Result;
            var postamate = (result.Value as PostamateDto);
            var actualId = postamate.Id;

            // Assert
            Assert.AreEqual(result.StatusCode, StatusCodes.Status200OK);
            Assert.AreEqual(expectedId, actualId);
        }

        [TestMethod]
        public void PostamateController_Get_ById_NotFound()
        {
            // Arrange
            var expectedId = "9999-999";
            using var pickPointContext = TestMemoryDataBuilder.GetInstance().GetContext();
            var postamatesRepository = new PostamateDbRepository(pickPointContext);
            var postamateController = new PostamateController(postamatesRepository);

            // Action
            var result = (NotFoundResult)postamateController.Get(expectedId).Result;

            // Assert
            Assert.AreEqual(result.StatusCode, StatusCodes.Status404NotFound);
        }
    }
}
