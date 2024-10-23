using Microsoft.AspNetCore.Mvc;
using NMShop.Shared.Scaffold;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NMShop.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReferenceInfoController : ControllerBase
    {
        private readonly NMShopContext _context;

        public ReferenceInfoController(NMShopContext context)
        {
            _context = context;
        }

        [HttpGet("{topic}")]
        public async Task<ActionResult<ReferenceTopic>> GetReferenceInfoByTopic(string topic)
        {
            var requestedInfo = await _context.ReferenceTopics
                .Include(rt => rt.ReferenceContents)
                .ThenInclude(rc => rc.TextSize)
                .FirstOrDefaultAsync(rt => EF.Functions.ILike(rt.Code, topic));

            if (requestedInfo == null)
            {
                return NotFound();
            }

            return Ok(requestedInfo);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReferenceTopic>>> GetAllReferenceInfo()
        {
            var referenceInfo = await _context.ReferenceTopics
                .Include(rt => rt.ReferenceContents)
                .ThenInclude(rc => rc.TextSize)
                .ToListAsync();

            return Ok(referenceInfo);
        }

        [HttpGet("{parentCode}/children")]
        public async Task<ActionResult<IEnumerable<ReferenceTopic>>> GetChildTopicsByParentCode(string parentCode)
        {
            var parentTopic = await _context.ReferenceTopics
                .Include(rt => rt.InverseParentTopic)
                .FirstOrDefaultAsync(rt => EF.Functions.ILike(rt.Code, parentCode));

            if (parentTopic == null)
            {
                return NotFound();
            }

            return Ok(parentTopic.InverseParentTopic);
        }
    }
}
