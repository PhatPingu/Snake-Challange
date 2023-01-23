using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMushroom : MonoBehaviour
{
    [SerializeField] private float timer = 10f;

    
    void Update()
    {
        timer -= 0.1f * Time.deltaTime;

        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
