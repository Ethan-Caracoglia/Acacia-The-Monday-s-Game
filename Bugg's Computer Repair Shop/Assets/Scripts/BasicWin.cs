using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWin : MonoBehaviour
{
    public bool won = false;
    [SerializeField] public MoveableObject[] parts;
    public int iceCount = 1;
    [SerializeField] SpriteRenderer victory;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (won) return;
        if (iceCount <= 0)
        {
            foreach (var part in parts)
            {
                if (!part.snapped)
                {
                    return;
                }
            }
            won = true;

        }
        if (won)
        {
            victory.enabled = true;
        }
    }

    public void updateIceCount()
    {
        iceCount -= 1;
    }
}
