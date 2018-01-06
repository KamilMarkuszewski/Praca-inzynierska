
// Require a character controller to be attached to the same game object
@script RequireComponent(CharacterController)

public var idleAnimation : AnimationClip;
public var walkAnimation : AnimationClip;
public var runAnimation : AnimationClip;
public var jumpPoseAnimation : AnimationClip;

public var walkMaxAnimationSpeed : float = 0.75;
public var trotMaxAnimationSpeed : float = 1.0;
public var runMaxAnimationSpeed : float = 1.0;
public var jumpAnimationSpeed : float = 1.15;
public var landAnimationSpeed : float = 1.0;
public var target : Vector3;
private var follow : boolean;
private var slow : boolean;

private var _animation;

enum CharacterState {
	Idle = 0,
	Walking = 1,
	Trotting = 2,
	Running = 3,
	Jumping = 4,
}

private var _characterState : CharacterState;

// The speed when walking
var walkSpeed = 2.0;
// after trotAfterSeconds of walking we trot with trotSpeed
var trotSpeed = 4.0;
// when pressing "Fire3" button (cmd) we start running
var runSpeed = 6.0;

var inAirControlAcceleration = 3.0;

// How high do we jump when pressing jump and letting go immediately
var jumpHeight = 0.5;

// The gravity for the character
var gravity = 20.0;
// The gravity in controlled descent mode
var speedSmoothing = 10.0;
var rotateSpeed = 500.0;
var trotAfterSeconds = 3.0;

var canJump = true;

private var jumpRepeatTime = 0.05;
private var jumpTimeout = 2.95;
private var groundedTimeout = 0.25;

// The camera doesnt start following the target immediately but waits for a split second to avoid too much waving around.
private var lockCameraTimer = 0.0;

// The current move direction in x-z
private var moveDirection = Vector3.zero;
// The current vertical speed
private var verticalSpeed = 0.0;
// The current x-z move speed
private var moveSpeed = 0.0;

// The last collision flags returned from controller.Move
private var collisionFlags : CollisionFlags; 

// Are we jumping? (Initiated with jump button and not grounded yet)
private var jumping = false;
private var jumpingReachedApex = false;

// Are we moving backwards (This locks the camera to not do a 180 degree spin)
private var movingBack = false;
// Is the user pressing any keys?
private var isMoving = false;
// When did the user start walking (Used for going into trot after a while)
private var walkTimeStart = 0.0;
// Last time the jump button was clicked down
private var lastJumpButtonTime = -10.0;
// Last time we performed a jump
private var lastJumpTime = -1.0;


// the height we jumped from (Used to determine for how long to apply extra jump power after jumping.)
private var lastJumpStartHeight = 0.0;


private var inAirVelocity = Vector3.zero;

private var lastGroundedTime = 0.0;

private var lastGroundedHeight = 0.0;
private var isControllable = true;

function Awake ()
{
	moveDirection = transform.TransformDirection(Vector3.forward);
	
	_animation = GetComponent(Animation);
	if(!_animation)
		Debug.Log("The character you would like to control doesn't have animations. Moving her might look weird.");
	
	/*
public var idleAnimation : AnimationClip;
public var walkAnimation : AnimationClip;
public var runAnimation : AnimationClip;
public var jumpPoseAnimation : AnimationClip;	
	*/
	if(!idleAnimation) {
		_animation = null;
		Debug.Log("No idle animation found. Turning off animations.");
	}
	if(!walkAnimation) {
		_animation = null;
		Debug.Log("No walk animation found. Turning off animations.");
	}
	if(!runAnimation) {
		_animation = null;
		Debug.Log("No run animation found. Turning off animations.");
	}
	if(!jumpPoseAnimation && canJump) {
		_animation = null;
		Debug.Log("No jump animation found and the character has canJump enabled. Turning off animations.");
	}
			
}


