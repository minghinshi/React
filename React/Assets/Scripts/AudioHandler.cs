using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioHandler : MonoBehaviour
{
    public static AudioHandler instance;

    public List<AudioClip> importedClips;
    private Dictionary<string, AudioClip> clips = new();
    private AudioSource source;

    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
        importedClips.ForEach(x => clips[x.name] = x);
    }

    private void Start()
    {
        foreach (Button button in FindObjectsOfType<Button>())
            button.onClick.AddListener(() => Play("Click"));
    }

    public void Play(string name)
    {
        source.PlayOneShot(clips[name]);
    }
}
