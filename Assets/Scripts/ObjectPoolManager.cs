using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public GameObject prefab;
        public int poolSize = 50;

        private List<GameObject> pool;

        void Awake()
        {
            pool = new List<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                pool.Add(obj);
            }
        }

        public GameObject GetObject()
        {
            foreach (GameObject obj in pool)
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    print("Reusing object from pool");
                    return obj;
                }
            }

            // Optional: Expand pool if needed
            print("Creating new object for pool");
            GameObject newObj = Instantiate(prefab);
            newObj.SetActive(true);
            pool.Add(newObj);
            return newObj;
        }

        public void ReturnObject(GameObject obj)
        {
            obj.SetActive(false);
        }
    }

}
