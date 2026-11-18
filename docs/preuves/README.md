# Preuves de tests — réception du 18 novembre 2026

Auteur de la Part 1 et testeur : **Benjamin JOANNARD**. Matériel déclaré : **Meta Quest 3** ; PCVR via **Virtual Desktop**. Volume, écran de fin et retour haptique sont confirmés fonctionnels sur les deux builds par le testeur.

## Sources conservées localement

| Nom fourni | Emplacement actuel | Durée | Taille |
|---|---|---:|---:|
| PCVR record.mp4 | videos/pcvr.mp4 | 86,208750 s | 30 721 186 octets |
| APK in quest record.mp4 | videos/quest_autonome.mp4 | 74,967767 s | 26 833 533 octets |

Les deux vidéos sont en 1280 × 720. Leur cadence moyenne d'encodage avoisine 30 images/s : elle ne mesure pas les FPS du jeu. Les vidéos ont été déplacées sans modification du contenu. Elles restent hors Git, avec les planches de travail ; les captures sélectionnées et les métadonnées sont versionnées. Les hashes des sources figurent dans `videos.json`.

Le repérage visuel a été réalisé sur des planches à une image par seconde sur toute la durée, puis à dix images par seconde autour des coupes/explosions. Les captures ci-dessous sont des images réelles extraites à pleine résolution, pas des images générées. Les comparaisons sont de simples juxtapositions : PCVR à gauche, Quest à droite. Les parties, angles de vue et instants ne sont pas synchronisés ; aucun gain de performance ni dénombrement exact des particules n'est déduit des captures.

## Captures retenues

| Fichier dans captures/ | Vidéo source | Repère demandé |
|---|---|---|
| pcvr_jus.jpg | PCVR | 00:32,50 |
| quest_jus.jpg | Quest autonome | 00:17,50 |
| pcvr_explosion.jpg | PCVR | 00:37,55 |
| quest_explosion.jpg | Quest autonome | 00:45,35 |
| pcvr_volume.jpg | PCVR | 00:43,60 |
| quest_volume.jpg | Quest autonome | 00:05,50 |
| pcvr_fin.jpg | PCVR | 00:40,00 |
| quest_fin.jpg | Quest autonome | 00:48,00 |
| pcvr_gameplay.jpg | PCVR | 01:00,00 |
| quest_gameplay.jpg | Quest autonome | 00:37,50 |

Les repères sont ceux demandés à FFmpeg ; avec une vidéo à cadence variable, l'image retenue est la première image disponible après le repère. `comparaison_jus.jpg`, `comparaison_explosion.jpg` et `volume_et_fin.jpg` sont les juxtapositions utilisées dans le rapport. Le script `tools/Extract-VideoEvidence.ps1` permet de reproduire ces extractions.

## Observations et portée

- PCVR : menu et variation du volume vers 4–12 s ; partie à partir de 13 s, score et vies mis à jour ; explosion/fin autour de 37,4 s, score final 15 ; retour au menu et nouvelle partie ; une seconde fin au score 20 est visible vers 79 s.
- Quest autonome : menu/volume vers 4–8 s ; partie à partir de 9 s, progression du score jusqu'à 25 ; explosion et écran final vers 45,3 s ; retour au menu et choix d'Extrême vers 53–56 s ; nouvelle partie, puis fin au score 30 vers 69 s.
- Les déplacements de vue, deux sabres, fruits tranchés, effets et changements de score/vies sont visibles sur les deux séquences. Aucun crash ni blocage n'est repéré dans les images examinées. Cela ne valide pas une session prolongée, la précision du tracking, la latence ni la stabilité thermique.
- Les vidéos commencent sur un écran de fin : le lancement initial et l'installation ne sont pas montrés intégralement. Leur réussite et l'exécution autonome de l'APK sont confirmées par le testeur. L'haptique est une confirmation sensorielle du testeur, pas une observation vidéo. Les variations du slider sont visibles ; l'efficacité du contrôle du volume est confirmée par le testeur.
- Des fragments/effets peuvent brièvement passer devant le panneau de fin puis disparaissent. Ce point est signalé dans le rapport comme limite visuelle résiduelle.
- FPS : **non mesurés**. Le relevé chiffré est facultatif dans le sujet ; le rapport ne transforme pas la cadence des vidéos en fréquence du moteur.

## Référence de rédaction

Sujet fourni : `Mini projet VR - Part 1 - Fruit Ninja VR.pdf`, ST2VAAS S9 Majeure T2IA 2026–2027, 3 pages. Le rapport suit ses cinq rubriques de livrables : état initial/organisation, effets, builds/tests, analyse/comparaison, difficultés/limites. Le document pédagogique original n'est pas republié dans le dépôt.
