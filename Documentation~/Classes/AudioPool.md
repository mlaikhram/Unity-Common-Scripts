[Back to main documentation page](https://github.com/mlaikhram/Unity-Common-Scripts/blob/master/Documentation~/Common.md)

# AudioPool
This class utilizes Unity's `AudioSource` component to manage positional audio from multiple sources in a controlled pool, allowing the max number of simultaneous sounds to be limited. This is useful when multiple objects in the scene have the potential to make sound, because if the max number of sounds has been reached and a new sound attempts to be played, the oldest sound will be stopped beforehand. The size of the audio pool is configurable. Note that this component requires the `AudioSource` component to be attached as well, as all `AudioSource` components in the generated pool will inherit the properties of this `AudioSource`, and will use this `AudioSource`'s `AudioClip` as a default value.

### Constants, Properties, and Methods
Name | Type | Description
-----|------|------------
`PoolSize` | `uint` Property | Determines the max number of audio sources that can be played at once from this `AudioPool`.
`PlaySound(AudioClip audio = null)` | `void` Method | Plays the passed in `AudioClip` from this `GameObject`'s position, or plays the default `AudioClip` if none is specified.
`PlaySoundAt(Vector3 position, AudioClip audio = null)` | `void` Method | Plays the passed in `AudioClip` from the specified position, or plays the default `AudioClip` if none is specified.
`PlaySoundAs(Transform targetTransform, AudioClip audio = null)` | `void` Method | Plays the passed in `AudioClip` as the specified `Transform`, or plays the default `AudioClip` if none is specified.
