# Lyon Monde Virtuel - Projet M2

### Promotion M2 Gamagora - Ingénieurie du jeu vidéo - 2022-2023

Emplacement : Gratte-Ciel - Lyon
Coordonnées : EPSG 3946
Normes : RGF93 CC46


# Processus de création - Données

Une grande partie des données ont été extraites sous le format GeoJSON via plusieurs sources.[^1]
[^1]: Data Grand Lyon, IGN et OpenStreetMap[^2] pour les GeoJSON.
[^2]: GéoDataMine a été utilisé pour facilité l’extraction de données sur OpenStreetMap et plus majoritairement des .osm.
*[IGN]: Institut National de l'Information Géographique et Forestière

## Bâtiments

GML — Python3DTiles → 3DTiles → Unity

### Afficher une TileSet3D dans Unity

1.  Cloner le repo : [Unity3DTiles](https://github.com/NASA-AMMOS/Unity3DTiles)
    
2.  Importer les tilesets GratteCiel_bati (ou Villeurbanne_bati) et Villeurbanne_relief dans le dossier “Assets/StreamingAssets”
    
3.  Ajouter le component “Tileset Behaviour (Script)” à un Empty Object et indiquer l’url de la tileset en utilisant : “data://Villeurbanne_relief/tileset.json”
    
4.  Réitérer l’étape 3 avec l’url : “data://GratteCiel_bati/tileset.json” (ou “data://Villeurbanne_bati/tileset.json”)
    
5.  Enregistrer et lancer la scène

**Remarque :** il faut ajouter un offset à la position du relief pour que les bâtiments soient positionnés correctement :

-   -38.6 -10.3 -462.1 avec Villeurbanne_bati
    
-   -901.6 2.64 -355.9 avec GratteCiel_bati

## Routes

On a utilisé [OSMOSIS](https://github.com/openstreetmap/osmosis/releases/tag/0.48.3) pour l’extraction des routes à partir de fichier .osm téléchargé sur OpenStreetMap. OSMOSIS permet également d’afficher et éditer la carte dans un outil Java.

## Arbres, Parc à vélos et autres données[^3]
[^3]: - Ligne de bus,
Arbres référencés par Grand Lyon,
Lieux surfaciques,
Parcs et jardins,
Mairies,
Données sur les espaces végétalisés et artificialisés,
Musées,
Mobilier urbain (abris voyageurs, abris taxi, etc…),
Corbeilles de rue,
Parc de stationnement de vélos.

A l'aide d'un [JSON Parser](https://github.com/Bunny83/SimpleJSON) pour facilité la lecture des GeoJSON nous avons créer un lecteur de GeoJSON adapté pour Unity afin de placer les différents objets.

# Processus de création - Assets

## Bâtiments

La méthode d’extraction des bâtiments fait que leurs assets sont déjà présents.

## Routes et trafics

Nous avons cherche plusieurs plugin qui permettrai de faire une simulation d’un trafic de voiture, cependant une majorité des plugins sont payants.

Puis l'on c'est mis en tête de faire notre simulation mais on c'est rendu compte que vu le temps restant, nous n'aurons pas le temps de réaliser le travail demander.

Nous avons finalement trouver un version gratuite du plugin [Global Roads & Traffic](https://assetstore.unity.com/packages/3d/environments/roadways/global-roads-traffic-lite-218045).

## Liste des Assets

[Arbres](https://assetstore.unity.com/packages/3d/vegetation/trees/realistic-tree-pack-vol-1-50418#description)
[Poubelles](https://assetstore.unity.com/packages/3d/props/waste-can-pbr-224775)
[Végétation](https://assetstore.unity.com/packages/3d/environments/smm-stylized-grass-184975)

# Participants

* [**Adrien Philippon**](https://github.com/adrimyla)
* [**Dina Randriazanamparany**](https://github.com/Dina-MR)
* [**Hugo Pasquier**](https://github.com/HugoPasquier)
* [**Joris Rossi**](https://github.com/Aikkyo)
* [**Julien Percheron**](https://github.com/JulienPercheron)
* [**Julien Martins**](https://github.com/julien-martins)
* [**Louis Celerier**](https://github.com/Louis-Celerier)
* [**Lucas Venturini**](https://github.com/venturinilucas)
* [**Mathieu Dagier**](https://github.com/Matda43)
* [**Nicolas Vivier**](https://github.com/Nicovi15)
* [**Nils Carron**](https://github.com/NilsCarron)
* [**Romain Savaete**](https://github.com/Savro01)
* [**Wesley Petit**](https://github.com/wesley-petit)

# Ressources

[Data Grand Lyon](https://data.grandlyon.com/)

[IGN](https://geoservices.ign.fr/)

[OpenStreetMap](https://www.openstreetmap.fr/)

[OSMOSIS by OpenStreetMap](https://github.com/openstreetmap/osmosis/releases/tag/0.48.3)

[GéoDataMine ](https://geodatamine.fr/)

[Unity3DTiles](https://github.com/NASA-AMMOS/Unity3DTiles)

[3DTiles in Unity by Lorenzo Marnat](https://github.com/VCityTeam/UD-SV/blob/master/ImplementationKnowHow/3DTiles_in_Unity.md)

[JSON Parser by Bunny83](https://github.com/Bunny83/SimpleJSON)
