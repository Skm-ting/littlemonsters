using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestV1 : MonoBehaviour
{
    GameObject cube;
    SpawnPoolV2 sp;
    GameObject obj;
    void Awake()
    {
        cube = Resources.Load("Prefabs/Sphere") as GameObject;
    }
    void Start()
    {
        obj = Instantiate(Resources.Load("Prefabs/SpawnSpot")) as GameObject;
        sp = obj.GetComponent<SpawnPoolV2>();
    }
    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 100), "Spawn"))
        {
            //未完成
            //sp.Spawn(cube.transform,obj.transform.position,obj.transform.rotation);
        }
        if (GUI.Button(new Rect(100, 0, 100, 100), "Despawn"))
        {
            sp.Despawn(cube.transform);
        }
    }
}
