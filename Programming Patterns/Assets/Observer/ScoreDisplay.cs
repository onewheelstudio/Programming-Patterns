using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreDisplay : MonoBehaviour
{
    private Text text;
    private int score;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        CritterDisable.critterKill += UpdateScore;
    }

    private void OnDisable()
    {
        CritterDisable.critterKill -= UpdateScore;
    }

    private void UpdateScore()
    {
        score++;
        text.text = "Score: " + score;
    }
}

