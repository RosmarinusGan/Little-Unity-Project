using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(setTrig());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator setTrig()
    {
        yield return new WaitForSeconds(10f);
        animator.SetTrigger("trig");
        StartCoroutine(setTrig());
    }
}
