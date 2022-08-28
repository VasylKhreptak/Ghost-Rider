using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public sealed class ObjectPooler : MonoBehaviour
{
    [Serializable]
    private class Pool
    {
        public Pools poolType;
        public GameObject prefab;
        public int size;
        [HideInInspector] public GameObject folder;
    }

    [Header("Pool")]
    private Dictionary<Pools, Queue<GameObject>> _poolDictionary;
    [SerializeField] private List<Pool> _pools;

    private DiContainer _diContainer;

    [Inject]
    private void Construct(DiContainer container)
    {
        _diContainer = container;
    }

    #region MonoBehaviour

    private void Awake()
    {
        Init();
    }
   
    private void Init()
    {
        CreatePoolFolders();

        FillPool();
    }

    private void OnEnable()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    #endregion

    private void CreatePoolFolders()
    {
        foreach (var pool in _pools)
        {
            pool.folder = new GameObject(pool.poolType.ToString());
            pool.folder.transform.parent = gameObject.transform;
        }
    }

    private void FillPool()
    {
        _poolDictionary = new Dictionary<Pools, Queue<GameObject>>();

        for (var i = 0; i < _pools.Count; i++)
        {
            var objectPool = new Queue<GameObject>();

            for (var j = 0; j < _pools[i].size; j++)
            {
                GameObject obj = _diContainer.InstantiatePrefab(_pools[i].prefab);
                obj.SetActive(false);

                obj.transform.SetParent(_pools[i].folder.transform);

                objectPool.Enqueue(obj);
            }

            _poolDictionary.Add(_pools[i].poolType, objectPool);
        }
    }

    public GameObject Spawn(Pools pool, Vector3 position, Quaternion rotation)
    {
        if (_poolDictionary.ContainsKey(pool) == false)
        {
            Debug.LogWarning("Pool with name " + pool + "doesn't exist");
            return null;
        }

        GameObject objectFromPool = _poolDictionary[pool].Dequeue();

        if (objectFromPool.activeSelf)
        {
            objectFromPool.SetActive(false);
        }

        objectFromPool.transform.position = position;
        objectFromPool.transform.rotation = rotation;


        objectFromPool.SetActive(true);

        _poolDictionary[pool].Enqueue(objectFromPool);

        return objectFromPool;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        DisableAllObjects();
    }

    private void DisableAllObjects()
    {
        foreach (Transform poolFolder in transform)
        {
            foreach (Transform poolObjTransform in poolFolder)
            {
                poolObjTransform.gameObject.SetActive(false);
            }
        }
    }
}
