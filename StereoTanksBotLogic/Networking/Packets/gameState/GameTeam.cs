using Newtonsoft.Json;
using StereoTanksBotLogic.Models;

namespace StereoTanksBotLogic.JsonConverters;

[JsonConverter(typeof(GameTeamJsonConverter))]
public abstract record class GameTeam(
    string Name,
    uint Color,
    List<GamePlayer> Players);