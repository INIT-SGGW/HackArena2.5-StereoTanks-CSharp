namespace StereoTanksBotLogic;

public class PerTile(int x, int y, float penalty)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
    public float Penalty { get; set; } = penalty;
}
