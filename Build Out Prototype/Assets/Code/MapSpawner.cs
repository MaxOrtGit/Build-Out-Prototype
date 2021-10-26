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
        tilePrefab.SetActive(true);
        Vector2 halfedMapSize = new Vector2(mapSize.x / 2, mapSize.y / 2);
        print(halfedMapSize);
        float halfedTileSize = tileSize / 2;
        for (float x = -halfedMapSize.x + halfedTileSize; x < halfedMapSize.x + halfedTileSize; x++) {
            for (float y = -halfedMapSize.y + halfedTileSize; y < halfedMapSize.y + halfedTileSize; y++) {
                GameObject genTile = Instantiate(tilePrefab, new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));
                if((Mathf.Pow(x+2, 2) + Mathf.Pow(y-4, 2)) <= 20){
                    genTile.GetComponent<SpriteRenderer>().sprite = tileSprites[1].sprite;
                }else if((Mathf.Pow(x-4, 2) + Mathf.Pow(y+3, 2)) <= 20){
                    genTile.GetComponent<SpriteRenderer>().sprite = tileSprites[2].sprite;
                } else {
                    genTile.GetComponent<SpriteRenderer>().sprite = tileSprites[0].sprite;
                }
            }
        }
    }

}

[System.Serializable]
public struct Tile
{
    public Sprite sprite;

}
