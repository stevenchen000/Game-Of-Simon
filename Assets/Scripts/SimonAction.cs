using UnityEngine;
using System.Collections;


[CreateAssetMenu(menuName = "Simon Action")]
public class SimonAction : ScriptableObject
{
    [SerializeField]
    private string animationName;

    [Space(20)]
    [SerializeField]
    private Sprite animationSprite;

    [Space(20)]
    [SerializeField]
    private bool overridePosition = false;
    [SerializeField]
    private Vector3 animationPosition;

    public void RunAction(Character character)
    {
        character.PlayAnimation(animationName);

        if (overridePosition)
        {
            character.SetPosition(animationPosition);
        }
    }

    public Sprite GetSprite() { return animationSprite; }
}