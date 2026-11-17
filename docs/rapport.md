# Fruit Ninja VR — Part 1 : reprise et déploiement multiplateforme

ST2VAAS 2026–2027. Auteurs à confirmer : Fabio Malta, Nicolas Porot, Matéo Biaut, Benjamin Joannard.

**Squelette de rapport : les builds, tests et mesures non réalisés ne sont pas des résultats.**

## État initial S7, corrections, organisation retenue

Référence : commit `9313f11`, tag `s7-initial`. Travail sur `Mini-projet-Ben`. Voir [audit](01_etat_initial.md).

Héritage S7 observé : canons fruits/bombes, découpe EzySlice, score/combos/vies, cinq difficultés, sélection par découpe au menu, deux prefabs d'effets communs. Classement, volume réglable et JSON absents des scripts examinés.

Corrections Tasks 2–3 et migration : à compléter avec les changements réellement validés. Organisation finale : `[À COMPLÉTER APRÈS MODIFICATIONS DANS L'ÉDITEUR]`.

[CAPTURE À INSÉRER] ProjectVersion, arborescence Assets et Hierarchy hors Play.

## Les deux effets et leurs variantes PCVR / Quest

Les quatre variantes sont à créer et à contrôler dans l'éditeur. Les valeurs héritées ne constituent pas des réglages finaux optimisés. Justifier chaque différence par le rendu recherché et une mesure sur la plateforme cible.

| Variante | Max particles | Lifetime | Start size | Emission | Collision | Matériau / shader | Modules actifs |
|---|---|---|---|---|---|---|---|
| Jus PCVR | À relever | À relever | À relever | À relever | À relever | À relever | À relever |
| Jus Quest | À relever | À relever | À relever | À relever | À relever | À relever | À relever |
| Explosion PCVR | À relever par système | À relever | À relever | À relever | À relever | À relever | À relever |
| Explosion Quest | À relever par système | À relever | À relever | À relever | À relever | À relever | À relever |

[CAPTURE À INSÉRER] Quatre prefabs, Inspector et rendu pour chaque plateforme.

Sélection de plateforme et références Inspector : `[À COMPLÉTER APRÈS IMPLÉMENTATION]`.

## Configuration des builds, matériel, résultats des tests

Voir [configuration](02_config_builds.md) et [matrice de tests](03_tests_builds.md). Unity cible : 6000.0.58f1, URP 17.0.4, XRI 3.2.1, OpenXR 1.15.1, XR Management 4.5.2. La migration est à valider avant de la déclarer terminée.

Matériel PCVR / Quest et réglages : `[À COMPLÉTER]`.

[TEST NON RÉALISÉ] Parcours de non-régression en Play Mode.

[TEST NON RÉALISÉ] Build Windows et test casque PCVR.

[TEST NON RÉALISÉ] Build APK et exécution Quest autonome.

[CAPTURE À INSÉRER] Build Profiles, Player Settings, Project Validation, rendu des deux plateformes.

## Analyse APK et comparaison tailles / qualité / fluidité

Suivre la [procédure d'analyse](04_analyse_apk.md), puis insérer le tableau produit par `tools/Compare-BuildSizes.ps1`.

| Plateforme | Taille livrable (Mo) | Qualité observée | FPS / méthode / durée |
|---|---|---|---|
| Windows PCVR | À mesurer | [TEST NON RÉALISÉ] | [TEST NON RÉALISÉ] |
| Quest autonome | À mesurer | [TEST NON RÉALISÉ] | [TEST NON RÉALISÉ] |

[CAPTURE À INSÉRER] APK Analyzer, contributions principales et Build Report. Expliquer le regroupement des ressources Unity et distinguer tailles compressées/décompressées.

## Difficultés, solutions, limitations

- MCP initial configuré pour un autre projet ; ajout autorisé de `com.unity.pipeline` 1.0.0 afin de connecter l'éditeur du projet.
- Premier import : Unity a annoncé des mises à jour automatiques de AI Navigation, Version Control et Rider ; consigner leur résultat final dans la configuration.
- Le contrôle statique de GUID ne prouve pas l'absence de Missing. Le contrôle éditeur et les tests sur casque sont indispensables.
- Aucune mesure réelle FPS, capture casque ou analyse Android Studio n'est présentée comme effectuée sans preuve.

Voir [actions manuelles](A_FAIRE_MANUELLEMENT.md). Documenter les difficultés supplémentaires et solutions effectivement appliquées.
