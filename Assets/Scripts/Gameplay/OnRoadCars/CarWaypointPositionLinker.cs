using UnityEngine;

public class CarWaypointPositionLinker : TransformPositionLinker
{
    #region MonoBehaviour

    private void OnEnable()
    {
        offset = new Vector3(linkTo.position.x, offset.y, offset.z);
    }

    #endregion
}
