using UnityEngine;

[CreateAssetMenu(fileName = "New Subtitle Trigger", menuName = "VR/Subtitle Trigger")]
public class SubtitleTrigger : ScriptableObject
{
    public SubtitleData subtitle;
    
    public void Play(SubtitleController controller)
    {
        controller.PlaySubtitle(subtitle);
    }
}