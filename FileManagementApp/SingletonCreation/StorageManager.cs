using System.Text.Json;
using FileManager.Models;

namespace FileManager.SingletonCreation {
  public sealed class StorageManager {
    private readonly List<FileItem> _activeFiles;

    private StorageManager() {
      _activeFiles = new List<FileItem>();
    }

    public static StorageManager Instance { get; } = new StorageManager();

    public void SaveFile(string fileName, string format, string content) {
      string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
      if (format == "json") {
        string json = JsonSerializer.Serialize(new { Content = content });
        File.WriteAllText(fullPath, json);
      } else {
        File.WriteAllText(fullPath, content);
      }

      _activeFiles.Add(new FileItem(fileName, content, format));
    }

    public IReadOnlyList<FileItem> GetActiveFiles() {
      return _activeFiles.AsReadOnly();
    }
  }
}