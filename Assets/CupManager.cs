using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CupManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject[] cupsInPlay;
    public List<GameObject> cupsList;
    private SpawnPointManager spManager;
    public Camera mainCam;
    public Vector3 camOffset;
    public Transform target;
    public float smoothSpeed;

    private int score;
    public TextMeshProUGUI scoreText;


    public GameObject cup;

    public List<Transform> points;

    [SerializeField]
    private GameObject startCup;
    [SerializeField]
    private GameObject secondCup;

    void Start()
    {
        score = 0;
        scoreText.text = "Score: " + score;
        spManager = GetComponent<SpawnPointManager>();
        cupsList = new List<GameObject>();
        updateChildren();
        getCupsInPlay();
        updateList();
        startCup = cupsList[0];
        checkCupCount();

    }

    IEnumerator updateCamPos()
    {
        Debug.Log("running updateCAmPos");
        Vector3 desiredPosition = target.position + camOffset;
        float timeToStart = Time.time;
        Debug.Log(Vector3.Distance(mainCam.transform.position, target.position));
        while (Vector2.Distance(mainCam.transform.position, target.position) > 0.05f)
        {
            Debug.Log("moving camera");
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, target.position + camOffset, smoothSpeed * Time.deltaTime);
             //Here speed is the 1 or any number which decides the how fast it reach to one to other end.

            yield return null;
        }

        Debug.Log("Reached the target.");

        Debug.Log("MyCoroutine is now finished.");
    }
    private void updateChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            points[i] = (transform.GetChild(i));
        }
    }
   
    private void getCupsInPlay()
    {
        cupsInPlay = GameObject.FindGameObjectsWithTag("Cup");
        
    }


    private void updateList()
    {
        cupsList = new List<GameObject>();
        foreach (GameObject cup in cupsInPlay)
        {
          if(cup != null)
            {
                cupsList.Add(cup);
            }
          else
            {
                cupsList.Remove(cup);   
            }

        }
    }

    private void updateScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        if(startCup != null && secondCup != null)
        {
            if (!startCup.GetComponent<Catch>().getInCup() && startCup.GetComponent<Catch>().getIsShot())
            {
                if (secondCup.GetComponent<Catch>().getInCup() && !secondCup.GetComponent<Catch>().getIsShot())

                {
                    Debug.Log("running procedure");
                    cupProcedure();
                }
            }
        }
     
    }
    

    private void cupProcedure()
    {
        GameObject cupToDestroy = startCup;
        startCup = secondCup;
        spawnNewCup();
        getCupsInPlay();
        updateList();
        Destroy(cupToDestroy);
        Debug.Log("going to update cam");
        updateScore();


    }


    private void checkCupCount()
    {
        if(cupsList.Count <= 1)
        {
          
            spawnNewCup();
            

        }
    }

    private void spawnNewCup()
    {
        secondCup = Instantiate(cup, determinePosition(), Quaternion.identity);
        spManager.moveUp();
        StartCoroutine(updateCamPos());
        

    }
    private void updateCupsList()
    {
        foreach (GameObject cup in cupsList)
        {
            if (cup != startCup || cup != secondCup)
            {
                Destroy(cup);
                
            }
        }
    }
    private Vector2 determinePosition()
    {
        Vector2 spawnPosVector = new Vector2(Random.Range(points[0].position.x, points[1].position.x), Random.Range(points[0].position.y, points[3].position.y));
       

            return spawnPosVector;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(points[0].transform.position, points[1].transform.position);
        Gizmos.DrawLine(points[0].transform.position, points[2].transform.position);
        Gizmos.DrawLine(points[2].transform.position, points[3].transform.position);
        Gizmos.DrawLine(points[1].transform.position, points[3].transform.position);
       
    }

}
