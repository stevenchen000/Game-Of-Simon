using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    private Animator anim;
    private Vector3 defaultPosition;
    private bool isIdle = true;
    private AudioSource audio;

    [SerializeField] private float crossfadeTime = 0.1f;

    // Use this for initialization
    void Start()
    {
        anim = transform.GetComponent<Animator>();
        audio = transform.GetComponent<AudioSource>();
        defaultPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       
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
        isIdle = true;
        ResetPosition();
    }
}
