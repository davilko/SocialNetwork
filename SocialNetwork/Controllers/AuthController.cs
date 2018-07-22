using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Attributes;
using SocialNetwork.Security;

namespace SocialNetwork.Controllers
 {
     [Route("api/[controller]")]
     public class AuthController : Controller
     {
         private readonly ITokenManager _tokenManager;

         public AuthController(ITokenManager tokenManager)
         {
             _tokenManager = tokenManager;
         }

         [HttpGet]
         [BasicAuth]
         [Route("Token")]
         public IActionResult CreateToken()
         {
             var tokenDefinition = _tokenManager.CreateTokenDefinition(User.Claims);
             return Ok(tokenDefinition);
         }
     }
 }
