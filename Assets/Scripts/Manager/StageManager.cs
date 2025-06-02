using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform virtualCamera;
    [SerializeField] private float moveDistance = 20f;
    [SerializeField] private float moveSpeed = 3f;

    private bool moving = false;
    private int currentStage = 0;

    void Update()
    {
        if (!moving && AllEnemiesDefeated() && AllFruitsCollected())
        {
            StartCoroutine(MoveToNextStage());
        }
    }
    private bool AllEnemiesDefeated()
    {
        return GameObject.FindGameObjectsWithTag("Monster").Length == 0;
    }

    private bool AllFruitsCollected()
    {
        return GameObject.FindGameObjectsWithTag("Fruit").Length == 0;
    }
    private IEnumerator MoveToNextStage()
    {
        moving = true;

        var controller = player.GetComponent<PlayerController>();
        if (controller != null)
            controller.enabled = false;

        currentStage++;

        Vector3 targetPlayerPos = player.position + Vector3.down * moveDistance;
        Vector3 targetCamPos = virtualCamera.position + Vector3.down * moveDistance;

        while (Vector3.Distance(player.position, targetPlayerPos) > 0.1f)
        {
            player.position = Vector3.MoveTowards(player.position, targetPlayerPos, moveSpeed * Time.deltaTime);
            virtualCamera.position = Vector3.MoveTowards(virtualCamera.position, targetCamPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        if (controller != null)
            controller.enabled = true;

        moving = false;
    }
}