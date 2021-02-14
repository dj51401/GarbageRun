using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageTruck : MonoBehaviour
{
    public int points;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Grabable")
        {
            AddScore(other.gameObject);
            StartCoroutine(DestroyTrash(other.gameObject));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Grabable")
        {
            RemoveScore(other.gameObject);
            StopAllCoroutines();
        }
    }

    private void Update()
    {
         
    }

    void AddScore(GameObject trash)
    {
        int newScore = trash.GetComponent<TrashScript>().so.pointValue;
        points += newScore;
    }

    void RemoveScore(GameObject trash)
    {
        int newScore = trash.GetComponent<TrashScript>().so.pointValue;
        points -= newScore;
    }

    IEnumerator DestroyTrash(GameObject obj)
    {
        yield return new WaitForSeconds(2);
        obj.SetActive(false);
    }

}
