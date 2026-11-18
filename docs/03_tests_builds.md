# Tests des builds

Mise à jour du 18 novembre 2026 : essais de **Benjamin JOANNARD**, **Meta Quest 3**, PCVR via **Virtual Desktop** et APK exécuté en autonome. Deux vidéos reçues et examinées ; captures, horodatages et portée des observations dans [preuves/README.md](preuves/README.md). Les confirmations sensorielles du testeur sont distinguées des observations vidéo.

Statuts : OK / KO / **NON TESTÉ**. OK décrit uniquement la vérification précisée, sans certifier les mesures non réalisées.

| Production du livrable | Statut | Résultat |
|---|---|---|
| Windows PCVR final | OK | Build réussi, 0 erreur/0 warning ; dossier 133,10 Mo, ZIP 55,28 Mo ; copie des 184 fichiers conforme par SHA256 |
| APK Quest final | OK | Build réussi, 0 erreur/2 recommandations OpenXR ; APK 55,17 Mo, min API 24, cible 36, ARM64 ; copie conforme par SHA256 |

## Éditeur — contrôles complémentaires du 17 novembre

Ces vérifications logiques ne remplacent pas les interactions au casque.

| Vérification | Statut | Résultat |
|---|---|---|
| Cinq difficultés et lancement | OK | Niveaux 0–4 reçus et spawn observé dans chaque mode |
| Fruits, bombes, coupe EzySlice | OK | Spawn naturel ; coupe exercée avec vitesse injectée dans l'estimateur du sabre |
| Score simple et combos | OK | Coupe : +1 ; deux gains rapprochés : +4 après délai du combo |
| Vies selon difficulté | OK | Fruit manqué : 0 vie en niveaux 0–1, 1 vie en 2–4 ; bombe : −1 vie |
| Fin, Rejouer et Menu | OK | UnityEvents invoqués ; score/vies réinitialisés, difficulté conservée |
| Volume et compteur FPS | OK | Valeur de volume conservée au retour menu ; activation/mise à jour du compteur vérifiée dans l'éditeur |
| Références et compilation | OK | Audit final : deux scènes et 17 prefabs sans Missing ; compilation sans erreur |

## Windows PCVR

Casque : **Meta Quest 3**. Connexion : **Virtual Desktop**. Vidéo : 86,21 s. Runtime OpenXR actif (VDXR/SteamVR), fréquence, résolution et débit : **non relevés**. Le PC de compilation est un i7-9700KF / RTX 2060 / 15,9 Gio RAM ; son identité avec le poste utilisé pour les essais n'est pas confirmée. URP PC : render scale 1, ombres 2048, HDR actif, MSAA désactivé.

| Domaine | Statut | Résultat et limite |
|---|---|---|
| Tracking et sabres | OK | Vue et deux sabres en mouvement, coupes visibles ; précision et latence non mesurées |
| Gameplay | OK | Fruits tranchés, bombes, progression du score et pertes de vies observés ; combos contrôlés séparément dans l'éditeur |
| Effets visuels PCVR | OK | Jus et explosion visibles ; captures comparatives dans le rapport |
| UI et fonctions présentes | OK | Volume manipulé et confirmé fonctionnel ; écran final/retour menu visibles ; haptique confirmé par le testeur |
| Exécution | OK | Lancement confirmé par le testeur ; retour menu et nouvelle partie filmés ; aucun crash visible sur la séquence |
| Stabilité prolongée / latence / FPS | NON TESTÉ | Pas de session longue ni mesure chiffrée ; l'encodage vidéo ne mesure pas les FPS du jeu |

## Android Quest autonome

Casque : **Meta Quest 3**. APK exécuté sur le casque, sans rendu PCVR, confirmé par le testeur. Vidéo : 74,97 s. Version système, fréquence et résolution effective : **non relevées**. URP Mobile : render scale 0,8, ombres 1024, HDR actif, MSAA désactivé ; Vulkan puis OpenGLES3 configurés (API réellement utilisée non relevée).

| Domaine | Statut | Résultat et limite |
|---|---|---|
| Tracking et sabres | OK | Vue et deux sabres en mouvement, coupes visibles ; précision et latence non mesurées |
| Gameplay | OK | Fruits séparés, bombes, score et vies mis à jour ; sélection d'Extrême puis nouvelle partie visibles |
| Effets visuels Quest | OK | Jus et explosion visibles ; variantes allégées conservant les matériaux et l'identité des effets |
| UI et fonctions présentes | OK | Slider volume, fin de partie et retour menu visibles ; volume et haptique confirmés fonctionnels par le testeur |
| Exécution | OK | Installation/lancement autonome confirmés ; application en jeu et relance d'une partie filmées ; aucun crash visible sur la séquence |
| Stabilité prolongée / chauffe / FPS | NON TESTÉ | Pas de mesure thermique ni de fréquence du rendu |

Les vidéos commencent dans l'application : elles ne montrent pas intégralement l'installation ou le démarrage initial. Le bouton Rejouer est vérifié dans l'éditeur ; son activation physique isolée n'est pas clairement démontrée par les séquences. Des fragments/particules peuvent brièvement passer devant le panneau final. Aucun bonus distinct, classement ou JSON n'est identifié dans la base ; le volume est un ajout Part 1 mémorisé par PlayerPrefs.

**Le relevé FPS chiffré est facultatif dans le sujet.** Il reste non mesuré. Une procédure optionnelle figure dans [A_FAIRE_MANUELLEMENT.md](A_FAIRE_MANUELLEMENT.md). Les captures APK Analyzer restent à fournir.
