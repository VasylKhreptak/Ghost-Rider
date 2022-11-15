using UnityEngine;
using Zenject;

public class ScoreCounterInstaller : MonoInstaller
{
    [Header("References")]
    [SerializeField] private ScoreCounter _scoreCounter;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_scoreCounter).AsSingle();
    }
}