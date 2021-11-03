using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private readonly QuizManager _quizManager;
        private string _quizTitle;
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

        public string QuizTitle
        {
            get => _quizTitle;
            set => SetProperty(ref _quizTitle, value);
        }

        public EditViewModel(QuizManager quizManager)
        {
            _quizManager = quizManager;
        }

        public string Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        public string Question
        {
            get => _question;
            set => SetProperty(ref _question, value);
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

        public string ImagePath
        {
            get => _imagePath;
            set => SetProperty(ref _imagePath, value);
        }

        public int CurrentQuizIndex
        {
            get => _currentQuizIndex;
            set
            {
                SetProperty(ref _currentQuizIndex, value);
                UpdateQuestions();
            }
        }

        public int CurrentQuestionIndex
        {
            get => _currentQuestionIndex;
            set
            {
                SetProperty(ref _currentQuestionIndex, value);
                UpdateCurrentQuestion();
            }
        }

        public int CorrectAnswer
        {
            get => _correctAnswer;
            set => SetProperty(ref _correctAnswer, value);
        }

        public List<string> AvailableQuizzes
        {
            get => _availableQuizzes;
            set
            {
                SetProperty(ref _availableQuizzes, value);
            }
        }

        public List<string> AvailableQuestions
        {
            get => _availableQuestions;
            set => SetProperty(ref _availableQuestions, value);
        }

        public ICommand UpdateListCommand => new RelayCommand(UpdateList);
        public ICommand ChooseImageCommand => new RelayCommand(ChoosePicture);
        public ICommand RemoveImageCommand => new RelayCommand(RemovePicture);
        public ICommand ApplyChangesCommand => new RelayCommand(ApplyChanges);
        public ICommand ClearFieldsCommand => new RelayCommand(ClearPropsAndFields);
        public ICommand DeleteQuestionCommand => new RelayCommand(DeleteQuestion);
        public ICommand DeleteQuizCommand => new RelayCommand(DeleteQuiz);
        public ICommand AddNewQuestionCommand => new RelayCommand(AddNewQuestion);
        public ICommand ImportQuizCommand => new RelayCommand(ImportQuiz);
        public ICommand ExportQuizCommand => new RelayCommand(ExportQuiz);
        

        public void UpdateList()
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
                    _ = MessageBox.Show("You have to chose an image file (*.jpg, *.jpeg, *.png)", "NOT A PICTURE");
                }
            }
        }

        private void RemovePicture()
        {
            ImagePath = string.Empty;
        }

        private void ApplyChanges()
        {
            if (_quizManager.Quizzes.Count == 0)
                return;

            _quizManager.Quizzes[CurrentQuizIndex].ChangeTitle(QuizTitle);
            _currentQuestion.UpdateQuestion(Category, Question, CorrectAnswer, ImagePath, Answer1, AnswerX, Answer2);
            int tempCurrentQuizIndex = CurrentQuizIndex;
            int tempCurrentQuestionIndex = CurrentQuestionIndex;
            UpdateList();
            CurrentQuizIndex = tempCurrentQuizIndex;
            UpdateQuestions();
            CurrentQuestionIndex = tempCurrentQuestionIndex;
            _quizManager.SaveQuizAsync();
        }

        private void DeleteQuestion()
        {
            if (_quizManager.Quizzes.Count == 0)
                return;

            _quizManager.Quizzes[_currentQuizIndex].RemoveQuestion(CurrentQuestionIndex);
            CurrentQuestionIndex = CurrentQuestionIndex <= 0 ? 0 : CurrentQuestionIndex - 1;
            UpdateQuestions();
            _quizManager.SaveQuizAsync();
        }

        private void DeleteQuiz()
        {
            _quizManager.DeleteQuiz(CurrentQuizIndex);
            UpdateList();
        }

        private void AddNewQuestion()
        {
            if (_quizManager.Quizzes.Count == 0)
                return;

            _quizManager.Quizzes[CurrentQuizIndex].AddQuestion(Category, Question, CorrectAnswer, ImagePath, Answer1, AnswerX, Answer2);
            UpdateQuestions();
            CurrentQuestionIndex = AvailableQuestions.Count - 1;
            _quizManager.SaveQuizAsync();
        }

        private void ImportQuiz()
        {
            ImportQuizAsync();
        }

        private void ExportQuiz()
        {
            _quizManager.ExportQuizAsync(CurrentQuizIndex);
        }

        private async Task ImportQuizAsync()
        {
            await _quizManager.ImportQuizAsync();
            UpdateList();
            CurrentQuizIndex = AvailableQuizzes.Count - 1;
        }

        private void UpdateQuestions()
        {
            if (_quizManager.Quizzes.Count == 0)
            {
                ClearPropsAndFields();
                AvailableQuestions.Clear();
                _ = MessageBox.Show("No quizzes found. Please go to the create tab to create a new quiz.", "NO QUIZZES");
                return;
            }

            AvailableQuestions = _quizManager.Quizzes[CurrentQuizIndex].Questions.Select(q => q.Statement).ToList();
            UpdateCurrentQuestion();
        }

        private void UpdateCurrentQuestion()
        {
            if (CurrentQuestionIndex < 0)
                CurrentQuestionIndex = 0;

            if (_quizManager.Quizzes[CurrentQuizIndex].Questions.Count == 0)
            {
                ClearPropsAndFields();
                return;
            }

            _currentQuestion = _quizManager.Quizzes[CurrentQuizIndex].Questions.ToList()[CurrentQuestionIndex];
            
            UpdatePropsAndFields();
        }

        private void UpdatePropsAndFields()
        {
            QuizTitle = _availableQuizzes[CurrentQuizIndex];
            Question = _currentQuestion.Statement;
            Answer1 = _currentQuestion.Answers[0];
            AnswerX = _currentQuestion.Answers[1];
            Answer2 = _currentQuestion.Answers[2];
            CorrectAnswer = _currentQuestion.CorrectAnswer;
            ImagePath = _currentQuestion.ImagePath;
            Category = _currentQuestion.Category;
        }

        private void ClearPropsAndFields()
        {
            Question = string.Empty;
            Answer1 = string.Empty;
            AnswerX = string.Empty;
            Answer2 = string.Empty;
            CorrectAnswer = 0;
            ImagePath = string.Empty;
            Category = "Geography";
        }

        private bool ValidateImageFile(string fileName)
        {
            try
            {
                _ = new BitmapImage(new Uri(fileName));
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
