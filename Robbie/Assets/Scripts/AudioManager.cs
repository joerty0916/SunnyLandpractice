using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    static AudioManager current;
    [Header("環境聲音")]
    public AudioClip ambientClip;
    public AudioClip musicClip;
    [Header("特效聲音")]
    public AudioClip deathFXClip;
    public AudioClip orbFXClip;
    public AudioClip opendoorFXClip;
    public AudioClip startlevelFXClip;
    public AudioClip winFXClip;
    [Header("角色音效")]
    public AudioClip[] walkStepClip;
    public AudioClip[] crouchStepClip;
    public AudioClip  jumpClip;
    public AudioClip deathClip;
    public AudioClip  jumpVoiceClip;
    public AudioClip deathVoiceClip;
    public AudioClip orbVoiceClip;
    
    AudioSource ambientSource;
    AudioSource musicSource;
    AudioSource fxSource;
    AudioSource playerSource;
    AudioSource voiceSource;
    public AudioMixerGroup ambientGroup,musicGroup,fxGroup,playerGroup,voiceGroup;



    private void Awake() {
        if(current!=null)
        {
            Destroy(gameObject);
            return;
        }
        current=this;
        DontDestroyOnLoad(gameObject);
        ambientSource=gameObject.AddComponent<AudioSource>();
        musicSource=gameObject.AddComponent<AudioSource>();
        fxSource=gameObject.AddComponent<AudioSource>();
        playerSource=gameObject.AddComponent<AudioSource>();
        voiceSource=gameObject.AddComponent<AudioSource>();
        ambientSource.outputAudioMixerGroup=ambientGroup;
        musicSource.outputAudioMixerGroup=musicGroup;
        fxSource.outputAudioMixerGroup=fxGroup;
        playerSource.outputAudioMixerGroup=playerGroup;
        voiceSource.outputAudioMixerGroup=voiceGroup;

        StartLevelAudio();

    }
    void StartLevelAudio()
    {
        current.ambientSource.clip=current.ambientClip;
        current.ambientSource.loop=true;
        current.ambientSource.Play();
        current.musicSource.clip=current.musicClip;
        current.musicSource.loop=true;
        current.musicSource.Play();
        current.fxSource.clip=current.startlevelFXClip;
        current.fxSource.Play();


    }
    public static void PlayerWonAudio(){
        current.fxSource.clip=current.winFXClip;
        current.fxSource.Play();
        current.playerSource.Stop();
    }

    public static void DoorOpenAudio(){
        current.fxSource.clip=current.opendoorFXClip;
        current.fxSource.PlayDelayed(1f);
    }

    public static void PlayerFootstepAudio(){
        int index= Random.Range(0,current.walkStepClip.Length);
        current.playerSource.clip=current.walkStepClip[index];
        current.playerSource.Play();
    }
    public static void PlayerCrouchAudio(){
        int index= Random.Range(0,current.crouchStepClip.Length);
        current.playerSource.clip=current.crouchStepClip[index];
        current.playerSource.Play();
    }
    public static void PlayerJumpAudio(){
        current.playerSource.clip=current.jumpClip;
        current.playerSource.Play();
        current.voiceSource.clip=current.jumpVoiceClip;
        current.voiceSource.Play();
    }
    public static void PlayerDeathAudio(){
        current.playerSource.clip=current.deathVoiceClip;
        current.playerSource.Play();
        current.fxSource.clip=current.deathFXClip;
        current.fxSource.Play();
        current.voiceSource.clip=current.deathClip;
        current.voiceSource.Play();

    }
    public static void PlayerordAudio()
    {
        current.playerSource.clip=current.orbVoiceClip;
        current.playerSource.Play();
        current.fxSource.clip=current.orbFXClip;
        current.fxSource.Play();
    }
    
}
