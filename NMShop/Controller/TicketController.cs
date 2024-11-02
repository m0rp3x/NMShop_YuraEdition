namespace NMShop.Controller;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NMShop.Shared.Scaffold;
using System.Collections.Generic;
using System.Threading.Tasks;
using NMShop.Shared.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AutoMapper;
using System.Text.Json;

[Route("api/[controller]")]
[ApiController]
public class TicketController : ControllerBase
{
    private readonly NMShopContext _context;

    public TicketController(NMShopContext context)
    {
        _context = context;
    }

    [HttpPost("addUser")]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Telegramid == user.Telegramid);
        if (existingUser != null)
        {
            return Ok(existingUser);
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(user);
    }
    [HttpPost("createTicket")]
    public async Task<IActionResult> CreateTicket([FromBody] Ticket ticket)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingUser = await _context.Users.FindAsync(ticket.Userid);
        if (existingUser == null)
        {
            return NotFound(new { message = "Пользователь с данным ID не найден." });
        }

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        return Ok(ticket);
    }


    [HttpGet("user/{userID}")]
    public async Task<IActionResult> GetTicketsByUser(int userID)
    {
        var tickets = await _context.Tickets
            .Where(t => t.Userid == userID)
            .ToListAsync();

        return Ok(tickets);
    }

    [HttpPost("addMessage")]
    public async Task<IActionResult> AddMessage([FromBody] Ticketmessage message)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Ticketmessages.Add(message);
        await _context.SaveChangesAsync();

        return Ok(message);
    }

    [HttpGet("{ticketID}/messages")]
    public async Task<IActionResult> GetMessagesByTicket(int ticketID)
    {
        var messages = await _context.Ticketmessages
            .Where(m => m.Ticketid == ticketID)
            .ToListAsync();

        return Ok(messages);
    }

    [HttpPut("{ticketID}/status")]
    public async Task<IActionResult> UpdateTicketStatus(int ticketID, [FromBody] string status)
    {
        var ticket = await _context.Tickets.FindAsync(ticketID);
        if (ticket == null)
        {
            return NotFound();
        }

        ticket.Status = status;
        await _context.SaveChangesAsync();

        return Ok(ticket);
    }
    
    [HttpGet("openTickets")]
    public async Task<IActionResult> GetOpenTickets()
    {
        try
        {
            Console.WriteLine("Получение открытых тикетов...");

            var openTickets = await _context.Tickets
                .Where(t => t.Status == "Open")
                .Include(t => t.User)
                .ToListAsync();

            Console.WriteLine($"Найдено {openTickets.Count} открытых тикетов.");

            if (openTickets == null || !openTickets.Any())
            {
                return NotFound(new { message = "Нет открытых тикетов." });
            }

            return Ok(openTickets);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при получении открытых тикетов: {ex.Message}");
            return StatusCode(500, "Ошибка сервера при получении открытых тикетов.");
        }
    }


    
    
    [HttpPost("replyTicket")]
    public async Task<IActionResult> ReplyTicket([FromBody] TicketReplyModel reply)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var ticket = await _context.Tickets
            .Include(t => t.Ticketmessages)
            .FirstOrDefaultAsync(t => t.Ticketid == reply.TicketId);

        if (ticket == null)
        {
            return NotFound(new { message = "Тикет с данным ID не найден." });
        }

        var user = await _context.Users.FindAsync(reply.UserId);
        if (user == null)
        {
            return NotFound(new { message = "Пользователь с данным ID не найден." });
        }

        var ticketMessage = new Ticketmessage
        {
            Ticketid = reply.TicketId,
            Userid = reply.UserId,
            Message = reply.Message,
            Sentat = DateTime.UtcNow
        };

        _context.Ticketmessages.Add(ticketMessage);
        await _context.SaveChangesAsync();

        return Ok(ticketMessage);
    }
    
    [HttpGet("getByTelegramId/{telegramId}")]
    public async Task<IActionResult> GetUserByTelegramId(long telegramId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Telegramid == telegramId);

        if (user == null)
        {
            return NotFound(new { message = "Пользователь с данным Telegram ID не найден." });
        }

        return Ok(user);
    }
    
    [HttpGet("getMessagesByTelegramId/{telegramId}")]
    public async Task<IActionResult> GetMessagesByTelegramId(long telegramId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Telegramid == telegramId);
        if (user == null)
        {
            return NotFound(new { message = "Пользователь с данным Telegram ID не найден." });
        }

        var userTickets = await _context.Tickets
            .Where(t => t.Userid == user.Userid && t.Status == "Open")
            .Select(t => t.Ticketid)
            .ToListAsync();

        if (userTickets == null || !userTickets.Any())
        {
            return NotFound(new { message = "Открытые тикеты для данного пользователя не найдены." });
        }

        var ticketMessages = await _context.Ticketmessages
            .Where(tm => userTickets.Contains(tm.Ticketid ?? 0))
            .ToListAsync();

        if (!ticketMessages.Any())
        {
            return NotFound(new { message = "Сообщения для открытых тикетов пользователя не найдены." });
        }

        return Ok(ticketMessages);
    }

    
    
    [HttpPut("{ticketID}/close")]
    public async Task<IActionResult> CloseTicket(int ticketID)
    {
        try
        {
            var ticket = await _context.Tickets.FindAsync(ticketID);
            if (ticket == null)
            {
                return NotFound(new { message = "Тикет с данным ID не найден." });
            }

            ticket.Status = "Close";
            ticket.Updatedat = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Тикет успешно закрыт." });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при закрытии тикета: {ex.Message}");
            return StatusCode(500, "Ошибка на сервере при закрытии тикета.");
        }
    }

    
    
    
    [HttpGet("{ticketId}")]
    public async Task<IActionResult> GetTicketById(int ticketId)
    {
        try
        {
            var ticket = await _context.Tickets
                .Include(t => t.User)
                .Include(t => t.Ticketmessages)
                .FirstOrDefaultAsync(t => t.Ticketid == ticketId);

            if (ticket == null)
            {
                return NotFound(new { message = "Тикет с данным ID не найден." });
            }

            return Ok(ticket);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при получении тикета: {ex.Message}");
            return StatusCode(500, "Ошибка на сервере при получении тикета.");
        }
    }


    
    
}
