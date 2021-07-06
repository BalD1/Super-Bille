using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject end;
    [SerializeField] private TilesSpawner tilesSpawner;

    private Quaternion rotation;

    private void Start()
    {
        if(tilesSpawner == null)
            tilesSpawner = GameObject.FindGameObjectWithTag("Tray").GetComponent<TilesSpawner>();
    }

    public Vector3 ObjStart
    {
        get => start.transform.position;
    }

    public Vector3 ObjEnd
    {
        get => end.transform.position;
    }

    public void SpawnNew()
    {
        tilesSpawner.SpawnNew();
    }
}

