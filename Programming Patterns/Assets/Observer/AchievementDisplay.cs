using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(AudioSource))]
public class AchievementDisplay : MonoBehaviour
{
    private CanvasGroup cg;
    private Image achievementPanel;
    private Text achievementText;
    private int score;
    private AudioSource audioSource;

    private void Start()
    {
        cg = this.GetComponent<CanvasGroup>();
        cg.alpha = 0f;
        achievementPanel = this.GetComponent<Image>();
        achievementText = this.GetComponentInChildren<Text>();
        audioSource = this.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        CritterDisable.critterKill += UpdateAchievement;
    }

    private void OnDisable()
    {
        CritterDisable.critterKill -= UpdateAchievement;
    }

    private void UpdateAchievement()
    {
        score++;

        if (score == 1 || score == 5 || score % 10 == 0)
            StartCoroutine(DoScoreAchievement(score));
    }

    IEnumerator DoScoreAchievement(int score)
    {
        if(score == 1)
            achievementText.text = "First Critter Kill!";
        else
            achievementText.text = score.ToString() + " Critter Kills!";

        cg.DOFade(1f, 1f); //dotween to make it look nice! and do it easily :)
        audioSource.Play();
        yield return new WaitForSeconds(5f);
        cg.DOFade(0f, 1f);
    }
}


