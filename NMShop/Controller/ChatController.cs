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
public class ChatController : ControllerBase
{
    private readonly NMShopContext _context;

    public ChatController(NMShopContext context)
    {
        _context = context;
    }

    // Создание нового чата
    [HttpPost("start")]
    public async Task<IActionResult> StartChat(int clientId)
    {
        var chat = new Chat
        {
            Clientid = clientId,
            Isopen = true,
            Createdat = DateTime.UtcNow
        };
        
        _context.Chats.Add(chat);
        await _context.SaveChangesAsync();

        return Ok(chat);
    }

    // Закрытие чата
    [HttpPost("close")]
    public async Task<IActionResult> CloseChat(int chatId)
    {
        var chat = await _context.Chats.FindAsync(chatId);
        if (chat == null) return NotFound();
        
        chat.Isopen = false;
        chat.Closedat = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return Ok(chat);
    }

    // Получение чатов клиента
    [HttpGet("getByClient/{clientId}")]
    public async Task<IActionResult> GetChatsByClient(int clientId)
    {
        var chats = await _context.Chats
            .Where(c => c.Clientid == clientId)
            .Include(c => c.Messages)
            .ToListAsync();
        
        return Ok(chats);
    }
}
