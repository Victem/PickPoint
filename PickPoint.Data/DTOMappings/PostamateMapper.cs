using PickPoint.Data.DTO;
using PickPoint.Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickPoint.Data.DTOMappings
{
    public static class PostamateMapper
    {
        public static PostamateDto ToPostamateDto(this Postamate postamate)
        {
            var postamateDto = new PostamateDto()
            {
                Id = postamate.Id,
                Address = postamate.Address,
                Status = postamate.Status,
            };
            return postamateDto;
        }
    }
}
