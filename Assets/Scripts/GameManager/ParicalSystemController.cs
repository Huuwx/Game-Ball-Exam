using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParicalSystemController : MonoBehaviour
{
    private static ParicalSystemController instance;
    public static ParicalSystemController Instance { get { return instance; } }

    public ParticleSystem movementPartical;
    public ParticleSystem FallPartical;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
