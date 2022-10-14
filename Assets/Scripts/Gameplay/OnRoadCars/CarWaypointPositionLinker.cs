using UnityEngine;

public class CarWaypointPositionLinker : TransformPositionLinker
{
    #region MonoBehaviour

    private void OnEnable()
    {
        _offset = new Vector3(_transform.position.x, _offset.y, _offset.z);
    }

    #endregion
}
