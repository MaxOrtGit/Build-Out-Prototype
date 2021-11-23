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

    public int distanceFromCenter;

    public int hazardLvl;



    public GameObject covered;
    public MapSpawner masterMapSpawner;
    public Vector2Int mapPosition;
    
    public MapSpawner mapSpawner;


    int direction = 0;


    // When W,A,S,D is pressed, save direction
    void Update()
    {
        //FindDirection();
        
    }

    private void FindDirection() {
    //1 is up 2 is left 3 is down 4 is right 5 is up-left 6 is down-left 7 is down-right 8 is up-right
        int totalHeld = 0;
        if (Input.GetKey(KeyCode.W)) {
            totalHeld++;
            direction = 1;
        }
        if (Input.GetKey(KeyCode.A)) {
            totalHeld++;
            direction = 2;
        }
        if (Input.GetKey(KeyCode.S)) {
            totalHeld++;
            direction = 3;
        }
        if (Input.GetKey(KeyCode.D)) {
            totalHeld++;
            direction = 4;
        }
        Debug.Log(totalHeld);

        if (totalHeld == 2) {
            Debug.Log("You can't hold both directions");
            if (Input.GetKeyDown(KeyCode.W)) {
                if(Input.GetKeyDown(KeyCode.D)) {
                    direction = 5;
                } else if (Input.GetKeyDown(KeyCode.A)) {
                    direction = 7;
                }
            }
            if (Input.GetKeyDown(KeyCode.S)) {
                if (Input.GetKeyDown(KeyCode.D)) {
                    direction = 6;
                } else if (Input.GetKeyDown(KeyCode.A)) {
                    direction = 8;
                }
            }
        } else if (totalHeld == 3) {
            if (!Input.GetKeyDown(KeyCode.W)) {
                direction = 3;
            } else if (!Input.GetKeyDown(KeyCode.A)) {
                direction = 4;
            } else if (!Input.GetKeyDown(KeyCode.S)) {
                direction = 1;
            } else if (!Input.GetKeyDown(KeyCode.D)) {
                direction = 2;
            }
        } else if (totalHeld == 4) {
            direction = 0;
        }
    }
    

    private void OnMouseDown() {
        FindDirection();
        Debug.Log(direction);
        //add miner to tile if V is held if tiletype equals 1 or 2 and covered is null
        if (hazardLvl == 0 && Input.GetKey(KeyCode.V) && (tileType == 1 || tileType == 2) && covered == null) {
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
        if (hazardLvl == 0 && Input.GetKey(KeyCode.B) && covered == null) {
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
                case 5:
                    newRotation = Quaternion.Euler(0, 0, 45);
                    break;
                case 6:
                    newRotation = Quaternion.Euler(0, 0, 135);
                    break;
                case 7:
                    newRotation = Quaternion.Euler(0, 0, 225);
                    break;
                case 8:
                    newRotation = Quaternion.Euler(0, 0, 315);
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
        if (hazardLvl == 0 && Input.GetKey(KeyCode.C) && covered == null) {
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

    public bool DropHazardLvl(int by) {
        if(hazardLvl != 0){
            hazardLvl -= by;
            if(hazardLvl <= 0){
                hazardLvl = 0;
            }
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f - (float)((float)hazardLvl / mapSpawner.maxHazardLvl), 1f - (float)((float)hazardLvl / mapSpawner.maxHazardLvl), 1);
            return true;
        } else {
            return false;
        }
    }

}
