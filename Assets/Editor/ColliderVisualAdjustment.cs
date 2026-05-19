using UnityEngine;
using UnityEditor;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(grabber))]
public class ColliderVisualAdjustment:Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        grabber gbbr = (grabber)target;
        if (GUILayout.Button("swap sprites"))
        {
            gbbr.SwapSprites();
        }
    }
}
