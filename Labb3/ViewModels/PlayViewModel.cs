using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Labb3.Managers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Labb3.ViewModels
{
    class PlayViewModel : ObservableObject
    {
        private IEnumerable<string> _availableQuizzes;
        private int _currentQuiz;
        private string _statement;
        private string _imagePath;
        private string[] _answers;
        private int _numberOfQuestions;
        private int _numOfQuestionsAsked;
        private int _numOfCorrectAnswers;
        private string _category;
        public QuizManager QuizManager { get; }

        //ToDO: Fixa det där med antal korrekta svar och sånt.
        //ToDo: Ändra till mer enhetlig syntax med lambdapilar på get.

        public PlayViewModel(QuizManager quizManager)
        {
            QuizManager = quizManager;
            Statement = "Choose a quiz and press play!";
            Answers = new string[3];
            ImagePath = @"C:\Users\olsso\OneDrive\Bilder\EvolutionOfBulbasaur1.png"; //ToDo: Ta bort!!!
            Answers[0] = "Bulbasaur"; //ToDO: Ta bort!!!
            Category = "Pokémon"; //ToDO: Ta bort!!!
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

        public string Statement
        {
            get { return _statement; }
            set
            {
                SetProperty(ref _statement, value);
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

        public ICommand PlayCommand => new RelayCommand(QuizManager.Play);
        public ICommand Answer1Command => new RelayCommand(QuizManager.Answer1);
        public ICommand AnswerXCommand => new RelayCommand(QuizManager.AnswerX);
        public ICommand Answer2Command => new RelayCommand(QuizManager.Answer2);
    }
}
