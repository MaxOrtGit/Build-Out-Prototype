using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafter : MonoBehaviour{

    // 0 = dont craft, 1 = equal order, 2 = ratio using alt
    int craftingMode = 1;

    int lastCrafted = -1;
    List<Vector3Int> lastCraftedRatio = new List<Vector3Int>();

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
    
    //1 is up 2 is left 3 is down 4 is right 5 is up left 6 is left down 7 is right down 8 is right up
    public int direction;

    public int lastDirection = 1;


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

        List<int> activatedRecipes = new List<int>();
        Recipe recipe = null;
        foreach(Recipe arecipe in recipes) {
            if(arecipe.activated) {
                activatedRecipes.Add(recipes.IndexOf(arecipe));
            }
        }
        if(activatedRecipes.Count == 0) {
            return;
        }

        if(craftingMode == 0) {

            return;

        } else if(craftingMode == 1) {

            if(lastCrafted == -1) {
                lastCrafted = activatedRecipes[0];
            } else if (activatedRecipes.Count > 1 && lastCrafted != activatedRecipes[activatedRecipes.Count - 1]) {
                lastCrafted = activatedRecipes[activatedRecipes.IndexOf(lastCrafted) + 1];
            } else {
                lastCrafted = activatedRecipes[0];
            }
            recipe = recipes[lastCrafted];

        } else if(craftingMode == 2) {
            if(lastCraftedRatio.Count == 0 || lastCraftedRatio.Count != activatedRecipes.Count) {
                for(int i = 0; i < activatedRecipes.Count; i++) {
                    lastCraftedRatio.Add(new Vector3Int(activatedRecipes[i], 0, recipes[activatedRecipes[i]].results[0].y));
                }
                recipe = recipes[lastCraftedRatio[0].x];
                lastCraftedRatio[0] = new Vector3Int(lastCraftedRatio[0].x, recipes[lastCraftedRatio[0].x].ratio, recipes[lastCraftedRatio[0].x].results[0].y);
            } else if(activatedRecipes.Count > 1) {
                int lowest = lastCraftedRatio[0].y;
                int lowestIndex = 0;
                foreach(Vector3Int ratio in lastCraftedRatio) {
                    if(lowest > ratio.y) {
                        lowest = ratio.y;
                        lowestIndex = lastCraftedRatio.IndexOf(ratio);
                    }
                }
                lastCraftedRatio[lowestIndex] = new Vector3Int(lastCraftedRatio[lowestIndex].x, lastCraftedRatio[lowestIndex].y + recipes[lowestIndex].results[0].y, lastCraftedRatio[lowestIndex].z);
                recipe = recipes[lowestIndex];
                
                //if all z's are bigger than the y's then subtract y's by all z's
                bool allBigger = true;
                foreach(Vector3Int ratio in lastCraftedRatio) {
                    if(ratio.z < ratio.y) {
                        allBigger = false;
                    }
                }
                if(allBigger) {
                    for(int i = 0; i < lastCraftedRatio.Count; i++) {
                        lastCraftedRatio[i] = new Vector3Int(lastCraftedRatio[i].x, lastCraftedRatio[i].y - lastCraftedRatio[i].z, lastCraftedRatio[i].z );
                    }
                }
            } else {
                recipe = recipes[lastCraftedRatio[0].x];
            }
        }


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

    public int GetRepetitions(int material, List<int> list) {
        int count = 0;
        foreach(int item in list) {
            if(item == material) {
                count++;
            }
        }
        return count;
    }

    private void OnMouseDown() {
        
        //if Shift is held down
        if (Input.GetKey(KeyCode.LeftShift)) {
            int numbHeld = 1;
            //get numbHeld from keyboard 0-9
            if (Input.GetKey(KeyCode.Alpha0)) {
                numbHeld = 10;
            }
            if (Input.GetKey(KeyCode.Alpha1)) {
                numbHeld = 1;
            }
            if (Input.GetKey(KeyCode.Alpha2)) {
                numbHeld = 2;
            }
            if (Input.GetKey(KeyCode.Alpha3)) {
                numbHeld = 3;
            }
            if (Input.GetKey(KeyCode.Alpha4)) {
                numbHeld = 4;
            }
            if (Input.GetKey(KeyCode.Alpha5)) {
                numbHeld = 5;
            }
            if (Input.GetKey(KeyCode.Alpha6)) {
                numbHeld = 6;
            }
            if (Input.GetKey(KeyCode.Alpha7)) {
                numbHeld = 7;
            }
            if (Input.GetKey(KeyCode.Alpha8)) {
                numbHeld = 8;
            }
            if (Input.GetKey(KeyCode.Alpha9)) {
                numbHeld = 9;
            }

            //get letterHeld from keyboard q-p
            if (Input.GetKey(KeyCode.Q)) {
                recipes[0].ratio = numbHeld;
            }
            if (Input.GetKey(KeyCode.W)) {
                recipes[1].ratio = numbHeld;
            }
            if (Input.GetKey(KeyCode.E)) {
                recipes[2].ratio = numbHeld;
            }
            if (Input.GetKey(KeyCode.R)) {
                recipes[3].ratio = numbHeld;
            }
            if (Input.GetKey(KeyCode.T)) {
                recipes[4].ratio = numbHeld;
            }
            if (Input.GetKey(KeyCode.Y)) {
                recipes[5].ratio = numbHeld;
            }
            if (Input.GetKey(KeyCode.U)) {
                recipes[6].ratio = numbHeld;
            }
            if (Input.GetKey(KeyCode.I)) {
                recipes[7].ratio = numbHeld;
            }
            if (Input.GetKey(KeyCode.O)) {
                recipes[8].ratio = numbHeld;
            }
            if (Input.GetKey(KeyCode.P)) {
                recipes[9].ratio = numbHeld;
            }
            
        } else if (Input.GetKey(KeyCode.LeftControl)) {
            if (Input.GetKey(KeyCode.Alpha0)) {
                craftingMode = 0;
            }
            if (Input.GetKey(KeyCode.Alpha1)) {
                craftingMode = 1;
            }
            if (Input.GetKey(KeyCode.Alpha2)) {
                craftingMode = 2;
            }

        } else {
            //if q is held delete covered
            if (Input.GetKey(KeyCode.Q)) {
                parentTile.GetComponent<TileMaster>().covered = null;
                MapSpawner.craftingText.GetComponent<UnityEngine.UI.Text>().text = "";
                MapSpawner.itemText.GetComponent<UnityEngine.UI.Text>().text = "";
                MapSpawner.productText.GetComponent<UnityEngine.UI.Text>().text = "";
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

    }


    //When Object is hovered over start a countdown for 2 seconds using timeSeinceHover
    private void OnMouseOver() {
        timeSeinceHover += Time.deltaTime;
        if(timeSeinceHover >= 1f) {
            updateNeat();
            //change the text in parentTile.GetComponent<TileMaster>().mapSpawner.craftingText to the activated recipes
            string recipeNames = "Crafting Mode " + craftingMode + ", ";
            foreach(Recipe recipe in recipes) {
                if(recipe.activated) {
                    recipeNames += recipe.name;
                    if(craftingMode == 2) {
                        recipeNames += " Ratio: " + recipe.ratio;
                    }
                    recipeNames += ", ";
                }
            }
            if(recipeNames != "Crafting Mode " + craftingMode + " ") {
                recipeNames = recipeNames.Substring(0, recipeNames.Length - 2);
            } else {
                recipeNames += "No Recipes Connected";
            }
            MapSpawner.craftingText.GetComponent<UnityEngine.UI.Text>().text = recipeNames;


            
            //change the text in parentTile.GetComponent<TileMaster>().mapSpawner.itemText to neat items
            string neatItemsText = "";
            foreach(Vector2Int item in neatItems) {
                neatItemsText += item.y + " " + MapSpawner.itemDictionary[item.x] + ", ";
            }
            if(neatItemsText != "") {
                neatItemsText = neatItemsText.Substring(0, neatItemsText.Length - 2);
            } else {
                neatItemsText = "No Items";
            }
            MapSpawner.itemText.GetComponent<UnityEngine.UI.Text>().text = neatItemsText;

            
            //change the text in parentTile.GetComponent<TileMaster>().mapSpawner.productText to neat products
            string neatProductsText = "";
            foreach(Vector2Int product in neatProducts) {
                neatProductsText += product.y + " " + MapSpawner.itemDictionary[product.x] + ", ";
            }
            if(neatProductsText != "") {
                neatProductsText = neatProductsText.Substring(0, neatProductsText.Length - 2);
            } else {
                neatProductsText = "No Products";
            }
            MapSpawner.productText.GetComponent<UnityEngine.UI.Text>().text = neatProductsText;

        }
    }

    //when mouse is no longer hovering over object stop the countdown and reset the text
    private void OnMouseExit() {
        timeSeinceHover = 0f;
        MapSpawner.craftingText.GetComponent<UnityEngine.UI.Text>().text = "";
        MapSpawner.itemText.GetComponent<UnityEngine.UI.Text>().text = "";
        MapSpawner.productText.GetComponent<UnityEngine.UI.Text>().text = "";
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
                neatItems.Add(new Vector2Int(item, GetRepetitions(item, items)));
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
                neatProducts.Add(new Vector2Int(product, GetRepetitions(product, products)));
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
                case 5:
                    //up left
                    switch(lastDirection){
                        case 1:
                            //up
                            lastDirection = 2;
                            tileDir = parentTile.GetComponent<TileMaster>().masterMapSpawner.GetComponent<MapSpawner>().tileMap[mapPosition.x][mapPosition.y + 1];
                            break;
                        case 2:
                            //left
                            lastDirection = 1;
                            tileDir = parentTile.GetComponent<TileMaster>().masterMapSpawner.GetComponent<MapSpawner>().tileMap[mapPosition.x - 1][mapPosition.y];
                            break;
                    }
                    break;
                case 6:
                    //left down
                    switch(lastDirection){
                        case 1:
                            //left
                            lastDirection = 2;
                            tileDir = parentTile.GetComponent<TileMaster>().masterMapSpawner.GetComponent<MapSpawner>().tileMap[mapPosition.x - 1][mapPosition.y];
                            break;
                        case 2:
                            //down
                            lastDirection = 1;
                            tileDir = parentTile.GetComponent<TileMaster>().masterMapSpawner.GetComponent<MapSpawner>().tileMap[mapPosition.x][mapPosition.y - 1];
                            break;
                    }
                    break;
                case 7:
                    //down right
                    switch(lastDirection){
                        case 1:
                            //down
                            lastDirection = 2;
                            tileDir = parentTile.GetComponent<TileMaster>().masterMapSpawner.GetComponent<MapSpawner>().tileMap[mapPosition.x][mapPosition.y - 1];
                            break;
                        case 2:
                            //right
                            lastDirection = 1;
                            tileDir = parentTile.GetComponent<TileMaster>().masterMapSpawner.GetComponent<MapSpawner>().tileMap[mapPosition.x + 1][mapPosition.y];
                            break;
                    }
                    break;
                case 8:
                    //right up
                    switch(lastDirection){
                        case 1:
                            //right
                            lastDirection = 2;
                            tileDir = parentTile.GetComponent<TileMaster>().masterMapSpawner.GetComponent<MapSpawner>().tileMap[mapPosition.x + 1][mapPosition.y];
                            break;
                        case 2:
                            //up
                            lastDirection = 1;
                            tileDir = parentTile.GetComponent<TileMaster>().masterMapSpawner.GetComponent<MapSpawner>().tileMap[mapPosition.x][mapPosition.y + 1];
                            break;
                    }
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
        public int ratio = 1;

    }
