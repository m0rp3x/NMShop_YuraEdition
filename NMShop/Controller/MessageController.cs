using NMShop.Shared.Scaffold;

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
public class MessageController : ControllerBase
{
    private readonly NMShopContext _context;

    public MessageController(NMShopContext context)
    {
        _context = context;
    }

    // Отправка сообщения
    [HttpPost("send")]
    public async Task<IActionResult> SendMessage(int chatId, int userId, string content)
    {
        var message = new Message
        {
            ChatId = chatId,
            UserId = userId,
            Content = content,
            Timestamp = DateTime.UtcNow
        };
        
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();

        return Ok(message);
    }

    // Получение сообщений чата
    [HttpGet("getByChat/{chatId}")]
    public async Task<IActionResult> GetMessagesByChat(int chatId)
    {
        var messages = await _context.Messages
            .Where(m => m.ChatId == chatId)
            .Include(m => m.User)
            .ToListAsync();
        
        return Ok(messages);
    }
}
