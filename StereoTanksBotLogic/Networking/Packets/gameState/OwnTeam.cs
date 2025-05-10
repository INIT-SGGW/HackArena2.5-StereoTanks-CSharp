using StereoTanksBotLogic.Models;

namespace StereoTanksBotLogic.JsonConverters;

public record class OwnTeam(
    string Name,
    uint Color,
    int Score,
    List<GamePlayer> Players)
    : GameTeam(
        Name, 
        Color, 
        Players);