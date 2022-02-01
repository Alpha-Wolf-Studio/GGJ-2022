using UnityEngine;

public class ChangingPlatform : Obstacle
{

    [SerializeField] GameObject YingGameobject;
    [SerializeField] GameObject YangGameobject;

    public override void Activate()
    {
        YingGameobject.SetActive(true);
        YangGameobject.SetActive(false);
    }

    public override void Disactivate()
    {
        YingGameobject.SetActive(false);
        YangGameobject.SetActive(true);
    }
}
