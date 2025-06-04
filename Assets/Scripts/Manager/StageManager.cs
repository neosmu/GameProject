using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform virtualCamera;
    [SerializeField] private Transform[] playerSpawnPoints;
    [SerializeField] private GameObject[] stageObjects;
    [SerializeField] private float moveDistance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float delayBeforeMove;

    private bool moving = false;
    private int currentStage = 0;
    private Coroutine stageClearCoroutine;

    void Start()
    {
        for (int i = 0; i < stageObjects.Length; i++)
        {
            stageObjects[i].SetActive(i == currentStage);
        }
    }
    void Update()
    {
        if (!moving && NoCatchObjectsExist() && stageClearCoroutine == null)
        {
            stageClearCoroutine = StartCoroutine(DelayedStageMove());
        }
    }
    private bool NoCatchObjectsExist()
    {
        return GameObject.FindGameObjectsWithTag("Catch").Length == 0 &&
            GameObject.FindGameObjectsWithTag("Monster").Length == 0;
    }
    private IEnumerator DelayedStageMove()
    {
        yield return new WaitForSeconds(delayBeforeMove);

        if (!moving && NoCatchObjectsExist())
        {
            yield return MoveToNextStage();
        }

        stageClearCoroutine = null;
    }
    private IEnumerator MoveToNextStage()
    {
        moving = true;

        var controller = player.GetComponent<PlayerController>();
        if (controller != null)
            controller.enabled = false;
        if (currentStage + 1 >= stageObjects.Length)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameClear");
            yield break;
        }

        Vector3 targetCamPos = virtualCamera.position + Vector3.down * moveDistance;

        while (Vector3.Distance(virtualCamera.position, targetCamPos) > 0.1f)
        {
            virtualCamera.position = Vector3.MoveTowards(virtualCamera.position, targetCamPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        stageObjects[currentStage].SetActive(false);
        currentStage++;
        stageObjects[currentStage].SetActive(true);

        if (currentStage < playerSpawnPoints.Length)
        {
            player.position = playerSpawnPoints[currentStage].position;
        }

        if (controller != null)
            controller.enabled = true;

        moving = false;
    }
}