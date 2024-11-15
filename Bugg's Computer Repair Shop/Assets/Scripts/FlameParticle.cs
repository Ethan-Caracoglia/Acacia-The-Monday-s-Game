using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameParticle : MonoBehaviour
{
    float timeAlive = 0.7f;
    float curTimeAlive;
    [SerializeField] SpriteRenderer spriteRenderer;
    float minInitSpeed = 2f;
    float maxInitSpeed = 0.3f;
    float dirDif = 0.02f;
    Vector3 speed;
    // Start is called before the first frame update
    void Start()
    {
        
        float spe = Random.Range(minInitSpeed, maxInitSpeed);
        float dir = Random.Range(-dirDif, dirDif);
        speed = new Vector3(dir, spe, 0);
        timeAlive /= spe;
        curTimeAlive = timeAlive;
    }

    // Update is called once per frame
    void Update()
    {
        curTimeAlive -= Time.deltaTime;
        spriteRenderer.color = new Color(255, 255, 255, curTimeAlive / timeAlive);
        transform.position +=  speed * Time.deltaTime;
        if(curTimeAlive < 0)
        {
            Destroy(gameObject);
        }
    }
}
