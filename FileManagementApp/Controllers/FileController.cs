using FileManager.Models;
using FileManager.Views;

namespace FileManager.Controllers {
  public class FileController {
    private readonly HistoryCaretaker _caretaker = new HistoryCaretaker();
    protected ConsoleView _view;

    public FileController() {
      _view = new ConsoleView();
    }

    public virtual void Run() {
      while (true) {
        _view.ShowMenu(["Create file", "Delete file", "Restore file"]);
        int choice = _view.GetMenuChoice();

        if (choice == 0) {
          _view.ShowMessage("Goodbye!");
          return;
        }

        switch (choice) {
          case 1:
            _view.ShowMessage("The file creation feature is not implemented");
            break;

          case 2:
            _view.ShowMessage("The delete file feature is not implemented");
            break;

          case 3:
            new RestoreFileController(_caretaker).Run();
            break;

          default:
            _view.ShowError("Invalid choice.");
            break;
        }
      }
    }
  }
}