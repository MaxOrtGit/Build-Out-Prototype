using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    public float mineingSpeed = 1.0f;

    //1 is energyium, 2 is stonium
    public List<int> ores = new List<int>();
    public int oreCount = 0;


    float timeSeinceMine = 0f;
    public float mineTime = 0.5f;


    float timeSeinceGive = 0f;
    public float giveTime = 0.5f;

    //non serialized
    [System.NonSerialized]
    public GameObject parentTile;
    public Vector2Int mapPosition;
    
    //1 is up 2 is left 3 is down 4 is right
    public int direction;


    public void Start() {
        //set time since craft to time

        timeSeinceMine = Time.time % mineTime;

        if((mapPosition.x + mapPosition.y) % 2 == 0) {
            timeSeinceMine += mineTime / 2;
        }

        timeSeinceGive = Time.time % giveTime;

        if((mapPosition.x + mapPosition.y) % 2 == 0) {
            timeSeinceGive += giveTime / 2;
        }
        
    }

    //update
    public void Update() {

        timeSeinceMine += Time.deltaTime;
        if(timeSeinceMine >= mineTime){
            Mine();
            timeSeinceMine %= mineTime;
        }
        timeSeinceGive += Time.deltaTime;
        if(timeSeinceGive >= giveTime){
            GiveItem();
            timeSeinceGive %= giveTime;
        }
    }

    public void Mine(){
        
        //every second add an ore to the storage
        if(parentTile.GetComponent<TileMaster>().tileType == 1){
            ores.Add(1);
            // Debug.Log(ores.Count);
            // Debug.Log("ores");
            oreCount++;
        } else if(parentTile.GetComponent<TileMaster>().tileType == 2){
            ores.Add(2);
            // Debug.Log(ores.Count);
            // Debug.Log("ores");
            oreCount++;
        }
    }

    public void GiveItem(){
        //get tile from parentTile.GetComponent<TileMaster>().tileMap based on direction and print it
        if(oreCount > 0){
            GameObject tileDir = null;
            switch(direction){
                case 1:
                    //up
                    tileDir = parentTile.GetComponent<TileMaster>().masterMapSpawner.GetComponent<MapSpawner>().tileMap[mapPosition.x][mapPosition.y + 1];
                    break;
                case 2:
                    //left
                    tileDir = parentTile.GetComponent<TileMaster>().masterMapSpawner.GetComponent<MapSpawner>().tileMap[mapPosition.x - 1][mapPosition.y];
                    break;
                case 3:
                    //down
                    tileDir = parentTile.GetComponent<TileMaster>().masterMapSpawner.GetComponent<MapSpawner>().tileMap[mapPosition.x][mapPosition.y - 1];
                    break;
                case 4:
                    //right
                    tileDir = parentTile.GetComponent<TileMaster>().masterMapSpawner.GetComponent<MapSpawner>().tileMap[mapPosition.x + 1][mapPosition.y];
                    break;
            }

            if(tileDir != null && tileDir.GetComponent<TileMaster>().covered != null && tileDir.GetComponent<TileMaster>().covered.GetComponent<Belt>() != null){
                if(tileDir.GetComponent<TileMaster>().covered.GetComponent<Belt>().AddItem(ores[0])){
                    ores.RemoveAt(0);
                    oreCount--;
                }
            }if(tileDir != null && tileDir.GetComponent<TileMaster>().covered != null && tileDir.GetComponent<TileMaster>().covered.GetComponent<Crafter>() != null){
                tileDir.GetComponent<TileMaster>().covered.GetComponent<Crafter>().AddItem(ores[0]);
                ores.RemoveAt(0);
                oreCount--;
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
