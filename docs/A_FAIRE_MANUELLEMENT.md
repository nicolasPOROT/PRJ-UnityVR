# Points à compléter

## Analyse Android Studio

Analyse APK Analyzer et captures : **non réalisées**.

1. Ouvrir **Build > Analyze APK** dans Android Studio.
2. Sélectionner Builds/Android_Quest/FruitNinjaVR_Quest.apk ou sa copie dans Livrable/Quest.
3. Capturer la vue d'ensemble, puis assets/bin/Data et lib/arm64-v8a avec libil2cpp.so et libunity.so. Faire apparaître classes.dex et resources.arsc.
4. Relever les colonnes de taille et leurs unités, puis ajouter les captures à la section 4 du rapport.

APK : **57 851 307 octets**, SHA256 **BEB89D96902D651B30F116E4FB14DF865CA2C7EEDD5E901E8D40C60E258053FC**.

## Conditions des essais

Le même PC a servi à la compilation et au test PCVR : **Intel i7-9700KF, RTX 2060 et 15,9 Gio RAM**. Paramètres non relevés : runtime OpenXR Windows actif, fréquence du Quest, résolution et débit Virtual Desktop, version système Quest.

FPS : **non mesurés**. Latence, stabilité prolongée et chauffe : **non testées**.

## Relevé FPS

Le compteur est masqué dans les builds de livraison. Il n'existe pas de raccourci ou de bouton en jeu pour l'activer ; un nouveau build est nécessaire pour l'afficher sur le player Windows ou Quest.

1. Dans Assets/Prefabs/Player.prefab, sélectionner Camera Offset/Main Camera/DebugFpsCanvas, puis cocher **Show Overlay** sur FpsOverlay.
2. Sauvegarder et produire un build de test dans un dossier distinct des livrables.
3. Après 30 s de jeu, relever le compteur toutes les 10 s pendant une minute en Facile, puis en Extrême. Noter casque, fréquence et réglages graphiques.
4. Calculer la moyenne et le minimum des échantillons ; le compteur utilise une fenêtre de 0,5 s.
5. Décocher Show Overlay et sauvegarder après l'essai.

## Remise

Rapport : [PDF](rapport_part1_benjamin_joannard.pdf). Archive : **Livrable/Remise_FruitNinjaVR_Part1_Benjamin_JOANNARD.zip**.

Accès enseignant au dépôt : à vérifier avant remise. Sources sur la branche **Mini-projet-Ben**.
