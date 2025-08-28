using UnityEngine;

public class ClickOpenTimeline : MonoBehaviour
{
    public StageManager stageManager;
    public int targetStageIndex = 1;  // 要切换到的阶段索引

    // 这个方法可直接在 Button 的 OnClick 事件里绑定
    public void OnButtonClick()
    {
        if (stageManager == null)
        {
            Debug.LogError("[SkillInteraction] 错误：未设置StageManager!");
            return;
        }

        Debug.Log($"[SkillInteraction] 点击按钮，准备切换到阶段 {targetStageIndex + 1}");
        stageManager.ActivateStage(targetStageIndex);
    }
}