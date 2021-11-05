using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Labb3.Managers;
using Labb3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Labb3.ViewModels
{
    internal class PlayViewModel : ObservableObject
    {
        private List<string> _availableQuizzes;
        private int _currentQuizIndex;
        private string _statement;
        private string _imagePath;
        private int _numberOfQuestions;
        private int _numOfQuestionsAsked;
        private int _numOfCorrectAnswers;
        private string _category;
        private string _resultDisplayText;
        private Quiz _currentQuiz;
        private Question _currentQuestion;
        private readonly QuizManager _quizManager;

        public ObservableCollection<string> Answers { get; set; }
        public List<string> SelectedCategories { get; set; }
        public ObservableCollection<Category> Categories { get; set; } = new();


        public PlayViewModel(QuizManager quizManager)
        {
            _quizManager = quizManager;
            Statement = "Choose a quiz and press play!";
            Answers = new ObservableCollection<string>() { string.Empty, string.Empty, string.Empty };
            AvailableQuizzes = _quizManager.Quizzes.Select(q => q.Title).ToList();
            SelectedCategories = new List<string>();
        }

        public List<string> AvailableQuizzes
        {
            get => _availableQuizzes;
            set => SetProperty(ref _availableQuizzes, value);
        }

        public int CurrentQuizIndex
        {
            get => _currentQuizIndex;
            set
            {
                _currentQuizIndex = value;
                UpdateCategories();
            }
        }

        public string Statement
        {
            get => _statement;
            set => SetProperty(ref _statement, value);
        }

        public string ImagePath
        {
            get => _imagePath;
            set => SetProperty(ref _imagePath, value);
        }

        public string Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        public string ResultDisplayText
        {
            get => _resultDisplayText;
            set => SetProperty(ref _resultDisplayText, value);
        }

        public ICommand PlayCommand => new RelayCommand(Play);
        public ICommand Answer1Command => new RelayCommand(() => Answer(0));
        public ICommand AnswerXCommand => new RelayCommand(() => Answer(1));
        public ICommand Answer2Command => new RelayCommand(() => Answer(2));

        private void Play()
        {
            SelectedCategories.Clear();

            foreach (var category in Categories.Where(c => c.IsSelected))
                SelectedCategories.Add(category.CategoryName);

            if (_availableQuizzes.Count == 0)
                return;

            _currentQuiz = _quizManager.Play(_availableQuizzes[CurrentQuizIndex]);

            // Om användaren valt några specifika kategorier beräknas antalet frågor som har någon av de kategorierna, annars summeras alla frågor i quizet.
            _numberOfQuestions = SelectedCategories.Count > 0 ?
                _currentQuiz.Questions.Count(q => SelectedCategories.Contains(q.Category)) :
                _currentQuiz.Questions.Count;

            _numOfCorrectAnswers = 0;
            _numOfQuestionsAsked = -1;
            _currentQuiz.ResetQuestions();
            ChangeQuestion();
        }

        private void Answer(int indexOfAnswer)
        {
            if (_currentQuestion == null)
                return;

            if (_currentQuestion.CorrectAnswer == indexOfAnswer)
                _numOfCorrectAnswers++;
            ChangeQuestion();
        }

        public void UpdateList()
        {
            AvailableQuizzes = _quizManager.Quizzes.Select(q => q.Title).ToList();
            if (_quizManager.Quizzes.Count < 1)
                return;
            _currentQuiz = _quizManager.Play(_availableQuizzes[CurrentQuizIndex]);
            UpdateCategories();
        }

        private void UpdateCategories()
        {
            Categories.Clear();

            if (_quizManager.Quizzes.Count == 0)
                return;

            var tempCategoryNames = _quizManager.Quizzes[CurrentQuizIndex].Questions.Select(q => q.Category).Distinct()
                .ToList();

            foreach (var category in tempCategoryNames)
                Categories.Add(new Category(category));
        }

        private void ChangeQuestion()
        {
            _currentQuestion = _currentQuiz.GetRandomQuestion(SelectedCategories);

            if (_currentQuestion == null)
            {
                _numOfQuestionsAsked++;
                UpdateResult();
                _ = MessageBox.Show($"Your score: {_numOfCorrectAnswers}/{_numberOfQuestions}", "End of quiz");
                return;
            }

            Answers = new(_currentQuestion.Answers);
            OnPropertyChanged(nameof(Answers));
            Statement = _currentQuestion.Statement;
            ImagePath = _currentQuestion.ImagePath;
            Category = _currentQuestion.Category;
            _currentQuestion.IsAsked = true;
            _numOfQuestionsAsked++;
            UpdateResult();
        }

        private void UpdateResult()
        {
            int tempNumOfQuestionsAsked = _numOfQuestionsAsked == 0 ? 1 : _numOfQuestionsAsked;
            int tempNumOfCurrentQuestion = _numOfQuestionsAsked < _numberOfQuestions ? _numOfQuestionsAsked + 1 : _numOfQuestionsAsked;
            ResultDisplayText = $"Question          {tempNumOfCurrentQuestion}/{_numberOfQuestions} \n\n" +
                                $"Correct answers   {_numOfCorrectAnswers}/{_numberOfQuestions} \n\n" +
                                $"Correct answers   {Math.Round((decimal)_numOfCorrectAnswers * 100 / tempNumOfQuestionsAsked, 2)}%";
        }
    }
}