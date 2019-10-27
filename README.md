# 3D-Space-Shooter-Demo
This demo was adapted from the Unity Space Shooter demo, found here: https://learn.unity.com/project/space-shooter-tutorial
## Tutorial/Wiki
1. Add the "Player" prefab to the scene and reset its position.
2. Our camera needs to be set up to follow the player. Begin by setting its position to (0, 1, -2.75) to place it behind the player.
3. Set "Clear Flags" to "Solid Color" on the camera, and set the background color to black.
4. We want the camera to follow the player, so we need to add code to the CameraMovement script. Open up the script.
5. Add the following lines of code to the conditional of the update (line 21) to move the camera with tha player (when the player exists):
```cs
transform.position = player.transform.position + offset;
transform.rotation = player.transform.rotation;
```
6. Attach the script to the camera.
7. In the scene hierarchy, create a directional light and rename it "Main Light".
8. Set the light's position to origin and set its rotation to (0, -90, 0).
9. Duplicate the light and rename it "Fill Light".
10. Set its rotation to (0, 90, 0) and reduce its intensity to 0.5. Change its color to a pastel blue (#aec6cf).
11. Add an empty game object to the scene and name it "Lighting".
12. Set its position to origin and drag the lights into it in the hierarchy (to make them children of "Lighting").
13. Add the starfield prefab to the scene.
14. Open up the PlayerController script. We ned to add some physics movement. In the fixed update, on line 30, add the following movement code:
```cs
float moveHorizontal = Input.GetAxis("Horizontal");
float moveVertical = Input.GetAxis("Vertical");

Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
rb.velocity = movement * speed;


Mathf.Clamp(rb.position.x, -15, 15);
Mathf.Clamp(rb.position.y, -15, 15);

if (Mathf.Abs(rb.position.x) > 15 || Mathf.Abs(rb.position.y) > 15)
{
    rb.velocity = Vector3.zero;
}
```
15. Attach the script to the player.
16. Back in the script component of the player, give the player a speed of 10.
17. The player's movement is currently sort of boring. Let's juice it up and add some tilt! Add the following line of code to the bottom of the fixed update of the PlayerController script:
```cs
rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
```
18. Give the player a tilt of 0-2 based on preference.
19. Now we want to let the player shoot! In the player's PlayerController script component, attach the Bullet prefab to "Shot" and attach the Shot Spawn gameobject child of the player to "Shot Spawn".
20. Open the PlayerController script once again and add the following code to the update so that we can shoot the bullet:
```cs
if (Input.GetButton("Fire1") && Time.time > nextFire)
{
    nextFire = Time.time + fireRate;
    Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
}
```
"Fire1" is left mouse click by default.
21. Set the fire rate in the script component of the player to something reasonable, like .25.
22. Notice how the bullets stay on the screen as they are shot. We need to add a boundary that deletes them. Add a boundary prefab to the scene and give it the DestroyByBoundary script.
23. Now let's work on the asteroids. Open the RandomRotator script and add this line to its Start method:
```cs
rb.angularVelocity = Random.insideUnitSphere * tumble;
```
24. Attach the script to the asteroid prefab and set the tumble to 5 in the script component of the asteroid.
25. Attach the mover script from before and set the movement speed to somewhere around -5.
26. The asteroid will now move, but cannot be destroyed by anything. Open the script DestroyByContact and add the following method:
```cs
void OnTriggerEnter(Collider other)
{
    Instantiate(explosion, transform.position, transform.rotation);
    if (other.tag == "Player")
    {
        Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
    }
    Destroy(other.gameObject);
    Destroy(gameObject);
}
```
27. Add the DestroybyContact script to the asteroid. Add the "astroid explosion" VFX prefab to the Explosion field and "player explosion" prefab to the Player Explosion field.
28. If you add asteroids to the scene, they will explode immediately. To avoid this, add the following condititonal to the top of the OnTriggerEnter method of the DestroyByContact script:
```cs
if (other.tag == "Boundary")
{
    return;
}
``` 
This uses a tag to detect that the asteroid should not be destroyed by the boundary.
29. Add the Game Controller prefab to the scene. 
30. Open the GameController script and add the following code:
```csharp
void Start()
{
    StartCoroutine(SpawnWaves());
}

IEnumerator SpawnWaves()
{
    yield return new WaitForSeconds(startWait);
    while (true)
    {
        for (int i = 0; i < hazardCount; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(hazard, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(spawnWait);
        }
        yield return new WaitForSeconds(waveWait);
    }
}
```
31. Add the script to the Game Controller. Set spawn position to (10, 10, 16), hazard count to something around 10, spawn wait to around .5, start wait to around 1, and wave wait to around 4.
32. Test it out to see the game in action!
