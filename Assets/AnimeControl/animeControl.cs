using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class animeControl : MonoBehaviour
{
    private Animator anim;
    private bool isJump;
    private bool isSlide;
    private float timeTarget = 3f;
    private int cal = 1;
    public float speed;

    public Transform rightFoot;
    public float matchStart, matchEnd;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isJump = false;
        isSlide = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Blend Tree") || anim.GetCurrentAnimatorStateInfo(0).IsName("SLIDE00"))
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical") * 0.5f;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                v *= 2f;
                if (Input.GetKey(KeyCode.C))
                {
                    isSlide = true;
                }
                else
                {
                    isSlide = false;
                }
            }
            anim.SetBool("slide", isSlide);
            anim.SetFloat("turn", h, 0.1f, Time.deltaTime);
            anim.SetFloat("speed", v, 0.1f, Time.deltaTime);
            this.transform.Translate(speed * h * Time.deltaTime, 0, speed * v * Time.deltaTime);

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("SLIDE00"))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    anim.SetTrigger("slideJump");
                }
            }
        }
        

        if (Input.GetKey(KeyCode.F))
        {
            anim.SetTrigger("waitState");
        }

        
        if( anim.GetCurrentAnimatorStateInfo(0).IsName("WAIT01") || 
            anim.GetCurrentAnimatorStateInfo(0).IsName("WAIT02") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("WAIT03") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("WAIT04"))
        {
            if (Input.anyKey && !Input.GetKey(KeyCode.F))
            {
                cal = 1;
                anim.SetTrigger("recover");
            }
            else
            {
                anim.SetTrigger("middleState");
            }

        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Middle"))
        {
            if (Input.anyKey && !Input.GetKey(KeyCode.F))
            {
                cal = 1;
                anim.SetTrigger("recover");
            }

            if (timeCal())
            {
                cal++;
                if (cal == 2) anim.SetTrigger("nextState2");
                if (cal == 3) anim.SetTrigger("nextState3");
                if (cal == 4) anim.SetTrigger("nextState4");
                if (cal == 5)
                {
                    cal = 1;
                    anim.SetTrigger("recover");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
        else
        {
            isJump = false;
        }
        anim.SetBool("jump", isJump);

        if (Input.GetKey(KeyCode.Q))
        {
            anim.SetTrigger("umaTobi");
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("UMATOBI00"))
        {
            anim.MatchTarget(rightFoot.position, rightFoot.rotation, AvatarTarget.RightFoot,
                new MatchTargetWeightMask(Vector3.one, 1), matchStart, matchEnd);
        }
    }

    private bool timeCal()
    {
        if(timeTarget - Time.deltaTime > 0)
        {
            timeTarget -= Time.deltaTime;
            return false;
        }
        timeTarget = 3f;
        return true;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        Vector3 target;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);
        if(plane.Raycast(ray, out float distance))
        {
            target = ray.GetPoint(distance);
            anim.SetLookAtPosition(target);
            anim.SetLookAtWeight(0.4f, 0.2f, 0.8f, 1f);
        }
    }
}
