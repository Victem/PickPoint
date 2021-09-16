using Microsoft.AspNetCore.Mvc;

using PickPoint.Data.DB;
using PickPoint.Data.DTO;
using PickPoint.Data.DTOMappings;
using PickPoint.Data.Entities;
using PickPoint.Infrastructure;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIckPopint.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostamateController : ControllerBase
    {
        private readonly IRepository<Postamate, string> _repository;

        public PostamateController(IRepository<Postamate, string> repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IEnumerable<PostamateDto> Get()
        {
            var result = _repository.Get()
                .Select(p=> p.ToPostamateDto());
            return result;
        }

        [HttpGet("{id}")]
        public PostamateDto Get(string id)
        {
            var result = _repository.FindById(id)
               .ToPostamateDto();
            return result;
        }
    }
}
