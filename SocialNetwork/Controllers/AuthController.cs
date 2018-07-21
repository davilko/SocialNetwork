using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Attributes;

namespace SocialNetwork.Controllers
 {
     [Route("api/[controller]")]
     public class AuthController : Controller
     {

         [HttpGet]
         [BasicAuth]
         [Route("Token")]
         public IActionResult CreateToken()
         {
             var user = User;
             return Ok(user);
         }
     }
 }
