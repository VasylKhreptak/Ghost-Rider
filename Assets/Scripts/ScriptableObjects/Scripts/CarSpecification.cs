using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CarSpecification", fileName = "CarSpecification")]
public class CarSpecification : ScriptableObject
{
    [Header("Preferences")]
    [SerializeField] private Pools _carPool;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _engineTorque;
    [SerializeField] private float _engineTorqueAtRPM;
    [SerializeField] private float _breakTorque;
    [SerializeField] private float _price;
    
    public Pools CarPool => _carPool;
    public float MaxSpeed => _maxSpeed;
    public float EngineTorque => _engineTorque;
    public float EngineTorqueAtRpm => _engineTorqueAtRPM;
    public float BreakTorque => _breakTorque;
    public float Price => _price;
}
