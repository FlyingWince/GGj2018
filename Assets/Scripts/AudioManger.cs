using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManger : MonoBehaviour {

    public static AudioManger instance;

    public AudioSource effectSource;
    public GameObject effectPrefab;

    public AudioClip[] effects;

    private void Start()
    {
        instance = this;

        //debug
        //StartCoroutine(testEffect());
    }

    public void PlayEffectAudio(int index)
    {
        effectSource.clip = effects[index];
        effectSource.Play();
    }

    public void ShowEffect(Vector3 pos)
    {
        Instantiate(effectPrefab, pos, Quaternion.identity);
    }


    //private IEnumerator testEffect()
    //{
    //    for (int i = 0; i < 4; i++)
    //    {
    //        AudioManger.instance.ShowEffect(transform.position);
    //        yield return new WaitForSeconds(2f);
    //    }
    //}
}
