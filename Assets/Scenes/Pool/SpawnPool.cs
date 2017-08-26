using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//人物SpawnPool
public class SpawnPool : MonoBehaviour {
    //脚本所挂接游戏对象的transform
    public Transform m_Group;
    private List<PrefabPool> m_lPrefabPools = new List<PrefabPool>();
    private void Awake()
    {
        
    }
    //激活相应的怪物
    //返回该怪物的transform
    public Transform Spawn(Transform trans, Vector3 pos, Quaternion rot)
    {
        Transform active;
        //先查找m_lPrefabPools列表中有没有想要激活的怪物
        //有的话调用接口激活这个怪物（DespawnList中有的情况下）或创建该怪物的游戏对象
        //没有的话调用接口新建一个PrefabPool，加到m_lPrefabPools中
        //创建该怪物的游戏对象，添加到m_lSpawn中
        //PrefabPool prefabpool = new PrefabPool(trans);
        //m_lPrefabPools.Count == 0 ? 
        for (int i = 1; i <= m_lPrefabPools.Count; i++)
        {
           // PrefabPool prefabpool = m_lPrefabPools[i];
            if (m_lPrefabPools[i].m_obj == trans.gameObject)
            {
                active = m_lPrefabPools[i].SpawnInstance(pos,rot);
                active.parent = m_Group;//??????????????????????????????????????????????????
                return active;
            }
        }
        PrefabPool pref = new PrefabPool(trans);
        m_lPrefabPools.Add(pref);
        pref.SpawnNew(pos, rot).parent = m_Group;//????????????????????????????????????????????
        return pref.SpawnNew(pos,rot);

    }
    //public Transform Despawn(Transform Destrans)
    //{
    //    Transform Disable;
    //    for(int i = 1;i <= )
    //}
    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 100), "Spawn"))
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/Cactus1")) as GameObject;
            Spawn(obj.transform,obj.transform.position,obj.transform.rotation);
        }
    }
    public class PrefabPool
    {
        public Transform m_trans;
        public GameObject m_obj;
        public SpawnPool sp;

        public List<Transform> m_lSpawn = new List<Transform>();
        public List<Transform> m_lDespawn = new List<Transform>();
       
        public PrefabPool(Transform prefab)
        {
            m_trans = prefab;
            m_obj = prefab.gameObject;

        }
        public Transform SpawnInstance(Vector3 pos, Quaternion rot)
        {
            if (m_lDespawn.Count > 0)
            {
                for (int i = 1; i <= m_lDespawn.Count; i++)
                {
                    m_lDespawn[i].gameObject.SetActive(true);
                    m_lDespawn.Remove(m_lSpawn[i]);
                    m_lSpawn.Add(m_lSpawn[i]);
                    // m_lDespawn[i].parent = sp.m_Group;//??????????????????????????????????????/
                    return m_lDespawn[i];
                }
            }
            return SpawnNew(pos, rot);
        }
        public Transform SpawnNew(Vector3 pos, Quaternion rot)
        {
            Transform tran = Instantiate(m_trans);
            GameObject obj = tran.gameObject;
            tran.position = pos;
            tran.rotation = rot;
            m_lSpawn.Add(tran);
            //Parent?????????????????????????????????????????????????????????????
            return tran;
        }
        public bool DespawnInstance(Transform trans)
        {
            if (m_lSpawn.Count != 0)
            {
                for (int i = 1; i <= m_lSpawn.Count; i++)
                {
                    if (trans.gameObject == m_lSpawn[i].gameObject)
                    {
                        m_lSpawn[i].gameObject.SetActive(false);
                        m_lSpawn.Remove(m_lDespawn[i]);
                        m_lDespawn.Add(m_lDespawn[i]);
                        
                    }
                }
                return true;
            }
            else
                return false;
        }

          
    }
}
