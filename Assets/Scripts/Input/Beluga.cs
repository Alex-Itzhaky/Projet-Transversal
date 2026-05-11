// using System;
// using UnityEngine;
// using Random = UnityEngine.Random;
//
// public class Beluga : MonoBehaviour
// {
//
//     [SerializeField] private Rigidbody2D _rigidbody2D;
//     private void Update()
//     {
//         BelugaMove();
//     }
//
//
//     private BelugaState _state;
//
//     private float _moveSpeed = 5f;
//
//     public Beluga()
//     {
//         _state = BelugaState.Idle;
//     }
//
//     private void BelugaTrajectory()
//     {
//         Vector3 direction = Vector3.zero;
//         direction.x += Random.Range(-2, 2);
//         direction.y += Random.Range(-2, 2);
//         direction.Normalize();
//     }
//     private void BelugaMove()
//     {
//         _rigidbody2D.linearVelocity = direction * _moveSpeed;
//
//     }
// }
