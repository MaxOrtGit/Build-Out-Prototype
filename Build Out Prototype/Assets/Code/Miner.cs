using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    public float mineingSpeed = 1.0f;

    //1 is energyium, 2 is stonium
    public List<int> ores = new List<int>();
    public int oreCount = 0;

    //non serialized
    [System.NonSerialized]
    public GameObject parentTile;
    public Vector2Int mapPosition;
    
    //1 is up 2 is left 3 is down 4 is right
    public int direction;


    private void Start() {
        
        InvokeRepeating("Mine", 0, 1.0f);
        InvokeRepeating("DropItem", 0.5f, 1.0f);
    }

    public void Mine(){
        
        //every second add an ore to the storage
        if(parentTile.GetComponent<TileMaster>().tileType == 1){
            ores.Add(1);
            Debug.Log(ores.Count);
            Debug.Log("ores");
            oreCount++;
        } else if(parentTile.GetComponent<TileMaster>().tileType == 2){
            ores.Add(2);
            Debug.Log(ores.Count);
            Debug.Log("ores");

            
            oreCount++;
        }
    }

    public void DropItem(){
        //get tile from parentTile.GetComponent<TileMaster>().tileMap based on direction and print it
        if(oreCount > 0){
            
            switch(direction){
                case 1:
                    //up
                    GameObject tileAbove = parentTile.GetComponent<TileMaster>().masterMapSpawner.GetComponent<MapSpawner>().tileMap[mapPosition.x][mapPosition.y + 1];
                    if(tileAbove.GetComponent<TileMaster>().covered != null && tileAbove.GetComponent<TileMaster>().covered.GetComponent<Belt>() != null){
                        tileAbove.GetComponent<Belt>().AddItem(ores[0]);
                        ores.RemoveAt(0);
                        oreCount--;
                    }
                    break;
                case 2:
                    //left
                    GameObject tileLeft = parentTile.GetComponent<TileMaster>().masterMapSpawner.GetComponent<MapSpawner>().tileMap[mapPosition.x - 1][mapPosition.y];
                    if(tileLeft.GetComponent<TileMaster>().covered != null && tileLeft.GetComponent<TileMaster>().covered.GetComponent<Belt>() != null){
                        tileLeft.GetComponent<Belt>().AddItem(ores[0]);
                        ores.RemoveAt(0);
                        oreCount--;
                    }
                    break;
                case 3:
                    //down
                    GameObject tileBelow = parentTile.GetComponent<TileMaster>().masterMapSpawner.GetComponent<MapSpawner>().tileMap[mapPosition.x][mapPosition.y - 1];
                    Debug.Log(tileBelow);
                    Debug.Log(tileBelow.GetComponent<TileMaster>().covered);
                    if(tileBelow.GetComponent<TileMaster>().covered != null && tileBelow.GetComponent<TileMaster>().covered.GetComponent<Belt>() != null){
                        tileBelow.GetComponent<Belt>().AddItem(ores[0]);
                        ores.RemoveAt(0);
                        oreCount--;
                    }
                    break;
                case 4:
                    //right
                    GameObject tileRight = parentTile.GetComponent<TileMaster>().masterMapSpawner.GetComponent<MapSpawner>().tileMap[mapPosition.x + 1][mapPosition.y];
                    if(tileRight.GetComponent<TileMaster>().covered != null && tileRight.GetComponent<TileMaster>().covered.GetComponent<Belt>() != null){
                        tileRight.GetComponent<Belt>().AddItem(ores[0]);
                        ores.RemoveAt(0);
                        oreCount--;
                    }
                    break;


            }
        }   

    }

    private void OnMouseDown() {
        
        //if q is held delete covered
        if (Input.GetKey(KeyCode.Q)) {
            parentTile.GetComponent<TileMaster>().covered = null;
            Destroy(gameObject);
        }
    }
    
}
