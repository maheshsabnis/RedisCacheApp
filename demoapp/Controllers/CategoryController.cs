using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using demoapp.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace demoapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        IDistributedCache redisCache;

        CitusTrainingContext context;
        public CategoryController(CitusTrainingContext context, IDistributedCache cache)
        {
            this.context = context;
            redisCache = cache;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // chec if the cache with name 'categories' is present
            string categoriesData = redisCache.GetString("categories");
            if (categoriesData == null)
            {
                // if the cache is null then add data in cache 
                
                List<Categories> categories = context.Categories.ToList();
                // serialize data in JSON Form
                categoriesData = JsonSerializer.Serialize<List<Categories>>(categories);
                // save data in cache
                // DistributedCacheEntryOptions: class used to define caching metadata e.g. life span for cache

                var cacheOptions = new DistributedCacheEntryOptions();
                // expiration time from the Cache Time
                cacheOptions.SetAbsoluteExpiration(DateTimeOffset.Now.AddMinutes(1));
                redisCache.SetString("categories", categoriesData, cacheOptions);
                return Ok(new
                {
                    message = "Data Received from Database",
                    data = categories
                });
            }
            else
            {
                // read data from cache and return it
                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };
                // deserilized the data from cache
            List<Categories> cats = JsonSerializer.Deserialize<List<Categories>>(categoriesData, options);
                // return dara from cache
                return Ok(new
                {
                    message = "Data Received from Cache",
                    data = cats
                });
            }
            
        }
    }
}