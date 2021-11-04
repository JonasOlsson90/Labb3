using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Labb3.Managers;
using Labb3.ViewModels;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Labb3.ViewModels
{
    class MainViewModel : ObservableObject
    {
        private int _selectedTabIndex;
        public QuizManager QuizManager { get; }
        public FileManager FileManager { get; }
        public PlayViewModel PlayViewModel { get; }
        public EditViewModel EditViewModel { get; }
        public CreateViewModel CreateViewModel { get; }


        public MainViewModel()
        {
            FileManager = new FileManager();
            QuizManager = new QuizManager(FileManager);
            PlayViewModel = new PlayViewModel(QuizManager);
            EditViewModel = new EditViewModel(QuizManager);
            CreateViewModel = new CreateViewModel(QuizManager);
            QuizManager.QuizzesLoaded += UpdateLists;
        }

        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set
            {
                _selectedTabIndex = value;
                UpdateLists();
            }
        }

        public void UpdateLists()
        {
            PlayViewModel.UpdateList();
            EditViewModel.UpdateList();
        }
    }
}
