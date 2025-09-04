using UnityEngine;

public class KeyToTimeline : MonoBehaviour
{
    public StageManager stageManager;
    public int targetStageIndex;  // 要切换到的阶段索引
    public KeyCode keyToPress;
    
    void Update()
    {
        // 检测是否按下 ESC 键
        if (Input.GetKeyDown(keyToPress))
        {
            if (stageManager == null)
            {
                Debug.LogError("[KeyToTimeline] 错误：未设置StageManager!");
                return;
            }

            Debug.Log($"[KeyToTimeline] 按下ESC键，准备切换到阶段 {targetStageIndex + 1}");
            stageManager.ActivateStage(targetStageIndex);
        }
    }
}