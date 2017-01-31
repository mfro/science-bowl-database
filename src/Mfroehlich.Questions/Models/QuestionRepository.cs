using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Mfroehlich.Questions.Models
{
    public class QuestionRepository : IQuestionRepository
    {
        private QuestionContext context;

        public QuestionRepository(QuestionContext context)
        {
            this.context = context;
        }

        public void Create(Question que)
        {
            context.Questions.Add(que);

            context.SaveChanges();
        }

        public IEnumerable<Group> GetAllGroups() {
            return context.Groups
                .Include(g => g.Categories)
                .Include(g => g.Questions);
        }

        public Group FindGroup(int id)
        {
            return GetAllGroups().SingleOrDefault(g => g.Id == id);
        }

        public void Update(Question que, Question update)
        {
            que.TossUp = update.TossUp;
            que.Content = update.Content;
            que.Comment = update.Comment;
            que.Flagged = update.Flagged;

            que.CategoryId = update.CategoryId;

            if (update.Answer != null) {
                que.Answer = update.Answer;
                que.Correct = 0;
                que.Answer1 = null;
                que.Answer2 = null;
                que.Answer3 = null;
                que.Answer4 = null;
            } else {
                que.Answer1 = update.Answer1;
                que.Answer2 = update.Answer2;
                que.Answer3 = update.Answer3;
                que.Answer4 = update.Answer4;
                que.Correct = update.Correct;
                que.Answer = null;
            }
            
            context.SaveChanges();
        }
    }
}