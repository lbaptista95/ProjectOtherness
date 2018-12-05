using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cradle;
using IStoryThread = System.Collections.Generic.IEnumerable<Cradle.StoryOutput>;
using Cradle.StoryFormats.Harlowe;

[System.Serializable]
public class Dialogue : MonoBehaviour {
    // Determina qual o script de Diálogo utilizado, quando o diálogo acaba, se o mesmo inicia alguma missão e o que aparecerá para o jogador no momento de apertar E para interagir
    public string[] dialogueNames;
    public string[] endOfStories;
    public string interaction;
    public string[] missions;
    public string[] endOfMissions;
}
