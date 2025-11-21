using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class Ward
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите номер палаты")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Укажите этаж")]
        public int Floor { get; set; }

        [Required(ErrorMessage = "Укажите вместимость")]
        public int Capacity { get; set; }

        public string Type { get; set; } 

        public ICollection<Patient>? Patients { get; set; }
    }
}