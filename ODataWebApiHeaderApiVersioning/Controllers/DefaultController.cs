using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ODataWebApiHeaderApiVersioning.Controllers
{
    public class DefaultController : ODataController
    {
        [ApiVersion("1.0")]
        [HttpGet]
        [ODataRoute("GetRatingUnbound(movieId={movieId})")]
        public IActionResult GetRatingUnbound([FromODataUri] int movieId)
        {
            var random = new Random(movieId);
            var rating = random.Next(0, 5);

            return Ok(rating);
        }

        [ApiVersion("2.0")]
        [HttpGet]
        [ODataRoute("GetRevenueUnbound(movieId={movieId})")]
        public IActionResult GetRevenueUnbound([FromODataUri] int movieId)
        {
            var random = new Random(movieId);
            var revenue = random.Next(100000, 10000000);

            return Ok(revenue);
        }
    }
}
