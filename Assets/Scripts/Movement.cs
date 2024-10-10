using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Movement : Stats
{ 
    private void Update()
    {
        float movement = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movement * moveSpeed * Time.deltaTime, 0, 0);
    }
}
