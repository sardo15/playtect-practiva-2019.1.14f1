using UnityEngine;

public static class ColorHex
{
    public static string GetStringFromColor(Color color)
    {
        var red = FloatNormalizedToHex(color.r);
        var green = FloatNormalizedToHex(color.g);
        var blue = FloatNormalizedToHex(color.b);

        return red + green + blue;
    }

    private static string DecToHex(int value)
    {
        return value.ToString("X2");
    }

    private static string FloatNormalizedToHex(float value)
    {
        return DecToHex(Mathf.RoundToInt(value * 255f));
    }
}