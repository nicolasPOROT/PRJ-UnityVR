# Fruit Ninja VR — Part 1

Mini-projet ST2VAAS 2026–2027. Membres à confirmer : Fabio Malta, Nicolas Porot, Matéo Biaut, Benjamin Joannard.

Unity **6000.0.58f1** (migration en cours de validation), URP 17.0.4, XR Interaction Toolkit 3.2.1, OpenXR 1.15.1, XR Management 4.5.2 et EzySlice. Ouvrir `Assets/Scenes/MainMenu.unity`. Scènes de build : MainMenu (0), PlayScene (1) ; TestScene exclue.

Dépôt actuellement configuré : [nicolasPOROT/PRJ-UnityVR](https://github.com/nicolasPOROT/PRJ-UnityVR). Branche de travail : `Mini-projet-Ben`. Sauvegarde initiale : tag `s7-initial`. Confirmer l'URL du dépôt rendu avant livraison.

Les sabres suivent les contrôleurs ; la coupe se fait par mouvement du sabre, sans gâchette requise dans la logique héritée. Le menu choisit la difficulté en tranchant un fruit. Les interactions UI par rayon/gâchette sont à vérifier dans l'éditeur et sur casque ; elles ne sont pas validées par l'audit. Score, combos et vies sont hérités de S7. Classement et sauvegarde JSON ne sont pas implémentés dans les scripts examinés.

Pour lancer en PCVR : ouvrir avec la version Unity indiquée, attendre la compilation, sélectionner un runtime OpenXR compatible et connecter le casque (Quest Link/Air Link si Quest). Vérifier Project Validation, ouvrir MainMenu puis Play, ou lancer l'exécutable du dossier Windows complet une fois le build produit. Voir [configuration des builds](docs/02_config_builds.md).

Pour Quest autonome : les modules Android/SDK/NDK/OpenJDK de 6000.0.58f1 sont installés. La configuration OpenXR Android et les variantes d'effets restent à compléter. Après production de l'APK, activer le mode développeur, autoriser le débogage USB, puis exécuter :

```powershell
adb devices
adb install -r "Builds/Android_Quest/FruitNinjaVR_Quest.apk"
adb logcat -s Unity
```

Lancer ensuite l'application sur le casque, sans rendu PCVR. Aucun APK ni test autonome n'est encore déclaré disponible. Les procédures et limites sont dans [A_FAIRE_MANUELLEMENT.md](docs/A_FAIRE_MANUELLEMENT.md), les résultats dans [03_tests_builds.md](docs/03_tests_builds.md).

Les builds vont dans `Builds/`, les livrables dans `Livrable/`, tous deux hors Git. Les sources `Assets` et leurs `.meta`, `Packages`, `ProjectSettings` doivent être versionnés. Déplacer les assets uniquement dans Unity pour conserver leurs GUID.

État initial et écarts : [audit](docs/01_etat_initial.md). Rapport provisoire : [Markdown](docs/rapport.md), [PDF](docs/rapport.pdf). Comparaison des tailles : `./tools/Compare-BuildSizes.ps1`.