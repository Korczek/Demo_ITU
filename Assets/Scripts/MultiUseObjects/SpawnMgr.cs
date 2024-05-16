using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnMgr : MonoBehaviourSingleton<SpawnMgr>
{
    private Transform _parent;
    private readonly List<SpawnableObject> _prefabs = new();
    private readonly List<List<SpawnableObject>> _callables = new();

    public override void Awake()
    {
        base.Awake();
        PrepareCallables(); 
    }
    
    private void PrepareCallables()
    {
        var parent = new GameObject("CallableHolder") { transform = { parent = transform } };
        _parent = parent.transform;

        GetPrefabs();
        SpawnCallables();
    }
    
    private void GetPrefabs()
    {
        var obj = Resources.LoadAll("MultiUseObjects", typeof(GameObject));

        foreach (var o in obj)
        {
            var callable = (GameObject)o;
            if (!callable.GetComponent<SpawnableObject>())
            {
                Debug.LogWarning($"Object {callable.name} is not callable, and won't be prepared");
                continue;
            }

            _prefabs.Add(callable.GetComponent<SpawnableObject>());
        }
    }
    
    private void SpawnCallables()
    {
        foreach (var p in _prefabs)
        {
            var list = new List<SpawnableObject>();
            var n = p.name;
            for (int i = 0; i < p.spawnAmount; i++)
            {
                var obj = Instantiate(p.gameObject, Vector3.zero, Quaternion.identity, _parent)
                    .GetComponent<SpawnableObject>();

                obj.gameObject.SetActive(false);
                obj.name = n;

                list.Add(obj);
            }

            _callables.Add(list);
        }
    }
    
    private void SpawnMoreOf(string cName)
    {
        if (_prefabs.All(p => p.name != cName))
        {
            Debug.LogError($"there is no prefab named {cName}");
            return;
        }

        var list = new List<SpawnableObject>();
        var pref = _prefabs.First(p => p.name == cName);
        var n = pref.name;
        for (int i = 0; i < pref.spawnAmount; i++)
        {
            var obj = Instantiate(pref.gameObject, Vector3.zero, Quaternion.identity, _parent);
            obj.SetActive(false);
            obj.name = n;
            list.Add(obj.GetComponent<SpawnableObject>());
        }

        _callables.First(c => c[0].name == cName).AddRange(list);
    }
    
    
    private SpawnableObject GetCallable(string cName)
    {
        if (_callables.All(c => c[0].name != cName))
        {
            Debug.LogError($"There is no {cName} to spawn! Check name and try again.");
            return null;
        }

        if (!IsThereFreeCallable(cName))
            SpawnMoreOf(cName);

        return GetFreeCallable(cName);
    }

    private SpawnableObject GetFreeCallable(string cName)
        => _callables.First(c => c[0].name == cName).First(c => !c.callableObjectInUse);

    private bool IsThereFreeCallable(string cName)
        => _callables.First(c => c[0].name == cName).Any(c => !c.callableObjectInUse);
    
    public GameObject Spawn(string callableName, Vector3 position, Vector3 rotation, Transform parent)
    {
        var co = GetCallable(callableName);
        co.callableObjectInUse = true;
        co.OnSpawn();

        var toReturn = co.gameObject;
        toReturn.SetActive(true);
        toReturn.transform.position = position;
        toReturn.transform.eulerAngles = rotation;
        toReturn.transform.parent = parent;
        return toReturn;
    }
    
    
    public void Despawn(SpawnableObject objectToDespawn)
    {
        objectToDespawn.transform.parent = _parent;
        objectToDespawn.transform.position = Vector3.zero;
        objectToDespawn.gameObject.SetActive(false);
        objectToDespawn.callableObjectInUse = false;
        objectToDespawn.OnDespawn();
    }
}