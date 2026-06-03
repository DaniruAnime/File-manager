namespace FileManager.Models {
  public class FileItem(string name, string content, string format) {
    public string Name { get; set; } = name;

    public string Content { get; set; } = content;

    public string Format { get; set; } = format;
  }
}