using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalingController : MonoBehaviour
{
    [SerializeField] private float _rateOfChangeAlarm;

    private AudioSource _audioSource;

    private int _maxVolume = 1;
    private int _minVolume = 0;

    private bool _canThisBeRun = true;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Hero>(out Hero hero))
        {
            if (_canThisBeRun == true)
            {
                StartCoroutine(ChangeAlarmVolume(_maxVolume));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Hero>(out Hero hero))
        {
            if (_canThisBeRun == true)
            {
                StartCoroutine(ChangeAlarmVolume(_minVolume));
            }
        }
    }

    private IEnumerator ChangeAlarmVolume(float targetVolume)
    {
        _canThisBeRun = false;

        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _rateOfChangeAlarm);

            yield return null;
        }

        _canThisBeRun = true;
    }
}
