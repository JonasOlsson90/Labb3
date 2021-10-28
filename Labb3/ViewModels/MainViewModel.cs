using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb3.Managers;
using Labb3.ViewModels;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Labb3.ViewModels
{
    class MainViewModel
    {
        public QuizManager QuizManager { get; }
        public FileManager FileManager { get; }
        public ObservableObject PlayViewModel { get; }
        public ObservableObject EditViewModel { get; }
        public ObservableObject CreateViewModel { get; }

        public MainViewModel()
        {
            FileManager = new();
            QuizManager = new QuizManager(FileManager);
            PlayViewModel = new PlayViewModel(QuizManager);
            EditViewModel = new EditViewModel(QuizManager);
            CreateViewModel = new CreateViewModel(QuizManager);
        }
    }
}
