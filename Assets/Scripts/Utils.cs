using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static bool CheckLayerInMask(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
}
