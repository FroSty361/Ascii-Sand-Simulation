using SandSimulation.PieceTypes;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace SandSimulation.Managers;

public class Manager
{
  public delegate void SimpleEventHandler();

  public int Frames { get; set; }

  public int Speed { get; set; }

  public void Start(int percentOfClearPiecesMin = 50, int percentOfClearPiecesMax = 50, int height = 32, int width = 64)
  {
    GridLayout gridLayout = new GridLayout(percentOfClearPiecesMin, percentOfClearPiecesMax, height, width);

    SimpleEventHandler sandEvent = InitalizeSandPieces();

    InitalizeTeleporters();

    string offset = "";

    StringBuilder builder = new StringBuilder();

    builder.AppendLine("<html><body style='background:black; color:white; font-family:monospace;'><pre>");

    for (int i = 0; i < Frames; i++)
    {
      builder.Clear();

      builder.AppendLine("<html>");
      builder.AppendLine("<head>");
      builder.AppendLine("<meta http-equiv='refresh' content='1' />");
      builder.AppendLine("<style>body { background:black; color:white; font-family:monospace; }</style>");
      builder.AppendLine("</head>");
      builder.AppendLine("<body><pre>");

      for (int y = gridLayout.Height; y > 0; y--)
      {
        for (int x = 1; x <= gridLayout.Width; x++)
        {
          var slot = SlotManager.GetSlotByCoords(x, y);
          
          builder.Append(GetColoredHtml(slot?.Piece)).Append(" ");
        }

        builder.AppendLine();
      }

      builder.AppendLine("</pre></body></html>");

      File.WriteAllText(@"D:\VisualStudioCodeProjects\Projects\Sand\Output\output.html", builder.ToString());

      sandEvent?.Invoke();

      Thread.Sleep(Speed);
    }
  }

  private string GetColoredHtml(Piece piece)
  {
    string value = piece?.Value ?? " ";

    string color = piece switch
    {
        Sand => "orange",
        Solid => "gray",
        Slime => "green",
        Slope s when s.SlopeType == Slope.TypeOfSlope.DownLeft => "blue",
        Slope s when s.SlopeType == Slope.TypeOfSlope.DownRight => "purple",
        Teleporter => "magenta",
        Empty => "black",
        _ => "black"
    };

    return $"<span style='color:{color}'>{value}</span>";
  }

  private void InitalizeTeleporters()
  {
    var teleporters = GridLayout.Pieces.OfType<Teleporter>();

    foreach (Teleporter teleporter in teleporters)
    {
      teleporter.GetCorrespondingTeleporter();
    }
  }

  private SimpleEventHandler InitalizeSandPieces()
  {
    var sandPieces = GridLayout.Pieces.OfType<Sand>();

    SimpleEventHandler sandEvent = null;

    foreach (Sand sand in sandPieces)
    {
      sandEvent += sand.UpdateDownPosition;
    }

    return sandEvent;
  }
}