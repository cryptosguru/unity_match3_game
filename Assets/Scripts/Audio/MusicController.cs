using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
	public AudioSource audioSource;
	public AudioClip[] songs;
    // Start is called before the first frame update
    void Start()
    {
		PlaySong();
	}

	int randomSongNumber = 0;
	void PlaySong()
	{
		audioSource.loop = true;
		StartCoroutine(playMusic());
	}

	IEnumerator playMusic()
	{
		randomSongNumber = Random.Range(0, songs.Length);
		audioSource.clip = songs[randomSongNumber];
		audioSource.Play();
		yield return new WaitForSeconds(songs[randomSongNumber].length);
		randomSongNumber = Random.Range(0, songs.Length);
		audioSource.clip = songs[randomSongNumber];
		audioSource.Play();
	}
}
