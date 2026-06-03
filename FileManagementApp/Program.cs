using FileManager.Controllers;

namespace FileManager {
  public class Program {
    private static void Main() {
      CreateFileController controller = new CreateFileController();
      controller.Run();
    }
  }
}