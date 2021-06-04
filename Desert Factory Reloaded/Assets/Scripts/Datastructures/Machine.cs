using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Machine", menuName = "Machine")]
public class Machine : ScriptableObject
{
    public GameObject machinePrefab;
    public Recipe[] recipes;
}