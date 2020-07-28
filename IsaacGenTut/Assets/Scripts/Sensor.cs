using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Sensor : MonoBehaviour
{
    public GameObject target;
    public float hearingThreshold = 7f;
    public float smellingThreshold = 7f;
    public bool isTouching = false;
    public bool isUsingNavigationDistance = true;
    NavMeshPath path = null;


    // Start is called before the first frame update
    void Start()
    {
        path = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching)
        {
            GetComponent<Renderer>().material.color =
                new Color(0, 255, 0);
            return;
        }
        bool isClose = false;
        if (isUsingNavigationDistance)
        {
            isClose = IsSmelled();
        }
        else
        {
            isClose = IsHeard();
        }

        if (isClose)
        {
            GetComponent<Renderer>().material.color =
                new Color(0, 0, 200);
        }
        else
        {
            GetComponent<Renderer>().material.color =
                new Color(200, 0, 200);
        }
    }


    public bool IsHeard()
    {
        float hearingDistance =
            Vector2.Distance(transform.position,
                target.transform.position);
        Debug.Log("hearing distance: " + hearingDistance);
        return (hearingDistance < hearingThreshold);

    }

    public bool IsSmelled()
    {
        bool isThereAPath = GetPath(path,
            transform.position,
            target.transform.position);
        if (!isThereAPath)
            return false;
        float smellingDistance =
            GetPathLength(path);
        Debug.Log("smelling distance: " + smellingDistance);
        return (smellingDistance < smellingThreshold);

    }

    public bool GetPath(NavMeshPath path, Vector2 fromPos, Vector2 toPos, int passableMask = NavMesh.AllAreas)
    {
        path.ClearCorners();

        if (NavMesh.CalculatePath(fromPos, toPos, passableMask, path) == false)
            return false;

        return true;
    }

    public float GetPathLength(NavMeshPath path)
    {
        float lng = 0.0f;

        if ((path.status != NavMeshPathStatus.PathInvalid) && (path.corners.Length > 1))
        {
            for (int i = 1; i < path.corners.Length; i++)
            {
                lng += Vector2.Distance(path.corners[i - 1], path.corners[i]);
            }
        }

        return lng;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered: " + other.name);
        if (other.name == "Other")
        {
            isTouching = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exited: " + other.name);
        if (other.name == "Other")
        {
            isTouching = false;
        }
    }
}
