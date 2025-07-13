using SandSimulation.Core;

namespace SandSimulation.PieceTypes;

public class Teleporter : Piece
{
  private static List<Teleporter> teleporters = new List<Teleporter>();

  public Teleporter CorrespondingTeleporter { get; private set; }

  public int ID { get; private set; }

  public int CorrespondingTeleporterID { get; private set; }

  public Teleporter(Slot slot, int id, int correspondingTeleporterID) : base(slot)
  {
    Value = "O";

    teleporters.Add(this);

    ID = id;

    CorrespondingTeleporterID = correspondingTeleporterID;
  }

  public void GetCorrespondingTeleporter()
  {
    if (ID == 0)
    {
      return;
    }

    CorrespondingTeleporter = teleporters.FirstOrDefault(t => t.ID == CorrespondingTeleporterID);
  }
}