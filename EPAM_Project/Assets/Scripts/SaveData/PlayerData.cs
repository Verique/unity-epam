﻿using System;
using Extensions;
using UnityEngine;

namespace SaveData
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private float speed;
        [SerializeField] private HealthData healthData;

        public PlayerData(HealthData healthData, float speed)
        {
            this.healthData = healthData;
            this.speed = speed;
        }

        public HealthData HealthData => healthData;
        public float Speed => speed;
    }
}