using SandSimulation.Managers;
using SandSimulation.PieceTypes;

namespace SandSimulation.Core;

public class Slot
{
  private static List<Slot> slots = new List<Slot>();

  public Coordinates Coords;

  public Piece Piece;

  public Slot(int x, int y)
  {
    Coords = new Coordinates(x, y);

    SlotManager.AddSlot(this);
  }
}