# P4 Elective: Machine Learning in Gaming  
*The Hunger Games*

## Set-up: 
The Environment consists of an arena, an agent and a player.

## Goal: 
The agent must eliminate the opposite Player.

## Agents: 
The environment contains one agent linked to a single brain.
It has one gun and a target/aim laser.

## Agent Reward Function:
+1.0 If the target is hit
0.001 When the target is in sight
-0.001 For each shot
1.0 When the agent is hit
0.001 Time penalty

## Brains: 
One brain with the following observation/action space.
(A second brain was originally built in to train the first brain, but is now only used as a player.)

## Vector Observation space: 
(Continuous) 16 variables corresponding to position of agent, the target and distance to target.

## Vector Action space: 
(Discrete) Size of 7, corresponding to movement, rotation and shooting.

## Visual Observations (Optional): 
No Visual Observations.
(A test run with additional visual observation failed because of the computing power of my computer.)

## Reset Parameters: 
After hit, the agent and the target are placed at random.

# Results & Learnings
The result is depressing and unsatisfactory.
However, there are too many possible factors and sources of error due to which it could have failed. I've been playing with the hyperparameters, the observations and the rewards. The experiments led only partially and if to a slight improvement.

Possible sources of error could be the environment and the game logic. Possibly also the wrong observations, or I may not have found the right hyperparameters.
Machine Learning is a very time consuming and experimental area, where faster hardware would be an advantage.  
  
I will continue to experiment in this area, and I have already had first successes in other projects.

I have learned a lot in this elective and as a proof of my progress I have finally successfully trained the test environment from the beginning of the semester in the folder "CatMouse".


# Link
[https://github.com/BrandlMax/ML-Unity-Elective](https://github.com/BrandlMax/ML-Unity-Elective)
