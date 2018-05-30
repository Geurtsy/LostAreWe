using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(AudioSource))]
public class HealthManager {

    // Health.
    [Header("Health")]
    public float _maxHealthPoints;
    public float _currentHealthPoints;

    public float HealthPercentage
    {
        // Returns currrent health as a percentage.
        get { return (_currentHealthPoints / _maxHealthPoints) * 100; }

        // Sets health value from a percentage.
        set
        {
            int changedValue = Mathf.RoundToInt((value / 100.0f) * _currentHealthPoints);

            if (_currentHealthPoints > changedValue)
            {
                Hurt(0);
            }
        }
    }

    // Audio.
    [Header("Audio")]
    public AudioClip[] _hurtAudio;
    public AudioClip _deathAudio;
    public AudioSource _as;

    // Effects.
    [Header("Effects")]
    public GameObject _hurtEffect;
    public GameObject _posionEffect;
    public Transform _hurtEffectSP;

    public GameObject _deathEffect;
    public Transform _deathEffectSP;

    private bool _posioned;

    // Initialisation.
    public HealthManager(AudioSource audioSource)
    {
        _as = audioSource;
    }

    public void RestoreHealth()
    {
        _currentHealthPoints = _maxHealthPoints;
    }

    // Performs hurt actions.
    public void Hurt(float damage)
    {
        _currentHealthPoints -= damage;

        if(_currentHealthPoints < 0)
        {
            Death();
        }

        if(_posioned)
            GameObject.Instantiate(_posionEffect, _hurtEffectSP.position, _hurtEffectSP.rotation);
        else
            GameObject.Instantiate(_hurtEffect, _hurtEffectSP.position, _hurtEffectSP.rotation);

        Debug.Log(_as);
        _as.PlayOneShot(_hurtAudio[Random.Range(0, _hurtAudio.Length)]);
    }

    // Performs death actions.
    private void Death()
    {
        //GameObject.Instantiate(_deathEffect, _deathEffectSP.position, _deathEffectSP.rotation);
        //_as.PlayOneShot(_deathAudio);
    }

    public IEnumerator Posion(float damage, int intervals)
    {
        _posioned = true;

        for(int interval = 0; interval < intervals; interval++)
        {
            Hurt(damage / intervals);
            yield return new WaitForSeconds(1.0f);
        }

        _posioned = false;
        yield return null;
    }
}