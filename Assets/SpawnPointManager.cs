using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public List<Transform> points;
    public Vector3 upMove;

    // Start is called before the first frame update
    void Start()
    {
        updateChildren();
    }
   
    public void updateChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            points[i] = (transform.GetChild(i));
        }
    }
    public void moveUp()
    {
        foreach (Transform point in points)
        {
            point.position = upMove + point.position;
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }

}
