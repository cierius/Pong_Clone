using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_Screen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        MeshRenderer[] childrenMesh = GetComponentsInChildren<MeshRenderer>();

        foreach(MeshRenderer m in childrenMesh)
        {
            m.enabled = false;
        }        
    }
}
