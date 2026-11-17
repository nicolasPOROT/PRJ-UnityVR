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

## Production et livraison à réaliser

1. Lire console, vérifier Missing dans MainMenu/PlayScene et prefabs, compiler et effectuer le parcours de non-régression avant chaque build.
2. Windows : sélectionner Standalone Windows x64, confirmer OpenXR et effets PCVR ; produire `Builds/Windows_PCVR/`, puis `Builds/FruitNinjaVR_Windows.zip` contenant le dossier complet.
3. Android : sélectionner le profil Android, vérifier tous les paramètres ci-dessus et Project Validation ; produire `Builds/Android_Quest/FruitNinjaVR_Quest.apk` avec effets Quest.
4. Copier le dossier Windows complet dans `Livrable/Windows_PCVR/` et l'APK dans `Livrable/Quest/`, puis archiver Livrable. Conserver les DLL, données et dépendances réellement générées (leur liste dépend du backend choisi).
5. Builds et Livrable doivent être ignorés par Git. Aucun binaire de livraison ne doit être committé.

Consigner ici les valeurs finales, la date, le commit source, le résultat du Build Report et les fichiers produits après réalisation. Une configuration lue dans les fichiers n'est pas un build validé.
