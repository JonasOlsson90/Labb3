using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Labb3.Managers;
using Labb3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Win32;

namespace Labb3.ViewModels
{
    class CreateViewModel : ObservableObject
    {
        private readonly QuizManager _quizManager;
        private string _title;
        private string _category;
        private string _question;
        private string _answer1;
        private string _answerX;
        private string _answer2;
        private int _numOfQuestions;
        private string _imagePath;
        private int _correctAnswer;
        public ObservableCollection<string> Categories => new(_quizManager.Categories);

        public CreateViewModel(QuizManager quizManager)
        {
            _quizManager = quizManager;
            Category = _quizManager.Categories[0];
        }

        public int CorrectAnswer
        {
            get => _correctAnswer;
            set
            {
                SetProperty(ref _correctAnswer, value);
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

        //ToDo: Fundera på om du ska ha med detta, om så: implementera
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

        public ICommand ChooseImageCommand => new RelayCommand(ChoosePicture);
        public ICommand CreateQuizCommand => new RelayCommand(CreateNewQuiz);
        public ICommand AddQuestionCommand => new RelayCommand(AddQuestion);

        private void ChoosePicture()
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Images (*.jpg, *.jpeg, *.png)|*.jpg; *.jpeg; *.png|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                var fileName = openFileDialog.FileName;
                if (ValidateImageFile(fileName))
                    ImagePath = fileName;
                else
                {
                    MessageBox.Show("You have to chose an image file (*.jpg, *.jpeg, *.png)", "NOT A PICTURE");
                }
            }
        }

        private bool ValidateImageFile(string fileName)
        {
            try
            {
                var bitmap = new BitmapImage(new Uri(fileName));
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private void AddQuestion()
        {
            if (string.IsNullOrEmpty(Question))
            {
                MessageBox.Show("You have to enter a question!", "NO QUESTION ASKED");
                return;
            }
            _quizManager.AddQuestion(Category, Question, CorrectAnswer, ImagePath, Answer1, AnswerX,
                Answer2);
            Question = string.Empty;
            CorrectAnswer = 0;
            ImagePath = string.Empty;
            Answer1 = string.Empty;
            AnswerX = string.Empty;
            Answer2 = string.Empty;
        }

        private void CreateNewQuiz()
        {
            if (_quizManager.TempQuestions.Count == 0)
            {
                MessageBox.Show("You have to add questions to your quiz", "NO QUESTIONS");
                return;
            }

            if (string.IsNullOrEmpty(Title))
            {
                MessageBox.Show("The quiz has to have a title", "NO TITLE");
                return;
            }

            _quizManager.CreateNewQuiz(Title);
            _quizManager.TempQuestions = new List<Question>();
            Title = string.Empty;
            Category = _quizManager.Categories[0];
        }
    }
}
