using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startup : MonoBehaviour {

    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            Destroy(GameObject.Find("Cube"));
            //GameObject.Find("Cube").GetComponent<AA>().enabled=false;
        }
    }
}
