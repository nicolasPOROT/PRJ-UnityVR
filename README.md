# Fruit Ninja VR — Part 1

Mini-projet individuel ST2VAAS 2026–2027 : **Benjamin JOANNARD**. Reprise du projet collectif réalisé en S7.

Unity **6000.0.58f1**, URP 17.0.4, XR Interaction Toolkit 3.2.1, OpenXR 1.15.1, XR Management 4.5.2 et EzySlice. Ouvrir `Assets/Scenes/MainMenu.unity`. Scènes de build : MainMenu (0), PlayScene (1) ; TestScene exclue. Pipeline 1.0.0 est utilisé pour le pilotage de l'éditeur.

Dépôt : [nicolasPOROT/PRJ-UnityVR](https://github.com/nicolasPOROT/PRJ-UnityVR). Branche de travail : `Mini-projet-Ben`. Sauvegarde initiale : tag `s7-initial`. Vérifier la publication de la branche et l'accès enseignant avant remise.

Les sabres suivent les contrôleurs ; la coupe se fait par mouvement du sabre, sans gâchette requise dans la logique héritée. Le menu choisit la difficulté en tranchant un fruit. Les nouvelles UI (volume, Rejouer/Menu) utilisent les rayons des contrôleurs et les gâchettes. Score, combos et vies sont hérités de S7. Part 1 ajoute lisibilité, animation du score, haptique, écran final, volume mémorisé et FPS de test activable. Classement et sauvegarde JSON de scores ne sont pas implémentés.

Pour lancer en PCVR : connecter le casque au PC et sélectionner un runtime OpenXR compatible. Le test fourni utilise **Quest 3 via Virtual Desktop**. Lancer l'exécutable dans le dossier Windows complet. Pour travailler dans l'éditeur, ouvrir MainMenu avec la version Unity indiquée, attendre la compilation, vérifier Project Validation puis Play. Voir [configuration des builds](docs/02_config_builds.md).

Pour Quest autonome : les modules Android/SDK/NDK/OpenJDK de 6000.0.58f1 sont installés. Profil `Android_Quest` : OpenXR + Meta Quest/Touch, IL2CPP, ARM64, Linear, API minimale 24, effets Quest. Après production de l'APK, activer le mode développeur, autoriser le débogage USB, puis exécuter :

```powershell
adb devices
adb install -r "Builds/Android_Quest/FruitNinjaVR_Quest.apk"
adb logcat -s Unity
```

Lancer ensuite l'application sur le casque, sans rendu PCVR. L'APK est produit (55,17 Mo, min API 24 / cible 36, ARM64). Le testeur confirme le lancement autonome sur **Quest 3**, ainsi que volume, écran de fin et haptique sur les deux builds. Les vidéos montrent gameplay, effets et retours menu. FPS non mesurés (facultatifs) ; captures APK Analyzer restant à ajouter. Voir [compléments](docs/A_FAIRE_MANUELLEMENT.md) et [résultats des tests](docs/03_tests_builds.md).

Livrables générés, hors Git : `Livrable/Windows_PCVR/` (exe + données et dépendances), `Livrable/Quest/FruitNinjaVR_Quest.apk`, archive commune `Livrable/FruitNinjaVR_Part1.zip`. ZIP Windows seul : `Builds/FruitNinjaVR_Windows.zip`. Les sources `Assets` et leurs `.meta`, `Packages`, `ProjectSettings` sont versionnés. Déplacer les assets uniquement dans Unity pour conserver leurs GUID.

État initial et écarts : [audit](docs/01_etat_initial.md). Rapport actuel : [Markdown](docs/rapport.md), [PDF Benjamin JOANNARD](docs/rapport_part1_benjamin_joannard.pdf), avec logo Efrei et captures des deux builds. Comparaison des tailles : `./tools/Compare-BuildSizes.ps1`.
