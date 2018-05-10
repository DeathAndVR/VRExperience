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
    public UnityEngine.Video.VideoClip[] videoClips; 
    

    // Use this for initialization
    void Start()
    {
        player = GetComponent<UnityEngine.Video.VideoPlayer>();
        // Change size of array for your requirement
        Keywords_array = new string[3];
        Keywords_array[0] = "hello";
        Keywords_array[1] = "go spiderman";
        Keywords_array[2] = "Just do it";

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
            player.clip = videoClips[0];
            player.Play();
        }
        else if (args.text == "go spiderman")
        {
            player.clip = videoClips[1];
            player.Play(); 
        }
    }
}
