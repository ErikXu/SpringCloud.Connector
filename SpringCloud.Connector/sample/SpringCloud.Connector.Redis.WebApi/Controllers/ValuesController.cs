using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace SpringCloud.Connector.Redis.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly string _cacheKey;

        public ValuesController(IDistributedCache cache)
        {
            _cacheKey = "cache";
            _cache = cache;
        }

        [HttpGet("distributed")]
        public IActionResult GetDistributedCache()
        {
            return Ok(Encoding.UTF8.GetString(_cache.Get(_cacheKey)));
        }

        [HttpPost("distributed/{value}")]
        public async Task<IActionResult> PostDistributedCache(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            await _cache.SetAsync(_cacheKey, bytes);
            return NoContent();
        }
    }
}
