using SandSimulation.Core;

namespace SandSimulation.PieceTypes;

public class Slope : Piece
{
  public double AccelerationAdder { get; private set; }

  public enum TypeOfSlope
  {
    DownLeft,
    DownRight
  }

  public TypeOfSlope SlopeType { get; private set; }

  public Slope(Slot slot, TypeOfSlope slopeType, double accelerationAdder) : base(slot)
  {
    SlopeType = slopeType;

    if (SlopeType is TypeOfSlope.DownLeft)
    {
      Value = "/";
    }
    else
    {
      Value = @"\";
    }

    AccelerationAdder = accelerationAdder;
  }
}