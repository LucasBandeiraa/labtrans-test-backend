using Domain.Interfaces;
using LabTrans.DTO;
using LabTrans.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabTrans.Controllers
{
    [Route("v1/[controller]")]
    public class LoginController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UserDTO userDTO)
        {
            var user = await _unitOfWork.UserRepository.List().FirstOrDefaultAsync(w => w.Username == userDTO.Username && w.Password == userDTO.Password);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);

            return new
            {
                user = new UserReadDTO
                {
                    Id = user.Id,
                    Username = user.Username,
                },
                token = token
            };
        }
    }
}
