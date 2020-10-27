using System;
using UnityEngine;

namespace Utils.Pool
{
    public interface IPoolable
    {
        event Action<IPoolable> ObjectDeactivation;
    }
}