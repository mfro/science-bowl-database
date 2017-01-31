using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Mfroehlich.Questions.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mfroehlich.Questions.Controllers
{
    [Route("[controller]")]
    public class GroupsController : Controller
    {
        private IQuestionRepository questions;

        public GroupsController(IQuestionRepository questions)
        {
            this.questions = questions;
        }

        [HttpGet]
        public IActionResult GetGroups()
        {
            return Ok(questions.GetAllGroups());
        }

        [HttpPost("{groupId}/questions")]
        public IActionResult CreateQuestion(int groupId, [FromBody] Question question)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var group = questions.FindGroup(groupId);
            if (group == null)
                return NotFound();

            question.Group = group;

            questions.Create(question);

            return Ok();
        }

        [HttpPut("{groupId}/questions/{id}")]
        public IActionResult PutQuestion(int groupId, int id, [FromBody] Question update)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var group = questions.FindGroup(groupId);
            if (group == null)
                return NotFound();

            var que = group.Questions.SingleOrDefault(q => q.Id == id);
            if (que == null)
                return NotFound();

            questions.Update(que, update);

            return Ok(que);
        }

        [HttpPost("{groupId}/questions/search")]
        public IActionResult SearchQuestions(int groupId, [FromQuery] bool random, [FromBody] SearchQuery query)
        {
            var group = questions.FindGroup(groupId);

            if (group == null)
                return NotFound();

            var matches = from q in @group.Questions

                          where q.Flagged == query.Flagged
                          where query.Categories == null || query.Categories.Contains(q.CategoryId)
                          where (q.TossUp && query.Tossup) || (!q.TossUp && query.Bonus)
                          where (q.Answer == null && query.Choice) || (q.Answer != null && query.Short)

                          where query.Skip == null || !query.Skip.Contains(q.Id)
                          where string.IsNullOrWhiteSpace(query.Search) || CultureInfo.InvariantCulture.CompareInfo.IndexOf(q.Content, query.Search, CompareOptions.IgnoreCase) >= 0

                          select q;

            var list = matches.ToList();
            object data;

            if (random) {
                if (!list.Any())
                    return NotFound();

                int rand = new Random().Next(list.Count);
                data =  list[rand];
            }
            else {
                if (query.Count == 0) query.Count = 25;

                if (query.Start > list.Count)
                    return NotFound();

                query.Count = Math.Min(query.Count, Math.Min(list.Count - query.Start, 25));

                data = list.Skip(query.Start).Take(query.Count);
            }

            return Ok(new {
                total = list.Count,
                data = data
            });
        }

        public class SearchQuery
        {
            public List<int> Categories { get; set; }

            public List<int> Skip { get; set; }

            public int Start { get; set; }

            public int Count { get; set; }

            public bool Tossup { get; set; }
            public bool Bonus { get; set; }
            public bool Choice { get; set; }
            public bool Short { get; set; }

            public bool Flagged { get; set; }

            public string Search { get; set; }
        }
    }
}