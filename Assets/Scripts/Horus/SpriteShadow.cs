using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Set the shadow casting mode to on. Making debug unnecessary.
        GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }

    public static implicit operator UnityEngine.Sprite(Sprite v)
    {
        throw new NotImplementedException();
    }
}
