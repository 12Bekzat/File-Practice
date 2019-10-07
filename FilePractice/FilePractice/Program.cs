using System;
using System.Collections.Generic;
using System.IO;

namespace PracticeMode
{
  class Program
  {
    static void DemoPracticeNumberOne()
    {
      var path = string.Empty;

      Console.WriteLine("Выберите диск по номеру: ");
      var drives = DriveInfo.GetDrives();

      for (int i = 0; i < drives.Length; i++)
      {
        if (drives[i].IsReady && drives[i].DriveType == DriveType.Fixed)
        {
          var counter = i + 1;
          Console.WriteLine($"{counter}. {drives[i].Name} - {drives[i].AvailableFreeSpace} байт");
        }
      }

      var driveNumberAsString = Console.ReadLine();
      if (int.TryParse(driveNumberAsString, out var driveUserPosition))
      {
        var driveIndex = driveUserPosition - 1;
        path = drives[driveIndex].Name;

        Console.WriteLine($"\n\nВсе директории: ");

        foreach (var directoryName in Directory.GetDirectories(path))
        {
          Console.WriteLine(directoryName);
        }

        Console.WriteLine("Введите имя новой папки: ");
        var UserdirectoryName = Console.ReadLine();

        path += UserdirectoryName;

        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        if (!directoryInfo.Exists)
          directoryInfo.Create();

        Console.WriteLine("Введите имя нового файла: ");
        var userFileName = Console.ReadLine();

        path += $@"\{userFileName}";

        using (var stream = new FileStream(path, FileMode.OpenOrCreate)) // File.Open - открывает FileStream
        {
          var text = "Какой то текст";
          var bytes = System.Text.Encoding.Default.GetBytes(text);

          stream.Write(bytes, 0, bytes.Length);
        }

        Dictionary<char, int> dict = new Dictionary<char, int>();
        using (var stream = new FileStream(path, FileMode.OpenOrCreate))
        {
          var bytes = new byte[stream.Length];
          stream.Read(bytes, 0, bytes.Length);

          char[] symbols = System.Text.Encoding.Default.GetString(bytes).ToCharArray();

          for (int i = 0; i < symbols.Length; i++)
          {
            if (dict.ContainsKey(symbols[i]))
            {
              dict[symbols[i]]++;
            }
            else
            {
              dict.Add(symbols[i], 1);
            }
          }
        }

        foreach (var item in dict)
        {
          Console.WriteLine(item.Key + " - " + item.Value);
        }
      }
    }


    static void DemoPracticeNumberTwo()
    {
      using (var file = new StreamWriter("Обо мне.txt"))
      {
        string lastName, FirstName, Age;

        Console.WriteLine("Введите имя: ");
        FirstName = Console.ReadLine();

        Console.WriteLine("Введите фамилию: ");
        lastName = Console.ReadLine();

        Console.WriteLine("Введите возраст: ");
        Age = Console.ReadLine();


        file.WriteLine($"Имя: {FirstName}");
        file.WriteLine($"Фамилия: {lastName}");
        file.WriteLine($"Возраст: {Age}");
      }
    }


    static void Main(string[] args)
    {
      DemoPracticeNumberOne();

      Console.ReadLine();
    }
  }
}
