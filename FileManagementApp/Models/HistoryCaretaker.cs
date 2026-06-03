namespace FileManager.Models {
  public class HistoryCaretaker {
    private readonly Stack<FileMemento> _history = new Stack<FileMemento>();

    public void Push(FileMemento memento) {
      _history.Push(memento);
    }

    public FileMemento? Pop() {
      if (_history.Count > 0) {
        return _history.Pop();
      } else {
        return null;
      }
    }
  }
}