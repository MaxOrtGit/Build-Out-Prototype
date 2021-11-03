using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    
    public List<int> items = new List<int>();

    public GameObject itemOverlay;
    public float itemOverlayFadeSpeed = 0.1f;

    public float alpha = 0f;

    public float moveTime = 0.5f;
    public float timeSeinceMove = 0f;


    public GameObject parentTile;
    public Vector2Int mapPosition;
    
    //1 is up 2 is left 3 is down 4 is right
    public int direction;

    public void Start() {
        //set time since move to time
        timeSeinceMove = Time.time % moveTime;
        if((mapPosition.x + mapPosition.y) % 2 == 0) {
            timeSeinceMove /= 2;
        }
        
    }


    public void Update() {

        timeSeinceMove += Time.deltaTime;
        if(timeSeinceMove >= moveTime){
            timeSeinceMove -= moveTime;
            GiveItem();
        }
        itemOverlay = transform.GetChild(0).gameObject;
        //change alpha of itemOverlay from 0f to 1f over itemOverlayFadeSpeed seconds
        if (alpha < 1f && items.Count > 0) {
            alpha += Time.deltaTime / itemOverlayFadeSpeed;
            itemOverlay.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
        }


    }



    private void OnMouseDown() {

        //if q is held delete covered
        if (Input.GetKey(KeyCode.Q)) {
            parentTile.GetComponent<TileMaster>().covered = null;
            Destroy(gameObject);
        }
    }

    public void AddItem(int item) {
        items.Add(item);
        if(items.Count == 1){
           itemOverlay.GetComponent<SpriteRenderer>().sprite = itemOverlay.GetComponent<ItemOverlay>().itemSprites[item];
        }
    }

    public void GiveItem(){
        if(items.Count > 0){
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
                tileDir.GetComponent<TileMaster>().covered.GetComponent<Belt>().AddItem(items[0]);
                items.RemoveAt(0);

                alpha = 0f;
                itemOverlay.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
                if(items.Count > 0){
                    itemOverlay.GetComponent<SpriteRenderer>().sprite = itemOverlay.GetComponent<ItemOverlay>().itemSprites[items[0]];
                } else {
                    itemOverlay.GetComponent<SpriteRenderer>().sprite = null;
                }
            }


            
        }   

    }
}

