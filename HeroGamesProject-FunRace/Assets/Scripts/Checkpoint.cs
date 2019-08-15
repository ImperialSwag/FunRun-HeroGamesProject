using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform loc;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered checkpoint");
            PlayerMover mover = other.GetComponent<PlayerMover>();
            mover.checkpoint = loc;
            if (this.gameObject.name.Equals("Finish"))
            {
                mover.FinishCourse();
            }
        }
    }
}
