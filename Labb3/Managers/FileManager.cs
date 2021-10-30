using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb3.Models;
using System.Text.Json;
using System.Windows;
using Labb3.DefaultData;

namespace Labb3.Managers
{
    class FileManager
    {
        //ToDo: Gör export-knappen enkel. Den exporterar quizet med quiztiteln som filnamn med ett .JQuiz-tillägg direkt till skrivbordet.
        private readonly string _directoryPath;
        private readonly string _pathToFile;
        //private List<Quiz> _standardQuizzes;

        public FileManager()
        {
            _directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Labb3ByJonas");
            _pathToFile = Path.Combine(_directoryPath, "Quizzes.json");
        }

        //ToDo: Gör async
        public async Task<List<Quiz>> LoadQuizzesAsync()
        {
            if (!File.Exists(_pathToFile))
                await CreateFileAsync();

            try
            {
                using FileStream fs = new FileStream(_pathToFile, FileMode.Open);
                return await JsonSerializer.DeserializeAsync<List<Quiz>>(fs);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Exception thrown: {e}", "ERROR");
                throw;
            }
        }

        public async Task SaveToFileAsync(List<Quiz> quizzesToSave)
        {
            //using var fileEraser = File.WriteAllText(_pathToFile, string.Empty);

            using var fileEraserTask = File.WriteAllTextAsync(_pathToFile, string.Empty);

            await using FileStream createStream = File.Open(_pathToFile, FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync(createStream, quizzesToSave);
            await createStream.DisposeAsync();
        }
        
        public async Task CreateFileAsync()
        {
            _ = Directory.CreateDirectory(_directoryPath);

            await Task.Run(() =>
            {
                using var fileCreator = File.Open(_pathToFile, FileMode.OpenOrCreate);

                fileCreator.Close();

                using var fileTask = File.WriteAllTextAsync(_pathToFile, DefaultQuiz.DefaultQuizJsonString);
            });
        }

        //private void CreateStandardQuiz()
        //{
        //    //ToDO: Gör ett standardquiz och lägg till i Quizzes.

        //    List<Question> tempQuestion = new();
        //    _standardQuizzes = new();

        //    tempQuestion.Add(new Question("Pokémon", "Select the best pokémon", 1, "", "Charmander", "Oddish", "Squirtle"));
        //    _standardQuizzes.Add(new Quiz("Pokémon", tempQuestion));
        //}
    }
}
