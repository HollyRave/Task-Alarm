using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalingController : MonoBehaviour
{
    [SerializeField] private float _rateOfChangeAlarm;

    private AudioSource _audioSource;

    private bool _isPlaying = false;

    private float _currentVolume = 0;
    private float _maxVolume = 1;
    private float _minVolume = 0;
    private float _targetVolume;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartAlarm();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopAlarm();
        }
    }

    private void Update()
    {
        if (_isPlaying)
        {
            _currentVolume = _audioSource.volume;

            if(_audioSource.volume == _minVolume)
            {
                _targetVolume = _maxVolume;
            }
            else if (_audioSource.volume == _maxVolume)
            {
                _targetVolume = _minVolume;
            }

            _audioSource.volume = Mathf.MoveTowards(_currentVolume, _targetVolume, _rateOfChangeAlarm);
        }
    }

    private void StartAlarm()
    {
        _audioSource.volume = _minVolume;
        _audioSource.Play();
        _isPlaying = true;
    }

    private void StopAlarm()
    {
        _audioSource.Stop();
        _isPlaying = false;
    }
}
