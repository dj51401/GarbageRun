using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Trash Asset", menuName = "Trash")]
public class TrashScriptableObject : ScriptableObject
{
    public int pointValue;
    public float slowDownSpeed;
    public float trashWeight;
    public float trashDrag;

}
