using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Mfroehlich.Questions.Models
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Question> Questions { get; set; }
        public ICollection<Category> Categories { get; set; }

        public static Group Create(string name)
        {
            return new Group {
                Name = name
            };
        }

        // public object ToJSON()
        // {
        //     return new
        //     {
        //         id = Id,
        //         name = Name,
        //         categories = Categories.ToDictionary(c => c.Id, c => c.Name),
        //         questions = Questions.Count
        //     };
        // }
    }
}