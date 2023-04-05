using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Mira.Domain.Entites
{
    public class ServiceItem : EntityBase // класс для услуг на сайте
    {
        [Required(ErrorMessage = "Заполните название услуги")]

        [Display(Name = "Название услуги")]
        public override string? Title { get; set; }


        [Display(Name = "Краткое описпние услуги")]
        public override string? Subtitle { get; set; }


        [Display(Name = "Полное описание услуги")]
        public override string? Text { get; set; }


    }
}
