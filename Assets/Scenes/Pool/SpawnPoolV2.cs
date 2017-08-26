using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoolV2 : MonoBehaviour {

    Transform m_tPoolsAnchor;
    public List<PrefabPool> m_lPoolsList = new List<PrefabPool>();
    //public PoolManager pm;
    

    public void Awake()
    {
        m_tPoolsAnchor = gameObject.transform;
    }

    //public SpawnPoolV2(PoolManager parent)
    //{
    //    pm = parent;
    //}
    //未完成
    //public GameObject Spawn(Transform tran,Vector3 pos,Quaternion rot)
    //{
    //    GameObject temp1;
    //    if (m_lPoolsList.Count > 0)
    //    {
    //        for (int i = 0; i < m_lPoolsList.Count; i++)
    //        {
    //            if (m_lPoolsList[i].obj == tran.gameObject)
    //            {
    //                m_lPoolsList[i].SpawnInstance(tran, pos, rot);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        PrefabPool prefabpool = new PrefabPool(tran, this);
    //        m_lPoolsList.Add(prefabpool);
    //        prefabpool.SpawnInstance(tran, pos, rot);
    //    }
    //    return temp1;
    //}
    public void Despawn(Transform tran)
    {
        bool Des = false;
        if (m_lPoolsList.Count > 0)
        {
            for (int i = 0; i < m_lPoolsList.Count; i++)
            {
                if (tran.gameObject == m_lPoolsList[i].obj)
                {
                    Des = m_lPoolsList[i].DespawnInstance();
                }
            }
            return;
        }
        if (!Des)
        {
            Debug.Log("请先创建一个游戏对象");
            return;
        }
    }
    public class PrefabPool
    {
        public Transform trans;
        public GameObject obj;
        public SpawnPoolV2 sp;

        public List<Transform> m_lSpawnList = new List<Transform>();
        public List<Transform> m_lDespawnList = new List<Transform>();

        public PrefabPool(Transform tra,SpawnPoolV2 spv2)
            {
                trans = tra;
                obj = tra.gameObject;
                sp = spv2;
            }
        //未完成
        //public GameObject SpawnInstance(Transform transform,Vector3 pos,Quaternion rot)
        //{
        //    GameObject temp;
        //    if (m_lDespawnList.Count > 0)//每个PrefabPool对应一个DespawnList，所以只要里面有值，就全部激活它
        //    {
        //        for (int i = 0; i < m_lDespawnList.Count; i = 0)
        //        {
        //            temp = m_lDespawnList[i].gameObject;
        //            m_lDespawnList[i].gameObject.SetActive(true);
        //            m_lSpawnList.Add(m_lDespawnList[i]);
        //            m_lDespawnList.Remove(m_lDespawnList[i]);
        //        }
        //        return temp;//????????????????????????????????
        //    }
        //        Transform prefab = Instantiate(transform);
        //        temp = prefab.gameObject;
        //        prefab.position = pos;
        //        prefab.rotation = rot;
        //        prefab.parent = sp.m_tPoolsAnchor;
        //        m_lSpawnList.Add(prefab);
        //         return temp;
        //}
        public bool DespawnInstance()
        {
            if (m_lSpawnList.Count > 0)
            {
                for (int i = 0; i < m_lSpawnList.Count; i=0)//每次将i置为1的原因在于每执行一次循环，
                                //m_lSpawnList.Count会减去1，如果再用i++的话，就会一次缩短两个单位；
                {
                    m_lSpawnList[m_lSpawnList.Count - 1].gameObject.SetActive(false);
                    m_lDespawnList.Add(m_lSpawnList[m_lSpawnList.Count - 1]);
                    m_lSpawnList.Remove(m_lSpawnList[m_lSpawnList.Count - 1]);
                
                }
                return true;
            }
           Debug.Log("你想要销毁的对象为空，请先创建一个游戏对象");
           return false;
            
        }
    }
}
/*遇到的比较难解决的问题：问题报错描述ArgumentOutOfRangeException: Argument is out of range.
 *                            指定的参数超出有效值范围，index值不存在
 *问题出在for循环中三条语句的顺序上。截取DespawnInstance中的一段代码作说明；
 * 原代码（出错代码：
 * for (int i = 0; i < m_lSpawnList.Count; i=0)
                {
                1：    m_lSpawnList.Remove(m_lSpawnList[m_lSpawnList.Count - 1]);
                2：    m_lDespawnList.Add(m_lSpawnList[m_lSpawnList.Count - 1]);
                3：    m_lSpawnList[m_lSpawnList.Count - 1].gameObject.SetActive(false);
                
                }
改后的代码：
 for (int i = 0; i < m_lSpawnList.Count; i=0)
                {
                 3：   m_lSpawnList[m_lSpawnList.Count - 1].gameObject.SetActive(false);
                 2：   m_lDespawnList.Add(m_lSpawnList[m_lSpawnList.Count - 1]);
                 1：   m_lSpawnList.Remove(m_lSpawnList[m_lSpawnList.Count - 1]);
                
                }
 首先，1和2的执行顺序，应先Add再Remove，否则会造成Add失败；
        3和1、2的执行顺序，应先执行3，再执行1.循环一次进入到循环体后应先失活当前i对应的游戏对象，
        否则结果就是如果层级列表中有5个游戏对象，执行完后会失活4个，剩下1个游戏对象没有失活成功。
        原因在于执行到最后一次循环时，先将当前游戏对象从SpawnList中删除了，再去设置活性的时候发现
        当前i所指向的游戏对象不存在了，造成i值越界。
     */
