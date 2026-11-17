# Vérifications manuelles et limites des tests

État initial : aucun test Play Mode, casque, build ou mesure FPS réalisé pendant l'audit statique. Mettre à jour cette liste avec les résultats réellement obtenus.

État actuel : les deux builds sont produits, les cinq difficultés et le parcours logique complet ont été vérifiés dans l'éditeur. La copie de livraison Windows (184 fichiers) et l'APK sont conformes par SHA256. Restent les tests physiques ci-dessous, APK Analyzer et les captures correspondantes ; ne pas les marquer OK sur la seule base des tests automatisés.

## Contrôles dans l'éditeur avant les builds

- Ouvrir ce projet avec Unity **6000.0.58f1**. Ne pas utiliser 6000.5.0f1. Après import, attendre la fin de compilation et relever erreurs et warnings dans Window > General > Console.
- Ouvrir MainMenu puis PlayScene hors Play : vérifier les composants et références des objets et des prefabs (Player, GameplayZone, canons, fruits, bombes, effets). Aucun Missing Script/Reference ne doit subsister. Sauvegarder chaque scène après modification.
- Dans Project Settings > XR Plug-in Management > OpenXR > Project Validation, sélectionner successivement Standalone et Android ; corriger les erreurs, puis capturer les résultats.
- Après chaque sous-étape : MainMenu, sélectionner chaque difficulté, vérifier en PlayScene les lancements de fruits/bombes, coupe EzySlice, score simple, combos, vies, fruit manqué selon difficulté et retour MainMenu à zéro vie. Une inspection du code ne valide pas ce parcours.

## Casque PCVR — NON TESTÉ

Amélioration D : confirmer une impulsion légère sur la bonne main à chaque coupe réussie (réglage initial 0,15 / 40 ms dans SliceHapticFeedback), aucune erreur si contrôleur sans support haptique. C : vérifier l'animation du score sans gêne de lecture.

F/G : tester le glissement du volume par rayon et sa conservation au redémarrage. Pour les mesures, sélectionner `Player/Main Camera/DebugFpsCanvas` (sous Camera Offset selon la hiérarchie), puis cocher Show Overlay dans FpsOverlay avant un build de test ; décocher pour la livraison. Le compteur est une moyenne des frames applicatives, pas une mesure GPU ni une validation du tracking.

E : tester physiquement les boutons Rejouer et Menu, vérifier que les canons et les sabres cessent d'affecter le score sur l'écran final. Le menu hérité sélectionne toujours les niveaux par découpe, les nouvelles UI utilisent les rayons/gâchettes.

1. Renseigner modèle du casque, runtime OpenXR actif, CPU/GPU/RAM, connexion Link/Air Link, résolution et fréquence.
2. Lancer le build Windows complet ; vérifier tracking tête/mains, sabres et actions des contrôleurs, accès au menu et lisibilité de l'UI.
3. Rejouer le parcours gameplay ci-dessus ; vérifier jus/explosion PCVR, absence d'erreur dans Player.log et démarrage répété.
4. Mesurer réellement les FPS avec un outil de profiling adapté et relever la méthode, la scène, la durée et les réglages. Ne pas confondre FPS de l'éditeur et FPS du casque.

### Protocole FPS à exécuter sur les players

1. Conserver les livrables Release existants. Dans Player.prefab, sélectionner Main Camera/DebugFpsCanvas et cocher Show Overlay ; produire un build de test séparé dans `Builds/Windows_PCVR_TestFPS/` ou `Builds/Android_Quest_TestFPS/`.
2. Lancer ce player avec le casque réellement suivi. Après 30 s d'échauffement, jouer 60 s en Facile puis en Extrême, avec coupes et explosions ; relever la valeur affichée toutes les 10 s, ainsi que modèle du casque, fréquence, résolution et mode de connexion.
3. Le compteur indique les FPS applicatifs du player, pas les performances du compositor ni les frames reprojetées. Pour CPU/GPU, faire un build de test Development avec Autoconnect Profiler, ouvrir Window > Analysis > Profiler, sélectionner le player connecté (USB/ADB pour Quest), enregistrer CPU Usage et GPU Usage si disponibles, sans Deep Profiling. Indiquer qu'un build Development et le profiling ont un coût.
4. Noter minimum et moyenne des échantillons, méthode et durée dans 03_tests_builds.md ; ne rien extrapoler d'une valeur de l'éditeur. Décocher Show Overlay et Development Build après les tests ; les livrables fournis gardent l'overlay désactivé.

## Quest autonome — NON TESTÉ

Activer le mode développeur du casque, brancher USB, accepter l'autorisation de débogage, puis exécuter depuis la racine du dépôt (avec adb du SDK Unity dans PATH) :

```powershell
adb devices
adb install -r "Builds/Android_Quest/FruitNinjaVR_Quest.apk"
adb logcat -s Unity
```

L'APK doit d'abord avoir été produit ; ces commandes n'ont pas été exécutées. Lancer l'application depuis les sources inconnues. Relever modèle Quest, version système, fréquence et réglages graphiques. Tester tracking, sabres, gameplay, effets Quest, UI et relance. Mesurer les FPS sur casque, relever chauffe et stabilité pendant une session représentative.

## Analyse, captures et rapport — NON RÉALISÉS

Captures déjà disponibles : `docs/captures/menu_editeur.png` et `gameover_editeur.png`, issues d'une caméra Unity dans un scénario automatisé, sans tracking physique. Elles ne remplacent pas les captures de rendu au casque. Les captures hors Play de diagnostic montrent l'état avant correction des masquages ; ne pas les utiliser comme preuve de l'interface finale.

- Android Studio : Build > Analyze APK, choisir l'APK final ; relever taille totale/compressée et contributions assets/bin/Data, lib/arm64-v8a/libil2cpp.so, libunity.so, classes.dex, resources.arsc. Capturer les vues et consigner la version exacte du build analysé.
- Capturer les deux scènes, les quatre prefabs d'effets avec Inspector, les configurations OpenXR/Android, les résultats de validation et les builds en fonctionnement sur chaque casque.

Pour les captures PCVR : casque connecté, afficher le miroir du player sur le PC, utiliser Win+Maj+S et enregistrer `docs/captures/pcvr_miroir_casque.png`. Pour Quest, essayer les commandes ci-dessous après avoir affiché le jeu dans le casque ; vérifier que le fichier représente bien le jeu (les surfaces VR peuvent ne pas apparaître dans une capture Android standard). Si la capture est noire ou incomplète, utiliser la fonction de capture du casque et exporter le fichier manuellement. Ce résultat reste NON RÉALISÉ ici.

```powershell
adb shell screencap -p /sdcard/FruitNinjaVR_Quest.png
adb pull /sdcard/FruitNinjaVR_Quest.png docs/captures/quest_casque.png
```
- Confirmer les membres Fabio Malta, Nicolas Porot, Matéo Biaut, Benjamin Joannard et l'URL du dépôt rendu avant finalisation du README/rapport.
- Toute optimisation (ASTC, imports, retrait d'assets/Samples) attend une validation explicite ; aucune suppression autorisée à ce stade.
