using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;

public class VoiceRec : MonoBehaviour
{

    KeywordRecognizer keywordRecognizer;
    // keyword array
    public string[] Keywords_array;
    public UnityEngine.Video.VideoPlayer player;
    

    // Use this for initialization
    void Start()
    {
        player = GetComponent<UnityEngine.Video.VideoPlayer>();
        // Change size of array for your requirement
        Keywords_array = new string[2];
        Keywords_array[0] = "hello";
        Keywords_array[1] = "how are you";
        Keywords_array[1] = "Just do it";

        // instantiate keyword recognizer, pass keyword array in the constructor
        keywordRecognizer = new KeywordRecognizer(Keywords_array);
        keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        // start keyword recognizer
        keywordRecognizer.Start();
    }

    void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword: " + args.text + "; Confidence: " + args.confidence + "; Start Time: " + args.phraseStartTime + "; Duration: " + args.phraseDuration);
        // write your own logic
        if (args.text == "Just do it")
        {
            player.Play();
        }
        else if (args.text == "how are you")
        {
            player.Play();
        }
    }
}
