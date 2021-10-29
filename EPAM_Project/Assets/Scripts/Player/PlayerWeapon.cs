﻿using System.Collections;
using Services;
using Stats;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerStatLoader))]
    public class PlayerWeapon : MonoBehaviour
    {
        private enum State
        {
            NotFiring,
            Firing,
            NeedReload
        }

        [SerializeField] [Range(0.001f, 1f)] private float rateOfFire;
        [SerializeField] [Range(1f, 10f)] private float reloadTime;

        private Transform player;
        private Stat<int> clipStat;
        private InputManager inputManager;
        private GameManager gameManager;
        private ObjectPool pool;
        private State state;

        private void Start()
        {
            player = transform;
            gameManager = ServiceLocator.Instance.Get<GameManager>();
            inputManager = ServiceLocator.Instance.Get<InputManager>();
            pool = ServiceLocator.Instance.Get<ObjectPool>();
            clipStat = GetComponent<PlayerStatLoader>().Stats.Clip;
            inputManager.ReloadKeyUp += Reload;
            clipStat.MinValueReached += Reload;
            inputManager.LmbHold += FireBullet;
            state = State.NotFiring;
            StartCoroutine(nameof(WeaponRoutine));
        }

        private IEnumerator WeaponRoutine()
        {
            while (true)
                switch (state)
                {
                    case State.Firing:
                        pool.Spawn("bullet", player.position, player.rotation);
                        clipStat.Value--;
                        yield return new WaitForSeconds(rateOfFire);
                        if (state == State.Firing) state = State.NotFiring;
                        break;
                    
                    case State.NeedReload:
                        yield return new WaitForSeconds(reloadTime);
                        clipStat.Value = clipStat.maxValue;
                        state = State.NotFiring;
                        break;
                    
                    case State.NotFiring:
                        yield return new WaitForEndOfFrame();
                        break;

                    default:
                        yield return new WaitForEndOfFrame();
                        break;
                }
        }

        private void Reload()
        {
            if (gameManager.State == GameState.Default) state = State.NeedReload;
        }

        private void FireBullet()
        {
            if (gameManager.State != GameState.Default) return;
            
            if (state == State.NotFiring && clipStat.Value > clipStat.minValue) state = State.Firing;
            if (clipStat.Value == clipStat.minValue) state = State.NeedReload;
        }
    }
}