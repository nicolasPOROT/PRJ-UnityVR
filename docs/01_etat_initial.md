# État initial — Fruit Ninja VR Part 1

Audit du 17 novembre 2026, commit initial `9313f11f43daf0a8f2167e8772ff591567b36002`.
Branche de travail : `Mini-projet-Ben`. Tag local `s7-initial` créé sur ce commit après `git fetch origin`. Aucun push.

Les observations ci-dessous proviennent des fichiers suivis. Elles ne constituent pas un test dans Unity ou sur casque. Le MCP initial pointe vers `D:\GitHub\OrbitBreaker\OrbitBreaker` et ne trouve aucune instance Pipeline. L'ajout de `com.unity.pipeline` a été autorisé par Benjamin pour rétablir le pilotage de ce projet.

| Fonctionnalité | État observé | Écart / suite |
|---|---|---|
| Task 1 : sauvegarde initiale | Tag `s7-initial` sur `9313f11`, branche dédiée créée | Sauvegarde Git locale réalisée |
| Unity | ProjectVersion : 6000.0.56f1 | Migration demandée vers 6000.0.58f1 à valider dans l'éditeur |
| Éditeurs installés | 6000.0.58f1 et 6000.5.0f1 listés par CLI | Utiliser uniquement 6000.0.58f1 |
| Modules Android de 6000.0.58f1 | CLI : Android, SDK & NDK Tools, OpenJDK ; dossiers SDK/NDK/OpenJDK présents | Installation constatée, build non vérifié |
| Rendu | URP 17.0.4 ; pipeline référencé dans GraphicsSettings | Vérification visuelle non réalisée |
| Packages XR | XRI 3.2.1, OpenXR 1.15.1, XR Management 4.5.2, Input System 1.14.2 | Conserver ces versions |
| XR Windows | Provider OpenXR référencé, initialisation au démarrage active | Project Validation et runtime non contrôlés |
| Profils Windows | Oculus Touch, Meta Quest Touch Plus/Pro activés ; autres profils également présents | Vérifier le casque et les profils requis dans l'éditeur |
| XR Android | XRGeneralSettingsPerBuildTarget ne contient que Standalone ; profils Touch et features Quest Android désactivés dans OpenXR Package Settings | Configuration Android incomplète |
| XR Origin / actions | Player.prefab : caméra, Camera Offset, LeftHand/RightHand, données d'Origin, asset d'actions référencé ; points de coupe et estimateurs assignés | Tracking et résolution des composants de packages à vérifier dans Unity |
| Task 2 : organisation | Dossiers Scripts, Scenes, Prefabs/Fruits, Bombs, Fx présents ; Imported_assets, ezy-slice-master, Samples, TextMesh Pro à la racine | Organisation partielle ; déplacements uniquement via AssetDatabase/MCP, aucune suppression autorisée |
| Scènes de build | MainMenu index 0, PlayScene index 1, toutes deux actives ; TestScene exclue | Conforme dans EditorBuildSettings |
| Références | Scan des deux scènes et 12 prefabs : aucun m_Script explicitement nul ; 18 GUID externes à Assets | Ces GUID peuvent appartenir aux packages : absence de Missing NON VALIDÉE avant contrôle Unity |
| Task 3 : effets | Seulement JuiceFx.prefab et ExplosionFx.prefab ; fruits et bombes les référencent directement, ainsi que les sélecteurs du menu | Quatre variantes PCVR/Quest absentes ; aucun sélecteur de plateforme dans Assets/Scripts |
| Paramètres des effets hérités | Juice : un système, max 1000, collision active ; Explosion : trois systèmes (Sparks, Smoke, ExplosionFx), max 1000 chacun, collisions désactivées | Mesurer les valeurs effectives et créer les variantes dans l'éditeur ; aucune performance déduite de ces plafonds |
| Difficultés | Cinq valeurs 0–4 dans MainMenu ; SelectDif + DifficultyTransfert ; cadence et perte de vie dépendent de la difficulté | Présent dans le code ; parcours Play non réalisé |
| Entraînement | Difficultés 0 et 1 : fruits manqués sans perte de vie ; bombe retire toujours une vie | Aucun mode distinct nommé entraînement identifié |
| Menu | Sélection par découpe, puis chargement différé de PlayScene | Présent dans scène/code ; interactions VR non testées |
| Score / combos / vies | GameManager + UI sérialisée ; retour immédiat à MainMenu quand les vies atteignent zéro | Logique héritée à conserver ; parcours non testé |
| Volume | Audio existant, aucun réglage de volume identifié dans Assets/Scripts | Fonction non implémentée dans les scripts examinés |
| Classement / JSON | Aucune implémentation identifiée dans Assets/Scripts ; README les classe comme futurs | Ne pas déclarer ces fonctions réalisées |
| Paramètres Android | IL2CPP (1), ARM64 (2), Linear (1), min API 23, cible auto (0), APIs sérialisées Vulkan puis OpenGLES3 | Validation OpenXR et choix final à faire ; noms de template à remplacer |
| Identité application | DefaultCompany / My project ; com.UnityTechnologies.com.unity.template.urpblank | Identité de livraison à configurer |
| Gitignore | Library, Temp, Logs, UserSettings et Builds exclus ; aucun fichier de ces quatre dossiers suivi | Ajouter Livrable avant constitution des livrables |
| Métadonnées | 1157 fichiers suivis dans Assets/Packages/ProjectSettings ; 14 fichiers Assets sans .meta suivi, détaillés ci-dessous | Vérifier les métadonnées EzySlice via l'éditeur, puis versionner celles des assets importables |
| LFS / attributs | .gitattributes : `* text=auto` ; git lfs ls-files ne liste rien | Aucune règle LFS active observée ; gros binaires déjà dans Git |

