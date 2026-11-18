# Tests des builds

Essais du **18 novembre 2026**, réalisés avec un **Meta Quest 3**. PCVR via **Virtual Desktop** ; APK installé et exécuté en autonome.

## Conditions

| Paramètre | Windows PCVR | Quest autonome |
|---|---|---|
| Casque | Meta Quest 3 | Meta Quest 3 |
| Exécution | Player Windows via Virtual Desktop | APK Android |
| Qualité | PC : render scale 1, ombres 2048 | Mobile : render scale 0,8, ombres 1024 |
| HDR / MSAA | Actif / désactivé | Actif / désactivé |
| Enregistrement | 86,21 s | 74,97 s |
| Fréquence / résolution effective | Non relevées | Non relevées |
| Runtime actif / débit streaming | Non relevés | Sans objet |
| FPS | Non mesurés | Non mesurés |

Même poste pour la compilation et les tests PCVR : **Intel i7-9700KF, RTX 2060, 15,9 Gio RAM**.

## Parcours de test

| Domaine | Windows PCVR | Quest autonome | Résultat |
|---|---|---|---|
| Tracking et sabres | OK | OK | Suivi de la vue et des contrôleurs, deux sabres utilisables |
| Gameplay | OK | OK | Fruits lancés et coupés, bombes, score et pertes de vies |
| Effets visuels | OK | OK | Jus à la coupe et explosion de bombe |
| UI et fonctions héritées | OK | OK | Menu, difficulté, score et vies lisibles |
| Volume ajouté | OK | OK | Slider utilisable par rayon |
| Écran final et retour menu | OK | OK | Score final et retour à la sélection |
| Retour haptique ajouté | OK | OK | Impulsion ressentie à la coupe |
| Exécution des parties enregistrées | OK | OK | Lancement et nouvelle partie, aucun crash ou blocage constaté |
| Stabilité prolongée / chauffe | NON TESTÉ | NON TESTÉ | Session longue non réalisée |
| Latence chiffrée | NON TESTÉ | NON TESTÉ | Mesure non réalisée |

## Contrôles complémentaires dans l'éditeur

| Vérification | Résultat |
|---|---|
| Cinq difficultés | Valeurs 0–4 reçues et spawn présent dans chaque mode |
| Coupe EzySlice | Coupe réussie avec vitesse injectée dans l'estimateur |
| Score et combos | Coupe simple +1 ; deux gains rapprochés +4 après délai du combo |
| Fruit manqué | 0 vie perdue en niveaux 0–1, 1 vie perdue en 2–4 |
| Bombe | Une vie perdue |
| Rejouer | Score/vies réinitialisés, difficulté conservée |
| Retour menu et volume | Volume conservé après une partie |
| Compteur FPS | Activation et mise à jour du texte fonctionnelles |
| Références | Deux scènes et 17 prefabs sans Missing |

Les fragments et particules encore actifs peuvent brièvement passer devant le panneau de fin. L'activation physique isolée du bouton Rejouer n'est pas relevée séparément des essais de l'écran final. Classement, JSON des scores et bonus spécifiques sont absents.

Les [captures horodatées](preuves/README.md) illustrent les deux parties. Résultats de compilation : Windows **0 erreur / 0 warning**, Android **0 erreur / 2 recommandations OpenXR**.
