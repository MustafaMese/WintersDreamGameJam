﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFollow : MonoBehaviour
{
    [SerializeField] Transform[] routes;
    private int routeToGo;
    private float tParam;
    private Vector2 objPosition;
    private float speedModifier;
    private bool coroutineAllowed;

    private void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 0.15f;
        coroutineAllowed = true;
    }

    private void Update()
    {
        if (coroutineAllowed)
            StartCoroutine(GoByRoute(routeToGo));
    }

    private IEnumerator GoByRoute(int routeNumber)
    {
        coroutineAllowed = false;

        Vector2 p0 = routes[routeNumber].GetChild(0).position; 
        Vector2 p1 = routes[routeNumber].GetChild(1).position; 
        Vector2 p2 = routes[routeNumber].GetChild(2).position; 
        Vector2 p3 = routes[routeNumber].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            objPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;

            transform.position = objPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
            routeToGo = 0;

        coroutineAllowed = true;
    }
}
