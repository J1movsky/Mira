using Mira.Domain.Repositories.Abstract;
namespace Mira.Domain
{
    public class DataManager  //Обслуживающий класс, для репозитория AppDbContext
    {
        public ITextFieldRepository TextField { get; set; }
        public IServiceItemsRepository ServiceItemsRepository { get; set; }

        public DataManager(ITextFieldRepository textField, IServiceItemsRepository serviceItemsRepository)
        {
            TextField = textField;
            ServiceItemsRepository = serviceItemsRepository;
        }


    }
}
