using System;

namespace Lib.Movement
{
    public static class Extensions
    {
        public static MoveEnum ParseFrom(this MoveEnum moveEnum, string enumString)
        {
            return (MoveEnum) Enum.Parse(typeof(MoveEnum), enumString, false);
        }
    }
    
    public enum MoveEnum 
    {
        N = 1,//"N",
        S = 2,//"S",
        E = 3,//"E",
        W = 4,//"W",
    
        NE = 5,//"NW",
        NW = 6,//"NW",
        SE = 7,//"SE",
        SW = 8//"SW"
    }
}
