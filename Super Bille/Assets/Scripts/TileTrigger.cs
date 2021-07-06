using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTrigger : MonoBehaviour
{
    [SerializeField] private Tile tileRelated;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("sphere"))
        {
            tileRelated.SpawnNew();
        }
    }

}
