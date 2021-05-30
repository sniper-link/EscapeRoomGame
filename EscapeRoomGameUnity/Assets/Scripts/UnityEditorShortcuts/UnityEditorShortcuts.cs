using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UnityEditorShortcuts : UnityEditor.Editor
{
    // create a shortcut for locking inspector or any other window under tools menu
    [MenuItem("Tools/Toggle Inspector Lock %l")] // Ctrl + L
    public static void ToggleInspectorLock()
    {
        ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
        ActiveEditorTracker.sharedTracker.ForceRebuild();
    }
}
