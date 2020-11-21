using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallUpkeep : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform walls;
    Vector2 wallPos;
    public float smoothSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 desiredPos = new Vector2(0f, this.transform.position.y);
        wallPos = Vector2.Lerp(walls.position, desiredPos, smoothSpeed * Time.deltaTime);
        walls.position = wallPos;
    }
    public void moveWalls()
    {
        
    }
}
