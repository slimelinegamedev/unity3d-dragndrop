# unity3d-dragndrop
A lightweight drag and drop system for unity 3d game engine. The code was originally generated for 2D board game style interaction. 

## Overview
unity3d-dragndrop comes with 4 simple components. 

- Draggable
    - Can be used by itself for simple mouse following behaviour.
- Dropable
    - Used on an object with a Draggable component
- Dropzone
    - Used on a target area for Dropable objects to land
- MoveToTarget
    - Used within the code for default auto-move behvaiour on drop / return

**Note**: Draggables, Dropables and Dropzones currently require a Collider2D component

##Usage
See the example project for a tic-tac-toe board with drag and drop. Just attach Collider2D, Draggable and Dropable components to your objects and Dropzone to your 'landing areas'. 
