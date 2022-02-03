using UnityEngine;
[ExecuteInEditMode]
public class Floor : MonoBehaviour
{
    private Vector3 posOffSet = new Vector3(0, -0.84f, 0);

    [Header("Configurable:")] 
    public bool updateSettings = false;
    public bool hasHole = false;
    
#if UNITY_EDITOR
    private void Update()
    {
        if (!updateSettings) return;
        updateSettings = false;
        Vector3 currentPos = transform.localPosition;

        transform.localPosition = new Vector3(currentPos.x, (hasHole ? posOffSet.y : 0), 0);
    }
#endif
}