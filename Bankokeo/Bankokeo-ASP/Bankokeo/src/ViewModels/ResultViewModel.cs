// ResultViewModel é responsável por armazenar os resultados da busca e exibi-los na tela.
// Usada para padronização de retorno de métodos: toda requisição vai retornar essa classe padronizada

using System.Collections.Generic;
using Bankokeo.Models;

namespace Bankokeo.ViewModels
{
    // <T> é um tipo genérico, ou seja, pode ser qualquer tipo de dado
    public class ResultViewModel<T>
    {
        public ResultViewModel(T data, List<string> errors)
        {
            Data = data;
            Errors = errors;
        }

        public T Data { get; private set; }
        public List<string> Errors { get; private set; } = new();
    }

   
}