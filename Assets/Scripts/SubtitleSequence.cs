using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Subtitle Sequence", menuName = "VR/Subtitle Sequence")]
public class SubtitleSequence : ScriptableObject
{
    [System.Serializable]
    public class SubtitleEntry
    {
        public float delay;
        public SubtitleTrigger trigger;
    }
    
    public List<SubtitleEntry> subtitles = new List<SubtitleEntry>();
}