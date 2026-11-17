# Configuration des builds

État : paramètres initiaux lus dans Git ; aucun build produit ni validation OpenXR réalisée à ce stade.

| Paramètre | Windows PCVR observé | Android Quest observé | Cible de livraison |
|---|---|---|---|
| Unity dans ProjectVersion initial | 6000.0.56f1 | 6000.0.56f1 | 6000.0.58f1, migration à valider |
| Scènes | MainMenu puis PlayScene | Identiques | TestScene exclue |
| Pipeline | URP 17.0.4 | URP 17.0.4 | Conserver |
| Provider XR | OpenXR référencé | Aucun provider Android sérialisé | OpenXR pour les deux plateformes |
| Profils Touch | Oculus Touch, Quest Touch Plus/Pro actifs | Désactivés | Activer profils nécessaires et support Quest Android |
| Project Validation | NON TESTÉ | NON TESTÉ | Aucune erreur avant build |
| Backend | À relever dans l'éditeur | IL2CPP (valeur 1) | IL2CPP Android |
| Architecture | À relever | ARM64 uniquement (valeur 2) | Windows x64 / Android ARM64 |
| Minimum API | Sans objet | 23 | Au moins 23, valeur finale selon validation OpenXR |
| Target API | Sans objet | Auto (0) | Relever l'API effectivement sélectionnée par le build |
| Color Space | Linear (1) | Linear (1) | Linear |
| API graphique | À relever | Vulkan puis OpenGLES3, sélection automatique désactivée | Valider dans l'éditeur et sur casque |
| Company / Product | DefaultCompany / My project | Identiques | Identité propre à renseigner |
| Application ID Android | Sans objet | com.UnityTechnologies.com.unity.template.urpblank | Identifiant propre à renseigner |
| Effets | Deux effets communs hérités | Deux effets communs hérités | Quatre prefabs et sélection effective par plateforme |
| Modules installés 6000.0.58f1 | WindowsStandaloneSupport présent | Android, SDK/NDK, OpenJDK listés ; dossiers présents | Installation vérifiée, fonctionnement au build non vérifié |

Ajout autorisé pour le pilotage de l'éditeur : `com.unity.pipeline` 1.0.0 dans Packages/manifest.json. Aucun autre ajout/mise à jour de package prévu.

Lors du premier démarrage en 6000.0.58f1, Unity a automatiquement modifié AI Navigation 2.0.8 → 2.0.9, Version Control 2.8.2 → 2.9.3 et Rider 3.0.36 → 3.0.37 dans le manifest. Le lock résout aussi XR Core Utils 2.5.2 → 2.5.3 et Mono Cecil 1.11.4 → 1.11.6, et ajoute Newtonsoft JSON 3.2.1 (dépendance de Pipeline). URP/XRI/OpenXR/XR Management restent aux versions demandées. Ces modifications sont à valider après l'import ; elles ne constituent pas une mise à jour volontaire globale des packages.

L'éditeur 6000.0.58f1 (44b8bf3a3225) est lancé et a réécrit ProjectVersion.txt. L'import initial et le contrôle console/scènes restent en cours avant de déclarer la migration validée.

## Configuration finale appliquée avant builds

Migration compilée et vérifiée dans MainMenu/PlayScene. Profils réels : `Assets/Settings/Build Profiles/Windows_PCVR.asset` et `Android_Quest.asset`. MainMenu puis PlayScene, TestScene exclue. Company `ST2VAAS`, Product `Fruit Ninja VR`, application Android `fr.st2vaas.fruitninjavr.ben`.

Android : IL2CPP, ARM64 uniquement, Linear, minimum API **24** (minimum OpenXR 1.15.1 ; correction du 23 initial), target API auto. SDK installés : Android 34/35/36 ; API choisie effectivement à relever dans le build. Vulkan puis OpenGLES3, API automatique désactivée. Provider OpenXR et initialisation au démarrage configurés ; profils Oculus Touch / Meta Quest Touch Plus / Pro et Meta Quest Support activés. Windows : OpenXR et profils hérités conservés, Windows x64.

