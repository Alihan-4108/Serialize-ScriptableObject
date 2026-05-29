using System;
using UnityEngine;

namespace Alihan4108.SerializeScriptableObject
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class SerializeSOAttribute : PropertyAttribute
    {

    }
}