
using UnityEngine;
 
public enum GameColor
{
    Red,
    Green,
    Blue
}
 
public static class GameColorExtensions
{
    public static Color ToColor(this GameColor c)
    {
        switch (c)
        {
            case GameColor.Red: return Color.red;
            case GameColor.Green: return Color.green;
            default: return Color.blue;
        }
    }
 
    public static GameColor Next(this GameColor c)
    {
        int count = System.Enum.GetValues(typeof(GameColor)).Length;
        return (GameColor)(((int)c + 1) % count);
    }
}
 