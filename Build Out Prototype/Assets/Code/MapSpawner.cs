using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MapSpawner : MonoBehaviour
{

    public float tileSize = 8f;

    public Vector2 mapSize = new Vector2(18, 10);

    public GameObject tilePrefab;

    public Tile[] tileSprites;

    void Start() {
        tilePrefab.transform.localScale = new Vector3(tileSize, tileSize, tileSize);
        Vector2 halfedMapSize = new Vector2(mapSize.x / 2, mapSize.y / 2);
        for (float x = -halfedMapSize.x - tileSize; x < mapSize.x; x++) {
            for (float y = -halfedMapSize.y - tileSize; y < mapSize.y; y++) {
                GameObject GenTile = Instantiate(tilePrefab, new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));
                //GenTile.transform.localScale = new Vector3(tileSize, tileSize, tileSize);
            }
        }
    }

}


public struct Tile
{
    public Sprite sprite;
    public int tileType;

}
