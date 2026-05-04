using UnityEngine;
using System.Collections;

public class SubtitleScheduler : MonoBehaviour
{
    public SubtitleController subtitleController;
    
    public void PlayLocutionDelay(float delay, SubtitleTrigger trigger)
    {
        StartCoroutine(PlayWithDelay(delay, trigger));
    }
    
    IEnumerator PlayWithDelay(float delay, SubtitleTrigger trigger)
    {
        yield return new WaitForSeconds(delay);
        trigger.Play(subtitleController);
    }
}