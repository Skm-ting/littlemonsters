using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoolV1 : MonoBehaviour
{
    public Transform m_Group;
    List<PrefabPool> m_lPrefabPools = new List<PrefabPool>();
    private void Awake()
    {
        m_Group = gameObject.transform;
    }
    public Transform Spawn(Transform tra, Vector3 pos, Quaternion rot)
    {
        //1.先判断当前m_PrefabPools列表是否大于零,否的话（即等于零，当前列表为空，当前场景中从未创建任何游戏对象的对象池）
        ////新建一个当前要创建的游戏对象的PrefabPool,将其添加到m_lPrefabPools列表，将其父对象设为m_Group，
        //2.转到SpawnNew 
        //3.大于零，开始for循环查找当前列表中是否有想要创建的游戏对象的PrefabPool（tra.gameobject == m_lPrefabPools[i].obj）
        //  没有的话执行2
        //4.有的话，转到SpawnInstance
        if (null == m_lPrefabPools)
        {
            Debug.Log("需要初始化");
        }
        if (m_lPrefabPools.Count > 0)
        {
            for (int i = 1; i <= m_lPrefabPools.Count; i++)
            {
                if (m_lPrefabPools[i].obj == tra.gameObject)
                {
                    return m_lPrefabPools[i].SpawnInstance(pos, rot);
                }
            }
        }
        Debug.Log("开始进入New PrefabPool");
        //return NewPrefabPool(tra,pos,rot);
        PrefabPool prefabpool = new PrefabPool(tra,this);
        m_lPrefabPools.Add(prefabpool);
        //prefabpool.sp = this;
        //prefabpool.SpawnNew(pos, rot).parent = m_Group;
        return prefabpool.SpawnNew(pos, rot);
    }
    //public Transform NewPrefabPool(Transform trans,Vector3 pos,Quaternion rot)
    //{
    //    PrefabPool prefabpool = new PrefabPool(trans);
    //    m_lPrefabPools.Add(prefabpool);
    //    prefabpool.SpawnNew(pos, rot).parent = m_Group;
    //    return prefabpool.SpawnNew(pos, rot);
    //}
    public void Despawn(Transform trans)
    {
        bool Des = false;
        //开始for循环在m_lPrefabPools.Count列表中查找想要销毁的游戏对象，没有的话则代码逻辑错误，debug.log
        //有的话调用DespawnInstance// 
        for(int i = 0; i <= m_lPrefabPools.Count; i++)
        {
            if (m_lPrefabPools[i].m_lSpawn.Contains(trans))
            {
                Des = m_lPrefabPools[i].DespawnInstance(trans);
                //for (int j = 1; j <= m_lPrefabPools[i].m_lSpawn.Count; j++)
                //{
                //    if (trans == m_lPrefabPools[i].m_lSpawn[j])
                //    {
                //        Des = m_lPrefabPools[i].DespawnInstance(trans);
                //    }
                //    else if (trans == m_lPrefabPools[i].m_lDespawn[j])
                //    {
                //        return;
                //    }
                //}
            }
            else return;
        }
        if (!Des)
        {
            return;
        }
    }
    //if (m_lPrefabPools.Count > 0)
    //{
    //    for (int i = 1; i <= m_lPrefabPools.Count; i++)
    //    {
    //        if (m_lPrefabPools[i].obj == trans.gameObject)
    //        {
    //            if (m_lPrefabPools[i].DespawnInstance(trans) == false)
    //            {
    //                m_lPrefabPools[i].obj.SetActive(m_lPrefabPools[i].DespawnInstance(trans));
    //            }
    //            else
    //            {
    //                Debug.Log("你想要销毁的对象为空，请先创建一个游戏对象");
    //            }
    //        }
    //    }
    //}
    //else
    //{
    //    Debug.Log("你想要销毁的对象为空，请先创建一个游戏对象");
    //}

    public class PrefabPool
    {
        public Transform trans;
        public GameObject obj;
        public SpawnPoolV1 sp;

        public List<Transform> m_lSpawn = new List<Transform>();
        public List<Transform> m_lDespawn = new List<Transform>();
        public PrefabPool(Transform Prefab,SpawnPoolV1 spawnpool)
        {
            trans = Prefab;
            obj = Prefab.gameObject;
            sp = spawnpool;
        }
        public Transform SpawnNew(Vector3 pos, Quaternion rot)
        {
            //通过instantiate复制一个想要创建的游戏对象，将其添加到m_lSpawn
            Transform prefab = Instantiate(trans);
            GameObject PreObj = prefab.gameObject;
            prefab.position = pos;
            prefab.rotation = rot;
            prefab.parent = sp.m_Group;
            m_lSpawn.Add(prefab);
            return prefab;
        }
        public Transform SpawnInstance(Vector3 pos, Quaternion rot)
        {
            //先判断当前m_lDespawn列表是否大于零，否的话转到SpawnNew
            //是的话开始for循环查找当前PrefabPool的m_lDespawn列表中是否有想要创建的游戏对象，否的话转到SpawnNew
            //  有的话，通过obj.setactive[true]将其激活，在m_lDespawn中删除，添加到m_lSpawn，返回tra

            if (m_lDespawn.Count > 0)
            {
                for (int i = 0; i <= m_lDespawn.Count; i++)
                {
                    if (trans.gameObject == m_lDespawn[i])
                    {
                        m_lDespawn[i].gameObject.SetActive(true);
                        m_lDespawn.Remove(m_lDespawn[i]);
                        m_lSpawn.Add(m_lDespawn[i]);
                        return trans;
                    }
                }
            }
            return SpawnNew(pos, rot);
        }
        public bool DespawnInstance(Transform trans)
        {
            trans.gameObject.SetActive(false);
            m_lSpawn.Remove(trans);
            m_lDespawn.Add(trans);
            return true;
            //for循环查找当前m_lSpawn列表中是否有想要销毁的游戏对象
            //没有的话返回true，逻辑错误
            //有的话将其添加到m_lDespawn列表中，在m_lSpawn列表中删除
            //返回false
            //
            //if (m_lSpawn.Count > 0)
            //{
            //    for (int i = 1;i <= m_lSpawn.Count;i++)
            //    {
            //        if (trans.gameObject == m_lSpawn[i])
            //        {
            //            m_lSpawn[i].gameObject.SetActive(false);
            //            m_lSpawn.Remove(m_lSpawn[i]);
            //            m_lDespawn.Add(m_lSpawn[i]);
            //        }  
            //    }
            //}
            //else
            //{
            //    Debug.Log("你想要销毁的对象为空，请先创建一个游戏对象");
            //}
        }
    }
}
   

