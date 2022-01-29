using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static Color OniWhite = new Color(245f / 255f, 245f / 255f, 245f / 255f, 1f);
    public static Color OniBlack = new Color(5f / 255f, 5f / 255f, 5f / 255f, 1f);
    public static bool CheckLayerInMask(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
}
