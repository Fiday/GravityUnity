https://mikhail-szugalew.medium.com/simulating-gravity-in-unity-ae8258a80b6d#:~:text=First%2C%20I%20used%20the%20formula,be%20applied%20to%20the%20planet

AttractionComponent -> min/max_weight

BlackHole -> require AttractionComponent
		  -> give horizon tag for better getchild function
		  
Orbiting -> require AttractionComponent, RigidBody, Gravity


InputControl -> require AttractionComponent, Rigidbody