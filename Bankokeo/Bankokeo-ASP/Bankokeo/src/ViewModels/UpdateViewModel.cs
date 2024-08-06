// ViewModel é responsável por representar os dados que são exibidos na tela, tirando a responsabilidade da Model de fazer isso. Ou seja, a ViewModel é uma representação da Model, mas com a responsabilidade de exibir os dados na tela. 
// UpdateViewModel representa os dados que são exibidos na tela de atualização de um usuário.

using System.ComponentModel.DataAnnotations;

namespace Bankokeo.ViewModels
{
    public class UpdateViewModel
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Login { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public int Account { get; set; }
        [Required]        
        public string? Balance { get; set; }
    }
}