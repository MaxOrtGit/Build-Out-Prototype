using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMaster : MonoBehaviour
{

    private void Update() { 
        
        if (Input.GetMouseButtonDown(0)) {  
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
            RaycastHit hit;  
            if (Physics.Raycast(ray, out hit)) {  
                if (hit.transform.tag == "Tile") {  
                        print("tes2");
                    //if key m is held down
                    if (Input.GetKey(KeyCode.M)) {
                        print("test");
                    }
                }   
            }
        }  
    }
}
