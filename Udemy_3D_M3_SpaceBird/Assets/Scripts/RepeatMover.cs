using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatMover : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector3 movingPath;
    [SerializeField] float moveTime;
    float nextActionTime;

    Vector3 initialPosition;
    Vector3 destinitionPosition;
    float realNextActionTime;
    
    void Start()
    {
        nextActionTime = moveTime+1f;
        realNextActionTime = Time.time+nextActionTime;
        initialPosition = transform.position;
        destinitionPosition = initialPosition+movingPath;
    }

    // Update is called once per frame
    void Update()
    {
        PositionIndicator();
        
    }
    void PositionIndicator()
    {
        if(Time.time>realNextActionTime)
        {
            realNextActionTime=Time.time+moveTime;
            transform.position = initialPosition;

        }
        else
        {
            transform.Translate(movingPath/moveTime*Time.deltaTime);
        }
    }

    void PositionTranslator()
    {
        transform.Translate(movingPath/moveTime*Time.deltaTime);
    }
}
