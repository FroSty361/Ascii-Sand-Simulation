using SandSimulation.Core;

namespace SandSimulation.PieceTypes;

public class Empty : Piece
{
  public Empty(Slot slot) : base(slot)
  {
    Value = " ";
  }
}