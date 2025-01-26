using Microsoft.AspNetCore.Mvc;
using NMShop.Shared.Scaffold;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomOrdersController : ControllerBase
    {
        private readonly NMShopContext _context;

        public CustomOrdersController(NMShopContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Создать новый заказ
        /// </summary>
        /// <param name="order">Данные заказа</param>
        /// <returns>Результат операции</returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CustomOrder order)
        {
            if (order == null)
            {
                return BadRequest("Заказ не может быть пустым.");
            }

            try
            {
                _context.CustomOrders.Add(order);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Заказ успешно создан.", OrderId = order.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при сохранении заказа: {ex.Message}");
            }
        }

        /// <summary>
        /// Получить все заказы
        /// </summary>
        /// <returns>Список заказов</returns>
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var orders = await _context.CustomOrders.ToListAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при получении заказов: {ex.Message}");
            }
        }

        /// <summary>
        /// Получить заказ по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns>Данные заказа</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var order = await _context.CustomOrders.FindAsync(id);

                if (order == null)
                {
                    return NotFound($"Заказ с ID {id} не найден.");
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при получении заказа: {ex.Message}");
            }
        }

        /// <summary>
        /// Удалить заказ по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns>Результат удаления</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var order = await _context.CustomOrders.FindAsync(id);

                if (order == null)
                {
                    return NotFound($"Заказ с ID {id} не найден.");
                }

                _context.CustomOrders.Remove(order);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Заказ успешно удалён." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при удалении заказа: {ex.Message}");
            }
        }

        /// <summary>
        /// Обновить данные заказа
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <param name="updatedOrder">Обновлённые данные заказа</param>
        /// <returns>Результат обновления</returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] CustomOrder updatedOrder)
        {
            if (id != updatedOrder.Id)
            {
                return BadRequest("ID заказа не совпадает.");
            }

            try
            {
                var existingOrder = await _context.CustomOrders.FindAsync(id);

                if (existingOrder == null)
                {
                    return NotFound($"Заказ с ID {id} не найден.");
                }

                existingOrder.UserName = updatedOrder.UserName;
                existingOrder.UserPhone = updatedOrder.UserPhone;
                existingOrder.ProductDescription = updatedOrder.ProductDescription;

                await _context.SaveChangesAsync();
                return Ok(new { Message = "Заказ успешно обновлён." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при обновлении заказа: {ex.Message}");
            }
        }
    }
}
