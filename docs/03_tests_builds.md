# Tests des builds

Statuts : OK / KO / **NON TESTÉ**. Premier test logique Play Mode le 17 novembre 2026 sous 6000.0.58f1 via `tools/Smoke-Gameplay.cs` ; les appels de méthodes ne remplacent pas les gestes physiques et le test casque.

| Production du livrable | Statut | Résultat |
|---|---|---|
| Windows PCVR final | OK | Build réussi, 0 erreur/0 warning ; dossier 133,10 Mo, ZIP 55,28 Mo ; copie des 184 fichiers conforme par SHA256 |
| APK Quest final | OK | Build réussi, 0 erreur/2 recommandations OpenXR ; APK 55,17 Mo, min API 24, cible 36, ARM64 ; copie conforme par SHA256 |

Les lignes ci-dessus valident la production/copie, pas l'exécution physique. Dernier smoke test complet après reconstruction Windows réussi, éditeur laissé hors Play sur MainMenu.

## Éditeur — référence de non-régression

Complément après A–G : test à vitesse injectée de SliceObject/EzySlice réussi ; affichage final et boutons Rejouer/Menu invoqués via leurs UnityEvents, réinitialisation score/vies et conservation difficulté réussies. Slider volume à 37 %, retour au menu avec volume conservé puis restauration de la valeur précédente : OK. Activation et mise à jour du FPS : OK. Ces tests logiques ne valident ni pointage par contrôleur ni perception visuelle/haptique au casque. Audit final avant builds : deux scènes et 17 prefabs sans Missing ; compilation réussie.

| Test | Statut | Résultat / preuve à compléter |
|---|---|---|
| MainMenu : cinq difficultés et lancement | OK | Les cinq SelectDif.IsSliced invoqués : valeur 0–4 correctement reçue, spawn observé dans chaque mode ; sélection physique non testée |
| PlayScene : fruits et bombes lancés | OK | Objets FruitBehaviour et BombBehaviour observés après lancement naturel |
| Coupe EzySlice, jus et explosion | NON TESTÉ | Coupe EzySlice d'un fruit avec vitesse synthétique réussie après D ; rendu jus/explosion et geste physique restent à confirmer |
| Score simple et combos | OK | Appel FruitBehaviour.IsSliced : +1 ; deux GainScore : +4 après délai de combo |
| Vies, fruits manqués selon difficulté | OK | Parcours des cinq modes : fruit manqué coûte 0 vie en 0–1, 1 vie en 2–4 ; bombe retire une vie dans le smoke test |
| Retour au menu à zéro vie | OK | LoseLife jusqu'à zéro puis transition MainMenu observée |
| Absence de Missing / erreurs console | OK | Audit éditeur des 2 scènes et 12 prefabs : aucun Missing détecté ; aucune erreur, 2 warnings historiques d'import du modèle sword.001 |

## Windows PCVR

Casque : à compléter. Runtime OpenXR : à compléter. PC (CPU/GPU/RAM) : à compléter. Connexion : à compléter. Qualité, résolution, fréquence : à compléter. Date / version du build : à compléter.

| Domaine | Statut | Résultat / preuve à compléter |
|---|---|---|
| Tracking et sabres | NON TESTÉ | |
| Gameplay (fruits, coupe, bombes, score, combos, vies) | NON TESTÉ | |
| Effets visuels PCVR | NON TESTÉ | |
| UI et fonctions héritées effectivement présentes | NON TESTÉ | |
| Exécution (démarrage, retour menu, relance, stabilité, FPS) | NON TESTÉ | |

## Android Quest autonome

Casque / OS : à compléter. Fréquence, résolution, qualité : à compléter. Version de l'APK / date : à compléter. PC utilisé pour ADB (sans rendu PCVR) : à compléter.

| Domaine | Statut | Résultat / preuve à compléter |
|---|---|---|
| Tracking et sabres | NON TESTÉ | |
| Gameplay (fruits, coupe, bombes, score, combos, vies) | NON TESTÉ | |
| Effets visuels Quest | NON TESTÉ | |
| UI et fonctions héritées effectivement présentes | NON TESTÉ | |
| Exécution (installation, lancement autonome, relance, stabilité, FPS) | NON TESTÉ | |

Volume, classement et JSON ne sont pas des fonctions héritées validées : leur absence est relevée dans l'audit. Ne pas les marquer OK sans implémentation et test effectifs.
