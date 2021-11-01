using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMaster : MonoBehaviour
{
    public GameObject miner;
    public GameObject crafter;
    public GameObject belt;

    //0 = empty, 1 = energy, 2 = stone
    public int tileType = 0;


    //non serialized
    [System.NonSerialized]
    public GameObject covered;
    public MapSpawner masterMapSpawner;
    public Vector2Int mapPosition;

    private void OnMouseDown() {
        
        //1 is up 2 is left 3 is down 4 is right
        int direction = 0;
        if (Input.GetKey(KeyCode.W))
        {
            direction = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction = 2;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction = 3;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = 4;
        }



        //add miner to tile if V is held if tiletype equals 1 or 2 and covered is null
        if (Input.GetKey(KeyCode.V) && (tileType == 1 || tileType == 2) && covered == null) {
            //position changed by -1 in z axis
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);

            //rotation based on direction using case
            Quaternion newRotation = new Quaternion();
            switch (direction)
            {
                case 1:
                    newRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case 2:
                    newRotation = Quaternion.Euler(0, 0, 90);
                    break;
                case 3:
                    newRotation = Quaternion.Euler(0, 0, 180);
                    break;
                case 4:
                    newRotation = Quaternion.Euler(0, 0, 270);
                    break;
            }
            if(direction != 0) {
                covered = Instantiate(miner, newPosition, newRotation);
                covered.GetComponent<Miner>().parentTile = gameObject;
                covered.GetComponent<Miner>().direction = direction;
                covered.GetComponent<Miner>().mapPosition = mapPosition;
                //Debug.Log("Miner created");
            }
            
        }

        //add crafter to tile if B is held
        if (Input.GetKey(KeyCode.B) && covered == null) {
            //position changed by -1 in z axis
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            
            //rotation on z based on direction using case
            Quaternion newRotation = new Quaternion();

            switch (direction)
            {
                case 1:
                    newRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case 2:
                    newRotation = Quaternion.Euler(0, 0, 90);
                    break;
                case 3:
                    newRotation = Quaternion.Euler(0, 0, 180);
                    break;
                case 4:
                    newRotation = Quaternion.Euler(0, 0, 270);
                    break;
            }

            if(direction != 0) {
                covered = Instantiate(crafter, newPosition, newRotation);
                covered.GetComponent<Crafter>().parentTile = gameObject;
                covered.GetComponent<Crafter>().direction = direction;
                covered.GetComponent<Crafter>().mapPosition = mapPosition;
                //Debug.Log("Crafter created");
            }
        }

        //add belt to tile if C is held
        if (Input.GetKey(KeyCode.C) && covered == null) {
            //position changed by -1 in z axis
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            
            //rotation on z based on direction using case
            Quaternion newRotation = new Quaternion();

            switch (direction)
            {
                case 1:
                    newRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case 2:
                    newRotation = Quaternion.Euler(0, 0, 90);
                    break;
                case 3:
                    newRotation = Quaternion.Euler(0, 0, 180);
                    break;
                case 4:
                    newRotation = Quaternion.Euler(0, 0, 270);
                    break;
            }

            if(direction != 0) {
                covered = Instantiate(belt, newPosition, newRotation);
                covered.GetComponent<Belt>().parentTile = gameObject;
                covered.GetComponent<Belt>().direction = direction;
                covered.GetComponent<Belt>().mapPosition = mapPosition;
                //Debug.Log("Belt created");
            }
        }
    }
}
