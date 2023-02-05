using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FactoryTrigger : MonoBehaviour
{
    [SerializeField] private int maxBottles = 5;
    [SerializeField] private GameObject[] customerPrefabs;
    [SerializeField] private Tilemap customersTilemap;
    [SerializeField] private Tilemap groundTilemap;

    private System.Random _random;
    private Vector3[][] _spawnerPositions;
    private GameObject[] _customers;
    private int[] _indices;

    private static readonly int EnumCount = Enum.GetValues(typeof(BottleColor)).Length;

    private void Awake()
    {
        _random = new System.Random();

        PopulateTilemapGroupTransforms(customersTilemap);

        _customers = new GameObject[_spawnerPositions.Length];
        _indices = new int[_spawnerPositions.Length];
        for (var i = 0; i < _spawnerPositions.Length; i++)
        {
            _indices[i] = i;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Car"))
        {
            var carInventory = col.gameObject.GetComponent<CarInventory>();
            if (!carInventory.IsEmpty)
            {
                print("CAR IS NOT EMPTY, STILL MORE TO DELIVER!");
                return;
            }

            print("LOADING CAR");
            for (var i = 0; i < maxBottles; i++)
            {
                var color = (BottleColor)_random.Next(0, EnumCount);
                carInventory.AddBottle(color);
            }

            RespawnCustomers();
        }
    }

    private void PopulateTilemapGroupTransforms(Tilemap tilemap)
    {
        var list = new List<Vector3[]>();

        tilemap.CompressBounds();
        var bounds = tilemap.cellBounds;
        for (var y = bounds.yMin; y < bounds.yMax; y++)
        {
            for (var x = bounds.xMin; x < bounds.xMax; x++)
            {
                if (tilemap.GetTile(new Vector3Int(x, y)) == null) continue;
                if (tilemap.GetTile(new Vector3Int(x, y - 1)) == null)
                {
                    var width = 1;
                    var height = 1;
                    while (tilemap.GetTile(new Vector3Int(x + width, y)) != null) width += 1;
                    while (tilemap.GetTile(new Vector3Int(x, y + height)) != null) height += 1;
                    var innerList = new List<Vector3>();
                    for (var i = -1; i < width + 1; i++)
                    {
                        for (var j = -1; j < height + 1; j++)
                        {
                            if (groundTilemap.GetTile(new Vector3Int(x + i, y + j)) != null)
                            {
                                innerList.Add(new Vector3(x + i + 0.5f, y + j + 0.5f));
                            }
                        }
                    }

                    list.Add(innerList.ToArray());
                }

                // Skip neighbours
                while (tilemap.GetTile(new Vector3Int(x + 1, y)) != null) x += 1;
            }
        }

        _spawnerPositions = list.ToArray();
    }

    private void RespawnCustomers()
    {
        ShuffleIndices();

        var i = 0;
        foreach (var index in _indices)
        {
            var spawnerPositions = _spawnerPositions[index];
            var spawnerIndex = _random.Next(spawnerPositions.Length);
            var prefab = customerPrefabs[i % customerPrefabs.Length];
            Destroy(_customers[index]);
            _customers[index] = Instantiate(prefab, spawnerPositions[spawnerIndex], Quaternion.identity);
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