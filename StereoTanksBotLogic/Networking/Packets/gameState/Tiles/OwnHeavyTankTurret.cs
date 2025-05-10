using StereoTanksBotLogic.Enums;

namespace StereoTanksBotLogic.Models;

/// <summary>
/// Represents turret of an player own heavy tank.
/// </summary>
/// <param name="Direction">Represents turret direction.</param>
/// <param name="BulletCount">Represents number of available bullets.</param>
/// <param name="TicksToBullet">Represents time in ticks to regenerate bullet.</param>
/// <param name="TicksToLaser">Represents time in ticks to regenerate laser.</param>
public record class OwnHeavyTankTurret(Direction Direction, int BulletCount, int? TicksToBullet, int? TicksToLaser);
