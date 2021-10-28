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
    class EditViewModel : ObservableObject
    {
        private QuizManager _quizManager;
        private string _category;
        private string _question;
        private string _answer1;
        private string _answerX;
        private string _answer2;
        private string _imagePath;
        private int _currentQuizIndex;
        private int _currentQuestionIndex;
        private int _correctAnswer;
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

        public int CorrectAnswer
        {
            get => _correctAnswer;
            set => SetProperty(ref _correctAnswer, value);
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
        public ICommand ChooseImageCommand => new RelayCommand(ChoosePicture);
        public ICommand ApplyChangesCommand => new RelayCommand(ApplyChanges);
        public ICommand DeleteQuestionCommand => new RelayCommand(DeleteQuestion);
        public ICommand AddNewQuestionCommand => new RelayCommand(AddNewQuestion);

        private void UpdateList()
        {
            AvailableQuizzes = _quizManager.Quizzes.Select(q => q.Title).ToList();
        }

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

        private void ApplyChanges()
        {
            _currentQuestion.UpdateQuestion(Category, Question, CorrectAnswer, ImagePath, Answer1, AnswerX, Answer2);
            
            UpdateList();
        }

        private void DeleteQuestion()
        {
            throw new NotImplementedException();
        }

        private void AddNewQuestion()
        {
            throw new NotImplementedException();
        }

        private void UpdateQuestions()
        {
            if (_quizManager.Quizzes.Count == 0)
            {
                MessageBox.Show("No quizzes found. Please go to the create tab to create a new quiz.", "NO QUIZZES");
                return;
            }

            AvailableQuestions = _quizManager.Quizzes[CurrentQuizIndex].Questions.Select(q => q.Statement).ToList();
            UpdateCurrentQuestion();
        }

        private void UpdateCurrentQuestion()
        {
            //ToDo: fixa så att det inte kraschar!
            //ToDo: fixa så att radioknappisarna hänger med!
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
