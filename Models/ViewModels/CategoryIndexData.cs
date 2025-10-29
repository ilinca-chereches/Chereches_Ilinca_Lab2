namespace Chereches_Ilinca_Lab2.Models.ViewModels
{
    public class CategoryIndexData
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<BookCategory> BookCategories { get; set; } = default!;
        public IEnumerable<Book> Books { get; set; }
    }
}
