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
    private bool bPreparedVideo;

    // TODO Get rid of this.
    public float x;
    public float y;


    // Use this for initialization
    void Start()
    {
        bIsIdle = true;
        bPreparedVideo = false;
        player = GetComponent<UnityEngine.Video.VideoPlayer>();
        player.Stop();
        idleVideoPlayer = IdlePlayer.GetComponent<UnityEngine.Video.VideoPlayer>();

        // Change size of array for your requirement
        Keywords_array = new string[5];

        Keywords_array[0] = "go spiderman";
        Keywords_array[1] = "Just do it";
        Keywords_array[2] = "How did you get into soccer";
        Keywords_array[3] = "What was it like growing up with sisters as a kid";
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
        //TODO Converstaion trees.
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
        else if (args.text == "What was it like growing up with sisters as a kid")
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
        // Play clip if we are not idle.
        if (bIsIdle == true)
        {
            player.clip = videoClips[index];
            player.isLooping = false;
            bIsIdle = false;
            print("Playing Clip");
            player.Play();
        }
    }

    private void Update()
    {
        //For Debugging
        /*
        if (Input.GetKey(KeyCode.A))
        {
            print("init Clip");
            PlayClip(2);
        }
        if (Input.GetKey(KeyCode.S))
        {
            print("init Clip");
            PlayClip(3);
        }
        if (Input.GetKey(KeyCode.D))
        {
            print("init Clip");
            PlayClip(4);
        }
        */

        //Switch to clip after we have a frame ready.
        if (player.frame > 2 && bIsIdle == false && bPreparedVideo == false)
        {
            print("Stoping idle"); 
            idleVideoPlayer.Stop();
            IdlePlayer.GetComponent<Renderer>().enabled = false;
            GetComponent<Renderer>().enabled = true;
            bPreparedVideo = true;
        }

        if(player.isPlaying)
        {
            long frameCount = (long)player.frameCount;
            // if we are close to ending the clip start switching to idle.
            if(player.frame >= frameCount - 24 && bIsIdle == false)
            {
                print("Playing idle");
                idleVideoPlayer.Play();
                bIsIdle = true;
            }
            // Switch back and play idle.
            if(bIsIdle == true && idleVideoPlayer.frame >= 2)
            {
                print("Switching back to idle");
                IdlePlayer.GetComponent<Renderer>().enabled = true;
                GetComponent<Renderer>().enabled = false;
                player.Stop();
                bPreparedVideo = false;
            }
            
        }

        // Billboard towards camera.
        this.transform.rotation = Quaternion.Euler(x, y, -Camera.main.transform.eulerAngles.y);
    }
}
