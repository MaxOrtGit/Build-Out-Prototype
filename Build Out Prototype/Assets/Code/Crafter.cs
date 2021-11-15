using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafter : MonoBehaviour{


    public List<int> items = new List<int>();
    public List<int> products = new List<int>();

    public List<Vector2Int> neatItems = new List<Vector2Int>();
    public List<Vector2Int> neatProducts = new List<Vector2Int>();

    public List<Recipe> recipes = new List<Recipe>();

    float timeSeinceCraft = 0f;
    public float craftTime = 3f;

    float timeSeinceHover = 0f;


    float timeSeinceGive = 0f;
    public float giveTime = 0.5f;


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

        timeSeinceGive = Time.time % giveTime;

        if((mapPosition.x + mapPosition.y) % 2 == 0) {
            timeSeinceGive += giveTime / 2;
        }
        
    }

    //update
    public void Update() {

        timeSeinceCraft += Time.deltaTime;
        if(timeSeinceCraft >= craftTime){
            Craft();
            timeSeinceCraft %= craftTime;
        }
        timeSeinceGive += Time.deltaTime;
        if(timeSeinceGive >= giveTime){
            GiveItem();
            timeSeinceGive %= giveTime;
        }
    }

    public void Craft() {
        if(items.Count == 0) {
            return;
        }

        foreach(Recipe recipe in recipes) {
            if(recipe.activated) {
                bool craftable = true;
                foreach(Vector2Int material in recipe.materials) {
                    if(GetRepetitions(material.x, items) < material.y) {
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
                            products.Add(result.x);
                        }
                    }
                }
            }
        }
    }

    public int GetRepetitions(int material, List<Vector2Int> list) {
        int count = 0;
        foreach(int item in list) {
            if(item == material) {
                count++;
            }
        }
        return count;
    }

    private void OnMouseDown() {
        
        //if q is held delete covered
        if (Input.GetKey(KeyCode.Q)) {
            parentTile.GetComponent<TileMaster>().covered = null;
            Destroy(gameObject);
        }

        //0 = bolt, 1 = rod, 2 = plate, 3 = frame, 4 = power cell, 5 = power cell alt
        //activate recipes based on held keys, if already activated deactivate
        if (Input.GetKey(KeyCode.Alpha1)) {
            recipes[0].activated = !recipes[0].activated;
        }
        if (Input.GetKey(KeyCode.Alpha2)) {
            recipes[1].activated = !recipes[1].activated;
        }
        if (Input.GetKey(KeyCode.Alpha3)) {
            recipes[2].activated = !recipes[2].activated;
        }
        if (Input.GetKey(KeyCode.Alpha4)) {
            recipes[3].activated = !recipes[3].activated;
        }
        if (Input.GetKey(KeyCode.Alpha5)) {
            recipes[4].activated = !recipes[4].activated;
        }
        if (Input.GetKey(KeyCode.Alpha6)) {
            recipes[5].activated = !recipes[5].activated;
        }


        //if o is held display the activated recipe names
        if (Input.GetKey(KeyCode.O)) {
            string recipeNames = "";
            foreach(Recipe recipe in recipes) {
                if(recipe.activated) {
                    recipeNames += recipe.name + " ";
                }
            }
            Debug.Log(recipeNames);

        }

    }

    private void OnMouseEnter() {
        updateNeat();
    }

    //When Object is hovered over start a countdown for 2 seconds using timeSeinceHover
    private void OnMouseOver() {
        timeSeinceHover += Time.deltaTime;
        if(timeSeinceHover >= 1f) {
            //change the text in parentTile.GetComponent<TileMaster>().mapSpawner.craftingText to the activated recipes
            string recipeNames = "";
            foreach(Recipe recipe in recipes) {
                if(recipe.activated) {
                    recipeNames += recipe.name + ", ";
                }
            }
            if(recipeNames != "") {
            recipeNames = recipeNames.Substring(0, recipeNames.Length - 2);
            } else {
                recipeNames = "No Recipes Connected";
            }
            MapSpawner.craftingText.GetComponent<UnityEngine.UI.Text>().text = recipeNames;
        }
    }

    //when mouse is no longer hovering over object stop the countdown and reset the text
    private void OnMouseExit() {
        timeSeinceHover = 0f;
        MapSpawner.craftingText.GetComponent<UnityEngine.UI.Text>().text = "";
    }


    public void updateNeat() {
        neatItems.Clear();
        
        foreach(int item in items) {
            bool unique = true;
            foreach(Vector2Int neatItem in neatItems) {
                if(neatItem.x == item) {
                    unique = false;
                }
            }
            if(unique) {
                neatItems.Add(new Vector2Int(item, GetRepetitions(item)));
            }
        }

        neatProducts.Clear();
        
        foreach(int product in products) {
            bool unique = true;
            foreach(Vector2Int neatProduct in neatProducts) {
                if(neatProduct.x == product) {
                    unique = false;
                }
            }
            if(unique) {
                neatProducts.Add(new Vector2Int(product, GetRepetitions(products)));
            }
        }
    }



    public void AddItem(int item) {
        items.Add(item);
    }

    public void GiveItem(){
        //get tile from parentTile.GetComponent<TileMaster>().tileMap based on direction and print it
        if(products.Count > 0){
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
                if(tileDir.GetComponent<TileMaster>().covered.GetComponent<Belt>().AddItem(products[0])){
                    products.RemoveAt(0);
                }
            } else if(tileDir != null && tileDir.GetComponent<TileMaster>().covered != null && tileDir.GetComponent<TileMaster>().covered.GetComponent<Crafter>() != null){
                tileDir.GetComponent<TileMaster>().covered.GetComponent<Crafter>().AddItem(products[0]);
                products.RemoveAt(0);
            } else if(tileDir != null && tileDir.GetComponent<TileMaster>().covered == null && products[0] == 7){
                    Debug.Log("? hazard");
                if(tileDir.GetComponent<TileMaster>().DropHazardLvl(1)){
                    Debug.Log("Dropped hazard");
                    products.RemoveAt(0);
                }
            }
        }   

    }
    
}

[System.Serializable]
    public class Recipe
    {

        public string name;
        //0 = empty, 1 = energy, 2 = stone, 3 = bolt, 4 = rod, 5 = plate, 6 = frame, 7 = power cell
        //0 = material, 1 = amount
        public List<Vector2Int> materials;
        public List<Vector2Int> results;

        //[System.NonSerialized]
        public bool activated = false;

    }
