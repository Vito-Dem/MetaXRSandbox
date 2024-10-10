using System;
using System.Collections.Generic;
using Recognissimo.Components;
using TMPro;
using UnityEngine;

namespace Runtime.Voice
{
    /// <summary>
    /// gesrione componenti Recognissimo
    /// </summary>
    public class VoiceRecognition : MonoBehaviour
    {
        private SpeechRecognizer speechRecognizer;
        private MicrophoneSpeechSource speechSource;

        /// <summary>
        /// testo rilevato in tempo reale
        /// </summary>
        /// <param name="responseData">testo</param>
        public delegate void OnSpeak(string responseData);
        public event OnSpeak OnSpeakEvent;

        /// <summary>
        /// testo completo alla fine della rilevazione
        /// </summary>
        /// <param name="responseData"></param>
        public delegate void EndedRecord(string responseData);
        public event EndedRecord OnEndedRecord;

        private string VoiceBuffer;

        public TextMeshProUGUI debugTxt;

        private bool needToStop = false;
        /// <summary>
        /// true -> mic locked;     
        /// false -> mic unlocked
        /// </summary>
        private bool lockMic = false;

        /// <summary>
        /// setup di tutti i componenti utili a recognissimo
        /// </summary>
        private void Awake()
        {
            try
            {
                // Create components.
                speechRecognizer = gameObject.AddComponent<SpeechRecognizer>();
                var languageModelProvider = gameObject.AddComponent<StreamingAssetsLanguageModelProvider>();
                speechSource = gameObject.AddComponent<MicrophoneSpeechSource>();

                // Setup StreamingAssets language model provider.
                // Set the language used for recognition.
                languageModelProvider.language = SystemLanguage.Italian;
                // Set paths to language models.
                languageModelProvider.languageModels = new List<StreamingAssetsLanguageModel>
                    {
                        new() {language = SystemLanguage.Italian, path = "LanguageModels/it-IT"}
                    };

                // Setup microphone speech source. The default settings can be left unchanged, but we will do it as an example.
                speechSource.DeviceName = null;
                speechSource.TimeSensitivity = 1;

                // Bind speech processor dependencies.
                speechRecognizer.LanguageModelProvider = languageModelProvider;
                speechRecognizer.SpeechSource = speechSource;

                // Handle events.        
                speechRecognizer.PartialResultReady.AddListener(res => OnSpeakEvent?.Invoke(res.partial));
                speechRecognizer.ResultReady.AddListener(res => AppendBufferText(res.text));

                speechRecognizer.StartProcessing();
            }
            catch(Exception)
            {
                //SimpleLogger.Log("VoiceRecognition->AWAKE\t"+e.Message, LogLevel.ERROR);
            }
        }

        /// <summary>
        /// inizio rilevazione
        /// </summary>
        public void StartRecognition()
        {
            needToStop = false;
            VoiceBuffer = "";
            speechSource.IsPaused=false;
        }

        /// <summary>
        /// fine rilevazione
        /// </summary>
        public void StopRecognition()
        {
            needToStop = true;
        }

        /// <summary>
        /// aggiorna testo di tutta la sessione di rilevazione. se la rilevazione è terminata lancia evento OnEndedRecord
        /// </summary>
        /// <param name="text">testo da aggiuingere</param>
        private void AppendBufferText(string text)
        {
            VoiceBuffer += text + " ";
            if (needToStop)
            {
                if (!string.IsNullOrWhiteSpace(VoiceBuffer))
                {
                    OnEndedRecord?.Invoke(VoiceBuffer); 
                    speechSource.IsPaused = true; 
                }
                              
            }
        }

        /// <summary>
        /// set microphone state
        /// </summary>
        /// <param name="state">microphone state</param>
        public void LockMic(bool state)
        {            
            if (debugTxt != null)
            {
                debugTxt.text = state.ToString();
            }
            lockMic = state;
        }

        /// <summary>
        /// get microphone state
        /// </summary>
        /// <returns>microphone state</returns>
        public bool MicIsActive()
        {
            return !lockMic;
        }
    }
}