function UpdateSmoothedMovementDirection ()
{

	var cameraTransform = Camera.main.transform;
	var grounded = IsGrounded();



	// Forward vector relative to the camera along the x-z plane	
	var forward = cameraTransform.TransformDirection(Vector3.forward);

	
	forward.y = 0;
	forward = forward.normalized;

	// Right vector relative to the camera
	// Always orthogonal to the forward vector
	var right = Vector3(forward.z, 0, -forward.x);

	
	var v = Input.GetAxisRaw("Vertical");
	var h = Input.GetAxisRaw("Horizontal");
	if(target != null && follow && target.x != 0 && !(v == 1 || v == -1 || h==1 ||  h== -1 )) {
		transform.LookAt (target);
	}
	
	if(follow && (v == 1 || v == -1 || h==1 ||  h== -1 )){
		follow = false;
		target = new Vector3();
		var clickObj : UnityEngine.Object = GameObject.Find("click(Clone)");
		if(clickObj !=null ) Destroy (clickObj, 1);
	}
	
	if(target != null && follow) {
		v = 1.0;
	}
	// Are we moving backwards or looking backwards
	if (v < -0.2)
		movingBack = true;
	else
		movingBack = false;
	
	var wasMoving = isMoving;
	isMoving = Mathf.Abs (h) > 0.1 || Mathf.Abs (v) > 0.1;
		
	// Target direction relative to the camera
	var targetDirection = h * right + v * forward;

	// Grounded controls
	if (grounded)
	{
		// Lock camera for short period when transitioning moving & standing still
		lockCameraTimer += Time.deltaTime;
		if (isMoving != wasMoving)
			lockCameraTimer = 0.0;

		// We store speed and direction seperately,
		// so that when the character stands still we still have a valid forward direction
		// moveDirection is always normalized, and we only update it if there is user input.
		if (targetDirection != Vector3.zero)
		{
			// If we are really slow, just snap to the target direction
			if (moveSpeed < walkSpeed * 0.9 && grounded)
			{
				moveDirection = targetDirection.normalized;
			}
			// Otherwise smoothly turn towards it
			else
			{
				moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);
				
				moveDirection = moveDirection.normalized;
			}
		}
		
		// Smooth the speed based on the current target direction
		var curSmooth = speedSmoothing * Time.deltaTime;
		
		// Choose target speed
		//* We want to support analog input but make sure you cant walk faster diagonally than just forward or sideways
		var targetSpeed = Mathf.Min(targetDirection.magnitude, 1.0);
	
		_characterState = CharacterState.Idle;
		if (target != null && follow && slow == false){
			targetSpeed *= runSpeed;
			_characterState = CharacterState.Running;
		}else if(slow == false && !(Input.GetKey (KeyCode.LeftShift) | Input.GetKey (KeyCode.RightShift)))
		{
			targetSpeed *= runSpeed;
			_characterState = CharacterState.Running;
		}
		else if (Time.time - trotAfterSeconds > walkTimeStart)
		{
			targetSpeed *= trotSpeed;
			_characterState = CharacterState.Trotting;
		}
		else
		{
			targetSpeed *= walkSpeed;
			_characterState = CharacterState.Walking;
		}
		
		moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, curSmooth);
		
		// Reset walk time start when we slow down
		if (moveSpeed < walkSpeed * 0.3)
			walkTimeStart = Time.time;
	}
	// In air controls
	else
	{
		// Lock camera while in air
		if (jumping)
			lockCameraTimer = 0.0;

		if (isMoving)
			inAirVelocity += targetDirection.normalized * Time.deltaTime * inAirControlAcceleration;
	}
	

		
}

function setTargetPos (_t : Vector3)
{
	target = _t;
}


function stopFollow ()
{
	follow = false;
	target = new Vector3();
	var clickObj : UnityEngine.Object = GameObject.Find("click(Clone)");
    if(clickObj !=null ) Destroy (clickObj);
}

function ApplyJumping ()
{
	// Prevent jumping too fast after each other
	if (lastJumpTime + jumpRepeatTime > Time.time)
		return;

	if (IsGrounded()) {
		// Jump
		// - Only when pressing the button down
		// - With a timeout so you can press the button slightly before landing		
		if (canJump && Time.time < lastJumpButtonTime + jumpTimeout) {
			verticalSpeed = CalculateJumpVerticalSpeed (jumpHeight);
			SendMessage("DidJump", SendMessageOptions.DontRequireReceiver);
		}
	}
}


function ApplyGravity ()
{
	if (isControllable)	// don't move player at all if not controllable.
	{
		// Apply gravity
		var jumpButton = Input.GetButton("Jump");
		
		
		// When we reach the apex of the jump we send out a message
		if (jumping && !jumpingReachedApex && verticalSpeed <= 0.0)
		{
			jumpingReachedApex = true;
			SendMessage("DidJumpReachApex", SendMessageOptions.DontRequireReceiver);
		}
	
		if (IsGrounded ())
			verticalSpeed = 0.0;
		else
			verticalSpeed -= gravity * Time.deltaTime;
	}
}

function CalculateJumpVerticalSpeed (targetJumpHeight : float)
{
	// From the jump height and gravity we deduce the upwards speed 
	// for the character to reach at the apex.
	return Mathf.Sqrt(2 * targetJumpHeight * gravity);
}

function DidJump ()
{
	jumping = true;
	jumpingReachedApex = false;
	lastJumpTime = Time.time;
	lastJumpStartHeight = transform.position.y;
	lastJumpButtonTime = -10;
	
	_characterState = CharacterState.Jumping;
}

function setControllable(ustaw: boolean){
	isControllable = ustaw;
}

