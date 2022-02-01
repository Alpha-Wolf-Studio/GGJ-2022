using UnityEngine;
public class Utils : MonoBehaviourSingleton<Utils>
{
    public static Color OniWhite = new Color(245f / 255f, 245f / 255f, 245f / 255f, 1f);
    public static Color OniBlack = new Color(5f / 255f, 5f / 255f, 5f / 255f, 1f);
    public LayerMask playerLayer = default;
    public LayerMask spikeLayer = default;
    public LayerMask interactableLayer = default;
    public bool CheckLayerInMask(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
    public bool CheckLayerInSpike(int layer)
    {
        return spikeLayer == (spikeLayer | (1 << layer));
    }
    public bool CheckLayerInPlayer(int layer)
    {
        return playerLayer == (playerLayer | (1 << layer));
    }
    public bool CheckLayerInInteractable(int layer)
    {
        return interactableLayer == (interactableLayer | (1 << layer));
    }
}