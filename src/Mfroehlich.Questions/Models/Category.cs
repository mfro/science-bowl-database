namespace Mfroehlich.Questions.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static Category Create(string name)
        {
            return new Category {
                Name = name
            };
        }
    }
}