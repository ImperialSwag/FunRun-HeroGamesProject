using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public Transform obj, checkpoint;
    public float speed = 0.2f;
    public Animator animController;
    public bool finished = false;
    bool dead = false, respawning = false;

    void Update()
    {
        if (!dead)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        animController.SetBool("isRunning", true);
                        break;
                    case TouchPhase.Ended:
                        animController.SetBool("isRunning", false);
                        break;
                }
            }
        }
        else
        {
            if (!respawning)
            {
                Invoke("Respawn", 3);
                respawning = true;
            }
        }
    }
    void FixedUpdate()
    {
        if (!finished && !dead)
        {
            if (Input.touchCount > 0)
            {
                obj.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Untagged")) { Debug.Log(other.tag); }
        if (other.CompareTag("Obstacle") && !dead)
        {
            dead = true;
            animController.enabled = false;
            Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody rb in bodies)
            {
                rb.isKinematic = false;
            }
        }
    }

    public void FinishCourse()
    {
        finished = true;
        animController.SetBool("finished", true);
        CreateConfetti();
        if(SceneManager.GetActiveScene().name.Equals("Level01"))
        {
            Invoke("StartLevel02", 5);
        } else if (SceneManager.GetActiveScene().name.Equals("Level02"))
        {
            Invoke("StartLevel01", 5);
        }
    }

    void Respawn()
    {
        dead = false;
        animController.enabled = true;
        animController.SetBool("isRunning", false);
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = true;
        }

        obj.SetPositionAndRotation(checkpoint.position, checkpoint.rotation);
        respawning = false;
    }

    void CreateConfetti()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale += new Vector3(-0.8f, -0.8f, -0.8f);
            cube.transform.position = obj.position;
            cube.AddComponent<Rigidbody>();
            cube.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-2f, 2f), Random.Range(40f, 60f), Random.Range(-2f, 2f)));
            cube.AddComponent<Renderer>();
            cube.GetComponent<Renderer>().material.SetColor("_Color", new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f), 1));
            Destroy(cube, 5);
        }
    }

    void StartLevel01()
    {
        SceneManager.LoadScene("Level01");
    }
    void StartLevel02()
    {
        SceneManager.LoadScene("Level02");
    }
}
