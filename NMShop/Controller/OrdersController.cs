﻿using Microsoft.AspNetCore.Mvc;
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
// GET: api/orders
[HttpGet]
public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
{
    var orders = await _context.Orders
        .Include(o => o.DeliveryType)
        .Include(o => o.PaymentType)
        .Include(o => o.OrderStatus)
        .Include(o => o.OrderParts) // Включаем OrderParts
            .ThenInclude(op => op.StockInfo) // Включаем StockInfo
            .ThenInclude(si => si.Product) // Включаем Product
        .Include(o => o.PromoCode) // Включаем промокод
        .Select(o => new OrderDto
        {
            Id = o.Id,
            ClientFullName = o.ClientFullName,
            DeliveryAdress = o.DeliveryAdress,
            DeliveryTypeName = o.DeliveryType.Name,
            PaymentTypeName = o.PaymentType.Name,
            OrderStatusName = o.OrderStatus.Name,
            ContactValue = o.ContactValue,
            Total = o.OrderParts.Sum(op => (op.StockInfo.DiscountPrice ?? op.StockInfo.Price) * op.Amount), // Рассчитываем Total
            Products = o.OrderParts.Select(op => new ProductDto
            {
                Id = op.StockInfo.Product.Id,
                Name = op.StockInfo.Product.Name,
                Article = op.StockInfo.Product.Article,
                Size = op.StockInfo.Size,
                Price = op.StockInfo.Price,
                DiscountPrice = op.StockInfo.DiscountPrice,
                Amount = op.Amount
            }).ToList(),
            PromoCodeId = o.PromoCodeId, // Добавляем ID промокода
            PromoCodeInfo = o.PromoCode != null && IsPromoCodesValid(o.PromoCode, _context) // Проверяем и добавляем информацию о промокоде
                ? new PromoCodeInfoDto
                {
                    Code = o.PromoCode.Code,
                    DiscountPercent = o.PromoCode.DiscountPercent,
                    ExpirationDate = o.PromoCode.ExpirationDate
                }
                : null
        })
        .ToListAsync();

    // Применяем скидку к каждому заказу, если есть действительный промокод
    foreach (var order in orders)
    {
        if (order.PromoCodeInfo != null)
        {
            order.Total -= order.Total * (order.PromoCodeInfo.DiscountPercent / 100m);
        }
    }

    return Ok(orders);
}

private static bool IsPromoCodesValid(PromoCode promoCode, NMShopContext context)
{
    // Проверяем, не истек ли срок действия промокода
    if (promoCode.ExpirationDate.HasValue && promoCode.ExpirationDate < DateOnly.FromDateTime(DateTime.Today))
    {
        return false; // Промокод истек
    }

    // Проверяем, не превышено ли максимальное количество использований
    if (promoCode.MaxUsages > 0)
    {
        int usagesCount = context.Orders.Count(o => o.PromoCodeId == promoCode.Id);
        if (usagesCount >= promoCode.MaxUsages)
        {
            return false; // Промокод использован максимальное количество раз
        }
    }

    return true; // Промокод действителен
}

       [HttpGet("by-contact")]
public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByContact(string contactValue)
{
    Console.WriteLine($"Received contactValue: '{contactValue}'");

    contactValue = contactValue.Trim().ToLower();

    var orders = await _context.Orders
        .Include(o => o.DeliveryType)
        .Include(o => o.PaymentType)
        .Include(o => o.OrderStatus)
        .Include(o => o.OrderParts) // Включаем OrderParts
            .ThenInclude(op => op.StockInfo) // Включаем StockInfo
            .ThenInclude(si => si.Product) // Включаем Product
        .Include(o => o.PromoCode) // Включаем промокод
        .Where(o => o.ContactValue.Trim().ToLower() == contactValue) // Фильтруем по contactValue
        .Select(o => new OrderDto
        {
            Id = o.Id,
            ClientFullName = o.ClientFullName,
            DeliveryAdress = o.DeliveryAdress,
            DeliveryTypeName = o.DeliveryType.Name,
            PaymentTypeName = o.PaymentType.Name,
            OrderStatusName = o.OrderStatus.Name,
            ContactValue = o.ContactValue,
            Total = o.OrderParts.Sum(op => (op.StockInfo.DiscountPrice ?? op.StockInfo.Price) * op.Amount), // Рассчитываем Total
            Products = o.OrderParts.Select(op => new ProductDto
            {
                Id = op.StockInfo.Product.Id,
                Name = op.StockInfo.Product.Name,
                Article = op.StockInfo.Product.Article,
                Size = op.StockInfo.Size,
                Price = op.StockInfo.Price,
                DiscountPrice = op.StockInfo.DiscountPrice,
                Amount = op.Amount
            }).ToList(),
            PromoCodeId = o.PromoCodeId, // Добавляем ID промокода
            PromoCodeInfo = o.PromoCode != null && IsPromoCodesValid(o.PromoCode, _context) // Проверяем и добавляем информацию о промокоде
                ? new PromoCodeInfoDto
                {
                    Code = o.PromoCode.Code,
                    DiscountPercent = o.PromoCode.DiscountPercent,
                    ExpirationDate = o.PromoCode.ExpirationDate
                }
                : null
        })
        .ToListAsync();

    // Применяем скидку к каждому заказу, если есть действительный промокод
    foreach (var order in orders)
    {
        if (order.PromoCodeInfo != null)
        {
            order.Total -= order.Total * (order.PromoCodeInfo.DiscountPercent / 100m);
        }
    }

    return Ok(orders);
}



        
        [HttpPut("update-status/{id}")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] int newStatusId)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound("Заказ не найден.");
            }

            // Проверка статуса на корректность (если статусы фиксированы)
            var validStatuses = new[] { 1, 2, 3, 4 };
            if (!validStatuses.Contains(newStatusId))
            {
                return BadRequest("Некорректный статус заказа.");
            }

            // Обновление статуса
            order.OrderStatusId = newStatusId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка обновления статуса заказа: {ex.Message}");
            }

            return NoContent();
        }
    
        [HttpPut("update-delivery-date/{id}")]
        public async Task<IActionResult> UpdateOrderDeliveryDate(int id, [FromBody] string newDeliveryDateRange)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound("Заказ не найден.");
            }

            // Проверка формата даты (если необходимо)
            if (string.IsNullOrWhiteSpace(newDeliveryDateRange))
            {
                return BadRequest("Дата доставки не может быть пустой.");
            }

            // Обновление даты доставки
            order.EstimatedDeliveryDateRange = newDeliveryDateRange;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка обновления даты доставки: {ex.Message}");
            }

            return NoContent();
        }