## Métadonnées non suivies

Sous `Assets/ezy-slice-master/` : `.gitignore`, `LICENSE`, `README.md`, `Graphics/icon.png`, `EzySlice/SlicedHull.cs`, `Slicer.cs`, `SlicerExtensions.cs` et `EzySlice/Framework/{IntersectionResult,Intersector,Line,Plane,TextureRegion,Triangle,Triangulator}.cs`.
Les fichiers cachés comme `.gitignore` ne sont pas nécessairement importés par Unity. Ne pas fabriquer leurs métadonnées arbitrairement.

Cause confirmée : `Assets/ezy-slice-master/.gitignore` contient initialement `*.meta`, ce qui masque les métadonnées EzySlice. Correction engagée : remplacer cette exclusion par une inclusion des `.meta`, puis ajouter les fichiers réellement produits par Unity.

## Gros fichiers source déjà suivis

| Fichier | Taille source (octets) |
|---|---:|
| AllSkyFree/Cold Sunset/Cold Sunset Equirect.png | 79 869 056 |
| Pure Poly/Free Low Poly Nature Pack/Terrain/Terrain.asset | 67 670 364 |
| AllSkyFree/Deep Dusk/Deep Dusk Equirect.png | 50 754 568 |
| AllSkyFree/Cold Night/Cold Night Equirect.png | 43 417 291 |

Chemins relatifs à `Assets/Imported_assets`. Ces tailles source ne représentent pas leur poids dans un APK : seuls le Build Report et l'analyse de l'APK permettront cette conclusion. Aucune suppression, conversion LFS ou modification d'import appliquée.

## Priorités de reprise

1. Rétablir la connexion éditeur après l'ajout Pipeline autorisé ; ouvrir en 6000.0.58f1, compiler et lire la console.
2. Contrôler Missing et Project Validation, tester MainMenu/PlayScene, puis committer la migration seulement si validée.
3. Compléter Tasks 2–3 dans l'éditeur : métadonnées, organisation, quatre effets réels et sélection par plateforme, références Inspector.
4. Valider les améliorations de confort avant leur implémentation ; ensuite configurer et produire les builds.

Console Unity : **NON LUE**, erreurs/warnings **INCONNUS** au moment de cet audit initial. Aucun test Play Mode ou casque déclaré réalisé.

Suite de la mise en place : Pipeline 1.0.0 installé avec accord, éditeur 6000.0.58f1 lancé, ProjectVersion mis à jour par Unity. Le serveur est détecté mais reste en état starting pendant l'import initial ; son tampon console vide avec groundTruth=null ne permet pas de conclure à zéro erreur. Les changements automatiques de packages sont détaillés dans `02_config_builds.md`.

Contrôle après import : dialogue natif « Deprecated packages » fermé par Dismiss sans action sur les packages. Audit Pipeline exécuté hors Play sous 6000.0.58f1 : MainMenu 39 objets, PlayScene 1277 objets et 12 prefabs inspectés, aucun Missing détecté. Validation OpenXR Standalone sans problème ; Android avertit que l'API minimale 23 doit être au moins 24. Console après import : compilation réussie, 0 erreur, deux warnings d'import de `sword.001` dans `Assets/Models/sao_sword.fbx` (polygone auto-intersectant écarté). Après Play, console courante 0 erreur/0 warning, historique du tampon conservant ces deux warnings. Premier smoke test logique décrit dans `03_tests_builds.md`, gestes de coupe et casque non testés.
