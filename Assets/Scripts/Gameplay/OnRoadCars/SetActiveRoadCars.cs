using UnityEngine;
public class SetActiveRoadCars : SetActiveTriggerAreaObjects
{
    protected override void SetActiveObject(Transform areaObject, bool enabled)
    {
        areaObject.parent.parent.gameObject.SetActive(enabled);
    }
}
