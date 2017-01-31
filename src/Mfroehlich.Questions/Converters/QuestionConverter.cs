using System;
using Mfroehlich.Questions.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Mfroehlich.Questions.Converters
{
    public class QuestionConverter : JsonConverter<Question>
    {
        public override Question ReadJson(JsonReader reader, Question existingValue, JsonSerializer serializer)
        {
            var json = JObject.ReadFrom(reader);

            var q = json.ToObject<Question>();
            q.CategoryId = (int)json["category"];

            if ((string)json["type"] == "choice") {
                q.Answer = null;

                if (json["answers"] != null) {
                    var answers = json["answers"].ToObject<string[]>();
                    q.Answer1 = answers[0];
                    q.Answer2 = answers[1];
                    q.Answer3 = answers[2];
                    q.Answer4 = answers[3];
                }
            }
            else {
                q.Answer1 = q.Answer2 = q.Answer3 = q.Answer4 = null;
                q.Correct = 0;
            }

            return q;
        }

        public override void WriteJson(JsonWriter writer, Question q, JsonSerializer serializer)
        {
            var json = new JObject {
                ["id"] = q.Id,
                ["content"] = q.Content,
                ["comment"] = q.Comment,
                ["tossup"] = q.TossUp,
                ["flagged"] = q.Flagged,
                ["created"] = q.DateCreated,
                ["modified"] = q.DateModified,
                ["category"] = q.CategoryId
            };


            if (q.Answer == null) {
                json["type"] = "choice";
                json["answers"] = new JArray { q.Answer1, q.Answer2, q.Answer3, q.Answer4 };
                json["correct"] = q.Correct;
            }
            else {
                json["type"] = "short";
                json["answer"] = q.Answer;
            }

            serializer.Serialize(writer, json);
        }
    }
}