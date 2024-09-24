using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _playerHitAudio;
    [SerializeField] private AudioSource _enemyHitAudio;
    [SerializeField] private AudioSource _deathAudio;
    [SerializeField] private AudioSource _loseAudio;

    public void PlayPlayerHitAudio()
    {
        if ( _playerHitAudio != null)
        {
            if(!_playerHitAudio.isPlaying )
            {
                _playerHitAudio.Play();
            }
        }
    }

    public void PlayEnemyHitAudio()
    {
        if (_enemyHitAudio != null)
        {
            if (!_enemyHitAudio.isPlaying)
            {
                _enemyHitAudio.Play();
            }
        }
    }
    public void PlayDeathAudio()
    {
        if (_deathAudio != null)
        {
            if (!_deathAudio.isPlaying)
            {
                _deathAudio.Play();
            }
        }
    }

    public void PlayLoseAudio()
    {
        if (_loseAudio != null)
        {
            if (!_loseAudio.isPlaying)
            {
                _loseAudio.Play();
            }
        }
    }
}