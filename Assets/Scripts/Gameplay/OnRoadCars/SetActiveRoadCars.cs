using UnityEngine;
public class SetActiveRoadCars : SetActiveTriggerAreaObjects
{
    protected override void SetActiveObject(Collider collider, bool enabled)
    {
        collider.transform.parent.parent.gameObject.SetActive(enabled);
    }
}
