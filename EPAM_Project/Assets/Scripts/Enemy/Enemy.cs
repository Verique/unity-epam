using System;
using Player;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody), typeof(Health))]
    public class Enemy : MonoBehaviour
    {
        private const float Height = -5f;

        private Transform target;

        private Rigidbody rgbd;
        private Transform eTransform;

        [SerializeField] private float enemySpeed = 100f;

        private void Start()
        {
            GetComponent<Health>().IsDead += () => gameObject.SetActive(false);
            target = FindObjectOfType<PlayerMovement>()?.transform;
            rgbd = GetComponent<Rigidbody>();
            eTransform = transform;
        }

        private void FixedUpdate()
        {
            if (target is null)
            {
                return;
            }
        
            var lookPos = target.position;
            lookPos.z = Height;
            eTransform.LookAt(lookPos);
            rgbd.velocity = eTransform.forward * enemySpeed;
        }
    }
}