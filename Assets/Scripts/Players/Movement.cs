using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Windows.Speech;

public class Movement : Stats
{
    [Header("References")]
    //public Ball ball;
    private Rigidbody playerRb;
    [SerializeField] private GameObject PlayerBarrier;

    [Header("AI")]
    public float aiDeadzone = 1f;
    public float aiMoveSpeedMultiplierMin = 0.5f, aiMoveSpeedMultiplierMax = 1.5f;
    private float timeLastSpeedChange = 0f, minTimeSpeedChange = 1f;

    private float moveSpeedMultiplier = 1f;
    private float direction = 0;
    private Vector3 startPosition;
    //private Vector3 ballPos;

    //Collider[] Balls;
    [SerializeField] private LayerMask ballLayer;
    [SerializeField] private int _radius;
    [SerializeField]private bool isPlayer;
    [SerializeField] private bool isLeft;

    private void Start()
    {
        startPosition=transform.position;
        playerRb=gameObject.GetComponent<Rigidbody>();
        GameManager.instance.onReset += GameEnd;        
    }

    private void GameEnd()
    {
        direction = 0;
        transform.position=startPosition;
        PlayerBarrier.SetActive(false);
        healthPoints = 20;
        isDead=false;
    }

    public void Dead()
    {
        PlayerBarrier.SetActive(true);
    }

    private void FixedUpdate()
    {
        if(isPlayer && !isDead)
        {
            float movement = GetInput();
            MovimientoSexy(movement);
        }
        else 
        {
            //CheckSphere();
            MoveAI();
        }                
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }    


    private void MoveAI()
    {
        if(!isDead)
        {            
            timeLastSpeedChange += Time.deltaTime;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius, ballLayer);

            if (hitColliders.Length > 0)
            {
                Vector3 ballPos = hitColliders[0].transform.position;
                float distanceToBall = Vector3.Distance(transform.position, ballPos);
                if (isLeft)
                {
                    if (Mathf.Abs(ballPos.z - transform.position.z) > aiDeadzone)
                    {
                        direction = ballPos.z > transform.position.z ? 1 : -1;
                    }

                    if (timeLastSpeedChange >= minTimeSpeedChange && Random.value < 0.1f) //Tiene una chance de 1 entre 10 chances en cada segundo en cambiar de velocidad
                    {
                        moveSpeedMultiplier = Random.Range(0.35f, 1.5f);
                        timeLastSpeedChange = 0f;
                    }
                    MovimientoSexyL(direction);
                    // transform.position = new Vector3(startPosition.x, 1, ballPos.z);
                }
                else
                {
                    if (Mathf.Abs(ballPos.x - transform.position.x) > aiDeadzone)
                    {
                        direction = ballPos.x > transform.position.x ? 1 : -1;
                    }

                    if (timeLastSpeedChange >= minTimeSpeedChange && Random.value < 0.1f) //Tiene chance cada 3 seg de cambiar velocidad
                    {
                            moveSpeedMultiplier = Random.Range(0.50f, 1.5f);
                            timeLastSpeedChange = 0f;                        
                    }
                    MovimientoSexy(direction);
                    //transform.position = new Vector3(ballPos.x, 1, startPosition.z);
                }

            }
            else //Si no tiene ninguna pelota en la vista, entonces regresa en al punto medio de su porteria
            {
                if(isLeft)
                {
                    
                    if (Mathf.Abs(startPosition.z - transform.position.z) > aiDeadzone)
                    {
                        direction = startPosition.z > transform.position.z ? 1 : -1;
                    }
                    MovimientoSexyL(direction);
                }
                if(!isLeft)
                {
                    
                    if (Mathf.Abs(startPosition.x - transform.position.x) > aiDeadzone)
                    {
                        direction = startPosition.x > transform.position.x ? 1 : -1;
                    }
                    MovimientoSexy(direction);
                }
            }
        }
    }

    private float GetInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    private void MovimientoSexy(float movement)
    {
        Vector3 velo = playerRb.velocity;
        velo.x = moveSpeed * moveSpeedMultiplier * movement;
        playerRb.velocity = velo;
        //transform.position += new Vector3(movement * moveSpeed * Time.deltaTime, 0, 0);
        //6.41,-6.41
    }

    private void MovimientoSexyL(float movement)
    {
        Vector3 velo = playerRb.velocity;
        velo.z = moveSpeed * moveSpeedMultiplier * movement;
        playerRb.velocity = velo;
    }
}
