using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public GameObject Monster;
    SpawnPoolV1 sp;
    Transform trans;
    Vector3 vec;
    Quaternion rot;
    private void Awake()
    {
        //GameObject obj = Instantiate(Resources.Load("Prefabs/Cactus1")) as GameObject;
        GameObject obj1 = Instantiate(Resources.Load("Prefabs/SpawnSpot")) as GameObject;

        sp = Monster.GetComponent<SpawnPoolV1>();
        trans = Monster.transform;
        vec = obj1.transform.position;
        rot = obj1.transform.rotation;
    }
    private void OnGUI()
    {
        
        if (GUI.Button(new Rect(0, 0, 100, 100), "Spawn"))
        {
            sp.Spawn(trans,vec,rot);//有错误，还没解决
        }
        if (GUI.Button(new Rect(100, 0, 100, 100), "Despawn"))
        {
            sp.Despawn(trans);
        }
    }
}
