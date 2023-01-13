using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeColor : MonoBehaviour
{
    [SerializeField] private Material[] allMaterials;
    [SerializeField] private GameObject Body;
    [SerializeField] private GameObject Ears;
    [SerializeField] private GameObject Legs;
    [SerializeField] private GameObject Arms;
    void Start()
    {
        Body.GetComponent<SkinnedMeshRenderer>().material = allMaterials[Random.Range(0, allMaterials.Length)];
        Ears.GetComponent<SkinnedMeshRenderer>().material = allMaterials[Random.Range(0, allMaterials.Length)];
        Legs.GetComponent<SkinnedMeshRenderer>().material = allMaterials[Random.Range(0, allMaterials.Length)];
        Arms.GetComponent<SkinnedMeshRenderer>().material = allMaterials[Random.Range(0, allMaterials.Length)];
    }

}
