using UnityEngine;
using UnityEngine.Playables;

public class StageManager : MonoBehaviour
{
    [System.Serializable]
    public class Stage
    {
        public string stageName;                    // 阶段名称
        public PlayableDirector timeline;           // 阶段Timeline
        public GameObject[] stageObjects;           // 阶段相关的所有物体
        public GameObject clickTarget;              // 点击触发下一阶段的物体
    }

    public Stage[] stages;                         // 所有阶段
    private int currentStageIndex = -1;            // 当前阶段索引

    void Start()
    {
        Debug.Log("[StageManager] 初始化开始");
        
        // 初始化：先禁用所有阶段的物体
        for (int i = 0; i < stages.Length; i++)
        {
            if (stages[i] == null) continue;
            
            Debug.Log($"[StageManager] 初始化阶段 {i + 1}");
            foreach (var obj in stages[i].stageObjects)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
            
            // 确保Timeline初始状态
            if (stages[i].timeline != null)
            {
                stages[i].timeline.time = 0;
                stages[i].timeline.Stop();
            }
        }

        // 启动第一个阶段
        Debug.Log("[StageManager] 开始激活第一个阶段");
        ActivateStage(0);
    }

    public void ActivateStage(int stageIndex)
    {
        if (stageIndex < 0 || stageIndex >= stages.Length)
        {
            Debug.LogError($"[StageManager] 错误：无效的阶段索引 {stageIndex}");
            return;
        }

        Debug.Log($"[StageManager] ========= 开始切换到阶段 {stageIndex + 1} =========");

        // 1. 先停止当前阶段的Timeline（如果有）
        if (currentStageIndex >= 0 && currentStageIndex < stages.Length)
        {
            Stage currentStage = stages[currentStageIndex];
            if (currentStage.timeline != null && currentStage.timeline.playableGraph.IsValid())
            {
                Debug.Log($"[StageManager] 停止阶段 {currentStageIndex + 1} 的Timeline");
                currentStage.timeline.Stop();
                currentStage.timeline.time = 0;
            }
        }

        // 2. 然后再禁用当前阶段的物体
        if (currentStageIndex >= 0 && currentStageIndex < stages.Length)
        {
            foreach (var obj in stages[currentStageIndex].stageObjects)
            {
                if (obj != null)
                {
                    // 在禁用前重置状态
                    ResetGameObject(obj);
                    obj.SetActive(false);
                    Debug.Log($"[StageManager] 禁用物体: {obj.name}");
                }
            }
        }

        // 3. 更新当前阶段索引
        currentStageIndex = stageIndex;
        Stage newStage = stages[currentStageIndex];

        // 4. 激活新阶段的物体
        Debug.Log($"[StageManager] 激活阶段 {currentStageIndex + 1} 的物体");
        foreach (var obj in newStage.stageObjects)
        {
            if (obj != null)
            {
                obj.SetActive(true);
                Debug.Log($"[StageManager] 激活物体: {obj.name}");
            }
        }

        // 5. 最后播放新阶段的Timeline
        if (newStage.timeline != null)
        {
            Debug.Log($"[StageManager] 播放阶段 {currentStageIndex + 1} 的Timeline");
            newStage.timeline.time = 0;
            newStage.timeline.Play();
        }

        Debug.Log($"[StageManager] ========= 阶段 {stageIndex + 1} 切换完成 =========");
    }

    private void ResetGameObject(GameObject obj)
    {
        if (obj == null) return;

        // 只有在物体激活时才重置Animator
        if (obj.activeInHierarchy)
        {
            Animator animator = obj.GetComponent<Animator>();
            if (animator != null)
            {
                animator.Rebind();
            }
        }

        // 重置Transform
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;
    }
}