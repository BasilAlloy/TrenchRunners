using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
	[Header("Volume Setting")]
	[SerializeField] private TextMeshProUGUI volumeTextValue = null;
	[SerializeField] private Slider volumeSlider = null;
	[SerializeField] private float defaultVolume = 0.7f;

	[Header("Confirmation")]
	[SerializeField] private GameObject confirmationPrompt = null;

	[Header("Level to Load")]
    public string _newGameLevel;

	// Start is called before the first frame update
	void Start()
	{
		float volume = PlayerPrefs.GetFloat("masterVolume", defaultVolume);
		AudioListener.volume = volume;
		volumeSlider.value = volume;
		volumeTextValue.text = volume.ToString("P0");
	}

	public void NewGame()
	{
		SceneManager.LoadScene(_newGameLevel);
	}

	public void Exit()
	{
		Application.Quit();
	}

	public void SetVolume(float volume)
	{
		AudioListener.volume = volume;
		volumeTextValue.text = volume.ToString("P0");
	}

	public void VolumeApply()
	{
		PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
		StartCoroutine(ConfirmationBox());
	}

	public void Reset()
	{
		AudioListener.volume = defaultVolume;
		volumeSlider.value = defaultVolume;
		volumeTextValue.text = defaultVolume.ToString("P0");
		VolumeApply();
	}

	public IEnumerator ConfirmationBox()
	{
		confirmationPrompt.SetActive(true);
		yield return new WaitForSeconds(2);
		confirmationPrompt.SetActive(false);
	}
}
