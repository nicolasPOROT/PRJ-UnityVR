# Fruit Ninja VR — Part 1

**Benjamin JOANNARD** · ST2VAAS · 2026–2027

Reprise individuelle du projet Fruit Ninja VR réalisé en groupe en S7 : organisation des ressources, effets adaptés à PCVR et Quest, améliorations de l'interface et déploiement sur les deux plateformes.

## Ouvrir le projet

- Unity **6000.0.58f1**.
- URP **17.0.4**, XR Interaction Toolkit **3.2.1**, OpenXR **1.15.1**, XR Management **4.5.2**, EzySlice.
- Scène de démarrage : **Assets/Scenes/MainMenu.unity**.
- Scènes de build : **MainMenu (0)**, puis **PlayScene (1)**. TestScene est réservée aux essais.
- Sources : Assets et ses fichiers .meta, Packages, ProjectSettings.

Branche : [**Mini-projet-Ben**](https://github.com/nicolasPOROT/PRJ-UnityVR/tree/Mini-projet-Ben). État S7 conservé au tag **s7-initial**.

## Contrôles

Les sabres suivent les contrôleurs : déplacer un sabre pour trancher les fruits et éviter les bombes. Dans le menu, trancher le fruit correspondant à la difficulté souhaitée. Le volume et les boutons Rejouer/Menu s'utilisent avec le rayon du contrôleur et la gâchette.

Le score, les combos, les vies et les cinq difficultés proviennent de S7. Cette reprise ajoute une interface plus lisible, une animation du score, l'haptique à la coupe, un écran de fin, le volume mémorisé et quatre variantes d'effets PCVR/Quest.

## Windows PCVR

Connecter le casque au PC avec un runtime OpenXR compatible, puis lancer **FruitNinjaVR_PCVR.exe** dans le dossier Windows complet. Essais réalisés avec un **Meta Quest 3 via Virtual Desktop**.

## Meta Quest autonome

Activer le mode développeur du casque et autoriser le débogage USB, puis installer l'APK :

```powershell
adb devices
adb install -r "Livrable/Quest/FruitNinjaVR_Quest.apk"
adb logcat -s Unity
```

Lancer Fruit Ninja VR depuis les applications de sources inconnues du casque. L'APK fonctionne en autonome sur **Meta Quest 3**.

## Résultats et documents

Gameplay, jus/explosion, volume, écran final et haptique fonctionnent sur les deux builds. **FPS non mesurés.** Classement et sauvegarde JSON des scores absents.

- [Rapport PDF](docs/rapport_part1_benjamin_joannard.pdf) et [version Markdown](docs/rapport.md).
- [État initial](docs/01_etat_initial.md), [configuration](docs/02_config_builds.md), [tests](docs/03_tests_builds.md).
- [Effets PCVR/Quest](docs/05_effets_plateformes.md), [analyse de l'APK](docs/07_contenu_apk.md), [historique des modifications](docs/CHANGELOG_BEN.md).

Les builds sont distribués séparément des sources. L'archive de remise **Remise_FruitNinjaVR_Part1_Benjamin_JOANNARD.zip** contient Windows complet, l'APK Quest, le rapport et la notice avec le lien GitHub.
