using UnityEngine;
using UnityEngine.Playables;

public class SkillInteraction : MonoBehaviour
{
    public PlayableDirector clickTimeline;
    public PlayableDirector currentTimeline;

    private void Start()
    {
        InitializeTimeline(clickTimeline);
        InitializeTimeline(currentTimeline);
    }

    private void InitializeTimeline(PlayableDirector timeline)
    {
        if (timeline != null && timeline.playableAsset != null)
        {
            timeline.time = 0;
            timeline.Stop();
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("[SkillInteraction] Mouse Down");

        // 停止当前Timeline
        if (currentTimeline != null && currentTimeline.playableAsset != null)
        {
            if (currentTimeline.playableGraph.IsValid() && currentTimeline.playableGraph.IsPlaying())
            {
                currentTimeline.Stop();
                currentTimeline.time = 0;
            }
        }

        // 播放点击Timeline
        if (clickTimeline != null && clickTimeline.playableAsset != null)
        {
            clickTimeline.time = 0;
            clickTimeline.Play();
        }
    }
}
