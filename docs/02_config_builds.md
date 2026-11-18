# Configuration des builds

Unity **6000.0.58f1**, URP **17.0.4**, XRI **3.2.1**, OpenXR **1.15.1**, XR Management **4.5.2**.

| Paramètre | Windows PCVR | Android Quest |
|---|---|---|
| Profil | Assets/Settings/Build Profiles/Windows_PCVR.asset | Assets/Settings/Build Profiles/Android_Quest.asset |
| Scènes | MainMenu (0), PlayScene (1) | MainMenu (0), PlayScene (1) |
| Architecture | Windows x64 | ARM64 uniquement |
| Backend | Mono | IL2CPP |
| XR | OpenXR | OpenXR et Meta Quest Support |
| Contrôleurs | Oculus Touch / Quest Touch | Oculus Touch / Quest Touch Plus / Pro |
| Couleur | Linear | Linear |
| Qualité URP | PC / PC_RPAsset | Mobile / Mobile_RPAsset |
| Render scale | 1 | 0,8 |
| Ombre principale | 2048 | 1024 |
| Shadow distance | 50 | 50 |
| HDR / MSAA | Actif / désactivé | Actif / désactivé |
| Depth / Opaque texture | Actives | Désactivées |
| API graphiques Android | Sans objet | Vulkan puis OpenGLES3 |
| API minimale | Sans objet | 24 |
| API cible / compilation | Sans objet | 36, résolue par la cible automatique |
| Effets | Variantes _PCVR | Variantes _Quest |

Company : **ST2VAAS**. Product : **Fruit Ninja VR**. Identifiant Android : **fr.st2vaas.fruitninjavr.ben**. Version : **0.1.0**, code **1**.

Modules Android Build Support, SDK/NDK et OpenJDK installés avec l'éditeur : NDK **27.2.12479018**, OpenJDK **17.0.9**, SDK Android 34/35/36.

## Validation et compilation

- Windows : validation OpenXR sans erreur ni recommandation ; build final réussi, **0 erreur / 0 warning**.
- Android : validation sans erreur bloquante ; build final réussi, **0 erreur / 2 recommandations OpenXR**, concernant le SSAO des pipelines configurés et Prioritize Input Polling. Mobile_Renderer ne possède aucune Renderer Feature SSAO.
- Deux scènes et 17 prefabs contrôlés sans Missing.
- Réglage Input Handling passé de Both à **Input System Package (New)** après le blocage du premier essai Android.
- Versions URP/XRI/OpenXR/XR Management conservées. Les dépendances complètes figurent dans Packages/manifest.json et packages-lock.json.

Poste de compilation et de test PCVR : **Intel i7-9700KF, RTX 2060, 15,9 Gio RAM**. Les symboles Windows sont conservés séparément dans Builds/Windows_Symbols.

## Fichiers produits

| Sortie | Octets |
|---|---:|
| Dossier Windows complet | 139 566 307 |
| Builds/FruitNinjaVR_Windows.zip | 57 963 597 |
| Builds/Android_Quest/FruitNinjaVR_Quest.apk | 57 851 307 |

## Essais du 18 novembre 2026

J'ai testé les deux versions avec un **Meta Quest 3**, en PCVR via **Virtual Desktop** puis en autonome. Gameplay, effets, volume, écran final et haptique fonctionnent sur les deux plateformes. Runtime OpenXR actif, fréquence du casque et paramètres de streaming : non relevés. FPS : non mesurés.

Voir [03_tests_builds.md](03_tests_builds.md).
