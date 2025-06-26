using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatRecord : MonoBehaviour
{
    [SerializeField] Animator _animator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            Debug.Log("Grabando");
            _animator.SetBool("PlayAnimation", true);
        }
    }
}
