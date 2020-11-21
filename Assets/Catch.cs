using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catch : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isInCup;
    private bool isShot;
    public Transform ballLocation;
   
    public void setInCup(bool inCup)
    {
        isInCup = inCup;
    }

    public bool getInCup()
    {
        return isInCup;
    }
    public Transform getBallLocation()
    {
        return ballLocation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool getIsShot()
    {
        return isShot;
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        isInCup = true;
        isShot = false;
        Debug.Log(collision.transform.name);
        Debug.Log("ball is in cup");
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        isInCup = false;
        isShot = true;
        Invoke("rotateToPlace", 1f);
        Debug.Log("ball left the cup");
    }
    private void rotateToPlace()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, .75f);
    }
}
