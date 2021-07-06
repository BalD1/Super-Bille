using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesSpawner : MonoBehaviour
{
    [SerializeField] private int tilesToCross = 5;

    private List<GameObject> originalTilesList;
    private List<GameObject> tiles;
    private GameObject newTile;

    private Vector3 spawnPos;

    private void Start()
    {
        originalTilesList = new List<GameObject>();
        tiles = new List<GameObject>();

        foreach(Transform child in this.transform)
        {
            if (child.tag.Equals("tile"))
            {
                originalTilesList.Add(child.gameObject);
            }
        }

        for (int i = 0; i < 10; i++)
        {
            newTile = PoolManager.Instance.SpawnFromPool(PoolManager.tags.Tiles, originalTilesList[originalTilesList.Count - 1].GetComponentInChildren<Tile>().ObjEnd, Quaternion.identity);
            newTile.transform.parent = this.transform;
            originalTilesList.Add(newTile);

        }

        tiles = originalTilesList;
    }
    

    public void SpawnNew()
    {
        if (tilesToCross > 0)
        {
            tilesToCross--;
            return;
        }
        if(tiles.Count == 0)
            tiles = originalTilesList;

        newTile = PoolManager.Instance.SpawnFromPool(PoolManager.tags.Tiles, tiles[tiles.Count - 1].GetComponentInChildren<Tile>().ObjEnd, newTile.transform.parent.rotation);
        tiles.Remove(newTile);
        tiles.Add(newTile);
    }

}
