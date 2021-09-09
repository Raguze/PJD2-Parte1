using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public List<GameObject> prefabs = new List<GameObject>();

    private void Awake() {
        // prefabs.Add(Resources.Load<GameObject>("Prefabs/Block-1-1"));
        // prefabs.Add(Resources.Load<GameObject>("Prefabs/Block-2-1"));
        // prefabs.Add(Resources.Load<GameObject>("Prefabs/Block-5-1"));
        // prefabs.Add(Resources.Load<GameObject>("Prefabs/Block-10-1"));

         prefabs.AddRange(Resources.LoadAll<GameObject>("Prefabs/Blocks"));

        foreach (var prefab in prefabs)
        {
            Vector3 position = new Vector3(Random.Range(-8,8),Random.Range(-2,2),0);
            Instantiate<GameObject>(prefab,position,Quaternion.identity);
        }

    }
}
