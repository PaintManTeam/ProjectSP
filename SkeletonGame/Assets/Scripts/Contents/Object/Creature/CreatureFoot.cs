using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CreatureFoot : MonoBehaviour
{
    public bool IsLandingGround { get; private set; } = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == ETag.Ground.ToString())
        {
            IsLandingGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == ETag.Ground.ToString())
        {
            IsLandingGround = false;
        }
    }
}