// GET: api/orders/{id}
[HttpGet("{id}")]
public async Task<ActionResult<OrderDto>> GetOrder(int id)
{
    var order = await _context.Orders
        .Include(o => o.DeliveryType)
        .Include(o => o.PaymentType)
        .Include(o => o.OrderStatus)
        .Include(o => o.OrderParts) // Включаем OrderParts
            .ThenInclude(op => op.StockInfo) // Включаем StockInfo
            .ThenInclude(si => si.Product) // Включаем Product
        .Select(o => new OrderDto
        {
            Id = o.Id,
            ClientFullName = o.ClientFullName,
            DeliveryAdress = o.DeliveryAdress,
            DeliveryTypeName = o.DeliveryType.Name,
            PaymentTypeName = o.PaymentType.Name,
            OrderStatusName = o.OrderStatus.Name,
            ContactValue = o.ContactValue,
            Total = o.OrderParts.Sum(op => (op.StockInfo.DiscountPrice ?? op.StockInfo.Price) * op.Amount), // Рассчитываем Total
            Products = o.OrderParts.Select(op => new ProductDto
            {
                Id = op.StockInfo.Product.Id,
                Name = op.StockInfo.Product.Name,
                Article = op.StockInfo.Product.Article,
                Size = op.StockInfo.Size,
                Price = op.StockInfo.Price,
                DiscountPrice = op.StockInfo.DiscountPrice,
                Amount = op.Amount
            }).ToList(),
            PromoCodeId = o.PromoCodeId // Добавляем ID промокода
        })
        .FirstOrDefaultAsync(o => o.Id == id);

    if (order == null)
    {
        return NotFound();
    }

    // Применяем скидку и добавляем информацию о промокоде, если он есть
    if (order.PromoCodeId.HasValue)
    {
        var promoCode = await _context.PromoCodes
            .FirstOrDefaultAsync(pc => pc.Id == order.PromoCodeId);

        if (promoCode != null && IsPromoCodeValid(promoCode))
        {
            // Применяем скидку
            order.Total -= order.Total * (promoCode.DiscountPercent / 100m);

            // Добавляем информацию о промокоде в ответ
            order.PromoCodeInfo = new PromoCodeInfoDto
            {
                Code = promoCode.Code,
                DiscountPercent = promoCode.DiscountPercent,
                ExpirationDate = promoCode.ExpirationDate
            };
        }
    }

    return Ok(order);
}

// Проверка действительности промокода
private bool IsPromoCodeValid(PromoCode promoCode)
{
    // Проверяем, не истек ли срок действия промокода
    if (promoCode.ExpirationDate.HasValue && promoCode.ExpirationDate < DateOnly.FromDateTime(DateTime.Today))
    {
        return false; // Промокод истек
    }

    // Проверяем, не превышено ли максимальное количество использований
    if (promoCode.MaxUsages > 0)
    {
        int usagesCount = _context.Orders.Count(o => o.PromoCodeId == promoCode.Id);
        if (usagesCount >= promoCode.MaxUsages)
        {
            return false; // Промокод использован максимальное количество раз
        }
    }

    return true; // Промокод действителен
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
        public async Task<IActionResult> SubmitOrder([FromBody] CreateOrderDto orderDto)
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

            int discountPercent = 0;

            if (!string.IsNullOrWhiteSpace(orderDto.PromoCode))
            {
                var discountCheck = await GetPromoCodeDiscount(orderDto.PromoCode);

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
                order.PromoCodeId = (await _context.PromoCodes
                    .FirstOrDefaultAsync(pc => EF.Functions.ILike(pc.Code, orderDto.PromoCode)))?.Id;
            }

            order.OrderStatusId = 1;

            // Загружаем StockInfo из базы
            var stockInfoDict = await _context.StockInfos
                .Where(si => orderDto.OrderParts.Select(op => op.StockInfoId).Contains(si.Id))
                .ToDictionaryAsync(si => si.Id);

            foreach (var part in order.OrderParts)
            {
                if (stockInfoDict.TryGetValue(part.StockInfoId, out var stockInfo))
                {
                    part.StockInfo = stockInfo; // Присваиваем актуальные данные
                }
                else
                {
                    return BadRequest(new { message = $"Товар с StockInfoId {part.StockInfoId} не найден в базе." });
                }
            }

            // Теперь корректно рассчитываем стоимость заказа
            order.Total = order.OrderParts.Sum(op => (op.StockInfo.DiscountPrice ?? op.StockInfo.Price) * op.Amount);

            if (discountPercent > 0)
            {
                order.Total *= 1 - (discountPercent / 100m);
            }

            Console.WriteLine($"Total before save: {order.Total}"); // Проверка перед сохранением
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok("Заказ успешно оформлен");
        }
        
    }
}
