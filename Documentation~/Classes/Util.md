[Back to main documentation page](https://github.com/mlaikhram/Unity-Common-Scripts/blob/master/Documentation~/Common.md)

# Util
This class contains several extension methods meant to increase the flexibility of pre-existing classes in Unity.

### Constants, Properties, and Methods
Name | Type | Description
-----|------|------------
`CopyComponent<T>(this GameObject go, T toAdd)` | `static T where T : Component` Method | Creates a copy of the specified component, `toAdd`, and attaches it to `go`.
