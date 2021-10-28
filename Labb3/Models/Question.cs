using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Labb3.Models
{
    class Question
    {
        public string Category { get; private set; }
        public string Statement { get; private set; }
        public string[] Answers { get; private set; }
        public int CorrectAnswer { get; private set; }
        public string ImagePath { get; private set; }
        public bool IsAsked { get; set; }

        [JsonConstructor]
        public Question(string category, string statement, int correctAnswer, string imagePath, params string[] answers)
        {
            Category = category;
            Statement = statement;
            CorrectAnswer = correctAnswer;
            ImagePath = imagePath;
            Answers = answers;
        }

        public void UpdateQuestion(string category, string statement, int correctAnswer, string imagePath, params string[] answers)
        {
            Category = category;
            Statement = statement;
            CorrectAnswer = correctAnswer;
            ImagePath = imagePath;
            Answers = answers;
        }
    }
}
