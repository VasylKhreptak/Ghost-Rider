using UnityEngine;
using Zenject;

public class BankInstaller : MonoInstaller
{
    [Header("References")]
    [SerializeField] private Bank _bank;

    public override void InstallBindings()
    {
        Container.BindInstance(_bank).AsSingle();
    }
}