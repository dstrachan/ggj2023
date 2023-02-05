using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FactoryPointer : MonoBehaviour
{
    public Collider2D factoryCollider;
    public Tilemap factoryMap;
    public Transform carLocation;
    public float maxDist = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        CarInventory.OnBottlesChanged += DisplayArrow;
        factoryMap.CompressBounds();       
    }

    // Update is called once per frame
    void Update()
    {
        var facLoc = new Vector2(factoryMap.cellBounds.center.x, factoryMap.cellBounds.center.y);
        var carLoc = new Vector2(carLocation.position.x, carLocation.position.y);
        // var facVec = factoryCollider.transform.position;

        var factoryDirection = (facLoc - carLoc);
        var pointDist = Mathf.Min(maxDist, factoryDirection.magnitude / 2.0f);

        transform.position = carLoc + pointDist * factoryDirection.normalized;
        transform.up = facLoc - new Vector2(transform.position.x, transform.position.y);
        transform.position += transform.up * 0.15f * Mathf.Sin(10.0f * Time.time);
    }

    private void DisplayArrow(List<Bottle> bottles)
    {
        var renderers = GetComponentsInChildren<Renderer>();
        foreach(var renderer in renderers)
        {
            renderer.enabled = (bottles.Count == 0.0);
        }
    }

    private void OnDestroy()
    {
        CarInventory.OnBottlesChanged -= DisplayArrow;
    }
}
