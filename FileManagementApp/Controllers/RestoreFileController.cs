using FileManager.Models;
using FileManager.SingletonCreation;

namespace FileManager.Controllers {
  public class RestoreFileController : FileController {
    private readonly StorageManager _storage;
    private readonly HistoryCaretaker _caretaker;

    public RestoreFileController(HistoryCaretaker caretaker) {
      _storage = StorageManager.Instance;
      _caretaker = caretaker;
    }

    public override void Run() {
      FileMemento? lastDeletedSnapshot = _caretaker.Pop();

      if (lastDeletedSnapshot == null) {
        _view.ShowError("No files found in the recovery history.");
      }

      return;
    }

    try {
      FileItem restoredFile = new FileItem(lastDeletedSnapshot.Name, lastDeletedSnapshot.Content, lastDeletedSnapshot.Format);

      _storage.SaveFile(restoredFile.Name, restoredFile.Format, restoredFile.Content);
      _view.ShowMessage($"File '{restoredFile.Name}' successfully restored from history.")
    }
    catch (Exception ex) {
        _view.ShowError($"Failed to restore file: {ex.Message}");
    }
  }
}