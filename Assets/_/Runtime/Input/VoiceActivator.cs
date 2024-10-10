using TMPro;
using UnityEngine;
using Runtime.Voice;

namespace Runtime.Input {

    /// <summary>
    /// gestione del pannello vocale e input 
    /// </summary>
    public class VoiceActivator : MonoBehaviour{

        public TextMeshProUGUI SpeackText;

        public VoiceRecognition VoiceComands;

        public GameObject Icon;

        private void Awake()
        {
            if (VoiceComands != null)
                VoiceComands.OnSpeakEvent += ShowText;
        }

        private void OnDestroy()
        {
            if (VoiceComands != null)
                VoiceComands.OnSpeakEvent -= ShowText;
        }

        /// <summary>
        /// attiva o disattiva riconoscimento vocale
        /// </summary>
        /// <param name="state">stato</param>
        public void VoicePanel(bool state)
        {
            if (VoiceComands.MicIsActive())
            {
                if (state)
                {
                    //if (MainPlayer.IsMainPlayer())//TODO: scommentare per multiplayer
                    //{
                        VoiceComands.StartRecognition();
                        Icon.SetActive(state);
                    //}
                }
                else
                {
                    VoiceComands.StopRecognition();
                    Icon.SetActive(state);
                }
            }
        }

        /// <summary>
        /// aggiorna testo in UI
        /// </summary>
        /// <param name="text"></param>
        private void ShowText(string text)
        {
            SpeackText.text = text;
        }
    }
}