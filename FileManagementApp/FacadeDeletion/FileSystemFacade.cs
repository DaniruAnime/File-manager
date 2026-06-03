namespace FileManager.FacadeDeletion {
  public class FileSystemFacade {
    private readonly string _workDirectory;
    private readonly string _recycleBinPath;
    private readonly string _logFilePath;

    public FileSystemFacade() {
      _workDirectory = Directory.GetCurrentDirectory();
      _recycleBinPath = Path.Combine(_workDirectory, "RecycleBin");
      _logFilePath = Path.Combine(_workDirectory, "deletion_log.txt");

      if (!Directory.Exists(_recycleBinPath)) {
        _ = Directory.CreateDirectory(_recycleBinPath);
      }
    }

    public List<string> GetAllFiles() {
      List<string> files = new List<string>();
      string[] extensions = ["*.txt", "*.json", "*.csv"];

      foreach (string ext in extensions) {
        string[] found = Directory.GetFiles(_workDirectory, ext);
        foreach (string file in found) {
          files.Add(Path.GetFileName(file));
        }
      }

      return files;
    }

    public bool FileExists(string fileName) {
      string fullPath = Path.Combine(_workDirectory, fileName);
      return File.Exists(fullPath);
    }

    public void DeleteFile(string fileName) {
      string sourcePath = Path.Combine(_workDirectory, fileName);
      if (!File.Exists(sourcePath)) {
        throw new Exception($"File '{fileName}' not found.");
      }

      string destPath = Path.Combine(_recycleBinPath, fileName);
      if (File.Exists(destPath)) {
        string nameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
        string ext = Path.GetExtension(fileName);
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        destPath = Path.Combine(_recycleBinPath, $"{nameWithoutExt}_{timestamp}{ext}");
      }

      File.Move(sourcePath, destPath);
      LogDeletion(fileName, destPath);
    }

    private void LogDeletion(string originalName, string newPath) {
      string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | Deleted: {originalName} -> Moved to: {newPath}";
      File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
    }
  }
}