function Update() {
	
	if (!isControllable)
	{
		// kill all inputs if not controllable.
		Input.ResetInputAxes();
	}



	if (Input.GetButtonDown ("Jump"))
	{
		lastJumpButtonTime = Time.time;
	}

	UpdateSmoothedMovementDirection();
	
	// Apply gravity
	// - extra power jump modifies gravity
	// - controlledDescent mode modifies gravity
	ApplyGravity ();

	// Apply jumping logic
	ApplyJumping ();
	slow = false;
	follow = false;
	if( target.x != 0){
		target.y = transform.position.y;
		var odl = (target.x -transform.position.x )*(target.x -transform.position.x ) + (target.z - transform.position.z)*(target.z - transform.position.z);
		if(odl<1000000 && odl>100){
			follow = true;
			
		}else{
			if(odl < 100 && odl > 3){
				slow = true;
				follow = true;
			}else{
				follow = false;
				target = new Vector3();
				var clickObj : UnityEngine.Object = GameObject.Find("click(Clone)");
				if(clickObj !=null ) Destroy (clickObj);
			}
		}
	}
	
	// Calculate actual motion
	var movement = moveDirection * moveSpeed + Vector3 (0, verticalSpeed, 0) + inAirVelocity;
	movement *= Time.deltaTime;
	
	// Move the controller
	var controller : CharacterController = GetComponent(CharacterController);
	collisionFlags = controller.Move(movement);
	
   // ANIMATION sector
    if(_animation) {
        if(_characterState == CharacterState.Jumping) 
        {
            SendMessage("PlayAnimation", "jump_pose");
            if(!jumpingReachedApex) {
                _animation[jumpPoseAnimation.name].speed = jumpAnimationSpeed;
                _animation[jumpPoseAnimation.name].wrapMode = WrapMode.ClampForever;
                _animation.CrossFade(jumpPoseAnimation.name);
            } else {
                _animation[jumpPoseAnimation.name].speed = -landAnimationSpeed;
                _animation[jumpPoseAnimation.name].wrapMode = WrapMode.ClampForever;
                _animation.CrossFade(jumpPoseAnimation.name);            
            }
        } 
        else 
        {
            if(controller.velocity.sqrMagnitude < 0.1) {
                _animation.CrossFade(idleAnimation.name);
                SendMessage("PlayAnimation", "idle");
            }
            else 
            {
                if(_characterState == CharacterState.Running) {
                    _animation[runAnimation.name].speed = Mathf.Clamp(controller.velocity.magnitude, 0.0, runMaxAnimationSpeed);
                    _animation.CrossFade(runAnimation.name);    
					SendMessage("PlayAnimation", "run");
                }
                else if(_characterState == CharacterState.Trotting) {
                    _animation[walkAnimation.name].speed = Mathf.Clamp(controller.velocity.magnitude, 0.0, trotMaxAnimationSpeed);
                    _animation.CrossFade(walkAnimation.name);  
					SendMessage("PlayAnimation", "run");
                }
                else if(_characterState == CharacterState.Walking) {
                    _animation[walkAnimation.name].speed = Mathf.Clamp(controller.velocity.magnitude, 0.0, walkMaxAnimationSpeed);
                    _animation.CrossFade(walkAnimation.name);   
                    SendMessage("PlayAnimation", "walk");
                }
                
            }
        }
    }
    // ANIMATION sector
	
	// Set rotation to the move direction
	if (IsGrounded())
	{
		if(!follow && target.x == 0){
			transform.rotation = Quaternion.LookRotation(moveDirection);
		}
	}	
	else
	{
		var xzMove = movement;
		xzMove.y = 0;
		if (xzMove.sqrMagnitude > 0.001)
		{
			if( !follow && target.x == 0)	transform.rotation = Quaternion.LookRotation(xzMove);
		}
	}	
	

	
	// We are in jump mode but just became grounded
	if (IsGrounded())
	{
		lastGroundedTime = Time.time;
		inAirVelocity = Vector3.zero;
		if (jumping)
		{
			jumping = false;
			SendMessage("DidLand", SendMessageOptions.DontRequireReceiver);
		}
		
		var roznica = lastGroundedHeight - transform.position.y;
		if(roznica > 5){
			SendMessage("change_health", -roznica);
			//Debug.Log(gameObject.Find("localPlayer").GetComponent("MyData").mp);
		} 
		lastGroundedHeight = transform.position.y;
	}
	


}

function OnControllerColliderHit (hit : ControllerColliderHit )
{
//	Debug.DrawRay(hit.point, hit.normal);
	if (hit.moveDirection.y > 0.01) 
		return;
}

function GetSpeed () {
	return moveSpeed;
}

function IsJumping () {
	return jumping;
}

function IsGrounded () {
	return (collisionFlags & CollisionFlags.CollidedBelow) != 0;
}

function GetDirection () {
	return moveDirection;
}

function IsMovingBackwards () {
	return movingBack;
}

function GetLockCameraTimer () 
{
	return lockCameraTimer;
}

function IsMoving ()  : boolean
{
	return Mathf.Abs(Input.GetAxisRaw("Vertical")) + Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5;
}

function HasJumpReachedApex ()
{
	return jumpingReachedApex;
}

function IsGroundedWithTimeout ()
{
	return lastGroundedTime + groundedTimeout > Time.time;
}

function Reset ()
{
	gameObject.tag = "Player";
}

