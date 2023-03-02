using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Update()
    {
        transform.position += transform.right * 0.25f;
    }
}