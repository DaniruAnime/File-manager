using FileManager.Models;

namespace FileManager.SingletonCreation {
  public sealed class StorageManager {
    private readonly List<FileItem> _activeFiles;
    private readonly string _dbFilePath;

    private StorageManager() {
      _activeFiles = new List<FileItem>();
      _dbFilePath = Path.Combine(Directory.GetCurrentDirectory(), "app_db.txt");
      LoadMetadata();
    }

    public static StorageManager Instance { get; } = new StorageManager();

    public void SaveFile(string fileName, string format, string content) {
      string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
      File.WriteAllText(fullPath, content);

      FileItem newItem = new FileItem(fileName, content, format);
      _activeFiles.Add(newItem);
      AppendMetadata(newItem);
    }

    public IReadOnlyList<FileItem> GetActiveFiles() {
      return _activeFiles.AsReadOnly();
    }

    private void LoadMetadata() {
      if (!File.Exists(_dbFilePath)) {
        return;
      }

      string[] lines = File.ReadAllLines(_dbFilePath);

      foreach (string line in lines) {
        if (string.IsNullOrWhiteSpace(line)) {
          continue;
        }

        string[] parts = line.Split('|');

        if (parts.Length != 2) {
          continue;
        }

        string name = parts[0].Trim();
        string format = parts[1].Trim();
        string fullPath = Path.Combine(Directory.GetCurrentDirectory(), name);

        if (File.Exists(fullPath)) {
          string realContent = File.ReadAllText(fullPath);
          _activeFiles.Add(new FileItem(name, realContent, format));
        }
      }
    }

    private void AppendMetadata(FileItem item) {
      string line = $"{item.Name}|{item.Format}";
      File.AppendAllText(_dbFilePath, line + Environment.NewLine);
    }
  }
}