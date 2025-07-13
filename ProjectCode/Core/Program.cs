using SandSimulation.Managers;

namespace SandSimulation.Core;

class Program
{
  static void Main(string[] args)
  {
    Manager manager = new Manager();

    try
    {
      Console.Write("How Many Frames? ");
      
      int frames = Convert.ToInt32(Console.ReadLine());

      manager.Frames = frames;
    }
    catch (Exception)
    {
      Console.WriteLine("Wrong Input. Setting Frams To 100");

      manager.Frames = 100;
    }

    Console.WriteLine();

    try
    {
      Console.Write("How Fast Of A Simulation? ");

      int speed = Convert.ToInt32(Console.ReadLine());

      manager.Speed = speed;
    }
    catch (Exception)
    {
      Console.WriteLine("Wrong Input. Setting Speed To 300");

      manager.Speed = 300;
    }

    Console.WriteLine();

    int height = 32;

    int width = 64;

    int percentOfClearPiecesMin = 50;

    int percentOfClearPiecesMax = 50;

    try
    {
      Console.Write("Height? ");

      height = Convert.ToInt32(Console.ReadLine());

      Console.WriteLine();

      Console.Write("Width? ");

      width = Convert.ToInt32(Console.ReadLine());

      Console.WriteLine();

      Console.Write("Minimum Percentage Of Clear Pieces? ");

      percentOfClearPiecesMin = Convert.ToInt32(Console.ReadLine());

      Console.WriteLine();

      Console.Write("Maximum Percentage Of Clear Pieces? ");

      percentOfClearPiecesMax = Convert.ToInt32(Console.ReadLine());
    }
    catch (Exception)
    {

    }

    if (percentOfClearPiecesMin > percentOfClearPiecesMax)
    {
      int temp = percentOfClearPiecesMin;

      percentOfClearPiecesMin = percentOfClearPiecesMax;

      percentOfClearPiecesMax = temp;
    }

    manager.Start(percentOfClearPiecesMin, percentOfClearPiecesMax, height, width);
  }
}