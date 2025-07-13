using SandSimulation.Core;

namespace SandSimulation.PieceTypes;

public class Solid : Piece
{
  public Solid(Slot slot) : base(slot)
  {
    Value = "#";
  }
}