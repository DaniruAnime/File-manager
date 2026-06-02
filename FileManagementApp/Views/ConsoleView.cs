using System;
using System.Collections.Generic;
using FileManager.Models;

namespace FileManager.Views {
  public class ConsoleView {
    public void ShowMenu() {
      string menu =
        "\n=== FILE MANAGER ===\n" +
        "0. Exit\n";

      Console.WriteLine(menu);
    }

    public void ShowFiles(IReadOnlyList<FileItem> files) {
      if (files.Count == 0) {
        Console.WriteLine("\nNo files found.\n");
        return;
      }

      Console.WriteLine("\n--- ACTIVE FILES ---");

      for (int index = 0; index < files.Count; ++index) {
        Console.WriteLine($"{index + 1}. {files[index].Name} ({files[index].Format}) - {files[index].Content.Length} chars");
      }

      Console.WriteLine("--------------------\n");
    }

    public void ShowMessage(string message) {
      Console.WriteLine($"\n{message}\n");
    }

    public void ShowError(string error) {
      Console.WriteLine($"\n[ERROR] {error}\n");
    }

    public string GetUserInput(string prompt) {
      Console.Write(prompt);
      string? input = Console.ReadLine();
      return input ?? "";
    }

    public int GetMenuChoice() {
      string input = GetUserInput("Choose action: ");
      if (int.TryParse(input, out int choice)) {
        return choice;
      }

      return -1;
    }
  }
}