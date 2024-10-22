using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NMShop.Shared.Scaffold;
using System.Collections.Generic;
using System.Threading.Tasks;
using NMShop.Shared.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AutoMapper;
using System.Text.Json;

namespace NMShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly NMShopContext _context;
        private readonly IMapper _mapper;

        public OrdersController(NMShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.DeliveryType)
                .Include(o => o.PaymentType)
                .Include(o => o.OrderStatus)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    ClientFullName = o.ClientFullName,
                    DeliveryAdress = o.DeliveryAdress,
                    DeliveryTypeName = o.DeliveryType.Name,
                    PaymentTypeName = o.PaymentType.Name,
                    OrderStatusName = o.OrderStatus.Name,
                    ContactValue = o.ContactValue
                })
                .ToListAsync();

            return Ok(orders);
        }
        
        // Пример контроллера на стороне API
        [HttpGet("by-contact")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByContact(string contactValue)
        {
            var orders = await _context.Orders
                .Where(o => o.ContactValue == contactValue)
                .ToListAsync();

            if (orders == null || orders.Count == 0)
            {
                return NotFound();
            }

            return Ok(orders);
        }



        // GET: api/orders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.DeliveryType)
                .Include(o => o.PaymentType)
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderParts)
                .Include(o => o.ContactValue)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        // PUT: api/orders/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/orders/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        [HttpGet("contact-methods")]
        public async Task<ActionResult<IEnumerable<ContactMethod>>> GetContactMethods()
        {
            var contactMethods = await _context.ContactMethods.ToListAsync();
            return Ok(contactMethods);
        }

        [HttpGet("delivery-types")]
        public async Task<ActionResult<IEnumerable<DeliveryType>>> GetDeliveryTypes()
        {
            var deliveryTypes = await _context.DeliveryTypes.ToListAsync();
            return Ok(deliveryTypes);
        }

        [HttpGet("payment-types")]
        public async Task<ActionResult<IEnumerable<PaymentType>>> GetPaymentTypes()
        {
            var paymentTypes = await _context.PaymentTypes.ToListAsync();
            return Ok(paymentTypes);
        }

        [HttpGet("promo-code/{code}")]
        public async Task<ActionResult<int>> GetPromoCodeDiscount(string code)
        {
            var promoCode = await _context.PromoCodes.FirstOrDefaultAsync(pc => EF.Functions.ILike(pc.Code, code));

            if (promoCode == null)
            {
                return Ok(-1);
            }

            if (promoCode.ExpirationDate.HasValue && promoCode.ExpirationDate < DateOnly.FromDateTime(DateTime.Now))
            {
                return Ok(0);
            }

            var usageCount = await _context.Orders.CountAsync(o => o.PromoCodeId == promoCode.Id);
            if (usageCount >= promoCode.MaxUsages)
            {
                return Ok(0);
            }

            return Ok(promoCode.DiscountPercent);
        }

        [HttpPost("submit-order")]
        public async Task<IActionResult> SubmitOrder([FromBody] OrderCreateDto orderDto)
        {
            if (orderDto == null)
            {
                return BadRequest(new { message = "Заказ не может быть пустым." });
            }

            if (string.IsNullOrWhiteSpace(orderDto.ClientFullName))
            {
                return BadRequest(new { message = "Поле 'ФИО клиента' является обязательным." });
            }

            if (string.IsNullOrWhiteSpace(orderDto.DeliveryAdress))
            {
                return BadRequest(new { message = "Поле 'Адрес доставки' является обязательным." });
            }

            if (string.IsNullOrWhiteSpace(orderDto.ContactValue))
            {
                return BadRequest(new { message = "Поле 'Контактная информация' является обязательным." });
            }

            if (orderDto.DeliveryTypeId == 0)
            {
                return BadRequest(new { message = "Поле 'Способ доставки' является обязательным." });
            }

            if (orderDto.PaymentTypeId == 0)
            {
                return BadRequest(new { message = "Поле 'Способ оплаты' является обязательным." });
            }

            if (orderDto.OrderParts == null || !orderDto.OrderParts.Any())
            {
                return BadRequest(new { message = "Заказ должен содержать хотя бы один товар." });
            }

            foreach (var part in orderDto.OrderParts)
            {
                if (part.ProductId == 0)
                {
                    return BadRequest(new { message = "Поле 'Product' является обязательным для всех товаров." });
                }
            }

            int discountPercent = 0;

            if (!string.IsNullOrWhiteSpace(orderDto.PromoCode))
            {
                var discountCheck = await GetPromoCodeDiscount(orderDto.PromoCode);

                // Проверяем тип результата и извлекаем скидку
                if (discountCheck.Result is OkObjectResult result && result.Value is int discountValue)
                {
                    discountPercent = discountValue;
                }

                if (discountPercent < 0)
                {
                    return BadRequest("Промокод не найден.");
                }
                else if (discountPercent == 0)
                {
                    return BadRequest("Промокод истёк или достиг максимального количества использований.");
                }
            }

            var order = _mapper.Map<Order>(orderDto);

            if (discountPercent > 0)
            {
                order.PromoCodeId = (await _context.PromoCodes.FirstOrDefaultAsync(pc => EF.Functions.ILike(pc.Code, orderDto.PromoCode)))?.Id;
            }

            order.OrderStatusId = 1;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok("Заказ успешно оформлен"); 
        }




    }
}
