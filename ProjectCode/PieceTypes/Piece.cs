using SandSimulation.Core;

namespace SandSimulation.PieceTypes;

public class Piece
{
  public enum NextSlotDirection
  {
    Up,
    Down,
    Left,
    Right,
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight
  }

  public Slot Slot;

  public string Value { get; protected set; }

  public Piece(Slot slot)
  {
    Slot = slot;
  }
}