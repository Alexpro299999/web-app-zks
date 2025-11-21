using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите фамилию")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Укажите имя")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();

        [Required(ErrorMessage = "Укажите специализацию")]
        public string Specialization { get; set; }

        [Required(ErrorMessage = "Укажите кабинет")]
        public string CabinetNumber { get; set; }

        public string Schedule { get; set; }
    }
}