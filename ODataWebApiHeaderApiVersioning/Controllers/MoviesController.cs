using ODataWebApiHeaderApiVersioning.Data;
using ODataWebApiHeaderApiVersioning.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ODataWebApiHeaderApiVersioning.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private MoviesDbContext _db;

        public MoviesController(MoviesDbContext db)
        {
            _db = db;
        }

        [ApiVersion("1.0")]
        [ApiVersion("2.0")]
        [EnableQuery]
        public IQueryable<Movie> Get()
        {
            return _db.Movies;
        }

        [ApiVersion("1.0")]
        [ApiVersion("2.0")]
        [EnableQuery]
        public SingleResult<Movie> Get([FromODataUri]int key)
        {
            return SingleResult.Create(_db.Movies.Where(d => d.Id.Equals(key)));
        }

        [ApiVersion("1.0")]
        [HttpGet]
        public IActionResult GetRating([FromODataUri] int key)
        {
            var random = new Random(key);
            var rating = random.Next(0, 5);

            return Ok(rating);
        }

        [ApiVersion("2.0")]
        [HttpGet]
        public IActionResult GetRevenue([FromODataUri] int key)
        {
            var random = new Random(key);
            var revenue = random.Next(100000, 10000000);

            return Ok(revenue);
        }
    }

}
