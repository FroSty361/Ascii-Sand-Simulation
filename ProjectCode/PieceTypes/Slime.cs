using SandSimulation.Core;

namespace SandSimulation.PieceTypes;

public class Slime : Piece
{
  public Slime(Slot slot) : base(slot)
  {
    Value = "=";
  }
}