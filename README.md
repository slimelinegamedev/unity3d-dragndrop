# unity3d-dragndrop
A lightweight drag and drop system for unity 3d game engine. The code was originally generated for 2D board game style interaction. By default, when using the Dropable component, failed drops will return the object to a their original position or lock to Dropzone position on success. (See <a href="#scriptevents">Scripting & Events</a> for overriding this behaviour)

## Overview
unity3d-dragndrop comes with 4 simple components. 

- Draggable
    - Can be used by itself for simple mouse following behaviour.
- Dropable
    - Used on an object with a Draggable component
- Dropzone
    - Used on a target area for Dropable objects to land. Stores the Dropable component that it hosts.
- MoveToTarget
    - Used within the code for default auto-move behvaiour on drop / return

**Note**: Draggables, Dropables and Dropzones currently require a Collider2D component

##Usage
See the example project for a tic-tac-toe board with drag and drop. Just attach Collider2D, Draggable and Dropable components to your objects and Dropzone to your 'landing areas'. 

<a id="scriptevents"></a>
## Scripting & Events 
The following events are available to interact with the system in your code.

###Draggable
    OnDragStart
    OnDragStop

###Dropable
Return a boolean from these: If true, allow the default drop/return behaviour. If false, interrupt the default behaviour

    bool OnDropAccepted
    bool OnDropRejected

These fire if/when the default auto move behaviour completes

    OnDropComplete
    OnReturnComplete

###Dropzone
Fire when an object is dropped here, or lifted off

    OnDrop
    OnLift

### MoveToTarget
If you are using DragNDrop default setup, you should not need to use MoveToTarget yourself. However if you do you can do the following:

``` cs
// static utility method to construct the 
// component and attach it to given GameObject
// this will use default behaviour, including destorying itself on arrival
MoveToTarget moveTo = MoveToTarget.Go(myGO, pos);
moveTo.OnArrival = MyAwesomeCallback;
```