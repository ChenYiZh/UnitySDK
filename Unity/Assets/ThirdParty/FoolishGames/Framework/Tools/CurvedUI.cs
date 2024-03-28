// using System;
// using System.Collections;
// using System.Collections.Generic;
// using FoolishGames.Attribute;
// using UnityEngine;
// using UnityEngine.UI;
//
// [RequireComponent(typeof(Graphic))]
// [ExecuteAlways]
// public class CurvedUI : MonoBehaviour
// {
//     [Header("UI")] [SerializeField, ReadOnly]
//     private Graphic _graphic = null;
//
//     [Header("UI 形状")] [SerializeField] private Mesh _mesh = null;
//
//     [SerializeField, ReadOnly] private Mesh _lastMesh = null;
//
//     private void Update()
//     {
//         if (_graphic == null)
//         {
//             _graphic = GetComponent<Graphic>();
//         }
//
//         if (_graphic != null && _mesh != _lastMesh)
//         {
//             _graphic.canvasRenderer.SetMesh(_mesh);
//             _lastMesh = _mesh;
//         }
//     }
// }