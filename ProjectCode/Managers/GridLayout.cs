using SandSimulation.Core;
using SandSimulation.PieceTypes;

namespace SandSimulation.Managers;

public class GridLayout
{
  public static List<Piece> Pieces { get; private set; } = new List<Piece>();

  public int Height { get; private set; }

  public int Width { get; private set; }

  public Dictionary<int, Column> Columns { get; private set; } = new Dictionary<int, Column>();

  public GridLayout(int percentOfClearPiecesMin, int percentOfClearPiecesMax, int height = 32, int width = 6)
  {
    Height = height;

    Width = width;

    SetUpColumns(percentOfClearPiecesMin, percentOfClearPiecesMax);
  }

  private void SetUpColumns(int percentOfClearPiecesMin, int percentOfClearPiecesMax)
  {
    Random random = new Random();

    int amountOfPieces = Height;

    for (int i = 1; i < Width; i++)
    {
      int percentOfClearPieces = random.Next(percentOfClearPiecesMin, percentOfClearPiecesMax + 1);

      Column column = new Column(amountOfPieces, i, i + 1, percentOfClearPieces);

      int pieceToLeaveOutIndex = random.Next((int)Column.PiecesForLeavingOut.Slime, (int)Column.PiecesForLeavingOut.SlopeRight + 1);

      // column.AddPieceToLeaveOut((Column.PiecesForLeavingOut)pieceToLeaveOutIndex);

      column.AddPieces();

      Columns.Add(i, column);
    }
  }
}