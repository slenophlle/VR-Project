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
        if (rb.velocity.y < -2f) // Eðer nesne hýzlýca düþüyorsa
        {
            StartCoroutine(CheckIfLanded());
        }
    }

    IEnumerator CheckIfLanded()
    {
        yield return new WaitForSeconds(0.5f); // Biraz bekleyerek zemine temas edip etmediðini kontrol et

        if (rb.velocity.magnitude < 0.1f) // Hareketsiz kaldýysa
        {
            fractureManager.TriggerFracture(gameObject);
        }
    }
}
