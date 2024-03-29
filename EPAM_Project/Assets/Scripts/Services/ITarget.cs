﻿using UnityEngine;

namespace Services
{
    public interface ITarget
    {
        public Vector3 Position { get; }
        public GameObject GameObject { get; }
    }
}