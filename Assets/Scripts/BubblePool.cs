using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePool : MonoBehaviour
{
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private int PoolSize;

    public static BubblePool Instance { get; private set; }
    private Queue<PooledBubble> pool = new Queue<PooledBubble>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Pool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Pool()
    {
        for (int i = 0; i < PoolSize; i++)
        {
            CreateBubble();
        }
    }

    private PooledBubble CreateBubble()
    {
        GameObject obj = Instantiate(bubblePrefab);
        obj.SetActive(false);

        PooledBubble bubble = obj.GetComponent<PooledBubble>();
        bubble.SetPool(this);

        pool.Enqueue(bubble);
        return bubble;
    }

    public PooledBubble GetBubble()
    {
        if (pool.Count == 0)
        {
            CreateBubble();
        }

        PooledBubble bubble = pool.Dequeue();
        bubble.gameObject.SetActive(true);
        return bubble;
    }

    public void ReturnBubble(PooledBubble bubble)
    {
        bubble.gameObject.SetActive(false);
        pool.Enqueue(bubble);
    }
}
