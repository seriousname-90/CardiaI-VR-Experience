using UnityEngine;

public class SubtitlePlayer : MonoBehaviour
{
    public SubtitleScheduler scheduler;
    
    public void PlaySequence(SubtitleSequence sequence)
    {
        foreach (var entry in sequence.subtitles)
        {
            scheduler.PlayLocutionDelay(entry.delay, entry.trigger);
        }
    }
}