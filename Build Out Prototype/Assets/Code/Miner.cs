using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    public float mineingSpeed = 1.0f;

    public ArrayList ores = new ArrayList();
    public int oreCount = 0;

    //non serialized
    [System.NonSerialized]
    public GameObject parentTile;


    private void Start() {
        
        InvokeRepeating("Mine", 0, 1.0f);
    }

    public void Mine(){
        
        //every second add an ore to the storage
        if(parentTile.GetComponent<TileMaster>().tileType == 1){
            ores.Add(1);
            oreCount++;
        } else if(parentTile.GetComponent<TileMaster>().tileType == 1){
            ores.Add(2);
            oreCount++;
        }
    }
    
}
