using System;
using System.Collections.Generic;
using FactorioMod.Factorio.Models;

namespace FactorioMod.Factorio.Temp
{
    public class PipeManager
    {
        public static Dictionary<Point, Pipe> Pipes = new Dictionary<Point, Pipe>();
        public static (Orientation orientation, Direction direction) SelectedPosition { get; set; }

        public void PlacePipe(int x, int y)
        {
            var location = new Point(x, y);
            var pipe = new Pipe(location, SelectedPosition.orientation, SelectedPosition.direction);

            Pipes.Add(location, pipe);


        }

        public void CalculateRotation(Pipe pipe)
        {

        }

        public bool TryToNextPipe(Pipe pipe, Point nextPipeLocation)
        {
            var pipeTile = Pipes[nextPipeLocation];//(PipeTile)main.Tile[nextPipeLocation.X, nextPipeLocation.Y];

            return false;
        }
    }
}