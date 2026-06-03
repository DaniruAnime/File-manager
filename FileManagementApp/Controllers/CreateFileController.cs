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
        _view.ShowMenu(["Create file"]);

        int choice = _view.GetMenuChoice();

        if (choice == 0) {
          return;
        }

        if (choice == 1) {
          HandleCreate();
        } else {
          _view.ShowError("Invalid choice.");
        }
      }
    }

    private void HandleCreate() {
      string name = _view.GetUserInput("Enter file name (without extension): ");

      if (string.IsNullOrWhiteSpace(name)) {
        _view.ShowError("Name cannot be empty.");
        return;
      }

      string format = _view.GetUserInput("Enter format (txt/json/csv): ");

      if (format is not "txt" and not "json" and not "csv") {
        _view.ShowError("Invalid format.");
        return;
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
    }
  }
}