using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using NMShop.Shared.Models;

namespace NMShop.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReferenceInfoController : ControllerBase
    {
        private const string DataFilePath = "data/reference_info.json";
        private static List<ReferenceInfo> _cachedReferenceInfo;

        public ReferenceInfoController()
        {
            if (_cachedReferenceInfo == null)
            {
                _cachedReferenceInfo = LoadReferenceInfoAsync().Result;
            }
        }

        [HttpGet("{topic}")]
        public ActionResult<ReferenceInfo> GetReferenceInfo(string topic)
        {
            var requestedInfo = _cachedReferenceInfo.FirstOrDefault(info => info.Topic.Equals(topic, System.StringComparison.OrdinalIgnoreCase));

            if (requestedInfo == null)
            {
                return NotFound();
            }

            return Ok(requestedInfo);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReferenceInfo>> GetAllReferenceInfo()
        {
            return Ok(_cachedReferenceInfo);
        }

        [HttpPost("refresh-cache")]
        public async Task<ActionResult> RefreshCache()
        {
            _cachedReferenceInfo = await LoadReferenceInfoAsync();
            return Ok();
        }

        private async Task<List<ReferenceInfo>> LoadReferenceInfoAsync()
        {
            if (!System.IO.File.Exists(DataFilePath))
            {
                return new List<ReferenceInfo>();
            }

            var jsonData = await System.IO.File.ReadAllTextAsync(DataFilePath);
            var result = JsonSerializer.Deserialize<List<ReferenceInfo>>(jsonData) ?? new List<ReferenceInfo>();

            return result;
        }
    }
}