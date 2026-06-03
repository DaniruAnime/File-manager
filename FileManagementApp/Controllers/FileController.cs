using FileManager.Models;
using FileManager.Views;

namespace FileManager.Controllers {
  public class FileController {
    protected List<FileItem> _files;
    protected ConsoleView _view;

    public FileController() {
      _files = new List<FileItem>();
      _view = new ConsoleView();
    }

    public virtual void Run() {
      while (true) {
        _view.ShowFiles(_files);
        _view.ShowMenu([]);
        int choice = _view.GetMenuChoice();

        if (choice == 0) {
          _view.ShowMessage("Goodbye!");
          return;
        }

        _view.ShowError("Unknown command");
      }
    }
  }
}