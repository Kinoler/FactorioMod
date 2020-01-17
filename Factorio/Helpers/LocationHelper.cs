using System;
using FactorioMod.Factorio.Models;

namespace FactorioMod.Factorio.Helpers
{
    public static class LocationHelper
    {
        public static Point GetWorldUp(this Pipe pipe) => pipe.Location.Offset(0, 1);
        public static Point GetWorldDown(this Pipe pipe) => pipe.Location.Offset(0, -1);
        public static Point GetWorldRight(this Pipe pipe) => pipe.Location.Offset(1, 0);
        public static Point GetWorldLeft(this Pipe pipe) => pipe.Location.Offset(-1, 0);

        public static DirectionOrientation GetDirectionOrientation(this Pipe pipe) =>
            pipe.Orientation == Orientation.Vertical
                ? (pipe.Direction == Direction.RightOrUp ? DirectionOrientation.Up : DirectionOrientation.Down)
                : (pipe.Direction == Direction.RightOrUp ? DirectionOrientation.Right : DirectionOrientation.Left);

        public static Point GetLocalLeft(this Pipe pipe) =>
            pipe.GetDirectionOrientation() switch
            {
                DirectionOrientation.Up => pipe.GetWorldLeft(),
                DirectionOrientation.Down => pipe.GetWorldRight(),
                DirectionOrientation.Right => pipe.GetWorldUp(),
                DirectionOrientation.Left => pipe.GetWorldDown(),
                _ => pipe.Location,
            };

        public static Point GetLocalRight(this Pipe pipe) =>
            pipe.GetDirectionOrientation() switch
            {
                DirectionOrientation.Up => pipe.GetWorldRight(),
                DirectionOrientation.Down => pipe.GetWorldLeft(),
                DirectionOrientation.Right => pipe.GetWorldDown(),
                DirectionOrientation.Left => pipe.GetWorldUp(),
                _ => pipe.Location,
            };

        public static Point GetLocalForward(this Pipe pipe) =>
            pipe.GetDirectionOrientation() switch
            {
                DirectionOrientation.Up => pipe.GetWorldUp(),
                DirectionOrientation.Down => pipe.GetWorldDown(),
                DirectionOrientation.Right => pipe.GetWorldRight(),
                DirectionOrientation.Left => pipe.GetWorldLeft(),
                _ => pipe.Location,
            };
    }
}