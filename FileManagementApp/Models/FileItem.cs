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
  }
}