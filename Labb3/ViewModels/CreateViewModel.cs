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
    class CreateViewModel : ObservableObject
    {
        //ToDo: QuizManager
        public QuizManager _quizManager;
        private string _title;
        private string _category;
        private string _question;
        private string _answer1;
        private string _answerX;
        private string _answer2;
        private int _numOfQuestions;
        private string _imagePath;
        public int CorrectAnswer { get; set; }
        public ObservableCollection<string> Categories => new (_quizManager.Categories);

        public CreateViewModel(QuizManager quizManager)
        {
            _quizManager = quizManager;
            CorrectAnswer = 0;
        }

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                SetProperty(ref _imagePath, value);
            }
        }

        public int NumOfQuestions
        {
            get { return _numOfQuestions; }
            set
            {
                SetProperty(ref _numOfQuestions, value);
            }
        }

        public string Answer1
        {
            get => _answer1;
            set => SetProperty(ref _answer1, value);
        }
        public string AnswerX
        {
            get => _answerX;
            set => SetProperty(ref _answerX, value);
        }

        public string Answer2
        {
            get => _answer2;
            set => SetProperty(ref _answer2, value);
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

        public string Title
        {
            get { return _title; }
            set
            {
                SetProperty(ref _title, value);
            }
        }

        public ICommand ChooseImageCommand => new RelayCommand( _quizManager.ChoosePicture);
        public ICommand CreateQuizCommand => new RelayCommand(CreateNewQuiz);
        public ICommand AddQuestionCommand => new RelayCommand(AddQuestion);

        private void AddQuestion()
        {
            _quizManager.AddQuestion(Category, Question, CorrectAnswer, ImagePath, Answer1, AnswerX,
                Answer2);
            Category = string.Empty;
            Question = string.Empty;
            CorrectAnswer = 0;
            ImagePath = string.Empty;
            Answer1 = string.Empty;
            AnswerX = string.Empty;
            Answer2 = string.Empty;
        }

        private void CreateNewQuiz()
        {
            _quizManager.CreateNewQuiz(Title);
            _quizManager.TempQuestions = new List<Question>();
        }
    }
}
