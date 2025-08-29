// using UnityEngine;
// using UnityEngine.Playables;

// public class SkillInteraction : MonoBehaviour
// {
//     public PlayableDirector clickTimeline;
//     public PlayableDirector currentTimeline;

//     private void Start()
//     {
//         InitializeTimeline(clickTimeline);
//         InitializeTimeline(currentTimeline);
//     }

//     private void InitializeTimeline(PlayableDirector timeline)
//     {
//         if (timeline != null && timeline.playableAsset != null)
//         {
//             timeline.time = 0;
//             timeline.Stop();
//         }
//     }

//     private void OnMouseDown()
//     {
//         Debug.Log("[SkillInteraction] Mouse Down");

//         // 停止当前Timeline
//         if (currentTimeline != null && currentTimeline.playableAsset != null)
//         {
//             if (currentTimeline.playableGraph.IsValid() && currentTimeline.playableGraph.IsPlaying())
//             {
//                 currentTimeline.Stop();
//                 currentTimeline.time = 0;
//             }
//         }

//         // 播放点击Timeline
//         if (clickTimeline != null && clickTimeline.playableAsset != null)
//         {
//             clickTimeline.time = 0;
//             clickTimeline.Play();
//         }
//     }
// }
using UnityEngine;

public class SkillInteraction : MonoBehaviour
{
    public StageManager stageManager;
    public int targetStageIndex = 1;  // 默认切换到下一个阶段

    public void OnMouseDown()
    {
        if (stageManager == null)
        {
            Debug.LogError("[SkillInteraction] 错误：未设置StageManager!");
            return;
        }

        Debug.Log($"[SkillInteraction] 点击技能，准备切换到阶段 {targetStageIndex + 1}");
        stageManager.ActivateStage(targetStageIndex);
    }
}