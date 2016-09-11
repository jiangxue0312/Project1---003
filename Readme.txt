group only one person
665401 Xue Jiang 
jiangx2

Terrain generated using diamond-square algorithm, 
first generate a 2d array, storing the height by using x and z value as the index.
mesh the gameobject with the 2d array.

The sun is a pointlight.
water surface is a plane, and the position is calculated with the seed and roughness of the landscape.
 

code references:
answers.unity3d.com/questions/1033085/heightmap-to-mesh.html
stackoverflow.com/questions/2755750/diamond-square-algorithm
custom shader is from the lab5 material
