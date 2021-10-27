using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3.Models
{
    class Question
    {
        public string Category { get; }
        public string Statement { get; }
        public string[] Answers { get; }
        public int CorrectAnswer { get; }
        public string ImagePath { get; }
        public bool IsAsked { get; set; }

        public Question(string category, string statement, int correctAnswer, string imagePath, params string[] answers)
        {
            Category = category;
            Statement = statement;
            CorrectAnswer = correctAnswer;
            ImagePath = imagePath;
            Answers = answers;
        }
    }
}
