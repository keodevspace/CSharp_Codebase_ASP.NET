// Configuration é responsável por armazenar as configurações da aplicação.
// Aqui vai ficar as configurações de banco de dados, chave de criptografia, etc.


namespace Bankokeo;

public static class Configuration
{
    public static string JwtKey { get; set; } = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyIjoia2VvIiwicGFzc3dvcmQiOjEyMywiaWF0IjoxNzIyOTc4MTc4fQ.uHaIez3MQZMNo_ZoCJkgkJU1yT3odIEGMsWJ4eYboO4";
}