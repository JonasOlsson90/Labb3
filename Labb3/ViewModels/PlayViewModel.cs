using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb3.Managers;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Labb3.ViewModels
{
    class PlayViewModel : ObservableObject
    {
        private QuizManager _quizManager;
        private IEnumerable<string> _availableQuizzes;
        private int _currentQuiz;
        private string _imagePath;
        private string[] _answers;
        private int _numberOfQuestions;
        private int _numOfQuestionsAsked;
        private int _numOfCorrectAnswers;

        public PlayViewModel(QuizManager quizManager)
        {
            _quizManager = quizManager;
        }

        public int NumOfCorrecrAnswers
        {
            get { return _numOfCorrectAnswers; }
            set
            {
                SetProperty(ref _numOfCorrectAnswers, value);
            }
        }

        public int NumOfQuestionsAsked
        {
            get { return _numOfQuestionsAsked; }
            set
            {
                SetProperty(ref _numOfQuestionsAsked, value);
            }
        }

        public int NumberOfQuestions
        {
            get { return _numberOfQuestions; }
            set
            {
                SetProperty(ref _numberOfQuestions, value);
            }
        }

        public string[] Answers
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

        public int CurrentQuiz
        {
            get { return _currentQuiz; }
            set { _currentQuiz = value; }
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
