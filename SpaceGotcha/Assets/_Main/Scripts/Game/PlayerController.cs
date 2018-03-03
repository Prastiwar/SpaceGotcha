﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameSettings settings;

    [SerializeField, HideInInspector] Vector3 vectorDir;
    [SerializeField, HideInInspector] Transform m_transform;
    float angle;

    void Reset()
    {
        m_transform = GetComponent<Transform>();
        vectorDir = new Vector3();
    }

    void Awake()
    {
        settings = Game.Instance.Settings[Game.Instance.SettingsIndex];
    }

    void Update()
    {
        if (Time.timeScale < 1)
        {
            return;
        }

        angle += -Input.GetAxis("Horizontal") * settings.PlayerSpeed * Time.deltaTime;

        Quaternion newRotation = m_transform.rotation;
        vectorDir.z = angle;
        newRotation.eulerAngles = vectorDir;
        m_transform.rotation = newRotation;
    }
}
