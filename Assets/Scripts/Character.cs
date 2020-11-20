using UnityEngine;
using System.Collections;
using SOEventSystem;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    private Animator anim;
    private Vector3 defaultPosition;
    private bool isIdle = true;
    private bool isDead = false;
    private AudioSource audio;
    private ExpressionManager expressions;

    [SerializeField] private float crossfadeTime = 0.1f;
    [SerializeField] private EventSO onPlayerIdle;

    // Use this for initialization
    void Start()
    {
        anim = transform.GetComponent<Animator>();
        audio = transform.GetComponent<AudioSource>();

        expressions = transform.GetComponentInChildren<ExpressionManager>();
        
        defaultPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isDead", isDead);
    }



    public void PlayAnimation(string animation)
    {
        anim.CrossFade(animation, crossfadeTime, 0, 0);
        isIdle = false;
    }

    public void PlayClip(AudioClip clip)
    {
        audio.Stop();
        audio.PlayOneShot(clip);
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
        if (!isIdle)
        {
            isIdle = true;
            ResetPosition();
            ChangeExpression("neutral");
            onPlayerIdle?.CallEvent();
        }
    }

    public void SetDead()
    {
        isDead = true;
    }

    public bool IsIdle() { return isIdle; }

    public void ChangeExpression(string expressionName)
    {
        expressions.ChangeExpression(expressionName);
    }

}
