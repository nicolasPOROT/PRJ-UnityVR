# État initial du projet S7

Audit du 17 novembre 2026. Référence initiale : commit **9313f11**, conservé au tag **s7-initial**.

| Élément | État initial | Correction ou suite |
|---|---|---|
| Unity et rendu | Unity 6000.0.56f1, URP 17.0.4 | Projet repris sous 6000.0.58f1 |
| Packages XR | XRI 3.2.1, OpenXR 1.15.1, XR Management 4.5.2 | Versions conservées |
| Windows XR | OpenXR présent, profils Oculus Touch / Quest Touch | Validation et build Windows |
| Android XR | Provider Android absent de la configuration sérialisée, support Quest désactivé | OpenXR Android, support Quest et profils Touch configurés |
| Rig | Player avec caméra, Camera Offset, LeftHand/RightHand et actions de contrôleurs | Références contrôlées dans l'éditeur |
| Scènes | MainMenu (0), PlayScene (1), TestScene hors build | Ordre conservé |
| Organisation | Dossiers de jeu présents, Imported_assets et ezy-slice-master à la racine | Regroupement sous Assets/ThirdParty |
| Effets | JuiceFx et ExplosionFx communs aux plateformes | Quatre variantes et sélection par plateforme |
| Gameplay | Fruits, bombes, deux sabres, EzySlice, score, combos et vies | Logique conservée |
| Difficulté | Cinq niveaux, sans pénalité pour les fruits manqués aux niveaux 0–1 | Vérification des cinq niveaux |
| Entraînement et bonus | Aucun mode distinct ni bonus spécifique identifié | Absents de cette version |
| Volume | Aucun réglage dans le menu | Slider VR et mémorisation ajoutés |
| Fin de partie | Retour immédiat à MainMenu | Écran final, score et boutons Rejouer/Menu |
| Classement et JSON | Aucune implémentation identifiée | Absents de cette version |
| Android | IL2CPP, ARM64, Linear, API minimale 23 et cible automatique | Minimum 24, cible 36, identité propre |
| Git | Dossiers de travail Unity exclus ; métadonnées EzySlice masquées par une règle locale | Inclusion des .meta et exclusion des livrables |

## Références et métadonnées

Le .gitignore livré avec EzySlice excluait ses fichiers .meta. La règle a été corrigée et les métadonnées produites par Unity ont été versionnées. Les déplacements des ressources ont été réalisés dans l'éditeur, avec conservation des GUID.

Après import, MainMenu, PlayScene et les 12 prefabs initiaux ne présentaient aucun Missing. La compilation était réussie. Deux warnings concernaient un polygone auto-intersectant du modèle sword.001 ; la validation Android signalait l'API minimale 23, ensuite portée à 24.

## Ressources source volumineuses

| Ressource initiale sous Imported_assets | Octets |
|---|---:|
| AllSkyFree/Cold Sunset/Cold Sunset Equirect.png | 79 869 056 |
| Pure Poly/Free Low Poly Nature Pack/Terrain/Terrain.asset | 67 670 364 |
| AllSkyFree/Deep Dusk/Deep Dusk Equirect.png | 50 754 568 |
| AllSkyFree/Cold Night/Cold Night Equirect.png | 43 417 291 |

La répartition des ressources incluses dans les builds est détaillée dans [10_repartition_builds.md](10_repartition_builds.md). Les sources utilisent les attributs Git texte automatiques ; aucun fichier n'est suivi par LFS.
