using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FracturePropManager : MonoBehaviour
{
    [System.Serializable]
    public class FracturableObject
    {
        public GameObject originalObject;    // Normal hali
        public GameObject fracturedPrefab;   // K�r�k hali
    }

    [Header("Fracturable Objects")]
    [SerializeField] private List<FracturableObject> fracturableObjects;

    private Dictionary<GameObject, GameObject> fractureMap = new Dictionary<GameObject, GameObject>();

    void Start()
    {
        // Listeyi Dictionary'ye �evirerek h�zl� eri�im sa�l�yoruz.
        foreach (var item in fracturableObjects)
        {
            if (item.originalObject != null && item.fracturedPrefab != null)
            {
                fractureMap[item.originalObject] = item.fracturedPrefab;
            }
        }
    }

    public void TriggerFracture(GameObject fallingObject)
    {
        if (fractureMap.ContainsKey(fallingObject))
        {
            GameObject fracturedVersion = fractureMap[fallingObject];

            // Orijinal nesneyi yok et ve fractured versiyonunu olu�tur
            Instantiate(fracturedVersion, fallingObject.transform.position, fallingObject.transform.rotation);
            Destroy(fallingObject);
        }
    }
}
