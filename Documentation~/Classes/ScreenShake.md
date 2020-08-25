[Back to main documentation page](https://github.com/mlaikhram/Unity-Common-Scripts/blob/master/Documentation~/Common.md)

# ScreenShake
Allows for dynamic addition of screen shake to the Camera object this script is attached to. The screen shake will naturally decay over time, imitating a dampening sine curve, where the pivot position is relative to the Camera's parent object (or the scene if the object does not have a parent). Intensity is determined by a `Vector3`, which represents the direction the camera is "hit" when applying the screenshake.

### Constants, Properties, and Methods
Name | Type | Description
-----|------|------------
`main` | `static ScreenShake` Property | The `ScreenShake` component associated with the main Camera. Will return null if either the ScreenShake component or the main Camera does not exist.
`shakeFrequency` | `float` Property | How fast the screen will shake (relates directly to the frequency associated with the underlying Sine curve).
`ShakeCamera(Vector3 intensity, float duration)` | `void` Method | Applies screen shake to the associated camera with the specified intensity and duration.

