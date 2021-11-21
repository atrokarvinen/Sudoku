using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace Sudoku.Services;

public class SudokuFileProvider : ISudokuProvider
{
    public Domain.Sudoku LoadSudoku(string gameFile)
    {
        try
        {
            string text = File.ReadAllText(gameFile);
            return JsonConvert.DeserializeObject<Domain.Sudoku>(text);
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to load sudoku '{gameFile}': {ex.Message}");
        }
    }

    public Domain.Sudoku LoadLatestSudoku(string folder)
    {
        try
        {
            FileInfo[] sudokuFiles = new DirectoryInfo(folder).GetFiles();
            if (sudokuFiles.Length == 0)
            {
                throw new Exception("No saved sudoku files.");
            }

            string gameName = sudokuFiles.OrderByDescending(x => x.LastWriteTime).First().FullName;
            return LoadSudoku(gameName);
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to load latest sudoku: {ex.Message}");
        }
    }

    public void SaveSudoku(Domain.Sudoku sudoku, string path)
    {
        try
        {
            string sudokuFileName = $"Sudoku_{DateTime.Now:yyyy_MM_dd__HH_mm_ss}.txt";
            string savePath = Path.Combine(path, sudokuFileName);
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
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
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
            {
                throw new ArgumentNullException(nameof(path));
            }

            CreateFolder(Path.GetDirectoryName(path));
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to create file '{path}': '{ex.Message}'");
        }
    }

}
