[Back to main documentation page](https://github.com/mlaikhram/Unity-Common-Scripts/blob/master/Documentation~/Common.md)

# BackgroundMusic
This class utilizes Unity's `AudioSource` component to manage background music. The `DontDestroyOnLoad` scene is utilized to allow the background music to keep playing across multiple scenes. The audio will be centered at the Main Camera to prevent volume falloff. Note that a `BackgroundMusic` and `AudioSource` component must be attached to an empty GameObject in the scene, and the `AudioSource` must contain an `AudioClip`. If a new scene is loaded while the background music is still playing, the background music will not be interrupted unless the new scene has a `BackgroundMusic` component with a different `AudioClip` on the associated `AudioSource`, in which case the new `AudioClip` will be played.

### Constants, Properties, and Methods
Name | Type | Description
-----|------|------------
`AudioSource` | `static AudioSource` Property | The `AudioSource` associated with the `BackgroundMusic`. Will return null if `BackgroundMusic` does not exist.
`IsPlaying` | `static bool` Property | Whether or not the background music is currently playing. Will return false if `BackgroundMusic` does not exist.
`Stop(float fadeTime = 0f)` | `static void` Method | Stops the background music by fading out for `fadeTime` seconds.
`Play(float volume = 0f)` | `static void` Method | Plays the background music with the specified `volume`, or plays at the initial volume if none is specified.
`ChangeAudio(AudioClip audioClip, bool loop = true)` | `static void` Method | Plays the specified `audioClip` as the background music, looping unless specified otherwise.
