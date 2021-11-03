using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Labb3.Models;
using System.Text.Json;
using System.Windows;
using Microsoft.Win32;

namespace Labb3.Managers
{
    internal class FileManager
    {
        private readonly string _directoryPath;
        private readonly string _pathToFile;
        private readonly string _pathToDesktop;

        public FileManager()
        {
            _directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Labb3ByJonas");
            _pathToFile = Path.Combine(_directoryPath, "Quizzes.json");
            _pathToDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        public async Task<List<Quiz>> LoadQuizzesAsync()
        {
            if (!File.Exists(_pathToFile))
                await CreateFileAsync();

            try
            {
                using var fs = new FileStream(_pathToFile, FileMode.Open);
                return await JsonSerializer.DeserializeAsync<List<Quiz>>(fs);
            }
            catch (Exception e)
            {
                _ = MessageBox.Show($"Exception thrown: {e}", "ERROR");
                throw;
            }
        }

        public async Task SaveToFileAsync(List<Quiz> quizzesToSave)
        {
            await using var createStream = File.Open(_pathToFile, FileMode.Create);
            await JsonSerializer.SerializeAsync(createStream, quizzesToSave);
            await createStream.DisposeAsync();
        }
        
        public async Task CreateFileAsync()
        {
            _ = Directory.CreateDirectory(_directoryPath);

            await Task.Run(() =>
            {
                using var fileCreator = File.Open(_pathToFile, FileMode.Create);

                fileCreator.Close();

                using var fileTask = File.WriteAllTextAsync(_pathToFile, DefaultData.DefaultData.DefaultQuizJsonString);
            });
        }

        public async Task ExportQuizAsync(Quiz quiz)
        {
            string tempQuizTitle = $"{quiz.Title}.JQuiz";
            string pathToQuizFile = Path.Combine(_pathToDesktop, tempQuizTitle);
            int tempTitleAddition = 2;

            while (File.Exists(pathToQuizFile))
            {
                pathToQuizFile = Path.Combine(_pathToDesktop, $"{quiz.Title}({tempTitleAddition}).JQuiz");
                tempTitleAddition++;
            }

            await using var fileCreator = File.Open(pathToQuizFile, FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync(fileCreator, quiz);
            await fileCreator.DisposeAsync();

            _ = MessageBox.Show("The quiz has been successfully exported to your desktop and can now be shared with your friends by floppy. " +
                                "Remember that locally stored images has to be shared separately.", "QUIZ SUCCESSFULLY EXPORTED");
        }

        public async Task<Quiz> ImportQuizAsync()
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Quizzes (*.JQuiz)|*.JQuiz"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                var fileName = openFileDialog.FileName;

                try
                {
                    await using FileStream fs = new FileStream(fileName, FileMode.Open);
                    return await JsonSerializer.DeserializeAsync<Quiz>(fs);
                }
                catch(Exception e)
                {
                    _ = MessageBox.Show($"The selected file is invalid\n\nException thrown:\n {e}.", "INVALID FILE");
                }
            }
            return null;
        }
    }
}
