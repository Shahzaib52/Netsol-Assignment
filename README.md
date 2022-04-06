# Netsol-Assignment

## Overview

Project loosely follows Observer Pattern in a way that objects communicate via Actions and Funcs, focus mainly remained on data-driven approach and modularity with extensive use of interfaces, abstraction and inheritance.

## Flow

- WrldApiRequestHandler.cs communicates with WRLD restful APIs to fetch POI-Sets and POIs (I was unable to find out of the box functionality to get POIs through Unity SDK so I made my own)
- EnemyHandler.cs calls WrldApiRequestHandler.RequestPOIs(callback) method and pass a callback as a parameter for a function to invoke as/when UnityWebRequest completes. 
- callback returns with a List of all the POIs placed via Map/Place Designer
- EnemyHandler.cs then iterates over this collection of POIs and start placing enemies on given coordinates (some are placed over the buildings using Building-API and some are placed on the Road using Transport-API)
- Finally, after EnemyHandler.cs done placing enemies, an event is triggered for AutoTargetHandler.cs to cache this List of enemy objects and iterate over them as/when needed.

## Note
Open <b>Demo</b> scene playtest the project.

<img width="663" alt="image" src="https://user-images.githubusercontent.com/26239787/162052432-7c839c7b-f5b1-41ca-bd9d-375f48e38d2f.png">


<b>Points Of Interest Placement</b>

https://user-images.githubusercontent.com/26239787/162039962-9108437f-9717-484b-a5aa-ac6381f856d0.mov

<b>Physics-Based Drone Controller (Yaw, Thrust, Tilt, Lift) </b>

https://user-images.githubusercontent.com/26239787/162040018-399212b9-e5e5-47c1-86cb-5dee3b7a0d17.mov

<b>Manual-Firing Mode (Spacebar To Shoot)</b>

https://user-images.githubusercontent.com/26239787/162039997-92d1506c-d5a1-4093-a8cf-9b7d403f0230.mov

<b>Auto-Targetting Closest Object In Front Of The Camera, Ignores Objects Behind Buildings</b>

https://user-images.githubusercontent.com/26239787/162039895-d8a308b4-18ef-4818-aedd-4d397ed522b8.mov

<b>Auto-Targetting With Auto-Fire Mode (Press O To Toggle)</b>

https://user-images.githubusercontent.com/26239787/162039933-3c6150fd-8c5c-44a3-a130-4e622bdbb1e2.mov
