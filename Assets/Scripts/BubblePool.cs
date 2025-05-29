using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePool : MonoBehaviour
{
    [SerializeField] private PooledBubble prefab; 
    [SerializeField] private int poolSize;    

    private Queue<PooledBubble> pool = new Queue<PooledBubble>();

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreateBubble();
        }
    }

    private void CreateBubble()
    {
        PooledBubble bubble = Instantiate(prefab, transform);
        bubble.SetPool(this);
        bubble.gameObject.SetActive(false);
        pool.Enqueue(bubble);
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
