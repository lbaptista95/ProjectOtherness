using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cradle;
using IStoryThread = System.Collections.Generic.IEnumerable<Cradle.StoryOutput>;
using Cradle.StoryFormats.Harlowe;

[System.Serializable]
public class Dialogue : MonoBehaviour {

    public string[] dialogueNames;
    public string[] endOfStories;
    public string interaction;
    public string[] missions;
    public string[] endOfMissions;
}
