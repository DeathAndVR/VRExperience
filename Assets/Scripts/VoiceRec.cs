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
    public UnityEngine.Video.VideoClip idleClip;
    public GameObject IdlePlayer;
    private UnityEngine.Video.VideoPlayer idleVideoPlayer;
    private bool bIsIdle;
    

    // Use this for initialization
    void Start()
    {
        bIsIdle = true;
        player = GetComponent<UnityEngine.Video.VideoPlayer>();
        idleVideoPlayer = IdlePlayer.GetComponent<UnityEngine.Video.VideoPlayer>();

        // Change size of array for your requirement
        Keywords_array = new string[5];
        
        Keywords_array[0] = "go spiderman";
        Keywords_array[1] = "Just do it";
        Keywords_array[2] = "How did you get into soccer";
        Keywords_array[3] = "What was it liking growing up with sisters as a kid";
        Keywords_array[4] = "What was your favorite thing about going to college in Florida";

        // instantiate keyword recognizer, pass keyword array in the constructor
        keywordRecognizer = new KeywordRecognizer(Keywords_array);
        keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        // start keyword recognizer
        keywordRecognizer.Start();
        GetComponent<Renderer>().enabled = false;
        IdlePlayer.GetComponent<Renderer>().enabled = true;
    }

    void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword: " + args.text + "; Confidence: " + args.confidence + "; Start Time: " + args.phraseStartTime + "; Duration: " + args.phraseDuration);
        // write your own logic
        if (args.text == "Just do it")
        {
            PlayClip(0);
        }
        else if (args.text == "go spiderman")
        {
            PlayClip(1);
        }
        else if (args.text == "How did you get into soccer")
        {
            PlayClip(2);
        }
        else if (args.text == "What was it liking growing up with sisters as a kid")
        {
            PlayClip(3);
        }
        else if (args.text == "What was your favorite thing about going to college in Florida")
        {
            PlayClip(4);
        }
    }

    void PlayClip(uint index)
    {
        player.clip = videoClips[index];
        player.isLooping = false;

        player.Play();
        StartCoroutine(TurnOnRender());

       
    }

    private void Update()
    {
        if(player.isPlaying == false && bIsIdle == false)
        {
            StartCoroutine(TurnOffRender());
        }
    }

    IEnumerator TurnOnRender()
    {
        yield return new WaitForSeconds(0.4f);
        idleVideoPlayer.Stop();
        IdlePlayer.GetComponent<Renderer>().enabled = false;
        GetComponent<Renderer>().enabled = true;
        bIsIdle = false;
    }
    IEnumerator TurnOffRender()
    {
        bIsIdle = true;
        idleVideoPlayer.Play();
        yield return new WaitForSeconds(0.4f);
        IdlePlayer.GetComponent<Renderer>().enabled = true;
        GetComponent<Renderer>().enabled = false;
       
    }
}
