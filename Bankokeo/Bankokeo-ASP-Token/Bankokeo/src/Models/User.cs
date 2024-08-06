// Model é responsável por representar a estrutura de dados da aplicação. Ou seja, a Model é a representação da estrutura de dados da aplicação.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bankokeo.Models;

// especifica a tabela que será criada no banco de dados
[Table("User")]
public class User
    {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; } 
    public int Account { get; set; }
    public string? Balance { get; set; }
    }

