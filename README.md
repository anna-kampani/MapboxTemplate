# MapboxTemplate
Template for 3D mapping and geospatial visualisations in Unity3D

## Requirements
The project is developed with Unity 2019.4.0f1
You will need a Mapbox Token. To create a Mapbox account go here: https://account.mapbox.com/auth/signup/?route-to=%22https://account.mapbox.com/%22
You will also need to upload the ASALondon1000.zip to your mapbox account.

## Project Description
There are 2 scenes in the project. The first generates a map from open street map data and implements some simple interactions by selectig buildings and showing their data. The second generates a map by harnessing some space syntax analysis results and colour-code them based on the values.

## Setup
After cloning the repo, to add your mapbox token to the project:
- Go to mapbox setup and add your token.
- Upload the ASALondon1000.zip to your mapbox account. Copy the tileset ID.
- In the "SpaceSyntaxMap" scene, go to the map object. Go to the layers section in the inspector and paste ',' and the tilesetID.
- Set the data source of the ASALondon feature to match the tilesetID you just pasted.
