# GE2-ASSIGNMENT
GE2 Assignment
# Bird and deforestation simulation

Name: Simon Kennedy

Student Number: C19496436

Class Group: DT211c / TU857

Video: [Youtube]()

## Descripotion of the Project


## Instructions for use
there are four main buttons on the screen
they are fairly self explanatory, spawn hunter will create a prefab that will seek to the nearest tree and cut it down. spawn bird will create a new bird boid. spawn tree will grow a new tree in a random spot. save nature will remove all instances of the hunter prefab

## How it works
the boids are controlled via a state machine. they will start in the alone state, and swap to the group state when nearby another boid. The boids will group and flock together. After some time, the boids will grow tired and want to rest, entering the landing state. they will create a gameobject somewhere near to them which will fire a ray downwards. if it hits a tree, it will travel towards it. when it arrives, it will enter the resting state and regain its energy, then reenter the alone state. if it hits the ground, ot will migrate away, entering the dead state, and will leave then be destroyed. while resting, boids fire a ray down to check if the tree is cut down while they are in it. if it is, they will enter the dead state. 
the boids compare their distance from one another by iterating through a full list of all boids and comparing their distances. if its below a certain threshold, they are nearby. as this is running for 100+ boids each frame, this is inefficient but still works. 
the boids states do not move the boids, only change their rotation. there is a script attached to each one that moves it along its forward vector. 

the hunter is controlled by a simple that compares its distance to each of the trees and travels to the closest one. when it collides with one, it will destroy the tree, and remove it from the tree list.

as multiple objects are comparing these lists, the lists are stored in an empty gameobject so that they are all working with the same data. this prevents indexoutofboundserrors

the UI implements 4 buttons. these buttons call a relevant gameobject with a script to instantiate or destroy the relevant gameobjects.




## Classes and assets

| Class/asset | Source |
| ----------- | ------ |
|  |  |
| | |
|  | Self Written |
|  | Self Written |
|  | modified from [refrence]() |
|  | modified from [refrence]() |
|  | Self Written |
|  | from [refrence]() |
|  | Self Made |
|  | Self Made |
|  | Self Made |
|  | modified from [refrence]() |
|  | from [refrence]() |
|  | from [refrence]() |




## Refrences 
* 
* 
*  
* 
* 
 
# What I am most proud of in the assignment
I am most proud of the boids implementation of a state machine. as there are many, many different things being checked each frame, along with the awkward distance check algorithm, running all these in a single script would have exacerbated issues. to remedy this, the boids implement a finite state machine, running only the necessary scripts based on their current condition. 

```cs
{
    
}
```

# What I learned
in this assignment, I was able to learn how to implement boids, to flock, align and avoid each other. I learned how to implement a finite state machine, and got experience in using a user interface. 

## Known issues

*boids will not avoid trees or each other and phase through them
* boids when flocking trend upwards 
* distance checking algorithm needs optimisation. 
* timer checks are run in the update methods rather than a co-routine. this is because when they were tried, they would correctly wait the correct time once, then execute every frame.
*missing animations on tree growth and cut down 
