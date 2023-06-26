//  SettingStartSceneWindow.cs
//  http://kan-kikuchi.hatenablog.com/entry/playModeStartScene
//
//  Created by kan.kikuchi on 2017.09.30.

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// �G�f�B�^��ōŏ��ɕ\������V�[���̐ݒ���s���E�B���h�E
/// </summary>
public class SettingStartSceneWindow : EditorWindow
{

    //�ݒ肵���V�[���̃p�X��ۑ�����KEY
    private const string SAVE_KEY = "StartScenePathKey";

    //=================================================================================
    //������
    //=================================================================================

    //���j���[����E�B���h�E��\��
    [MenuItem("Window/SettingStartSceneWindow")]
    public static void Open()
    {
        SettingStartSceneWindow.GetWindow<SettingStartSceneWindow>(typeof(SettingStartSceneWindow));
    }

    //������(�E�B���h�E���J���������Ɏ��s)
    private void OnEnable()
    {
        //�ۑ�����Ă���ŏ��̃V�[���̃p�X������΁A�ǂݍ���Őݒ�
        string startScenePath = EditorUserSettings.GetConfigValue(SAVE_KEY);
        if (!string.IsNullOrEmpty(startScenePath))
        {

            //�p�X����V�[�����擾�A�V�[�����Ȃ���Όx���\��
            SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(startScenePath);
            if (sceneAsset == null)
            {
                Debug.LogWarning(startScenePath + "������܂���I");
            }
            else
            {
                EditorSceneManager.playModeStartScene = sceneAsset;
            }

        }
    }

    //=================================================================================
    //�\������GUI�̐ݒ�
    //=================================================================================

    private void OnGUI()
    {

        //�X�V�O��playModeStartScene�ɐݒ肳��Ă�V�[���̃p�X���擾
        string beforeScenePath = "";
        if (EditorSceneManager.playModeStartScene != null)
        {
            beforeScenePath = AssetDatabase.GetAssetPath(EditorSceneManager.playModeStartScene);
        }

        //GUI�ŃV�[���t�@�C�����擾���AplayModeStartScene�ɐݒ肷��
        EditorSceneManager.playModeStartScene = (SceneAsset)EditorGUILayout.ObjectField(new GUIContent("Start Scene"), EditorSceneManager.playModeStartScene, typeof(SceneAsset), false);

        //�X�V���playModeStartScene�ɐݒ肳��Ă�V�[���̃p�X���擾
        string afterScenePath = "";
        if (EditorSceneManager.playModeStartScene != null)
        {
            afterScenePath = AssetDatabase.GetAssetPath(EditorSceneManager.playModeStartScene);
        }

        //playModeStartScene���ύX���ꂽ��p�X��ۑ�
        if (beforeScenePath != afterScenePath)
        {
            EditorUserSettings.SetConfigValue(SAVE_KEY, afterScenePath);
        }
    }

}