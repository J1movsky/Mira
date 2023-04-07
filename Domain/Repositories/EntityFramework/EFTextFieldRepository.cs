using Microsoft.EntityFrameworkCore;
using Mira.Domain.Entites;
using Mira.Domain.Repositories.Abstract;
using System.Security.Cryptography.Xml;

namespace Mira.Domain.Repositories.EntityFramevork
{
    public class EFTextFieldRepository : ITextFieldRepository
    {
        private readonly AppDbContext context;

        public EFTextFieldRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IQueryable<TextField> GetTextFields()
        {
            return context.TextFields;
        }

        public TextField GetTextFieldById(Guid id)
        {
            return context.TextFields.FirstOrDefault(x => x.Id ==id);
        }


        public TextField GetTextFieldByCodeWord(string codeWord)
        {
            return context.TextFields.FirstOrDefault(x => x.CodeWord == codeWord);

        }

        public void SaveTextField(TextField entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();

        }

        public void DeleteTextField(Guid id)
        {
            context.TextFields.Remove(new TextField() { Id = id });
            context.SaveChanges();
        }


    }
}
