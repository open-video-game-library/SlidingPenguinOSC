using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace penguin
{
    public class PlayButton : MonoBehaviour
    {
        // UIスイッチ処理に伴うSE再生のために参照
        [SerializeField] private StartSceneAudio audio;
        
        // ボタン押下時のUIスイッチ処理のために参照
        [SerializeField] private HomeUISwitcher uiSwitcher;

        [SerializeField] private ParameterReader parameterReader;

        [SerializeField] private OSCGameStartManager oscStartInput;

        private bool isStarted;
        
        // Start is called before the first frame update
        private void Start()
        {
            gameObject.GetComponent<Button>().onClick.AddListener(Clicked);
        }

        // Update is called once per frame
        void Update()
        {
            if (oscStartInput.start && !isStarted)
            {
                isStarted = true;
                Clicked();
            }
        }

        private void Clicked()
        {
            if (ParameterManager.continuousPlay) { parameterReader.SetParameters(ExperimentManager.trialCount); }
            GameDataExport.CreateScoreCSV();
            GameDataExport.CreateTrailCSV();

            audio.bgm.Pause();
            audio.TransitionClick.Play();
            uiSwitcher.StartCoroutine("ActivateInGameUI");
        }
    }
}
