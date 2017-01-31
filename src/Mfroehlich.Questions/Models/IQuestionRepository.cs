using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Mfroehlich.Questions.Models
{
    public interface IQuestionRepository
    {
        void Create(Question que);

        IEnumerable<Group> GetAllGroups();
        
        Group FindGroup(int id);

        void Update(Question que, Question update);
    }
}