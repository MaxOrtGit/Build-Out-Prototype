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
        float halfedTileSize = tileSize / 2;
        for (float x = -halfedMapSize.x - halfedTileSize; x < mapSize.x + halfedTileSize; x++) {
            for (float y = -halfedMapSize.y - halfedTileSize; y < mapSize.y + halfedTileSize; y++) {
                GameObject genTile = Instantiate(tilePrefab, new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));
                genTile.enabled = true;
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

    //a function that creates grid of tiles with a width of mapSize.x and a height of mapSize.y. each tile is a tilePrefab. each tile is a child of the MapSpawner object. the tilePrefab have a width and height of tileSize. the grid of tiles are cented around the origin

    //create a function that instanciates a 2darray of tiles with a x of mapSize.x and a y of mapSize.y

    //a function that creates grid of tiles with a width of mapSize.x and a height of mapSize.y. each tile is a tilePrefab. each tile is a child of the MapSpawner object. the tilePrefab have a width and height of tileSize. the grid of tiles are cented around the origin




}

[System.Serializable]
public struct Tile
{
    public Sprite sprite;

}
