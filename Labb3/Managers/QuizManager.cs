﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Labb3.Models;

namespace Labb3.Managers
{
    class QuizManager
    {
        private readonly FileManager _fileManager;

        public List<string> Categories => new()
            {"Geography", "Entertainment", "History", "Arts & Literature", "Science & Nature", "Pokémon"};

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

        public void AddTempQuestion(string category, string question, int correctAnswer, string imagePath,
            string answer1, string answerX, string answer2)
        {
            TempQuestions.Add(new Question(category, question, correctAnswer, imagePath, answer1, answerX, answer2));
        }

        public void CreateNewQuiz(string title)
        {
            Quizzes.Add(new Quiz(title, TempQuestions));
            TempQuestions.Clear();
        }

        public async Task ExportQuizAsync(int quizIndex)
        {
            await _fileManager.ExportQuizAsync(Quizzes[quizIndex]);
        }

        public async Task ImportQuizAsync()
        {
            var tempQuiz = await Task.Run(() => _fileManager.ImportQuizAsync());
            if (tempQuiz != null)
            {
                string tempTitle = tempQuiz.Title;
                int tempTitleAddition = 2;

                while (Quizzes.Exists(q => q.Title == tempTitle))
                {
                    tempTitle = $"{tempQuiz.Title}({tempTitleAddition})";
                    tempTitleAddition++;
                }

                Quizzes.Add(new Quiz(tempTitle, tempQuiz.Questions));
                await _fileManager.SaveToFileAsync(Quizzes);
            }
        }
    }
}
