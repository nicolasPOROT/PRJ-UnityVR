# Améliorations proposées — validation avant implémentation

Estimations indicatives de réalisation et vérification dans l'éditeur ; aucun élément ci-dessous n'est implémenté. Les corrections obligatoires Tasks 2–3 restent prioritaires.

| Choix | Amélioration Part 1 | Effort estimé | Périmètre |
|---|---|---|---|
| A | Libellés de difficulté plus lisibles et présentation cohérente du menu | 30–45 min | Textes et Canvas déjà présents ; sélection par découpe conservée |
| B | Score et vies : taille, contraste, placement adaptés à la vue | 30–45 min | Réglages UI dans prefab/scène ; validation de lisibilité au casque requise |
| C | Animation légère du score à chaque gain affiché | 30–45 min | Logique visuelle uniquement ; aucun changement du calcul des combos |
| D | Retour haptique léger lors d'une coupe | 45–60 min | Références Inspector aux contrôleurs ; intensité réglable, test casque requis |
| E | Écran de fin avec score final, Rejouer et Menu | 60–90 min | Canvas et boutons posés dans l'éditeur ; nécessite de remplacer le retour immédiat existant, accord explicite requis |
| F | Réglage du volume depuis le menu | 60–90 min | Slider/AudioMixer et références sérialisées ; fonction absente de S7 |
| G | Compteur FPS de développement activable | 30–45 min | Texte et composant présents avant Play ; mesure indicative, pas une validation de fluidité sur casque |

Pas de feedback hover proposé par défaut : le menu hérité sélectionne les difficultés par découpe de fruits. Ajouter de vrais boutons impliquerait un choix d'interface supplémentaire.

Le sélecteur d'effets PCVR/Quest et ses quatre prefabs relèvent de la correction obligatoire de Task 3. Ils ne sont pas une amélioration facultative. Chaque amélioration validée aura un commit et une entrée distincts dans CHANGELOG_BEN.md.
