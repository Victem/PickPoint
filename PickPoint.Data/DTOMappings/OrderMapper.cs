using PickPoint.Data.DTO;
using PickPoint.Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickPoint.Data.DTOMappings
{
    public static class OrderMapper
    {
        public static OrderDto ToOrderDto(this Order order)
        {
            var orderDto = new OrderDto()
            {
                Id = order.Id,
                Items  = order.Items,
                PostamateId = order.PostamateId,
                Price = order.Price,
                Customer = order.Customer,
                Phone = order.Phone,
                Status = order.Status
            };
            return orderDto;
        }

        public static Order ToOrder(this OrderCreateDto orderCreateDto)
        {
            var order = new Order()
            {
                Items = orderCreateDto.Items,
                PostamateId = orderCreateDto.PostamateId,
                Price = orderCreateDto.Price,
                Customer = orderCreateDto.Customer,
                Phone = orderCreateDto.Phone,
            };
            return order;
        }

        public static Order FromUpdateDto(this Order order, OrderUpdateDto orderUpdateDto)
        {
            order.Items = orderUpdateDto.Items;
            order.Phone = orderUpdateDto.Phone;
            order.Customer = orderUpdateDto.Customer;
            order.Price = orderUpdateDto.Price;
            return order;
        }
    }
}
