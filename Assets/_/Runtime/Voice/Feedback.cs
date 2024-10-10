
using Oculus.Interaction;
using UnityEngine;

namespace Runtime.Voice
{
    /// <summary>
    /// Strumenti per far interagire componente STT e componente API AI
    /// </summary>
    public class Feedback : MonoBehaviour
    {
        [SerializeField]
        private VoiceRecognition VoiceComands;



        private void Awake()
        {

            if (VoiceComands != null)
                VoiceComands.OnEndedRecord += SendChatMessage;


        }

        private void OnDestroy()
        {
            if (VoiceComands != null)
                VoiceComands.OnEndedRecord -= SendChatMessage;

        }

        /// <summary>
        /// Invio messaggio a componente AI
        /// </summary>
        /// <param name="message">messaggio</param>
        private void SendChatMessage(string message)
        {            
            //VoiceComands.LockMic(true);
        }
    }
}
