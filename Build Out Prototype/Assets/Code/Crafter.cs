using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafter : MonoBehaviour
{


    public List<int> items = new List<int>();
    public List<int> products = new List<int>();

    public List<Recipe> recipes = new List<Recipe>();

    float timeSeinceCraft = 0f;
    float craftTime = 0.5f;

    public GameObject parentTile;
    public Vector2Int mapPosition;
    
    //1 is up 2 is left 3 is down 4 is right
    public int direction;


    public void Start() {
        //set time since craft to time

        timeSeinceCraft = Time.time % craftTime;

        if((mapPosition.x + mapPosition.y) % 2 == 0) {
            timeSeinceCraft += craftTime / 2;
        }
        
    }

    //update
    public void Update() {

        timeSeinceCraft += Time.deltaTime;
        if(timeSeinceCraft >= craftTime){
            Craft();
            timeSeinceCraft -= craftTime;
        }
    }

    public void Craft() {
        if(items.Count == 0) {
            return;
        }

        foreach(Recipe recipe in recipes) {
            if(recipe.activated) {
                bool craftable = true;
                foreach(Vector2 material in recipe.materials) {
                    if(!GetRepetitions(material)) {
                        craftable = false;
                    }
                }
                if(craftable) {
                    foreach(Vector2Int material in recipe.materials) {
                        for(int i = 0; i < material.y; i++) {
                            items.Remove(material.x);
                        }
                    }

                    foreach(Vector2Int result in recipe.results) {
                        for(int i = 0; i < result.y; i++) {
                            items.Add(result.x);
                        }
                    }
                }
            }
        }
    }

    public bool GetRepetitions(Vector2 material) {
        int count = 0;
        foreach(int item in items) {
            if(item == material.x) {
                count++;
            }
        }
        return count >= material.y;
    }

    private void OnMouseDown() {
        
        //if q is held delete covered
        if (Input.GetKey(KeyCode.Q)) {
            parentTile.GetComponent<TileMaster>().covered = null;
            Destroy(gameObject);
        }

        //0 = bolt, 1 = rod, 2 = plate, 3 = frame, 4 = power cell, 5 = power cell alt
        //activate recipes based on held keys, if already activated deactivate
        if (Input.GetKey(KeyCode.Alpha0)) {
            recipes[0].activated = !recipes[0].activated;
        }
        if (Input.GetKey(KeyCode.Alpha1)) {
            recipes[1].activated = !recipes[1].activated;
        }
        if (Input.GetKey(KeyCode.Alpha2)) {
            recipes[2].activated = !recipes[2].activated;
        }
        if (Input.GetKey(KeyCode.Alpha3)) {
            recipes[3].activated = !recipes[3].activated;
        }
        if (Input.GetKey(KeyCode.Alpha4)) {
            recipes[4].activated = !recipes[4].activated;
        }
        if (Input.GetKey(KeyCode.Alpha5)) {
            recipes[5].activated = !recipes[5].activated;
        }


    }

    public void AddItem(int item) {
        items.Add(item);
    }
    
}

[System.Serializable]
    public class Recipe
    {
        //0 = empty, 1 = energy, 2 = stone, 3 = bolt, 4 = rod, 5 = plate, 6 = frame, 7 = power cell
        //0 = material, 1 = amount
        public List<Vector2Int> materials;
        public List<Vector2Int> results;

        //[System.NonSerialized]
        public bool activated = false;

    }
