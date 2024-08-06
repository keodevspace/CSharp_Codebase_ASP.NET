// TokenService é responsável por gerar o token JWT para o usuário logado.

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Bankokeo.Models;
using Microsoft.IdentityModel.Tokens;

namespace Bankokeo.Services;

public class TokenService
{
    public string GenerateToken(User user)
    {
        // JwtSecurityTokenHandler é responsável por manipular o token JWT.
        var tokenHandler = new JwtSecurityTokenHandler();
        // Não posso passar a key como uma string pq o tokenHandler espera um array de bits: usar `Encoding.ASCII.GetBytes()`
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);

        // tokenDescriptor é responsável por armazenar as informações do token.
        var tokenDescriptor = new SecurityTokenDescriptor
        {

            Expires = DateTime.UtcNow.AddHours(2), // O token expira em 2 horas.
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        // Cria o token com base no tokenDescriptor
        var token = tokenHandler.CreateToken(tokenDescriptor);
        // Retorna o token como uma string
        return tokenHandler.WriteToken(token);
    }
}