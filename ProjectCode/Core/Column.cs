using SandSimulation.Managers;
using SandSimulation.PieceTypes;

namespace SandSimulation.Core;

public class Column
{
  public enum PiecesForLeavingOut
  {
    Solid,
    Slime,
    Teleporter,
    SlopeLeft,
    SlopeRight
  }

  public HashSet<PiecesForLeavingOut> PiecesToLeaveOut { get; private set; } = new HashSet<PiecesForLeavingOut>();

  public List<Piece> Pieces { get; private set; }

  public int AmountOfPieces { get; private set; }

  private int percentOfClearPieces;

  public int ID { get; private set; }

  public int XValue { get; private set; }

  public int PercentOfClearPieces
  {
    get { return percentOfClearPieces; }
    set
    {
      if (value <= 5)
      {
        value = 6;
      }

      percentOfClearPieces = value;
    }
  }

  public Column(int amountOfPieces, int id, int xValue, int percentOfClearPieces)
  {
    AmountOfPieces = amountOfPieces;

    ID = id;

    XValue = xValue;

    PercentOfClearPieces = percentOfClearPieces;
  }

  public void AddPieceToLeaveOut(PiecesForLeavingOut pieceToLeaveOut)
  {
    PiecesToLeaveOut.Add(pieceToLeaveOut);
  }

  public void RemovePieceToLeaveOut(PiecesForLeavingOut pieceToLeaveOut)
  {
    PiecesToLeaveOut.Remove(pieceToLeaveOut);
  }

  public void AddPieces()
  {
    Pieces = new List<Piece>();

    Random random = new Random();

    for (int i = AmountOfPieces; i > 0; i--)
    {
      int roll = random.Next(1, 101);

      Slot slot = new Slot(XValue, i);

      Piece piece;

      if (roll <= PercentOfClearPieces)
      {
        piece = new Empty(slot);
      }
      else if (roll <= PercentOfClearPieces + 10)
      {
        piece = new Sand(slot, 4, random.Next(1, 4));
      }
      else if (roll <= PercentOfClearPieces + 20)
      {
        if (PiecesToLeaveOut.Contains(PiecesForLeavingOut.Solid))
        {
          piece = new Empty(slot);
        }
        else
        {
          piece = new Solid(slot);
        }
      }
      else if (roll <= PercentOfClearPieces + 25)
      {
        if (PiecesToLeaveOut.Contains(PiecesForLeavingOut.Slime))
        {
          piece = new Empty(slot);
        }
        else
        {
          piece = new Slime(slot);
        }
      }
      else if (roll <= PercentOfClearPieces + 40)
      {
        if (PiecesToLeaveOut.Contains(PiecesForLeavingOut.SlopeLeft) && PiecesToLeaveOut.Contains(PiecesForLeavingOut.SlopeRight))
        {
          piece = new Empty(slot);
        }
        else
        {
          if (PiecesToLeaveOut.Contains(PiecesForLeavingOut.SlopeLeft))
          {
            piece = new Slope(slot, Slope.TypeOfSlope.DownRight, 1.50);
          }
          else if (PiecesToLeaveOut.Contains(PiecesForLeavingOut.SlopeRight))
          {
            piece = new Slope(slot, Slope.TypeOfSlope.DownLeft, 1.50);
          }
          else
          {
            int chanceNumber = random.Next((int)Slope.TypeOfSlope.DownLeft, (int)Slope.TypeOfSlope.DownRight + 1);

            Slope.TypeOfSlope typeOfSlope = (Slope.TypeOfSlope)chanceNumber;

            piece = new Slope(slot, typeOfSlope, 1.50);
          }
        }
      }
      else
      {
        if (PiecesToLeaveOut.Contains(PiecesForLeavingOut.Teleporter))
        {
          piece = new Empty(slot);
        }
        else
        {
          piece = new Teleporter(slot, i, i + 1);
        }
      }

      slot.Piece = piece;

      Pieces.Add(piece);

      GridLayout.Pieces.Add(piece);

      SlotManager.AddSlot(piece.Slot);
    }
  }
}