# Snow Simulation
## Environment
- Unity 2018.1
## Introduction
- Simulate the snowing based on four forces, *gravity*, *buoyant*, *lift* and *drag*. The detail is in [Modeling Falling and Accumulating Snow](http://citeseerx.ist.psu.edu/viewdoc/download?doi=10.1.1.217.2707&rep=rep1&type=pdf). Due to the subtle influence on the snow movement, we ignore the force of *buoyant*.
- Simulate the snow accumulation based on shader.
![](https://i.imgur.com/2ScsIP9.png)
## Run
- Clone this project
```git
git clone https://github.com/cyj407/Snow-Simulation.git
```
- Open in Unity
    - Run the `SnowScene` scene to see the result of considering the force of *gravity* and *drag*. You could drag the slider to change the wind velocity.
    ![](https://i.imgur.com/kbBVoBp.jpg)
    - Run the `pureFormula` scene to see the result of considering the force of *gravity*, *drag* and *lift*.
## Reference
- [Create wind zone](https://www.youtube.com/watch?v=iCMhrKOuBZg)