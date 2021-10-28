using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb3.Models;
using System.Text.Json;
using System.Windows;

namespace Labb3.Managers
{
    class FileManager
    {
        private readonly string _directoryPath;
        private readonly string _pathToFile;
        private List<Quiz> _standardQuizzes;

        public FileManager()
        {
            _directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Labb3ByJonas");
            _pathToFile = Path.Combine(_directoryPath, "Quizzes.json");
        }

        //ToDo: Gör async
        public List<Quiz> LoadQuizzes()
        {
            if (!File.Exists(_pathToFile))
                CreateFile();

            List<Quiz> quizList = new();
            string jsonString = string.Empty;

            try
            {
                jsonString = File.ReadAllText(_pathToFile);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Exception thrown: {e}", "ERROR");
                throw;
            }

            quizList = JsonSerializer.Deserialize<List<Quiz>>(jsonString);

            return quizList;
        }

        public void SaveToFile(List<Quiz> quizzesToSave)
        {
            string jsonString = JsonSerializer.Serialize(quizzesToSave);
            File.WriteAllText(_pathToFile, jsonString);
        }

        public void CreateFile()
        {
            CreateStandardQuiz();
            Directory.CreateDirectory(_directoryPath);
            using FileStream file = File.Create(_pathToFile);
            file.Close();
            SaveToFile(_standardQuizzes);
        }

        private void CreateStandardQuiz()
        {
            //ToDO: Gör ett standardquiz och lägg till i Quizzes.

            List<Question> tempQuestion = new();
            _standardQuizzes = new();

            tempQuestion.Add(new Question("Pokémon", "Select the best pokémon", 1, "", "Charmander", "Oddish", "Squirtle"));
            _standardQuizzes.Add(new Quiz("Pokémon", tempQuestion));
        }
    }
}
