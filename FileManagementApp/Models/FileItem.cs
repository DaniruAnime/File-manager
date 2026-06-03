namespace FileManager.Models {
  public class FileItem {
    public string Name { get; set; }

    public string Content { get; set; }

    public string Format { get; set; }

    public FileItem(string name, string content, string format) {
      Name = name;
      Content = content;
      Format = format;
    }

    public FileMemento SaveState() {
      return new FileMemento(Name, Content, Format);
    }

    public void RestoreState(FileMemento memento) {
      Name = memento.Name;
      Content = memento.Content;
      Format = memento.Format;
    }
  }
}