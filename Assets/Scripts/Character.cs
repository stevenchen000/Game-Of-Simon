using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    private Animator anim;
    private Vector3 defaultPosition;
    private bool isIdle = true;

    // Use this for initialization
    void Start()
    {
        anim = transform.GetComponentInChildren<Animator>();
        defaultPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isIdle)
        {
            var state = anim.GetCurrentAnimatorClipInfo(0);
            string currAnimName = state[0].clip.name;
            if(currAnimName == "Idle")
            {
                SetIdle();
            }
        }
    }

    public void PlayAnimation(string animation)
    {
        anim.Play(animation);
        isIdle = false;
    }

    public void ResetPosition()
    {
        transform.position = defaultPosition;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetIdle()
    {
        isIdle = true;
        ResetPosition();
    }
}
