using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Transformer2MRUK : MonoBehaviour
{
    void Start()
    {
        UnityEngine.Vector2 r = new UnityEngine.Vector2(-4.8f, -6.8f);      // Punkt im Weltkoordinatensystem
        UnityEngine.Vector2 p1 = new UnityEngine.Vector2(-3.5f, 5.8f);      // Ursprung des neuen Koordinatensystems
        UnityEngine.Vector2 p2 = new UnityEngine.Vector2(1.8f, 7.2f);      // Vektor des neuen Koordinatensystems
        Debug.Log("Start Punkt r: " + r);

        float rotationInRad = MathF.Atan2(p2.y - p1.y, p2.x - p1.x);
        float rotationInDegrees = rotationInRad * Mathf.Rad2Deg;
        Debug.Log("Alpha: " + rotationInRad + "   Grad: " + rotationInDegrees);

        // Schritt 1: In den Ursprung des neuen Koordinatensystems verschieben
        UnityEngine.Vector2 relative = r - p1;

        // Schritt 2: Gegen den Winkel alpha rotieren (um in das neue System zu kommen)
        UnityEngine.Vector2 transformedR = new UnityEngine.Vector2(
            relative.x * Mathf.Cos(-rotationInRad) - relative.y * Mathf.Sin(-rotationInRad),
            relative.x * Mathf.Sin(-rotationInRad) + relative.y * Mathf.Cos(-rotationInRad)
        );

        Debug.Log("Transformierter Punkt r im neuen Koordinatensystem: " + transformedR);

        // Matrix-Transformation

        // Matrix: Rotation um alpha, dann Verschiebung um P1
        Matrix3x2 transform =
            Matrix3x2.CreateTranslation(-p1.x, -p1.y) *
            Matrix3x2.CreateRotation(-1 * rotationInRad);

        Matrix3x2 transform2Zero =
          Matrix3x2.CreateTranslation(-p1.x, -p1.y) *
          Matrix3x2.CreateRotation(-1 * rotationInRad)*
          Matrix3x2.CreateTranslation(-r.x, 0);

        // Inverse Matrix: von Welt zurück ins lokale Koordinatensystem
        Matrix3x2.Invert(transform, out Matrix3x2 inverseTransform);

        // Transformiere Punkt
        System.Numerics.Vector2 numR = new System.Numerics.Vector2(r.x, r.y);

        System.Numerics.Vector2 toP1 = System.Numerics.Vector2.Transform(numR, transform);
        Debug.Log("Transformierter Punkt r im neuen Koordinatensystem: " + toP1);
        System.Numerics.Vector2 p_local = System.Numerics.Vector2.Transform(toP1, inverseTransform);
        Debug.Log("Transformierter Punkt p im neuen Koordinatensystem: " + p_local);
    }
}