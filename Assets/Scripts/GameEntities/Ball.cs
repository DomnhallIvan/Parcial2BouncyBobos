using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Referencias")]
    public Rigidbody rb;
    //private GameManager gameManager;

    [Header("Datos")]
    private int porteriaLayer;

    private void Awake()
    {
        porteriaLayer = LayerMask.NameToLayer("Porteria");
        //gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        GameManager.instance.onReset += ReturnToPool;
    }

    private void OnCollisionEnter(Collision collision)
    {       
        if (collision.gameObject.layer==porteriaLayer)
        {
            ScoreZone scoreZone = collision.gameObject.GetComponent<ScoreZone>();
            if (scoreZone)
                GameManager.instance.OnScoreZoneReached(scoreZone.id);

            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        ObjectPool pool = FindObjectOfType<ObjectPool>();
        if (pool != null)
        {
            pool.ReturnToPool(gameObject);
        }
    }
}
