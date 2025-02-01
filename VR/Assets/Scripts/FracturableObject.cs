using System.Collections;
using UnityEngine;

public class FracturableObject : MonoBehaviour
{
    private FracturePropManager fractureManager;
    private Rigidbody rb;

    void Start()
    {
        fractureManager = FindObjectOfType<FracturePropManager>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rb.velocity.y < -2f) // E�er nesne h�zl�ca d���yorsa
        {
            StartCoroutine(CheckIfLanded());
        }
    }

    IEnumerator CheckIfLanded()
    {
        yield return new WaitForSeconds(0.5f); // Biraz bekleyerek zemine temas edip etmedi�ini kontrol et

        if (rb.velocity.magnitude < 0.1f) // Hareketsiz kald�ysa
        {
            fractureManager.TriggerFracture(gameObject);
        }
    }
}
