using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3.Models
{
    class Quiz
    {
        private readonly Random _random;
        public ICollection<Question> Questions { get; }
        public string Title { get; }

        public Quiz(string title)
        {
            _random = new Random();
            Questions = new List<Question>();
            Title = title;
        }

        public Quiz(string title, ICollection<Question> questions)
        {
            Title = title;
            Questions = questions;
        }

        public Question GetRandomQuestion()
        {
            return Questions.ToList()[_random.Next(Questions.Count - 1)];
        }

        public void AddQuestion(string category, string statement, int correctAnswer, string imagePath, params string[] answers)
        {
            Questions.Add(new Question(category, statement, correctAnswer, imagePath, answers));
        }

        public void RemoveQuestion(int index)
        {
            if (Questions.Count > index)
                return;

            var temp = Questions.ToList()[index];
            Questions.Remove(temp);
        }
    }
}
