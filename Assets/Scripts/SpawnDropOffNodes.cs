using UnityEngine;

public class SpawnDropOffNodes : MonoBehaviour
{
    [SerializeField] private GameObject[] dropOffPrefabs;

    private Transform[] _spawnerTransforms;
    private GameObject[] _previousDropOffs;
    private int[] _indices;
    private System.Random _random;

    private void Awake()
    {
        _random = new System.Random();
        
        var spawnerNodes = GameObject.FindGameObjectsWithTag("Spawner");
        _spawnerTransforms = new Transform[spawnerNodes.Length];
        _previousDropOffs = new GameObject[spawnerNodes.Length];
        _indices = new int[spawnerNodes.Length];
        for (var i = 0; i < spawnerNodes.Length; i++)
        {
            _indices[i] = i;
            _spawnerTransforms[i] = spawnerNodes[i].transform;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Respawn();
    }

    public void Respawn()
    {
        foreach (var previousDropOff in _previousDropOffs)
        {
            Destroy(previousDropOff);
        }
        
        ShuffleIndices();

        var i = 0;
        foreach (var index in _indices)
        {
            var spawnerTransform = _spawnerTransforms[index];
            var prefab = dropOffPrefabs[i % dropOffPrefabs.Length];
            _previousDropOffs[index] = Instantiate(prefab, spawnerTransform);
            i += 1;
        }
    }

    private void ShuffleIndices()
    {
        var n = _indices.Length;
        while (n > 1)
        {
            n--;
            var k = _random.Next(n + 1);
            (_indices[k], _indices[n]) = (_indices[n], _indices[k]);
        }
    }
}