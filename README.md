## Escape
`Escape` is the arcade game with `Fluids` simulation and smart `AI`.  
`MonoGame` Framework is used.  

## **[Video](https://youtu.be/qsJHql4KkC0)**

![Intro](https://user-images.githubusercontent.com/5301844/43815883-9b56d466-9ada-11e8-9a6e-932fcee0a3cb.gif)
![Pressure](https://user-images.githubusercontent.com/5301844/43816055-9ad36742-9adb-11e8-8142-3973ec8a6657.gif)
![Velocity](https://user-images.githubusercontent.com/5301844/43816447-84906262-9add-11e8-82be-9fe00dcd920f.gif)
![Interaction](https://user-images.githubusercontent.com/5301844/43816663-84f10b34-9ade-11e8-966a-0b99fa1f5843.gif)

## Fluids
Escape uses `2D Fluid Simulation` on `GPU` with `Vorticity Confinement` and `MacCormak Advection` scheme based on `Navier-Stokes` equations for incompressible flow. To solve them numerical methods are used.

* Because of the large amount of parallelism in graphics hardware, the simulation runs significantly faster on the `GPU` than on the `CPU`. Implementation is based on `HLSL` shaders, samplers and `render to texture` technique.

* To achieve higher-order accuracy, Escape uses a `MacCormack` scheme that performs two intermediate `semi-Lagrangian` advection steps.

* Also there are arbitrary boundaries conditions around obstacles implemented.
In 3D realization, the `Fluid` is rendered by `Ray-Casting` technique.

Fluid integration into gameplay mechanics performed on `CPU` is shown on the diagram below:

![image](https://cloud.githubusercontent.com/assets/5301844/2763364/ab0f65a6-ca02-11e3-86f4-f85336b6b9ab.png)

## Game Objects model
It is based on the Composition design pattern. It provides uniform interface to handle either single `Object` or entire `Objects` hierarchy:

![image](https://cloud.githubusercontent.com/assets/5301844/2763449/e7cc7604-ca03-11e3-94bf-bebff0aa94da.png)    
![image](https://cloud.githubusercontent.com/assets/5301844/2763461/08b8e122-ca04-11e3-97c2-daff2d9e2d74.png)     

## Drawing
Drawing of game objects is performed by the `Drawable` class. 
It is included into `Object` through composition.

![image](https://cloud.githubusercontent.com/assets/5301844/2763524/ba0587b4-ca04-11e3-9f5a-da9fed113f81.png)

## Modules
![image](https://cloud.githubusercontent.com/assets/5301844/2763539/fa59014c-ca04-11e3-88ad-bd98603547b7.png)
