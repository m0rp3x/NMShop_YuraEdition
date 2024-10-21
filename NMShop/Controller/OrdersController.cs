using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NMShop.Shared.Scaffold;
using System.Collections.Generic;
using System.Threading.Tasks;
using NMShop.Shared.Models;

namespace NMShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly NMShopContext _context;

        public OrdersController(NMShopContext context)
        {
            _context = context;
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
            var promoCode = await _context.PromoCodes.FirstOrDefaultAsync(pc => pc.Code == code);

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
        public async Task<IActionResult> SubmitOrder([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest("Order cannot be null.");
            }

            var promoCode = order.PromoCode?.Code;
            if (!string.IsNullOrEmpty(promoCode))
            {
                var promoCheck = await GetPromoCodeDiscount(promoCode);
                var discount = promoCheck.Value;

                if (discount == -1)
                {
                    return BadRequest("Promo code not found.");
                }
                else if (discount == 0)
                {
                    return BadRequest("Promo code expired or maximum usage reached.");
                }

                // Check if promo code usage limit has been reached
                var promoUsageCount = await _context.Orders.CountAsync(o => o.PromoCodeId == order.PromoCodeId);
                if (promoUsageCount >= order.PromoCode.MaxUsages)
                {
                    return BadRequest("Promo code usage limit has been reached.");
                }
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }

    }
}
