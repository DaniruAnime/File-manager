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
        return;
      }

      try {
        // Создаётся чистый объект FileItem на основе архивного снимка Memento
        FileItem restoredFile = new FileItem(lastDeletedSnapshot.Name, lastDeletedSnapshot.Content, lastDeletedSnapshot.Format);

        // Возвращается файл в корень проекта через Singleton
        _storage.SaveFile(restoredFile.Name, restoredFile.Format, restoredFile.Content);

        string recycleBinPath = Path.Combine(Directory.GetCurrentDirectory(), "RecycleBin", restoredFile.Name);

        // Удаление файла из папки RecycleBin
        if (File.Exists(recycleBinPath)) {
          File.Delete(recycleBinPath);
        }

        _view.ShowMessage($"File '{restoredFile.Name}' successfully restored from history.");
      }
      catch (Exception ex) {
        _view.ShowError($"Failed to restore file: {ex.Message}");
      }
    }
  }
}