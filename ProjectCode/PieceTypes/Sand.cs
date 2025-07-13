using System.Linq.Expressions;
using SandSimulation.Core;
using SandSimulation.Managers;

namespace SandSimulation.PieceTypes;

public class Sand : Piece
{
  private int jumpBoost;

  private double jumpBoostFallMultiplier = 1;

  private int fallSpeed = 1;

  private double fallSpeedMultiplier = 0;

  public int TotalFallAmount { get; private set; } = 0;

  public int FallAmountSinceAContact { get; private set; } = 0;

  public Sand(Slot slot, int _jumpBoost, int speed) : base(slot)
  {
    Value = "*";

    fallSpeed = speed;

    jumpBoost = _jumpBoost;
  }

  public void UpdateDownPosition()
  {
    Slot nextSlot = GetNextSlot(NextSlotDirection.Down, 0, 1);

    if (nextSlot is null)
    {
      return;
    }

    switch (nextSlot.Piece)
    {
      case Empty:
        Fall();
        break;
      case Slime:
        BounceUp();
        FallAmountSinceAContact = 0;
        break;
      case Slope:
        SlopeDown((Slope)nextSlot.Piece);
        FallAmountSinceAContact = 0;
        break;
      case Teleporter:
        Teleport((Teleporter)nextSlot.Piece);
        break;
    }
  }

  private Slot? GetNextSlot(NextSlotDirection nextSlotDirection, int amountX, int amountY)
  {
    switch (nextSlotDirection)
    {
      case NextSlotDirection.Up:
        return SlotManager.GetSlotByCoords(Slot.Coords.X, Slot.Coords.Y + amountY);
      case NextSlotDirection.Down:
        return SlotManager.GetSlotByCoords(Slot.Coords.X, Slot.Coords.Y - amountY);
      case NextSlotDirection.Left:
        return SlotManager.GetSlotByCoords(Slot.Coords.X - amountX, Slot.Coords.Y);
      case NextSlotDirection.Right:
        return SlotManager.GetSlotByCoords(Slot.Coords.X + amountX, Slot.Coords.Y);
      case NextSlotDirection.TopLeft:
        return SlotManager.GetSlotByCoords(Slot.Coords.X - amountX, Slot.Coords.Y + amountY);
      case NextSlotDirection.TopRight:
        return SlotManager.GetSlotByCoords(Slot.Coords.X + amountX, Slot.Coords.Y + amountY);
      case NextSlotDirection.BottomLeft:
        return SlotManager.GetSlotByCoords(Slot.Coords.X - amountX, Slot.Coords.Y - amountY);
      case NextSlotDirection.BottomRight:
        return SlotManager.GetSlotByCoords(Slot.Coords.X + amountX, Slot.Coords.Y - amountY);
      default:
        return null;
    }
  }

  private void Fall()
  {
    int completeFallSpeed = Math.Abs((int)Math.Ceiling(fallSpeed * fallSpeedMultiplier));

    if (completeFallSpeed == 0)
    {
      fallSpeedMultiplier += 0.10;

      return;
    }

    Slot nextSlot = GetNextSlot(NextSlotDirection.Down, 0, completeFallSpeed);

    for (int i = Slot.Coords.Y - 1; i >= Slot.Coords.Y - completeFallSpeed; i--)
    {
      Slot potentialSlot = SlotManager.GetSlotByCoords(Slot.Coords.X, i);

      if (potentialSlot == null)
      {
        nextSlot = SlotManager.GetSlotByCoords(Slot.Coords.X, Slot.Coords.Y + 1);

        continue;
      }

      if (potentialSlot.Piece is not Empty)
      {
        nextSlot = SlotManager.GetSlotByCoords(Slot.Coords.X, potentialSlot.Coords.Y + 1);

        break;
      }
    }

    Slot lastSlot = Slot;

    Slot = nextSlot;

    Slot.Piece = this;

    lastSlot.Piece = new Empty(lastSlot);

    TotalFallAmount++;

    FallAmountSinceAContact++;
  }

  private void BounceUp()
  {
    int offset = 1;

    Slot upSlot = GetNextSlot(NextSlotDirection.Up, 0, (int)Math.Ceiling((jumpBoost - offset) / jumpBoostFallMultiplier));

    if (upSlot is null)
    {
      return;
    }

    if (jumpBoost <= 1)
    {
      return;
    }

    for (int i = Slot.Coords.Y + 1; i < Slot.Coords.Y + 1 + jumpBoost - offset; i++)
    {
      Slot potentialSlot = SlotManager.GetSlotByCoords(Slot.Coords.X, i);

      if (potentialSlot == null || potentialSlot.Piece is not Empty)
      {
        int extraOffset = i - Slot.Coords.Y;

        upSlot = GetNextSlot(NextSlotDirection.Up, 0, (int)Math.Ceiling((jumpBoost - offset - extraOffset) / jumpBoostFallMultiplier));
      }
    }

    if (upSlot.Piece is not Solid)
    {
      jumpBoost--;

      jumpBoostFallMultiplier += 0.50;

      Slot lastSlot = Slot;

      Slot = upSlot;

      Slot.Piece = this;

      lastSlot.Piece = new Empty(lastSlot);
    }
  }

  private void SlopeDown(Slope slope)
  {
    Slot nextSlot;

    if (slope.SlopeType is Slope.TypeOfSlope.DownLeft)
    {
      nextSlot = GetNextSlot(NextSlotDirection.BottomLeft, 1, 1);
    }
    else if (slope.SlopeType is Slope.TypeOfSlope.DownRight)
    {
      nextSlot = GetNextSlot(NextSlotDirection.BottomRight, 1, 1);
    }
    else
    {
      return;
    }

    if (nextSlot is null)
    {
      return;
    }

    if (nextSlot.Piece is Empty)
    {
      Slot lastSlot = Slot;

      Slot = nextSlot;

      Slot.Piece = this;

      lastSlot.Piece = new Empty(lastSlot);

      fallSpeedMultiplier = slope.AccelerationAdder;
    }
  }

  private void Teleport(Teleporter teleporter)
  {
    Slot lastSlot = Slot;

    if (teleporter.CorrespondingTeleporter is null)
    {
      teleporter.GetCorrespondingTeleporter();
    }

    try
    {
      Slot = SlotManager.GetSlotByCoords(teleporter.CorrespondingTeleporter.Slot.Coords.X, teleporter.CorrespondingTeleporter.Slot.Coords.Y - 1);

      Slot.Piece = this;

      lastSlot.Piece = new Empty(lastSlot);

      fallSpeedMultiplier += 0.15;
    }
    catch (Exception)
    {
      var newTeleporter = teleporter as Piece;

      newTeleporter = new Empty(teleporter.Slot);
    }
  }
}