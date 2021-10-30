using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMaster : MonoBehaviour
{
    public GameObject miner;
    public GameObject crafter;

    //0 = empty, 1 = energy, 2 = stone
    public int tileType = 0;


    //non serialized
    [System.NonSerialized]
    public GameObject covered;

    private void OnMouseDown() {
        
        //add miner to tile if w is held
        if (Input.GetKey(KeyCode.W)) {
            //if tiletype equals 1 or 2 
            if (tileType == 1 || tileType == 2) {
                //position changed by -1 in z axis
                Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
                covered = Instantiate(miner, newPosition, transform.rotation);
                covered.GetComponent<Miner>().parentTile = gameObject;
                Debug.Log("Miner created");
            }
        }

        //add crafter to tile if e is held
        if (Input.GetKey(KeyCode.E)) {
            //position changed by -1 in z axis
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            covered = Instantiate(crafter, newPosition, transform.rotation);
            covered.GetComponent<Crafter>().parentTile = gameObject;
            Debug.Log("Crafter created");
        }
        
        //if q is held delete covered
        if (Input.GetKey(KeyCode.Q)) {
            print("Q pressed");
            Destroy(covered);
            covered = null;
        }
    }
}
