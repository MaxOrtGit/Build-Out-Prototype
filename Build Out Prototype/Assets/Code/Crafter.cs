using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafter : MonoBehaviour
{
    public GameObject parentTile;

    private void OnMouseDown() {
        
        //if q is held delete covered
        if (Input.GetKey(KeyCode.Q)) {
            print("Q pressed");
            parentTile.GetComponent<TileMaster>().covered = null;
            Destroy(gameObject);
        }
    }
}
