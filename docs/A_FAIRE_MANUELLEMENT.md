# Vérifications manuelles et limites des tests

État initial : aucun test Play Mode, casque, build ou mesure FPS réalisé pendant l'audit statique. Mettre à jour cette liste avec les résultats réellement obtenus.

## Contrôles dans l'éditeur avant les builds

- Ouvrir ce projet avec Unity **6000.0.58f1**. Ne pas utiliser 6000.5.0f1. Après import, attendre la fin de compilation et relever erreurs et warnings dans Window > General > Console.
- Ouvrir MainMenu puis PlayScene hors Play : vérifier les composants et références des objets et des prefabs (Player, GameplayZone, canons, fruits, bombes, effets). Aucun Missing Script/Reference ne doit subsister. Sauvegarder chaque scène après modification.
- Dans Project Settings > XR Plug-in Management > OpenXR > Project Validation, sélectionner successivement Standalone et Android ; corriger les erreurs, puis capturer les résultats.
- Après chaque sous-étape : MainMenu, sélectionner chaque difficulté, vérifier en PlayScene les lancements de fruits/bombes, coupe EzySlice, score simple, combos, vies, fruit manqué selon difficulté et retour MainMenu à zéro vie. Une inspection du code ne valide pas ce parcours.

## Casque PCVR — NON TESTÉ

Amélioration D : confirmer une impulsion légère sur la bonne main à chaque coupe réussie (réglage initial 0,15 / 40 ms dans SliceHapticFeedback), aucune erreur si contrôleur sans support haptique. C : vérifier l'animation du score sans gêne de lecture.

1. Renseigner modèle du casque, runtime OpenXR actif, CPU/GPU/RAM, connexion Link/Air Link, résolution et fréquence.
2. Lancer le build Windows complet ; vérifier tracking tête/mains, sabres et actions des contrôleurs, accès au menu et lisibilité de l'UI.
3. Rejouer le parcours gameplay ci-dessus ; vérifier jus/explosion PCVR, absence d'erreur dans Player.log et démarrage répété.
4. Mesurer réellement les FPS avec un outil de profiling adapté et relever la méthode, la scène, la durée et les réglages. Ne pas confondre FPS de l'éditeur et FPS du casque.

## Quest autonome — NON TESTÉ

Activer le mode développeur du casque, brancher USB, accepter l'autorisation de débogage, puis exécuter depuis la racine du dépôt (avec adb du SDK Unity dans PATH) :

```powershell
adb devices
adb install -r "Builds/Android_Quest/FruitNinjaVR_Quest.apk"
adb logcat -s Unity
```

L'APK doit d'abord avoir été produit ; ces commandes n'ont pas été exécutées. Lancer l'application depuis les sources inconnues. Relever modèle Quest, version système, fréquence et réglages graphiques. Tester tracking, sabres, gameplay, effets Quest, UI et relance. Mesurer les FPS sur casque, relever chauffe et stabilité pendant une session représentative.

## Analyse, captures et rapport — NON RÉALISÉS

- Android Studio : Build > Analyze APK, choisir l'APK final ; relever taille totale/compressée et contributions assets/bin/Data, lib/arm64-v8a/libil2cpp.so, libunity.so, classes.dex, resources.arsc. Capturer les vues et consigner la version exacte du build analysé.
- Capturer les deux scènes, les quatre prefabs d'effets avec Inspector, les configurations OpenXR/Android, les résultats de validation et les builds en fonctionnement sur chaque casque.
- Confirmer les membres Fabio Malta, Nicolas Porot, Matéo Biaut, Benjamin Joannard et l'URL du dépôt rendu avant finalisation du README/rapport.
- Toute optimisation (ASTC, imports, retrait d'assets/Samples) attend une validation explicite ; aucune suppression autorisée à ce stade.
