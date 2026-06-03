using FileManager.FacadeDeletion;
using FileManager.Models;

namespace FileManager.Controllers {
  public class DeleteFileController : FileController {
    private readonly FileSystemFacade _facade;

    public DeleteFileController() {
      _facade = new FileSystemFacade();
    }

    public override void Run() {
      while (true) {
        List<string> fileNames = _facade.GetAllFiles();
        List<FileItem> files = new List<FileItem>();
        foreach (string name in fileNames) {
          string format = Path.GetExtension(name).TrimStart('.');
          files.Add(new FileItem(name, " ", format));
        }

        _view.ShowFiles(files);

        _view.ShowMenu(["Delete file"]);

        int choice = _view.GetMenuChoice();
        if (choice == 0) {
          _view.ShowMessage("Goodbye!");
          return;
        }

        if (choice == 1) {
          string fileName = _view.GetUserInput("Enter file name to delete (with extension): ");
          if (string.IsNullOrWhiteSpace(fileName)) {
            _view.ShowError("File name cannot be empty.");
            continue;
          }

          if (!_facade.FileExists(fileName)) {
            _view.ShowError($"File '{fileName}' not found.");
            continue;
          }

          try {
            _facade.DeleteFile(fileName);
            _view.ShowMessage($"File '{fileName}' deleted and moved to RecycleBin.");
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
