using TMPro;
using UnityEngine;

namespace Runtime.Voice
{
    public class MokTextToSpeachService : MonoBehaviour
    {
        public TextMeshProUGUI showtext;
        public void Speak(string text)
        {
            showtext.text = text;
        }
    }
}