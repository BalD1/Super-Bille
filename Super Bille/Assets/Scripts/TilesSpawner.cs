using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesSpawner : MonoBehaviour
{
    [SerializeField] private int tilesToCross = 5;
    private int baseTileToCross;

    [SerializeField] private List<GameObject> tilesPrefab;
    private List<GameObject> originalTilesList;
    private List<GameObject> tiles;
    private GameObject newTile;

    private Vector3 spawnPos;

    private void Start()
    {
        baseTileToCross = tilesToCross;
        originalTilesList = new List<GameObject>();
        tiles = new List<GameObject>();

        foreach(Transform child in this.transform)
        {
            if (child.tag.Equals("tile"))
            {
                originalTilesList.Add(child.gameObject);
            }
        }
        SpawnTiles();

        tiles = originalTilesList;
    }

    private void SpawnTiles()
    {
        for(int i = 0; i < tilesPrefab.Count; i++)  // Randomise les premières tiles
        {
            GameObject temp = tilesPrefab[i];
            int randomIndex = Random.Range(i, tilesPrefab.Count);
            tilesPrefab[i] = tilesPrefab[randomIndex];
            tilesPrefab[randomIndex] = temp;
        }
        for (int i = 0; i < tilesPrefab.Count; i++)     // Instantie les tiles
        {
            newTile = GameObject.Instantiate(tilesPrefab[i], originalTilesList[originalTilesList.Count - 1].GetComponentInChildren<Tile>().ObjEnd, tilesPrefab[i].transform.rotation, this.transform);
            originalTilesList.Add(newTile);
        }


    }


    public void SpawnNew()
    {
        if (tilesToCross > 0)   // Combien de tiles on doit passer avant de commencer à les respawn
        {
            tilesToCross--;
            return;
        }
        if(tiles.Count == 0)
            tiles = originalTilesList;

        newTile = tiles[Random.Range(0, baseTileToCross - 2)];      // respawn une tile au hasard parmis les premières
        newTile.transform.position = tiles[tiles.Count - 1].GetComponentInChildren<Tile>().ObjEnd;
        tiles.Remove(newTile);
        tiles.Add(newTile);
    }

}
