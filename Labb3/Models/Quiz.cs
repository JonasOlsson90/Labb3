using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Labb3.Models
{
    internal class Quiz
    {
        private readonly Random _random;
        public ICollection<Question> Questions { get; }
        public string Title { get; private set; }

        public Quiz(string title)
        {
            _random = new Random();
            Questions = new List<Question>();
            Title = title;
        }

        [JsonConstructor]
        public Quiz(string title, ICollection<Question> questions)
        {
            _random = new Random();
            Title = title;
            Questions = questions.ToList();
        }

        public void ChangeTitle(string title)
        {
            Title = title;
        }

        public Question GetRandomQuestion(ICollection<string> categories)
        {
            var notYetAskedQuestions = categories.Count > 0 ?
                Questions.ToList().FindAll(q => !q.IsAsked && categories.Contains(q.Category)) :
                Questions.ToList().FindAll(q => !q.IsAsked);

            if (notYetAskedQuestions.Count == 0)
                return null;

            return notYetAskedQuestions[_random.Next(notYetAskedQuestions.Count)];
        }

        public void AddQuestion(string category, string statement, int correctAnswer, string imagePath, params string[] answers)
        {
            Questions.Add(new Question(category, statement, correctAnswer, imagePath, answers));
        }

        public void RemoveQuestion(int index)
        {
            if (Questions.Count <= index)
                return;

            var temp = Questions.ToList()[index];
            Questions.Remove(temp);
        }

        public void ResetQuestions()
        {
            foreach (var question in Questions)
            {
                question.IsAsked = false;
            }
        }
    }
}
