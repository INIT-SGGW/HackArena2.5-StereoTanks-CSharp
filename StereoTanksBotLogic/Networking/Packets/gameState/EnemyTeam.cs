using StereoTanksBotLogic.Models;

namespace StereoTanksBotLogic.JsonConverters;

public record class EnemyTeam(
    string Name,
    uint Color,
    List<GamePlayer> Players) : GameTeam(Name, Color, Players);