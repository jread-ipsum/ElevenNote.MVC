using ElevenNote.Data;
using ElevenNote.Models.CategoryModels;
using ElevenNote.Models.NoteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class CategoryService
    {
        private readonly Guid _userId;

        public CategoryService(Guid userId)
        {
            userId = _userId;
        }

        public bool CreateCategory(CategoryCreate model)
        {
            var entity =
                new Category()
                {
                    Name = model.CategoryName,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Categories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CategoryListItem> GetCategories()
        {
            using(var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Categories
                    .Where(e => e.OwnerId == _userId)
                    .Select(e =>
                   new CategoryListItem
                   {
                       CategoryId = e.CategoryId,
                       CategoryName = e.Name,
                       CreatedUtc = e.CreatedUtc
                   });
                return query.ToArray();
            }
        }
    }
}
