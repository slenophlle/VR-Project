using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vase : MonoBehaviour

{
    [SerializeField] private GameObject brokenVase;
    [SerializeField] private GameObject vase;

    BoxCollider bc;

    private void Awake()
    {
        vase.SetActive(true);
        brokenVase.SetActive(false);

        bc =GetComponent<BoxCollider>();

    }

    private void OnMouseDown()
    {
        Break();
    }

    private void Break()
    {
        vase.SetActive(false);
        brokenVase.SetActive(true);
    }
}
