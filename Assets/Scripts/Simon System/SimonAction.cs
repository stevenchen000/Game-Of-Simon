using UnityEngine;
using System.Collections;


namespace SimonSystem
{
    [CreateAssetMenu(menuName = "Simon Action/Action")]
    public class SimonAction : ScriptableObject
    {
        [SerializeField] private string animationName;
        [SerializeField] private string expressionName;

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
            character.ChangeExpression(expressionName);

            if (playSound) character.PlayClip(clip);
            if (overridePosition) character.SetPosition(animationPosition);
        }


        public Sprite GetSprite() { return actionSprite; }
    }
}