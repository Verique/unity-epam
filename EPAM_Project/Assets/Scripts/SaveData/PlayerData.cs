﻿using System;
using Extensions;

namespace SaveData
{
    [Serializable]
    public class PlayerData
    {
        public SerializableVector3 position;
        public SerializableVector3 rotation;
        public int maxHealth;
        public int currentHealth;
        public int currentClip;
    }
}