using System;
using System.Collections.Generic;
using UnityEngine;

public interface IComponent : IUpdate
{
    void Initialize();
    void Awake();
    void Start();
    void OnEnable();
    void OnDisable();
    void OnDestroy();
    void Reset();
    void Release();
}
