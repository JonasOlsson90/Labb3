using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb3.Managers;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Labb3.ViewModels
{
    class CreateViewModel : ObservableObject
    {
        //ToDo: QuizManager
        private QuizManager _quizManager;
        private string _title;
        private string _category;
        private string _question;
        private string[] _answers;
        private int _numOfQuestions;
        private string _imagePath;

        public CreateViewModel(QuizManager quizManager)
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

        public int NumOfQuestions
        {
            get { return _numOfQuestions; }
            set
            {
                SetProperty(ref _numOfQuestions, value);
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

        public string Title
        {
            get { return _title; }
            set
            {
                SetProperty(ref _title, value);
            }
        }
    }
}
