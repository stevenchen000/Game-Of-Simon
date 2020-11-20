using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Expression
{
    public string expressionName;
    public Sprite expressionSprite;
}


public class ExpressionManager : MonoBehaviour
{
    [SerializeField] private string defaultExpression;
    [SerializeField] private List<Expression> expressions = new List<Expression>();
    private Dictionary<string, Sprite> _expressions = new Dictionary<string, Sprite>();

    [SerializeField] private SpriteRenderer face;

    // Start is called before the first frame update
    void Start()
    {
        SetupDict();
    }

    public void ChangeExpression(string expressionName)
    {
        face.sprite = GetExpression(expressionName);
    }

    public void SetExpressions(List<Expression> expressions) 
    { 
        this.expressions = expressions;
        SetupDict();
    }





    private Sprite GetExpression(string expressionName)
    {
        Sprite result = null;

        if (_expressions.ContainsKey(expressionName))
        {
            result = _expressions[expressionName];
        }
        else
        {
            result = _expressions[defaultExpression];
        }

        return result;
    }

    private void SetupDict()
    {
        _expressions = new Dictionary<string, Sprite>();

        for(int i = 0; i < expressions.Count; i++)
        {
            Expression expression = expressions[i];
            _expressions[expression.expressionName] = expression.expressionSprite;
        }
    }
}
