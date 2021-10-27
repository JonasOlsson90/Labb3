using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Labb3.Models;

namespace Labb3.Managers
{
    class QuizManager
    {
        //ToDo: Implementera edit quiz

        public List<string> Categories => new() { "Geography", "Entertainment", "History", "Arts & Literature", "Science & Nature", "Pokémon" };
        public List<Quiz> Quizzes { get; set; }
        public List<Question> TempQuestions { get; set; }

        public QuizManager()
        {
            TempQuestions = new List<Question>();
            Quizzes = new List<Quiz>();
        }

        public Quiz Play(string title)
        {
            return Quizzes.Find(q => q.Title == title);
        }

        public void Answer1()
        {
            //ToDo: Implementera
            throw new NotImplementedException();
        }
        public void AnswerX()
        {
            //ToDo: Implementera
            throw new NotImplementedException();
        }
        public void Answer2()
        {
            //ToDo: Implementera
            throw new NotImplementedException();
        }

        public void ChoosePicture()
        {
            //ToDo: Implementera
            throw new NotImplementedException();
        }

        public void AddQuestion(string category, string question, int correctAnswer, string imagePath, string answer1, string answerX, string answer2)
        {
            TempQuestions.Add(new Question(category, question, correctAnswer, imagePath, answer1, answerX, answer2));
        }

        public void CreateNewQuiz(string title)
        {
            Quizzes.Add(new Quiz(title, TempQuestions));
        }
    }
}
