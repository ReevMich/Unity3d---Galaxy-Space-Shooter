﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{

    private void Start()
    {
        Destroy(gameObject, 4f);
    }

}
