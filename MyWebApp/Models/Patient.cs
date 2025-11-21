using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите фамилию")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Укажите имя")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();

        [Required(ErrorMessage = "Укажите номер полиса")]
        [Display(Name = "Номер полиса ОМС")]
        public string InsuranceNumber { get; set; }

        [Phone(ErrorMessage = "Некорректный номер")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Display(Name = "Адрес проживания")]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime BirthDate { get; set; }

        public int? WardId { get; set; }
        public Ward? Ward { get; set; }
    }
}