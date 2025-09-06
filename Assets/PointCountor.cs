using UnityEngine;

public class PointCountor
{
    private static int score;

    public static void AddPoint(int point)
    {
        score += point;
    }

    public static int GetPoint()
    {
        return score;
    }

    public static void SavePoint(int point)
    {
        PlayerPrefs.SetInt("Point", point);
    }

    public static int ReadPoint()
    {
        return PlayerPrefs.GetInt("Point");
    }
}
