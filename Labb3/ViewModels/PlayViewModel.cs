using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Labb3.Managers;
using Labb3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Labb3.ViewModels
{
    class PlayViewModel : ObservableObject
    {
        private List<string> _availableQuizzes;
        private int _currentQuizIndex;
        private string _statement;
        private string _imagePath;
        private ObservableCollection<string> _answers;
        private int _numberOfQuestions;
        private int _numOfQuestionsAsked;
        private int _numOfCorrectAnswers;
        private string _category;
        private string _resultDisplayText;
        public int CurrentCategoryIndex { get; set; } //ToDo: Ta bort!
        public List<string> SelectedCategories { get; set; }
        public ObservableCollection<Category> Categories { get; set; } = new();
        public readonly QuizManager _quizManager;
        private Quiz CurrentQuiz { get; set; }
        private Question CurrentQuestion { get; set; }

        //ToDo: Fixa det där med kategorier; man ska kunna spela med bara frågor från en kategori till exempel.
        //ToDo: Ändra till mer enhetlig syntax med lambdapilar på get.

        public PlayViewModel(QuizManager quizManager)
        {
            _quizManager = quizManager;
            Statement = "Choose a quiz and press play!";
            Answers = new ObservableCollection<string>() {"", "", ""};
            AvailableQuizzes = _quizManager.Quizzes.Select(q => q.Title).ToList();
            SelectedCategories = new List<string>();
        }

        public string ResultDisplayText
        {
            get => _resultDisplayText;
            set
            {
                SetProperty(ref _resultDisplayText, value);
            }
        }

        public string Category
        {
            get => _category;
            set
            {
                SetProperty(ref _category, value);
            }
        }

        public int NumOfCorrectAnswers
        {
            get { return _numOfCorrectAnswers; }
            set
            {
                SetProperty(ref _numOfCorrectAnswers, value);
                // Borde kunna ta bort SetProperty
            }
        }

        public int NumOfQuestionsAsked
        {
            get { return _numOfQuestionsAsked; }
            set
            {
                SetProperty(ref _numOfQuestionsAsked, value);
                // Borde kunna ta bort SetProperty
            }
        }

        public int NumberOfQuestions
        {
            get { return _numberOfQuestions; }
            set
            {
                _numberOfQuestions = value;
                //SetProperty(ref _numberOfQuestions, value);
                // Borde kunna ta bort hela propertyn och ändra fältet direkt
            }
        }

        public ObservableCollection<string> Answers
        {
            get { return _answers; }
            set { _answers = value; }
        }

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                SetProperty(ref _imagePath, value);
            }
        }

        public string Statement
        {
            get { return _statement; }
            set
            {
                SetProperty(ref _statement, value);
            }
        }

        public int CurrentQuizIndex
        {
            get { return _currentQuizIndex; }
            set
            {
                _currentQuizIndex = value;
                UpdateCategories();
            }
        }

        public List<string> AvailableQuizzes
        {
            get => _availableQuizzes;
            set
            {
                SetProperty(ref _availableQuizzes, value);
            }
        }

        public ICommand PlayCommand => new RelayCommand(Play);
        public ICommand Answer1Command => new RelayCommand(() => Answer(0));
        public ICommand AnswerXCommand => new RelayCommand(() => Answer(1));
        public ICommand Answer2Command => new RelayCommand(() => Answer(2));
        public ICommand UpdateListCommand => new RelayCommand(UpdateList);

        private void UpdateList()
        {
            AvailableQuizzes = _quizManager.Quizzes.Select(q => q.Title).ToList();
            if (_quizManager.Quizzes.Count < 1)
                return;
            CurrentQuiz = _quizManager.Play(_availableQuizzes[CurrentQuizIndex]);
            UpdateCategories();
        }

        private void UpdateCategories()
        {
            Categories.Clear();

            var tempCategoryNames = _quizManager.Quizzes[CurrentQuizIndex].Questions.Select(q => q.Category).Distinct()
                .ToList();

            foreach (var category in tempCategoryNames)
                Categories.Add(new Category(category));
        }

        private void Play()
        {
            SelectedCategories.Clear();

            foreach (var category in Categories.Where(c => c.IsSelected))
                SelectedCategories.Add(category.CategoryName);

            if (_availableQuizzes.Count == 0)
                return;
            CurrentQuiz = _quizManager.Play(_availableQuizzes[CurrentQuizIndex]);
            NumberOfQuestions = CurrentQuiz.Questions.Count(q => SelectedCategories.Contains(q.Category));
            NumOfCorrectAnswers = 0;
            NumOfQuestionsAsked = -1;
            CurrentQuiz.ResetQuestions();
            QuestionChanged();
        }

        private void Answer(int indexOfAnswer)
        {
            if (CurrentQuestion == null)
                return;

            if (CurrentQuestion.CorrectAnswer == indexOfAnswer)
                NumOfCorrectAnswers++;
            QuestionChanged();
        }

        private void QuestionChanged()
        {
            Answers.Clear();
            CurrentQuestion = CurrentQuiz.GetRandomQuestion(SelectedCategories);

            if (CurrentQuestion == null)
            {
                NumOfQuestionsAsked++;
                UpdateResult();
                MessageBox.Show($"Your score: {NumOfCorrectAnswers}/{NumberOfQuestions}", "End of quiz");
                return;
            }

            foreach (var answer in CurrentQuestion.Answers)
            {
                Answers.Add(answer);
            }

            Statement = CurrentQuestion.Statement;
            ImagePath = CurrentQuestion.ImagePath;
            Category = CurrentQuestion.Category;
            CurrentQuestion.IsAsked = true;
            NumOfQuestionsAsked++;
            UpdateResult();
        }

        private void UpdateResult()
        {
            int tempNumOfQuestionsAsked = NumOfQuestionsAsked == 0 ? 1 : NumOfQuestionsAsked;
            ResultDisplayText = $"Question          {NumOfQuestionsAsked}/{NumberOfQuestions} \n\n" +
                                $"Correct answers:  {NumOfCorrectAnswers}/{NumberOfQuestions} \n\n" +
                                $"Correct answers   {Math.Round((decimal)NumOfCorrectAnswers * 100 / (decimal)tempNumOfQuestionsAsked, 2)}%";
        }
    }
}