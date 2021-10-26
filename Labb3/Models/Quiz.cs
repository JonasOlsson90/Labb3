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
            _random = new Random();
            Title = title;
            Questions = questions.ToList();
        }

        public Question GetRandomQuestion()
        {
            //ToDo: Se till att den inte kör samma frågor om och om igen

            var notYetAskedQuestions = Questions.ToList().FindAll(q => !q.IsAsked);

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
            if (Questions.Count > index)
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
