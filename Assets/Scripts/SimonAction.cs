using UnityEngine;
using System.Collections;


[CreateAssetMenu(menuName = "Simon Action")]
public class SimonAction : ScriptableObject
{
    [SerializeField] private string animationName;

    [Space(20)]
    [SerializeField] private Sprite actionSprite;

    [Space(20)]
    [SerializeField] private bool overridePosition = false;
    [SerializeField] private Vector3 animationPosition;

    [Space(20)]
    [SerializeField] private AudioClip clip;

    public void RunAction(Character character, bool playSound = true)
    {
        character.PlayAnimation(animationName);

        if (playSound) character.PlayClip(clip);
        if (overridePosition) character.SetPosition(animationPosition);
    }


    public Sprite GetSprite() { return actionSprite; }
}