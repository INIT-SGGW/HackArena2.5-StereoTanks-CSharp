using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StereoTanksBotLogic.Models;

namespace StereoTanksBotLogic.JsonConverters;

/// <summary>
/// Represents game state json converter.
/// </summary>
internal class GameStateJsonConverter : JsonConverter<GameState>
{
    /// <inheritdoc/>
    public override GameState? ReadJson(JsonReader reader, Type objectType, GameState? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);

        var playerId = jsonObject["playerId"]!.ToObject<string>()!;
        var id = jsonObject["id"]!.ToObject<string>()!;
        var tick = jsonObject["tick"]!.ToObject<int>();
        var rawMap = jsonObject["map"]!;

        List<GameTeam> teams = new();
        foreach (var player in (JArray)jsonObject["teams"]!)
        {
            teams.Add(player.ToObject<GameTeam>()!);
        }

        List<Zone> zones = new();
        foreach (var zone in (JArray)jsonObject["map"]!["zones"]!)
        {
            zones.Add(zone.ToObject<Zone>()!);
        }

        var rawTiles = (JArray)rawMap["tiles"]!;
        int columns = rawTiles.Count;
        int rows = rawTiles[0].Count();
        bool[,] visibility = new bool[columns, rows];
        for (int x = 0; x < columns; x++)
        {
            var columnArray = (JArray)rawTiles[x];

            for (int y = 0; y < columnArray.Count; y++)
            {
                var tile = columnArray[y].ToObject<Tile>()!;
                for (int z = 0; z < tile.Entities.Length; z++)
                {
                    if (tile.Entities[z] is Tile.OwnLightTank lightTank && lightTank.OwnerId == playerId)
                    {
                        visibility = lightTank.Visibility;
                    }
                    else if (tile.Entities[z] is Tile.OwnHeavyTank heavyTank && heavyTank.OwnerId == playerId)
                    {
                        visibility = heavyTank.Visibility;
                    }
                }
            }
        }

        var map = new Tile[rows, rows];
        for (int x = 0; x < columns; x++)
        {
            var columnArray = (JArray)rawTiles[x];

            for (int y = 0; y < columnArray.Count; y++)
            {
                var isVisible = this.IsVisible(visibility, x, y);
                var zoneIndex = this.ComputeZoneIndex(zones, x, y);
                var tile = columnArray[y].ToObject<Tile>()!;
                map[y, x] = new(isVisible, zoneIndex, tile.Entities);
            }
        }

        return new GameState(id, playerId, tick, [.. teams], map, [.. zones]);
    }

    /// <inheritdoc/>
    public override void WriteJson(JsonWriter writer, GameState? value, JsonSerializer serializer)
    {
        throw new NotSupportedException();
    }

    private bool IsVisible(bool[,] visibility, int x, int y)
    {
        return visibility[y, x] == true;
    }

    private int? ComputeZoneIndex(List<Zone> zones, int x, int y)
    {
        foreach (var zone in zones)
        {
            if ((zone.X <= x) && (x < zone.X + zone.Width))
            {
                if ((zone.Y <= y) && (y < zone.Y + zone.Height))
                {
                    return zone.Index;
                }
            }
        }

        return null;
    }
}
