using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCF.Entitys
{
    public interface IPileable
    {
        Mover Mover { get; }
        float Height { get; }
    }
}
