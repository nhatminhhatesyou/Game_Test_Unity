using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMaterialScript : MonoBehaviour
{
    public Material[] mats;

    void Start()
    {
        int matIndex = UnityEngine.Random.Range(0, mats.Length);
        this.GetComponent<Renderer>().material = mats[matIndex];
    }

}
