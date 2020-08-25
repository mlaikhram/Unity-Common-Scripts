[Back to main documentation page](https://github.com/mlaikhram/Unity-Common-Scripts/blob/master/Documentation~/Common.md)

# ObjectPool
This class utilizes List storage and object recycling to optimize performance in areas with high object spawning and despawning frequency by bypassing the need to Instantiate and Destroy objects repeatedly. An `ObjectPool` will instead create a fixed amount of copies of a given object, and cycle through those copies, enabling and disabling them for usage in the scene when required. Since Enable and Disable will be used instead of Instantiate and Destroy, The `OnEnable()` and `OnDisable()` functions should be created appropriately on objects that are designed for object pooling.

### Constants, Properties, and Methods
Name | Type | Description
-----|------|------------
`poolPosition` | `static Vector3` Property | The position where object pool copies will initially be spawned (these objects will be disabled by default). Default value is (0, -100, 0).
`Object` | `T` Property | The object that will be spawned by this pool.
`Count` | `int` Property | The max number of objects in the pool.
`ObjectPool(T objectTemplate, uint poolSize)` | `ObjectPool` Constructor | Creates a new `ObjectPool` with `Component` `objectTemplate` and size `poolSize`.
`GetObject(Vector3 position)` | `T` Method | Returns the oldest object in tihe pool, calling `OnDisable()` at its previously enabled location if appropriate, and calling `OnEnable()` at the new location.
