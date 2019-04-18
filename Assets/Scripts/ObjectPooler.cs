using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour 
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public static ObjectPooler instance;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () 
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool p in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            int pizzaCount = 0;

            for (int i = 0; i < p.size; i ++)
            {
                GameObject obj = Instantiate(p.prefab);

                if (obj.tag == "Pizza")
                {
                    obj.GetComponent<SpriteRenderer>().sortingOrder = (pizzaCount * 2) + 1;

                    if (obj.GetComponent<PizzaBehaviour>().pizzaSize == PizzaBehaviour.PizzaSizes.Small)
                    {
                        obj.GetComponent<SpriteRenderer>().sortingOrder++;
                    }

                    pizzaCount++;
                }

                obj.SetActive(false);
                objectPool.Enqueue(obj);

            }

            poolDictionary.Add(p.tag, objectPool);
        }
	}

    public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion rot)
    {

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with Tag " + tag + " Does Not Exist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.transform.position = pos;
        objectToSpawn.transform.rotation = rot;
        objectToSpawn.SetActive(true);

        iPoolerObject poolerObj = objectToSpawn.GetComponent<iPoolerObject>();

        if (poolerObj != null)
        {
            poolerObj.OnSpawnedByPooler();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
