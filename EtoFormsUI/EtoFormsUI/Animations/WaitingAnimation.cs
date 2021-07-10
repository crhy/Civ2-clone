using System.Collections.Generic;
using System.Linq;
using Civ2engine;
using Civ2engine.Units;
using Eto.Drawing;
using EtoFormsUI.Animations;

namespace EtoFormsUI.GameModes
{
    public class WaitingAnimation : BaseAnimation
    {
        public WaitingAnimation(Game game, Unit unit, int[] activeXY) : base(BuildAnimations(game, unit,activeXY),2,3, 0.2, activeXY ,game.CurrentMap.Ypx)
        {
            Unit = game.ActiveUnit;
        }

        private static Bitmap[] BuildAnimations(Game game, Unit unit, int[] activeXY)
        {
            var animationFrames = new List<Bitmap>();

            // Get coords of central tile & which squares are to be drawn
            var coordsOffsetsToBeDrawn = new List<int[]>
            {
                new[] {0, -2},
                new[] {-1, -1},
                new[] {1, -1},
                new[] {0, 0},
                new[] {-1, 1},
                new[] {1, 1},
                new[] {0, 2}
            };

            // Get 2 frames (one with and other without the active unit/moving piece)

            var map = game.CurrentMap;
            var unitX = activeXY[0];
            var unitY = activeXY[1];
            for (var frame = 0; frame < 2; frame++)
            {
                var bitmap = new Bitmap(2 * map.Xpx, 3 * map.Ypx, PixelFormat.Format32bppRgba);
                using (var g = new Graphics(bitmap))
                {
                    //Fill bitmap with black (necessary for correct drawing if image is on upper map edge)
                    g.FillRectangle(Brushes.Black, new Rectangle(0, 0, 2 * map.Xpx, 3 * map.Ypx));

                    int[] coordsOffsetsPx;
                    int x;
                    int y;
                    foreach (var coordsOffsets in coordsOffsetsToBeDrawn)
                    {
                        // Change coords of central offset
                        x = unitX + coordsOffsets[0];
                        y = unitY + coordsOffsets[1];
                        coordsOffsetsPx = new[] {coordsOffsets[0] * map.Xpx, coordsOffsets[1] * map.Ypx};

                        if (x < 0 || y < 0 || x >= 2 * map.XDim || y >= map.YDim ||
                            !map.IsTileVisibleC2(x, y, map.WhichCivsMapShown))
                        {
                            continue; // Make sure you're not drawing tiles outside map bounds
                        }

                        // Tiles
                        Draw.Tile(g, x, y, map.Zoom, new Point(coordsOffsetsPx[0], coordsOffsetsPx[1] + map.Ypx));

                        var city = game.CityHere(x, y);
                        if (city == null)
                        {
                            // Units
                            var unitsHere = game.UnitsHere(x, y);
                            if (unitsHere.Count > 0)
                            {
                                // If this is not tile with active unit or viewing piece, draw last unit on stack
                                if (unit == null || x != unitX || y != unitY)
                                {
                                    Draw.Unit(g,
                                        unitsHere.OrderByDescending(u => u.InShip != null ? int.MinValue : u.AttackBase)
                                            .First(), unitsHere.Count > 1, map.Zoom,
                                        new Point(coordsOffsetsPx[0], coordsOffsetsPx[1]));
                                }
                            }
                        }
                        else
                        {
                            // City
                            Draw.City(g, city, true, map.Zoom, new Point(coordsOffsetsPx[0], coordsOffsetsPx[1]));
                        }

                        // Draw active unit if it's not moving
                        if (x != unitX || y != unitY || unit == null) continue;
                        
                        // Draw unit only for 1st frame
                        if (frame == 0)
                            Draw.Unit(g, game.ActiveUnit, game.ActiveUnit.IsInStack, map.Zoom,
                                new Point(coordsOffsetsPx[0], coordsOffsetsPx[1]));
                    }


                    // Gridlines
                    if (game.Options.Grid)
                    {
                        foreach (var coordsOffsets in coordsOffsetsToBeDrawn)
                        {
                            coordsOffsetsPx = new int[] {coordsOffsets[0] * map.Xpx, coordsOffsets[1] * map.Ypx + map.Ypx};
                            Draw.Grid(g, map.Zoom, new Point(coordsOffsetsPx[0], coordsOffsetsPx[1]));
                        }
                    }

                    // City names
                    coordsOffsetsToBeDrawn.Add(new[] {-2, -2});
                    coordsOffsetsToBeDrawn.Add(new[] {2, -2});
                    coordsOffsetsToBeDrawn.Add(new[] {-1, -3});
                    coordsOffsetsToBeDrawn.Add(new[] {1, -3});
                    foreach (var coordsOffsets in coordsOffsetsToBeDrawn)
                    {
                        // Change coords of central offset
                        x = unitX + coordsOffsets[0];
                        y = unitY + coordsOffsets[1];

                        if (x < 0 || y < 0 || x >= 2 * map.XDim || y >= map.YDim)
                            continue; // Make sure you're not drawing tiles outside map bounds

                        var city = game.CityHere(x, y);
                        if (city != null) Draw.CityName(g, city, map.Zoom, new[] {coordsOffsets[0], coordsOffsets[1] + 1});
                    }
                    
                    // View piece (is drawn on top of everything)
                    if (unit == null)
                    {
                        if (frame == 1) Draw.ViewPiece(g, map.Zoom, new Point(0, map.Ypx));
                    }
                }

                animationFrames.Add(bitmap);
            }

            return animationFrames.ToArray();
        }

        public Unit Unit { get; }
        public override float GetXDrawOffset(int mapXpx, int startX)
        {
            return mapXpx * (XY[0] - startX);
        }

        public override int GetYDrawOffset(int mapYpx, int startY)
        {
            return mapYpx * (XY[1] - startY - 1);
        }
    }
}