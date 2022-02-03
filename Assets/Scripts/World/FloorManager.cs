using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class FloorManager : MonoBehaviour
{
    public GameObject platformWhite;
    private float spaceNextFloor = 3.4f;
    private int lastLenght;
    private List<GameObject> floors = new List<GameObject>();
    [Header("Configurable:")]
    public bool updateSettings;
    public int lenght = 10;
#if UNITY_EDITOR
    private void Update()
    {
        if (!updateSettings) return;
        updateSettings = false;
        if (lenght == 0)
        {
            Floor[] floorsChildren = gameObject.GetComponentsInChildren<Floor>();
            foreach (Floor floor in floorsChildren)
            {
                DestroyImmediate(floor.gameObject);
            }
            floors.Clear();

            lastLenght = lenght;
            return;
        }

        if (lenght > lastLenght)
        {
            for (int i = 0; i < lenght - lastLenght; i++)
            {
                GameObject go = Instantiate(platformWhite, Vector3.zero, Quaternion.identity, transform);
                floors.Add(go);
            }

            RepositionFlors();
        }
        if (lenght < lastLenght)
        {
            int iterator = lastLenght - lenght;

            while (iterator > 0)
            {
                GameObject removed = floors[lenght];
                floors.RemoveAt(lenght);
                DestroyImmediate(removed);
                iterator--;
            }
        }

        lastLenght = lenght;

    }
    void RepositionFlors()
    {
        for (int i = 0; i < floors.Count; i++)
        {
            Vector3 pos = Vector3.zero;
            pos.x = spaceNextFloor * i;
            floors[i].transform.localPosition = pos;
        }
    }
#endif
}