using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using Labb3.Models;

namespace Labb3.Managers
{
    class QuizManager
    {
        //ToDo: Implementera edit quiz

        private FileManager _fileManager;
        public List<string> Categories => new() { "Geography", "Entertainment", "History", "Arts & Literature", "Science & Nature", "Pokémon" };
        public List<Quiz> Quizzes { get; set; }
        public List<Question> TempQuestions { get; set; }

        public QuizManager(FileManager fileManager)
        {
            TempQuestions = new List<Question>();
            _fileManager = fileManager;
            Quizzes = new List<Quiz>();
            LoadQuizzesAsync();
        }

        public async Task LoadQuizzesAsync()
        {
            Quizzes = await Task.Run(() => _fileManager.LoadQuizzesAsync());
        }

        public Quiz Play(string title)
        {
            return Quizzes.Find(q => q.Title == title);
        }

        public async Task SaveQuizAsync()
        {
            await _fileManager.SaveToFileAsync(Quizzes);
        }

        public void DeleteQuiz(int index)
        {
            Quizzes.RemoveAt(index);
            SaveQuizAsync();
        }

        public void AddTempQuestion(string category, string question, int correctAnswer, string imagePath, string answer1, string answerX, string answer2)
        {
            TempQuestions.Add(new Question(category, question, correctAnswer, imagePath, answer1, answerX, answer2));
        }

        public void CreateNewQuiz(string title)
        {
            Quizzes.Add(new Quiz(title, TempQuestions));
            TempQuestions.Clear();
        }


    }
}
