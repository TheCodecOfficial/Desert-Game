using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusLag : MonoBehaviour
{

    private float size = 1;

    void Update()
    {
        transform.localScale = Vector3.one * size;
        size += Time.deltaTime/10f;
    }
}