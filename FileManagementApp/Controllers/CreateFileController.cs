using FileManager.Models;
using FileManager.SingletonCreation;

namespace FileManager.Controllers {
  public class CreateFileController : FileController {
    private readonly StorageManager _storage;

    public CreateFileController() {
      _storage = StorageManager.Instance;
    }

    public override void Run() {
      while (true) {
        IReadOnlyList<FileItem> files = _storage.GetActiveFiles();
        _view.ShowFiles(files);

        Console.WriteLine("\n=== FILE MANAGER ===");
        Console.WriteLine("1. Create file");
        Console.WriteLine("0. Exit");
        Console.Write("Choose action: ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out int choice)) {
          choice = -1;
        }

        if (choice == 0) {
          _view.ShowMessage("Goodbye!");
          return;
        }

        if (choice == 1) {
          string name = _view.GetUserInput("Enter file name (without extension): ");
          if (string.IsNullOrWhiteSpace(name)) {
            _view.ShowError("Name cannot be empty.");
            continue;
          }

          string format = _view.GetUserInput("Enter format (txt/json/csv): ");
          if (format is not "txt" and not "json" and not "csv") {
            _view.ShowError("Invalid format.");
            continue;
          }

          string content = _view.GetUserInput("Enter content: ");
          try {
            string fullFileName = name + "." + format;
            _storage.SaveFile(fullFileName, format, content);
            _view.ShowMessage($"File '{fullFileName}' created.");
          }
          catch (Exception ex) {
            _view.ShowError(ex.Message);
          }
        } else {
          _view.ShowError("Invalid choice.");
        }
      }
    }
  }
}