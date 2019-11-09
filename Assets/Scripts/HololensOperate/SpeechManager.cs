using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
    void Start()
    {
        /*
        keywords.Add("Reset world", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("OnReset");
        });
        */
        keywords.Add("Start Game", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                focusObject.SendMessage("OnStartGame", SendMessageOptions.DontRequireReceiver);
            }
        });

        keywords.Add("Display Information", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                focusObject.SendMessage("OnDisplayInformation", SendMessageOptions.DontRequireReceiver);
            }
        });

        keywords.Add("Close Information", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                focusObject.SendMessage("OnCloseInformation", SendMessageOptions.DontRequireReceiver);
            }
        });

        keywords.Add("Next Pokemon", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                focusObject.SendMessage("OnNextPokemon", SendMessageOptions.DontRequireReceiver);
            }
        });

        keywords.Add("Set Captain", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                focusObject.SendMessage("OnSetCaptain", SendMessageOptions.DontRequireReceiver);
            }
        });

        keywords.Add("Learn Skill", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                focusObject.SendMessage("OnLearnSkill", SendMessageOptions.DontRequireReceiver);
            }
        });

        keywords.Add("Learn This", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                focusObject.SendMessage("OnLearnThis", SendMessageOptions.DontRequireReceiver);
            }
        });
        keywords.Add("Refresh Enemy", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                focusObject.SendMessage("OnRefreshEnemy", SendMessageOptions.DontRequireReceiver);
            }
        });
        keywords.Add("Fight", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                focusObject.SendMessage("OnFight", SendMessageOptions.DontRequireReceiver);
            }
        });
        keywords.Add("Battle", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                focusObject.SendMessage("OnBattle", SendMessageOptions.DontRequireReceiver);
            }
        });
        keywords.Add("Change", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                focusObject.SendMessage("OnChange", SendMessageOptions.DontRequireReceiver);
            }
        });
        keywords.Add("Escape", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                focusObject.SendMessage("OnEscape", SendMessageOptions.DontRequireReceiver);
            }
        });
        keywords.Add("Catch", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                focusObject.SendMessage("OnCatch", SendMessageOptions.DontRequireReceiver);
            }
        });
        keywords.Add("Back", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                focusObject.SendMessage("OnBack", SendMessageOptions.DontRequireReceiver);
            }
        });
        keywords.Add("Use This Skill", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                focusObject.SendMessage("OnUseThisSkill", SendMessageOptions.DontRequireReceiver);
            }
        });
        keywords.Add("Change This Pokemon", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                focusObject.SendMessage("OnChangeThisPokemon", SendMessageOptions.DontRequireReceiver);
            }
        });

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}