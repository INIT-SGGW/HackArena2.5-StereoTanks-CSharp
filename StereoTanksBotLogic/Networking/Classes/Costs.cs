namespace StereoTanksBotLogic;

public class Costs(float forward, float backward, float rotate)
{
    public float forward { get; set; } = forward;
    public float backward { get; set; } = backward;
    public float rotate { get; set; } = rotate;
}
