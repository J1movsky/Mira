using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Mira.Domain.Entites
{
    public class TextField : EntityBase  // класс для текстовых полей (номер, телефон, емейл)
    {
        [Required]
        public string? CodeWord { get; set; }  /// будем обращаться к данному текстовому полю не по id  а по contacts или index и т.д. /

        [Display(Name = "Название страницы (Заголовок)")]
        public override string? Title { get; set; } = "Информационная страница";  

        [Display(Name = "Содержание страницы")]
        public override string? Text { get; set; } = "Содержание заполняется администратором";


    }
}
