using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafter : MonoBehaviour
{
    public GameObject parentTile;
    public Vector2Int mapPosition;
    
    //1 is up 2 is left 3 is down 4 is right
    public int direction;

    private void OnMouseDown() {
        
        //if q is held delete covered
        if (Input.GetKey(KeyCode.Q)) {
            parentTile.GetComponent<TileMaster>().covered = null;
            Destroy(gameObject);
        }
    }
}
