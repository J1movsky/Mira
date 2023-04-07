using Mira.Domain.Repositories.Abstract;

namespace Mira.Domain
{
    public class DataManager  //Обслуживающий класс, для репозитория AppDbContext
    {
        public ITextFieldRepository TextFields { get; set; }
        public IServiceItemsRepository ServiceItems { get; set; }

        public DataManager(ITextFieldRepository textFieldRepository, IServiceItemsRepository serviceItemsRepository)
        {
            TextFields = textFieldRepository;
            ServiceItems = serviceItemsRepository;
        }


    }
}
