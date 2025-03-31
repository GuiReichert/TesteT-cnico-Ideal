using System.ComponentModel.DataAnnotations;

namespace TesteTécnicoIdeal.API.DTO_s
{
    public class UserDTO
    {
        [Required(ErrorMessage = "O campo do nome não pode ser nulo.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo do sobrenome não pode ser nulo.")]
        public string Surname { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo do número de telefone não pode ser nulo.")]
        [RegularExpression(@"^\+?[0-9\s\-\(\)]{8,15}$", ErrorMessage = "Número de telefone inválido.")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
