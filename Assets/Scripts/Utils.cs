using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static GameObject InstantiatePrefab(string prefabPath, Transform parent = null)
    {
        GameObject resource = (GameObject)Resources.Load(prefabPath);
        if (resource == null)
        {
            Debug.LogError($"Prefab Resources load failed({prefabPath})");
        }
        GameObject gameObject = MonoBehaviour.Instantiate(resource, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        if (parent)
        {
            gameObject.transform.SetParent(parent, false);
        }
        return gameObject;
    }
}
