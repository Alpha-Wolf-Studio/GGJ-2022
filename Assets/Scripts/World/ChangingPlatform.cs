using UnityEngine;

public class ChangingPlatform : MonoBehaviour, IObstacle
{

    [SerializeField] GameObject YingGameobject;
    [SerializeField] GameObject YangGameobject;

    public void Activate()
    {
        YingGameobject.SetActive(true);
        YangGameobject.SetActive(false);
    }

    public void Disactivate()
    {
        YingGameobject.SetActive(false);
        YangGameobject.SetActive(true);
    }
}
