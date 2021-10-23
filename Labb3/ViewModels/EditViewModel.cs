using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb3.Managers;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Labb3.ViewModels
{
    class EditViewModel : ObservableObject
    {
        //ToDO: QuizManager
        private QuizManager _quizManager;
        private IEnumerable<string> _availableQuizzes;
        private IEnumerable<string> _availableQuestions;
        private string _category;
        private string _question;
        private string[] _answers;
        private string _imagePath;

        public EditViewModel(QuizManager quizManager)
        {
            _quizManager = quizManager;
        }

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                SetProperty(ref _imagePath, value);
            }
        }

        public string[] Answers
        {
            get { return _answers; }
            set
            {
                SetProperty(ref _answers, value);
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

        public IEnumerable<string> AvailableQuestions
        {
            get { return _availableQuestions; }
            set
            {
                SetProperty(ref _availableQuestions, value);
            }
        }

        public IEnumerable<string> AvailableQuizzes
        {
            get => _availableQuizzes;
            set
            {
                SetProperty(ref _availableQuizzes, value);
            }
        }
    }
}
