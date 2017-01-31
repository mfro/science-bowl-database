using Mfroehlich.Common.HttpOptions;

namespace Mfroehlich.Questions
{
    public class QuestionsOptions : HttpOptions
    {
        public string[] Origins { get; set; }
    }
}