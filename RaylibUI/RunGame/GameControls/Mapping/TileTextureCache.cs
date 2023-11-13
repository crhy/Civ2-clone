using Civ2engine.MapObjects;
using Model.ImageSets;
using Raylib_cs;

namespace RaylibUI.RunGame.GameControls.Mapping;

public class TileTextureCache
{
    private readonly GameScreen _parentScreen;

    private readonly List<Image?[,]> _mapTileTextures = new();

    private readonly List<int> _seenMaps = new List<int>();
    private readonly List<TerrainSet> _tileSets = new List<TerrainSet>();
    private readonly List<MapDimensions> _dimensions = new();

    public TileTextureCache(GameScreen parentScreen)
    {
        _parentScreen = parentScreen;
    }

    public Image GetTextureForTile(Tile tile)
    {
        var mapIndex = _seenMaps.IndexOf(tile.Map.MapIndex);
        if (mapIndex == -1)
        {
            mapIndex = SetupMap(tile.Map);
        }

        _mapTileTextures[mapIndex][tile.XIndex, tile.Y] ??=
            MapImage.MakeTileGraphic(tile, tile.Map, _tileSets[mapIndex], _parentScreen.Game);

        return _mapTileTextures[mapIndex][tile.XIndex, tile.Y]!.Value;
    }

    private int SetupMap(Map map)
    {
        int mapIndex;
        mapIndex = _seenMaps.Count;
        _seenMaps.Add(map.MapIndex);
        _mapTileTextures.Add(new Image?[map.XDim, map.YDim]);

        var tileSet = _parentScreen.Main.ActiveInterface.TileSets[map.MapIndex];
        _tileSets.Add(tileSet);
        _dimensions.Add(new MapDimensions
        {
            TotalWidth = map.Tile.GetLength(0) * tileSet.TileWidth,
            TotalHeight = map.Tile.GetLength(1) * tileSet.HalfHeight + tileSet.HalfHeight,
            HalfHeight = tileSet.HalfHeight,
            TileHeight = tileSet.TileHeight,
            TileWidth = tileSet.TileWidth,
            HalfWidth = tileSet.HalfWidth,
            DiagonalCut = tileSet.DiagonalCut
        });
        return mapIndex;
    }

    public MapDimensions GetDimensions(Map map)
    {
        var mapIndex = _seenMaps.IndexOf(map.MapIndex);
        if (mapIndex == -1)
        {
            mapIndex = SetupMap(map);
        }

        return _dimensions[mapIndex];
    }
}