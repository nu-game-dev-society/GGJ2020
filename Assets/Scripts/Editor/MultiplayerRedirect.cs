using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class MultiplayerRedirect
{
    static MultiplayerRedirect()
    {
        EditorSceneManager.sceneOpened += SceneOpenedCallback;
    }

    private static void SceneOpenedCallback(Scene scene, OpenSceneMode mode)
    {
        SceneAsset playScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.path);
        if (scene.name == "MultiplayerScene")
        {
            playScene = AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/Scenes/MainMenu.unity");
        }

        EditorSceneManager.playModeStartScene = playScene;
    }
}
