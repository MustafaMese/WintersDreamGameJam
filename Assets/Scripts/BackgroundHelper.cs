using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundHelper : MonoBehaviour
{
    public float speed = 0;
    float pos = 0;
    private RawImage image;

    private Transform cameraTransform;
    private Vector3 cameraLastPosition;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<RawImage>();

        cameraTransform = Camera.main.transform;
        cameraLastPosition = cameraTransform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float deltaX = cameraTransform.position.x - cameraLastPosition.x;

        pos += speed * deltaX * 10;

        if (pos > 1.0F)

            pos -= 1.0F;

        image.uvRect = new Rect(pos, 0, 1, 1);

        cameraLastPosition = cameraTransform.position;
    }
}
