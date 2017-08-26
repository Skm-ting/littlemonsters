using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utilities;

public class PoolManager : MonoBehaviour
{
    public static Transform m_tSpawnPoolsAnchor;
    public static List<SpawnPoolV2> m_lSpawnPoolsList = new List<SpawnPoolV2>();

    private static SpawnPoolV2 sp;

    private void Awake()
    {
        m_tSpawnPoolsAnchor = gameObject.transform;
    }

    public static void SetSpawnPoolByName(string name)
    {
        name.Replace("Pool","");
        string nomal = name + "Pool";
        GameObject obj = new GameObject(nomal);
        sp = obj.GetOrAddComponent<SpawnPoolV2>();
        obj.transform.parent = m_tSpawnPoolsAnchor;
        m_lSpawnPoolsList.Add(sp);
    }
    //未完成
    //public GameObject ins(string path)
    //{
    //    GameObject obj = Resources.Load(path) as GameObject;
    //    string SpawnPoolName = obj.name.Substring(0,name.Length-1) + "Pool";
    //    if (m_lSpawnPoolsList.Count > 0)
    //    {
    //        for (int i = 0; i < m_lSpawnPoolsList.Count; i++)
    //        {
    //            if (SpawnPoolName == m_lSpawnPoolsList[i].name)
    //            {
    //                m_lSpawnPoolsList[i].Spawn(obj.transform,obj.transform.position,obj.transform.rotation);
    //            }
    //        }
    //    return ;//???????????????????????
    //    }

    //}

    public void Des()
    {

    }
}
