using UnityEngine;

public class CarWaypointPositionLinker : TransformPositionLinker
{
    #region MonoBehaviour

    private void OnEnable()
    {
        _offset = new Vector3(_linkTo.position.x, _offset.y, _offset.z);
    }

    #endregion
}
