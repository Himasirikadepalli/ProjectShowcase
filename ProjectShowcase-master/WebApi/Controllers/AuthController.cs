using DataAccessLayer.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public readonly SignInManager<MyUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly UserManager<MyUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<AuthController> logger;

        //
        public AuthController(SignInManager<MyUser> signInManager, IConfiguration configuration, UserManager<MyUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<AuthController> logger)
        {
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
        }

        //register page for users to register with username and pwd
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestModel registerRequestModel)
        {
            try
            {
                var user = new MyUser
                {
                    FirstName = registerRequestModel.FirstName,
                    LastName = registerRequestModel.LastName,
                    Gender = registerRequestModel.Gender,
                    Email = registerRequestModel.Email,
                    UserName = registerRequestModel.Email,
                    RoleAlloted = registerRequestModel.RoleAlloted
                };
                var result = await userManager.CreateAsync(user, registerRequestModel.Password);

                if (result.Succeeded)
                {
                    if (!await roleManager.RoleExistsAsync(registerRequestModel.RoleAlloted))
                        await roleManager.CreateAsync(new IdentityRole(registerRequestModel.RoleAlloted));
                    if (await roleManager.RoleExistsAsync(registerRequestModel.RoleAlloted))
                        await userManager.AddToRoleAsync(user, registerRequestModel.RoleAlloted);
                    logger.LogInformation("Registration Successfully.");
                    return Ok("Success");
                }
                else
                {
                    logger.LogInformation("Bad request.");
                    return BadRequest(result.Errors);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured while registeration.");
                return StatusCode(500);
            }
        }

        //Login task which will login users and check for authentication
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestModel loginRequest)
        {
            try
            {
                var result = await signInManager.PasswordSignInAsync(loginRequest.UserName, loginRequest.Password, false, false);
                if (result.Succeeded)
                {
                    //return the JWT
                    var theUser = await userManager.FindByEmailAsync(loginRequest.UserName);
                    var theRole = await userManager.GetRolesAsync(theUser);
                    var token = GenerateToken(theUser.Id, theUser.UserName, String.Join(',', theRole));
                    logger.LogInformation("Login Successful.");
                    return Ok(token);
                }
                logger.LogInformation("Bad request.");
                return BadRequest(new { message = "Invalid Username or Password" });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured while login.");
                return StatusCode(500);
            }
        }

        //To generate a unique jwt token for authentication
        private string GenerateToken(string userId, string userName, string roleInfo)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("ZIkKq2Vr4zuq789E8lOJquNGeh");
            var expiresAt = DateTime.Now.AddDays(30);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
            new Claim[]
            {
                     new Claim(ClaimTypes.Name, userName),
                     new Claim("Id", userId),
                     new Claim(ClaimTypes.Role, roleInfo)
            }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
