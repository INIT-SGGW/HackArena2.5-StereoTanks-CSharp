using System.Collections.ObjectModel;

namespace StereoTanksBotLogic;

public class Penalties(float blindly, float bullet, float mine, float laser, Collection<PerTile> perTile)
{
    public float blindly { get; set; } = blindly;
    public float bullet { get; set; } = bullet;
    public float mine { get; set; } = mine;
    public float laser { get; set; } = laser;
    public Collection<PerTile> perTile { get; set; } = perTile;
}