Quatre Prefab Variants et sélection PCVR/Android en place, voir `05_effets_plateformes.md`. Show Overlay désactivé par défaut pour la livraison.

Validation avant builds : Standalone **0 erreur, 0 recommandation** ; Android **0 erreur, 2 recommandations** (SSAO dans un renderer configuré et option facultative Prioritize Input Polling). Aucune optimisation SSAO appliquée sans validation. Contrôle éditeur : deux scènes, 17 prefabs, aucun Missing détecté. Console courante avant lancement Windows : 0 erreur/0 warning, historique de deux warnings d'import `sword.001`.

Outils Android de l'installation Unity : NDK **27.2.12479018**, OpenJDK **17.0.9** (fichiers source.properties/release lus). Aucun outil de contournement installé.

Build Windows PCVR produit avec succès en **6 min 53 s**, Build Report **0 erreur / 0 warning**, backend Mono, cible Windows x64, profil Windows_PCVR. Rapport détaillé : `build_report_windows.json`. Machine de compilation : Intel i7-9700KF, 15,9 Gio RAM, RTX 2060 (adaptateurs d'affichage virtuels également présents). Aucun test d'exécution au casque n'en est déduit.

Qualités héritées conservées : Windows utilise PC/PC_RPAsset (render scale 1, MSAA 1 = désactivé, HDR actif, depth/opaque textures actives, ombre principale 2048). Android a Mobile/Mobile_RPAsset comme qualité par défaut (render scale 0,8, MSAA 1 = désactivé, HDR actif, depth/opaque textures désactivées, ombre principale 1024). Shadow distance URP 50 pour les deux. `Mobile_Renderer` n'a aucune Renderer Feature ; la recommandation SSAO provient des pipelines globalement configurés, dont PC, sans ajout de SSAO au renderer mobile.

Le dossier généré BurstDebugInformation_DoNotShip Windows est conservé séparément dans `Builds/Windows_Symbols` et exclu du ZIP/livrable joueur. Aucun symbole n'est supprimé.

Le processeur de build Pipeline confirme dans la console que ses assemblies et réglages runtime sont exclus du player Windows (build Release, Pipeline Runtime non inclus). Aucun serveur Pipeline n'est ajouté au jeu livré.

Premier essai APK interrompu : le dialogue natif Unity « Unsupported Input Handling » détecte `Both` (valeur 2), non pris en charge sur Android. Choix No, build annulé ; passage par l'éditeur à **Input System Package (New)** uniquement (valeur 1). Le rig et les UI utilisés s'appuient déjà sur Input System. Cet échec de configuration est distingué des résultats du build final ; les messages sont consignés avant nettoyage de la console.

## Production et livraison à réaliser

1. Lire console, vérifier Missing dans MainMenu/PlayScene et prefabs, compiler et effectuer le parcours de non-régression avant chaque build.
2. Windows : sélectionner Standalone Windows x64, confirmer OpenXR et effets PCVR ; produire `Builds/Windows_PCVR/`, puis `Builds/FruitNinjaVR_Windows.zip` contenant le dossier complet.
3. Android : sélectionner le profil Android, vérifier tous les paramètres ci-dessus et Project Validation ; produire `Builds/Android_Quest/FruitNinjaVR_Quest.apk` avec effets Quest.
4. Copier le dossier Windows complet dans `Livrable/Windows_PCVR/` et l'APK dans `Livrable/Quest/`, puis archiver Livrable. Conserver les DLL, données et dépendances réellement générées (leur liste dépend du backend choisi).
5. Builds et Livrable doivent être ignorés par Git. Aucun binaire de livraison ne doit être committé.

Consigner ici les valeurs finales, la date, le commit source, le résultat du Build Report et les fichiers produits après réalisation. Une configuration lue dans les fichiers n'est pas un build validé.
