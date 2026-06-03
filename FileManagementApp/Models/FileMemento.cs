namespace FileManager.Models {
  public class FileMemento {
    public string Name { get; }

    public string Content { get; }

    public string Format { get; }

    public FileMemento(string name, string content, string format) {
      Name = name;
      Content = content;
      Format = format;
    }
  }
}