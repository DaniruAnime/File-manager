using FileManager.Controllers;

namespace FileManager {
  public class Program {
    private static void Main() {
      DeleteFileController controller = new DeleteFileController();
      controller.Run();
    }
  }
}