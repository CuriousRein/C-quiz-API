using Microsoft.AspNetCore.Mvc.ModelBinding;
using simplequiz.Model;
using System.Text.Json.Serialization;

namespace simplequiz.Model
{
    public class Questionaire
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public string Trivia { get; set; }
        public string Module { get; set; }
        public string Topic { get; set; }
        public ICollection<Choice> Choices { get; set; }
    }

    public class Choice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAnswer { get; set; }
        [JsonIgnore]
        public Questionaire Question { get; set; }
    }

}
