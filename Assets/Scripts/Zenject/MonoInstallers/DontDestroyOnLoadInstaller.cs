using UnityEngine;
using Zenject;

public class DontDestroyOnLoadInstaller : MonoInstaller
{
    [Header("References")]
    [SerializeField] private GameObject[] _gameObjects;
    
    public override void InstallBindings()
    {

        foreach (var gameObj in _gameObjects)
        {
            GameObject instantiatedObject = GameObjectExtensions.InstantiateDontDestroyOnLoad(gameObj);
        }
    }
}