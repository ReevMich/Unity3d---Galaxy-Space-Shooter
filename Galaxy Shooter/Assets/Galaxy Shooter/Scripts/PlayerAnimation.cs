using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animation;

    private void Start()
    {
        _animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0)
        {
            _animation.SetBool("Turn_Left", false);
            _animation.SetBool("Turn_Right", true);
        }
        else if (horizontalInput < 0)
        {
            _animation.SetBool("Turn_Left", true);
            _animation.SetBool("Turn_Right", false);
        }
        else
        {
            _animation.SetBool("Turn_Left", false);
            _animation.SetBool("Turn_Right", false);
        }
    }
}
