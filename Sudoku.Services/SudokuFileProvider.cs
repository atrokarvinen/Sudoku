using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Services
{
    public class SudokuFileProvider : ISudokuProvider
    {
        private const string _SaveFolder = "Saved sudokus";

        public Domain.Sudoku LoadSudoku(string gameName)
        {
            try
            {
                string savePath = Path.Combine(_SaveFolder, gameName);
                string text = File.ReadAllText(savePath);
                return JsonConvert.DeserializeObject<Domain.Sudoku>(text);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load sudoku '{gameName}': {ex.Message}");
            }
        }

        public Domain.Sudoku LoadLatestSudoku()
        {
            try
            {
                FileInfo[] sudokuFiles = new DirectoryInfo(_SaveFolder).GetFiles();
                if (sudokuFiles.Length == 0)
                    throw new Exception("No saved sudoku files.");

                string gameName = sudokuFiles.OrderByDescending(x => x.LastWriteTime).First().Name;
                return LoadSudoku(gameName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load latest sudoku: {ex.Message}");
            }
        }

        public void SaveSudoku(Domain.Sudoku sudoku)
        {
            try
            {
                string sudokuFileName = $"Sudoku_{DateTime.Now:yyyy_MM_dd__HH_mm_ss}.txt";
                string savePath = Path.Combine(_SaveFolder, sudokuFileName);
                CreateTextFile(savePath);
                File.WriteAllText(savePath, JsonConvert.SerializeObject(sudoku));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save sudoku: {ex.Message}");
            }
        }


        private void CreateFolder(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                    throw new ArgumentNullException(nameof(path));

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create folder '{path}': '{ex.Message}'");
            }
        }

        private void CreateTextFile(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                    throw new ArgumentNullException(nameof(path));

                CreateFolder(Path.GetDirectoryName(path));
                if (!File.Exists(path))
                    File.Create(path).Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create file '{path}': '{ex.Message}'");
            }
        }

    }
}
