using FileManager.Controllers;

namespace FileManager {
  public class Program {
    private static void Main() {
      FileController controller = new FileController();
      controller.Run();
    }
  }
}