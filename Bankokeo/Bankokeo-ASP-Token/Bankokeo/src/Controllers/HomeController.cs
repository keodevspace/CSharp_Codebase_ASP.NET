// Controller é responsável por receber as requisições HTTP e retornar as respostas HTTP. Ele é o intermediário entre a View e o Model.

using Bankokeo.Data;
using Bankokeo.Models;
using Bankokeo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bankokeo.src.Controllers // Updated namespace to match folder structure
{
    // pra avisar que só vai retornar json 
    [ApiController]
    public class HomeController : Controller
    {
        //todo método de uma controller é chamado de ACTION
        // [HttpGet] é um atributo que indica que o método é chamado quando a requisição é do tipo GET
        [HttpGet("v1/getuser")]
        public async Task<IActionResult> GetUserAsync([FromServices] AppDbContext context)
        {
            var users = await context.Users.ToListAsync(); // Added await operator
            return Ok(users);
        }
        // [FromServices] é um atributo que indica que o parâmetro é um serviço lá de Program.cs

        // Como avisar a Program que agora estamos usando a Controller para lidar com os métodos?
        // BUILDER

        [HttpGet("v1/getuserid/{id:int}")]
        public async Task<IActionResult> GetUserById(
            [FromRoute] int id,
            [FromServices] AppDbContext context)
        {
            try
            {
                var userId = await context
                    .Users
                    .FirstOrDefaultAsync(user => user.Id == id);

                if (userId == null)
                    return NotFound();

                return Ok(new ResultViewModel<User>(userId, new List<string>())); // Simplified collection initialization
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "V01EX1 - Não foi possível incluir o registro. " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "V01EX1 - Erro ao tentar obter o registro. " + ex.Message);
                throw;
            }
        }

        [HttpPost("v1/createuser")]
        // Este método recebe um objeto User do corpo da requisição HTTP ([FromBody]) e o contexto do banco de dados ([FromServices])
        // Adiciona o usuário ao contexto e salva as mudanças no banco de dados
        // Retorna o objeto User que foi adicionado.
        public async Task<IActionResult> PostAsync(
            [FromBody] CreateUserViewModel user, // ViewModel
            [FromServices] AppDbContext context)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newUser = new User
                {
                    Id = 0,
                    Name = user.Name,
                    Login = user.Login,
                    Password = user.Password,
                    Account = user.Account,
                    Balance = user.Balance
                };

                await context.Users.AddAsync(newUser);
                await context.SaveChangesAsync();

                return Created($"v1/createuser/{newUser.Id}", newUser);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "V01EX2 - Não foi possível incluir o registro. " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "V01EX2 - Erro ao tentar incluir o registro. " + ex.Message);
                throw;
            }
        }

        [HttpPut("v1/uploaduser/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] UpdateViewModel user,
            [FromServices] AppDbContext context)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var uploadedUser = new User
                {
                    Id = user.Id,
                    Name = user.Name,
                    Login = user.Login,
                    Password = user.Password,
                    Account = user.Account,
                    Balance = user.Balance
                };

                var userToUpdate = await context
                .Users
                .FirstOrDefaultAsync(user => user.Id == id);

                if (userToUpdate == null)
                    return NotFound();

                userToUpdate!.Name = user.Name;
                userToUpdate.Login = user.Login;
                userToUpdate.Password = user.Password;
                userToUpdate.Account = user.Account;
                userToUpdate.Balance = user.Balance;

                context.Users.Update(userToUpdate);
                await context.SaveChangesAsync();

                return Created($"v1/uploaduser/{user.Id}", userToUpdate);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "V01EX3 - Não foi possível incluir o registro. " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "V01EX3 - Erro ao tentar incluir o registro. " + ex.Message);
                throw;
            }
        }

        [HttpDelete("v1/deleteuser/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] AppDbContext context)
        {
            try
            {
                var userToDelete = await context
                    .Users
                    .FirstOrDefaultAsync(user => user.Id == id);

                if (userToDelete == null)
                    return NotFound(); // Added null check

                context.Users.Remove(userToDelete);
                await context.SaveChangesAsync();

                return Ok(userToDelete);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "V01EX4 - Não foi possível incluir o registro. " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "V01EX4 - Erro ao tentar incluir o registro. " + ex.Message);
                throw;
            }
        }
    }
}