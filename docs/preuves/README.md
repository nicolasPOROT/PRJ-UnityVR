# Enregistrements et captures des essais

**Benjamin JOANNARD** — 18 novembre 2026. **Meta Quest 3**, PCVR via **Virtual Desktop** et APK autonome.

## Vidéos

| Enregistrement | Emplacement local | Durée | Octets |
|---|---|---:|---:|
| PCVR | videos/pcvr.mp4 | 86,208750 s | 30 721 186 |
| Quest autonome | videos/quest_autonome.mp4 | 74,967767 s | 26 833 533 |

Résolution : 1280 × 720. Les vidéos originales restent hors Git ; leurs métadonnées et SHA256 figurent dans videos.json.

Les captures ont été extraites avec FFmpeg. Les images comparatives juxtaposent PCVR à gauche et Quest à droite. Les parties et les prises de vue sont différentes.

## Captures

| Fichier dans captures/ | Vidéo | Repère |
|---|---|---|
| pcvr_jus.jpg | PCVR | 00:32,50 |
| quest_jus.jpg | Quest | 00:17,50 |
| pcvr_explosion.jpg | PCVR | 00:37,55 |
| quest_explosion.jpg | Quest | 00:45,35 |
| pcvr_volume.jpg | PCVR | 00:43,60 |
| quest_volume.jpg | Quest | 00:05,50 |
| pcvr_fin.jpg | PCVR | 00:40,00 |
| quest_fin.jpg | Quest | 00:48,00 |
| pcvr_gameplay.jpg | PCVR | 01:00,00 |
| quest_gameplay.jpg | Quest | 00:37,50 |

Comparaisons utilisées dans le rapport : comparaison_jus.jpg, comparaison_explosion.jpg et volume_et_fin.jpg. Les extractions sont reproductibles avec tools/Extract-VideoEvidence.ps1.

## Déroulement

- **PCVR :** menu et volume vers 4–12 s ; partie à partir de 13 s ; fin au score 15 autour de 37 s ; retour menu et deuxième partie, terminée au score 20.
- **Quest autonome :** menu et volume vers 4–8 s ; partie à partir de 9 s ; fin au score 25 autour de 45 s ; sélection d'Extrême, puis nouvelle partie terminée au score 30.
- Les deux essais montrent les sabres, les coupes, le score, les vies, les effets et les retours au menu.
- Volume, écran final et haptique fonctionnels sur les deux builds.
- Des fragments et particules peuvent brièvement passer devant le panneau final.
- FPS non mesurés. Stabilité prolongée et latence non testées.
