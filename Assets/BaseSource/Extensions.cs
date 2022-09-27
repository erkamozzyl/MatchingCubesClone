using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public static class Extensions
{
    public static ParticleSystem SetStartColor(this ParticleSystem particleSystem, Color color)
    {
        var main = particleSystem.main;
        main.startColor = color;
        return particleSystem;
    }
}
