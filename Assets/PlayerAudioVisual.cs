using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for handling the audios and visual of the player, e.g. animation and sound
/// </summary>
public class PlayerAudioVisual : MonoBehaviour, IPlayerEventHandler
{
    Animator anim;
    SpriteRenderer sr;
    AudioSource audioSource;
    ParticleSystem ps;

    [Range(0, 1)]
    public float volume;

    public AudioClip jumpClip;
    public AudioClip landingClip;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        audioSource = this.GetComponent<AudioSource>();
        sr = this.GetComponent<SpriteRenderer>();
        ps = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void IPlayerEventHandler.OnMovingChange(bool isMoving, Direction dir)
    {
        anim.SetBool("isMoving", isMoving);

        if (dir.Equals(Direction.Right))
        {
            sr.flipX = false;
        }

        else if (dir.Equals(Direction.Left))
        {
            sr.flipX = true;
        }
    }

    void IPlayerEventHandler.OnGrappledChange(bool isGrappled)
    {
        anim.SetBool("isGrappled", isGrappled);
    }

    void IPlayerEventHandler.OnGroundedChange(bool isGrounded)
    {
        anim.SetBool("isGrounded", isGrounded);
        if (isGrounded)
        {
            audioSource.PlayOneShot(landingClip, volume);
        }
    }

    void IPlayerEventHandler.OnJump()
    {
        ps.Emit(50);
        audioSource.PlayOneShot(jumpClip, volume);
    }
}
