using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace alexander_neumann.api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        [HttpGet()]
        public ActionResult GetUserClaims()
        {
            try
            {
                if (HttpContext.User == null)
                    return Unauthorized("User is null.");

                if (HttpContext.User.Identity == null)
                    return Unauthorized("Identity is null.");

                if (!HttpContext.User.Identity.IsAuthenticated)
                    return Unauthorized("User is not authenticated");

                var claims = HttpContext.User.Claims.Select(x => new { x.Type, x.Value }).ToList();

                return Ok(claims);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}