using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ballFling : MonoBehaviour
{
    // Start is called before the first frame update
    private bool canAim;

    public Transform mousePos;

    private LineRenderer lr;

    public GameObject cup;
    private bool isAiming;
    private Vector2 start;
    private Vector2 end;

    private Vector2 direction;

    [SerializeField]
    [Range(0, 10f)]
    private float maxDistance = 5f;

    private Vector2 cupRotation;

    public GameObject trajectoryDotPrefab;

    [SerializeField]
    private float distance;

    private List<GameObject> dotLists;

    [SerializeField]
    private int NUM_DOTS_TO_SHOW = 30;
    private float DOT_TIME_STEP = 0.05f;

    [SerializeField]
    private float force;

    [SerializeField]
    private Vector2 pushForce;

    [SerializeField]
    private float maxPoints;

    private Vector3 zOffset;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        dotLists = new List<GameObject>();
        zOffset = new Vector3(0f, 0f, -10);

    }

    // Update is called once per frame
    void Update()
    {
        
        if(canAim)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb.transform.parent = cup.transform;
                rb.isKinematic = true;
                calculateStart();
                startDots();

            }
            if (Input.GetMouseButton(0))
            {
               
                
                calculateTrejectory();
                calculateCupRotation();
                

            }
            else if (Input.GetMouseButtonUp(0))
            {
                rb.transform.parent = null;
                rb.isKinematic = false;
                calculateStop();
            }
        }
      

    }


    void calculateTrejectory()
    {
        end = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        distance = Mathf.Clamp(Vector2.Distance(start, end), 0f, maxDistance);
        Debug.DrawLine(start, end, Color.green);
        cupRotation = direction;
        direction = (start - end).normalized;
        pushForce = direction * distance * force;
        showDots();

    }
    void calculateCupRotation()
    {
        Vector2 diference = end - start;
        float sign = (end.x < start.x) ? -1.0f : 1.0f;
        float rotation = Vector2.Angle(Vector2.down, diference) * sign;

        Quaternion cupRotation = Quaternion.Euler(cup.transform.rotation.x,cup.transform.rotation.y,rotation);
        
        cup.transform.rotation = Quaternion.Lerp(cup.transform.rotation, cupRotation, .5f);
    }
    void startDots()
    {
      
        for (int i = 0; i < NUM_DOTS_TO_SHOW; i++)
        {
            
            GameObject trajectoryDot = Instantiate(trajectoryDotPrefab);
            trajectoryDot.transform.position = CalculatePosition(DOT_TIME_STEP * i);
            dotLists.Add(trajectoryDot);
           
        }
    }
    void showDots()
    {
        for (int i = 0; i < NUM_DOTS_TO_SHOW; i++)
        {

            dotLists[i].transform.position = CalculatePosition(DOT_TIME_STEP * i);

        }
    }
    void endDots()
    {
        for (int i = 0; i < NUM_DOTS_TO_SHOW; i++)
        {

            Destroy(dotLists[i]);

        }

        dotLists.Clear();
            
            

        
    }
    private void FixedUpdate()
    {
    
    }

    void calculateStart()
    {
        

        isAiming = true;
        start = Camera.main.ScreenToWorldPoint(Input.mousePosition);


    }
    void calculateStop()
    {
        
        isAiming = false;
        
        end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       
        rb.AddForce(pushForce, ForceMode2D.Impulse);
        endDots();
        
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(this.transform.position, maxDistance);

    }

    
    public void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.transform.tag.Equals("Cup"))
        {
            
            cup = collision.gameObject;
            this.canAim = true;

        }
    }
   

    public void OnTriggerExit2D(Collider2D collision)
    {
       
        if (collision.transform.tag.Equals("Cup"))
        {

            cup = collision.gameObject;
             this.canAim = false;
                
            
        }
    }

    private Vector2 CalculatePosition(float elapsedTime)
    {
        return new Vector2(Physics2D.gravity.x, Physics2D.gravity.y) * elapsedTime * elapsedTime * 0.5f +
                   pushForce * elapsedTime + new Vector2(this.transform.position.x, this.transform.position.y);
    }
   
    

}
    
        






