using FileManager.FacadeDeletion;
using FileManager.Models;

namespace FileManager.Controllers {
  public class DeleteFileController : FileController {
    private readonly FileSystemFacade _facade;
    private readonly HistoryCaretaker _caretaker;

    public DeleteFileController(HistoryCaretaker caretaker) {
      _facade = new FileSystemFacade();
      _caretaker = caretaker;
    }

    public override void Run() {
      while (true) {
        List<FileItem> files = _facade.GetAllFiles();
        _view.ShowFiles(files);
        _view.ShowMenu(["Delete file"]);
        int choice = _view.GetMenuChoice();

        if (choice == 0) {
          return;
        }

        if (choice == 1) {
          HandleDelete();
        } else {
          _view.ShowError("Invalid choice.");
        }
      }
    }

    private void HandleDelete() {
      string fileName = _view.GetUserInput("Enter file name to delete (with extension): ");

      if (string.IsNullOrWhiteSpace(fileName)) {
        _view.ShowError("File name cannot be empty.");
        return;
      }

      if (!_facade.FileExists(fileName)) {
        _view.ShowError($"File '{fileName}' not found.");
        return;
      }

      try {
        // Поиск удаляемого файла среди всех активных файлов на диске
        List<FileItem> allFiles = _facade.GetAllFiles();
        FileItem? fileToSnapshot = allFiles.Find(file => file.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase));

        // Создаётся снимок текущего состояния файла и отправляется в стек истории перед физическим удалением
        if (fileToSnapshot != null) {
          FileMemento snapshot = new FileMemento(fileToSnapshot.Name, fileToSnapshot.Content, fileToSnapshot.Format);
          _caretaker.Push(snapshot);
        }

        _facade.DeleteFile(fileName);
        _view.ShowMessage($"File '{fileName}' deleted and moved to RecycleBin.");
      }
      catch (Exception ex) {
        _view.ShowError(ex.Message);
      }
    }
  }
}