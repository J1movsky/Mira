using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Mira.Domain.Entites
{
    public class TextField : EntityBase
    {
        [Required]
        public string CodeWord { get; set; }

        [Display(Name = "Название страницы (Заголовок)")]
        public override string Title { get; set; } = "Информационная страница";

        [Display(Name = "Содержание страницы")]
        public override string Text { get; set; } = "Содержание заполняется администратором";


    }
}
