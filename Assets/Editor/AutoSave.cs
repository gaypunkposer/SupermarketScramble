using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class AutoSave
{
    //Constructor called by Unity Editor
    static AutoSave()
    {
        EditorApplication.playModeStateChanged += SaveWhenExitEdit;
    }

    private static void SaveWhenExitEdit(PlayModeStateChange change)
    {
        //If we're exiting edit mode (about to play the scene)
        if (change != PlayModeStateChange.ExitingEditMode) return;
        
        //Save the scene and the assets
        EditorSceneManager.SaveOpenScenes();
        AssetDatabase.SaveAssets();
    }
}