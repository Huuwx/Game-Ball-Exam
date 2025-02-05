using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrail : MonoBehaviour
{
    public Transform ball;
    public float offsetY = -0.5f;

    private void Update()
    {
        if(ball != null)
        {
            transform.position = new Vector3(ball.position.x, ball.position.y + offsetY, ball.position.z);
        }
    }
}
