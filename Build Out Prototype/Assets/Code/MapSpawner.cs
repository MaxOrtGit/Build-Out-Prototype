using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MapSpawner : MonoBehaviour
{
    //create dictionary of Ids and items
    //0 = empty, 1 = energy, 2 = stone, 3 = bolt, 4 = rod, 5 = plate, 6 = frame, 7 = power cell
    [System.NonSerialized]
    public static Dictionary<int, string> itemDictionary = new Dictionary<int, string>()
    {
        {0, "empty"},
        {1, "energy"},
        {2, "stone"},
        {3, "bolt"},
        {4, "rod"},
        {5, "plate"},
        {6, "frame"},
        {7, "power cell"}
    };


    public float tileSize = 8f;

    public int maxHazardLvl;
    public bool hazardEnabled = true;

    public Vector2 mapSize = new Vector2(18, 10);

    public GameObject tilePrefab;

    public List<List<GameObject>> tileMap = new List<List<GameObject>>();

    public Tile[] tileSprites;

    public static GameObject craftingText;
    public static GameObject itemText;
    public static GameObject productText;
    public GameObject setCraftingText;
    public GameObject setItemText;
    public GameObject setProductText;

    void Start() {
        craftingText = setCraftingText;
        itemText = setItemText;
        productText = setProductText;


        maxHazardLvl = (int)(Mathf.Max(mapSize.x / 2, mapSize.y / 2) - 1);


        //create empty gameobject called Tiles to hold all tiles
        GameObject tileParent = new GameObject("Tiles");


        tilePrefab.transform.localScale = new Vector3(tileSize, tileSize, tileSize);
        tilePrefab.SetActive(true);
        Vector2 halfedMapSize = new Vector2(mapSize.x / 2, mapSize.y / 2);
        print(halfedMapSize);
        float halfedTileSize = tileSize / 2;
        int mapx = 0;
        int mapy = 0;
        for (float x = -halfedMapSize.x + halfedTileSize; x < halfedMapSize.x + halfedTileSize; x++) {
            mapy = 0;
            List<GameObject> yList = new List<GameObject>();
            for (float y = -halfedMapSize.y + halfedTileSize; y < halfedMapSize.y + halfedTileSize; y++) {
                GameObject genTile = Instantiate(tilePrefab, new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0), tileParent.transform);
                
                yList.Add(genTile);

                if((Mathf.Pow(x-4, 2) + Mathf.Pow(y+3, 2)) <= 20){
                    genTile.GetComponent<SpriteRenderer>().sprite = tileSprites[1].sprite;
                    genTile.GetComponent<TileMaster>().tileType = 1;
                }else if((Mathf.Pow(x+2, 2) + Mathf.Pow(y-4, 2)) <= 20){
                    genTile.GetComponent<SpriteRenderer>().sprite = tileSprites[2].sprite;
                    genTile.GetComponent<TileMaster>().tileType = 2;
                } else {
                    genTile.GetComponent<SpriteRenderer>().sprite = tileSprites[0].sprite;
                    genTile.GetComponent<TileMaster>().tileType = 0;
                }
                genTile.GetComponent<TileMaster>().masterMapSpawner = this;
                genTile.GetComponent<TileMaster>().mapPosition = new Vector2Int(mapx, mapy);
                int genTileDFC = (int)Mathf.Max(Mathf.Abs(x),  Mathf.Abs(y));
                genTile.GetComponent<TileMaster>().distanceFromCenter = genTileDFC;

                if(hazardEnabled){
                    genTile.GetComponent<TileMaster>().hazardLvl = genTileDFC;
                    genTile.GetComponent<SpriteRenderer>().color = new Color(1f, 1f - ((float)genTileDFC/ maxHazardLvl), 1f - ((float)genTileDFC/ maxHazardLvl), 1f);
                }
                genTile.GetComponent<TileMaster>().mapSpawner = this;

                mapy++;
            }
            tileMap.Add(yList);
            mapx++;
        }
    }
}

[System.Serializable]
public struct Tile
{
    public Sprite sprite;

}
