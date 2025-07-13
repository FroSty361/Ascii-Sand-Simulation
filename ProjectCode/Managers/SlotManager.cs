using SandSimulation.Core;

namespace SandSimulation.Managers;

public static class SlotManager
{
  private static List<Slot> slots = new List<Slot>();

  public static void AddSlot(Slot slot)
  {
    slots.Add(slot);
  }

  public static Slot GetSlotByCoords(int x, int y)
  {
    return slots.FirstOrDefault(s => s.Coords.X == x && s.Coords.Y == y);
  }
}
