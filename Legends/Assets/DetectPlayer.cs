using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class DetectPlayerAndSpawn : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject objectToSpawn;
   

    private void Start()
    {
        objectToSpawn.SetActive(false);
    }
    private void Reset()
    {
        var collider = GetComponent<SphereCollider>();
        collider.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectToSpawn.SetActive(true);
        }
    }
}
