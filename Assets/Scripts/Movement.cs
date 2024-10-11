using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Movement : Stats
{
    [SerializeField]private bool isPlayer;

    private void FixedUpdate()
    {
        if(isPlayer)
        {
            MovimientoSexy();
        }
        else
        {
            
        }
        
        
    }

    private void MovimientoSexy()
    {
        float movement = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movement * moveSpeed * Time.deltaTime, 0, 0);
        //6.41,-6.41
    }
}
