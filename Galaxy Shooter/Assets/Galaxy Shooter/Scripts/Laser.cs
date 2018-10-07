﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _travelSpeed = 10f;

    void Update()
    {
        transform.Translate(Vector3.up * _travelSpeed * Time.deltaTime);

        if (transform.position.y >= 6)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }
}