using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesSpawner : MonoBehaviour
{
    private Queue<GameObject> tiles;

    private void Start()
    {
        tiles = new Queue<GameObject>();
        foreach(Transform child in transform)
        {
            if(child.tag.Equals("tile"))
                tiles.Enqueue(child.gameObject);
        }

        for (int i = 0; i < 10; i++)
        {
            GameObject newTile = PoolManager.Instance.SpawnFromPool(PoolManager.tags.Tiles, tiles.Dequeue().GetComponent<Tile>().ObjEnd, Quaternion.identity);
            tiles.Enqueue(newTile);
            if (newTile.transform.parent == null)
            newTile.transform.parent = this.transform;
        }
    }

    public void SpawnNew()
    {
        GameObject newTile = tiles.Dequeue();
        PoolManager.Instance.SpawnFromPool(PoolManager.tags.Tiles, newTile.GetComponent<Tile>().ObjEnd, newTile.transform.parent.rotation);
        tiles.Enqueue(newTile);
    }

}
