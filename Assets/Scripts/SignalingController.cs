using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalingController : MonoBehaviour
{
    [SerializeField] private float _rateOfChangeAlarm;

    private AudioSource _audioSource;
    
    private float _maxVolume = 1;
    private float _minVolume = 0;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Hero>())
        {
            StartCoroutine(ChangeAlarmVolume(_maxVolume));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Hero>())
        {
            StartCoroutine(ChangeAlarmVolume(_minVolume));
        }
    }

    private IEnumerator ChangeAlarmVolume(float targetVolume)
    {
        while(_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _rateOfChangeAlarm);
        
            yield return null;
        }

    }
}
