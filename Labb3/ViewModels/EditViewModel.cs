using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Labb3.Managers;
using Labb3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Labb3.ViewModels
{
    class EditViewModel : ObservableObject
    {
        //ToDO: _quizManager
        private readonly QuizManager _quizManager;
        private string _category;
        private string _question;
        private string _answer1;
        private string _answerX;
        private string _answer2;
        private string _imagePath;
        private int _currentQuizIndex;
        private int _currentQuestionIndex;
        private List<string> _availableQuizzes;
        private List<string> _availableQuestions;
        private Question _currentQuestion;
        public ObservableCollection<string> Categories => new(_quizManager.Categories);

        //ToDo: Gör färdigt!

        public EditViewModel(QuizManager quizManager)
        {
            _quizManager = quizManager;
        }

        public List<string> AvailableQuizzes
        {
            get => _availableQuizzes;
            set
            {
                SetProperty(ref _availableQuizzes, value);
                UpdateQuestions();
            }
        }

        public List<string> AvailableQuestions
        {
            get => _availableQuestions;
            set
            {
                SetProperty(ref _availableQuestions, value);
            }
        }

        public int CurrentQuizIndex
        {
            get { return _currentQuizIndex; }
            set { _currentQuizIndex = value; }
        }

        public int CurrentQuestionIndex
        {
            get { return _currentQuestionIndex; }
            set
            {
                _currentQuestionIndex = value;
                UpdateCurrentQuestion();
            }
        }

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                SetProperty(ref _imagePath, value);
            }
        }

        public string Answer1
        {
            get { return _answer1; }
            set
            {
                SetProperty(ref _answer1, value);
            }
        }

        public string AnswerX
        {
            get { return _answerX; }
            set
            {
                SetProperty(ref _answerX, value);
            }
        }

        public string Answer2
        {
            get { return _answer2; }
            set
            {
                SetProperty(ref _answer2, value);
            }
        }

        public string Question
        {
            get { return _question; }
            set
            {
                SetProperty(ref _question, value);
            }
        }

        public string Category
        {
            get { return _category; }
            set
            {
                SetProperty(ref _category, value);
            }
        }

        public ICommand UpdateListCommand => new RelayCommand(UpdateList);

        private void UpdateList()
        {
            AvailableQuizzes = _quizManager.Quizzes.Select(q => q.Title).ToList();
        }

        private void UpdateQuestions()
        {
            AvailableQuestions = _quizManager.Quizzes[CurrentQuizIndex].Questions.Select(q => q.Statement).ToList();
            UpdateCurrentQuestion();
        }

        private void UpdateCurrentQuestion()
        {
            _currentQuestion = _quizManager.Quizzes[CurrentQuizIndex].Questions.ToList()[CurrentQuestionIndex];
            UpdatePropsAndFields();
        }

        private void UpdatePropsAndFields()
        {
            Question = _currentQuestion.Statement;
            Answer1 = _currentQuestion.Answers[0];
            AnswerX = _currentQuestion.Answers[1];
            Answer2 = _currentQuestion.Answers[2];
            ImagePath = _currentQuestion.ImagePath;
            Category = _currentQuestion.Category;
        }


        //public IEnumerable<string> AvailableQuestions
        //{
        //    get { return _availableQuestions; }
        //    set
        //    {
        //        SetProperty(ref _availableQuestions, value);
        //    }
        //}

        //public IEnumerable<string> AvailableQuizzes
        //{
        //    get => _availableQuizzes;
        //    set
        //    {
        //        SetProperty(ref _availableQuizzes, value);
        //    }
        //}
    }
}
