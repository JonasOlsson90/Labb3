using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb3.ViewModels;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Labb3.Managers
{
    class ViewModelManager
    {
        public QuizManager QuizManager { get; }
        public ObservableObject PlayViewModel { get; }
        public ObservableObject EditViewModel { get; }
        public ObservableObject CreateViewModel { get; }

        public ViewModelManager()
        {
            QuizManager = new QuizManager();
            PlayViewModel = new PlayViewModel(QuizManager);
            EditViewModel = new EditViewModel(QuizManager);
            CreateViewModel = new CreateViewModel(QuizManager);
        }
    }
}
