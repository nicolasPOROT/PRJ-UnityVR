![Efrei Paris — Paris Panthéon-Assas Université](branding/efrei-logo.svg)

# Fruit Ninja VR — Part 1

**Reprise et déploiement multiplateforme**

**Benjamin JOANNARD** · ST2VAAS — S9 Majeure T2IA · 2026–2027

Projet : [github.com/nicolasPOROT/PRJ-UnityVR](https://github.com/nicolasPOROT/PRJ-UnityVR/tree/Mini-projet-Ben) — branche Mini-projet-Ben.

Pour cette première partie, j'ai repris le Fruit Ninja VR réalisé en groupe en S7. J'ai conservé le gameplay existant, réorganisé les ressources, adapté les effets aux deux plateformes et ajouté plusieurs améliorations de confort. J'ai ensuite testé le jeu sur Meta Quest 3, en PCVR avec Virtual Desktop et en autonome avec l'APK Android.

## 1. État initial, corrections et organisation

| Fonctionnalité / élément | État initial S7 | Modifications Part 1 |
|---|---|---|
| Fruits, bombes, sabres, coupe et score | Gameplay fonctionnel | Règles conservées ; vérification des coupes, combos et pénalités |
| Cinq difficultés | Présentes ; pas de pénalité pour un fruit manqué aux niveaux 0–1 | Vérification des cinq niveaux, sans changement des règles |
| Entraînement, bonus, classement et JSON | Pas de mode d'entraînement distinct, de bonus spécifique, de classement ni de sauvegarde JSON identifiés | Fonctions absentes de cette version |
| Effets | Un jus et une explosion communs aux plateformes | Quatre variantes PCVR/Quest, sélectionnées automatiquement |
| Configuration XR | OpenXR Windows présent, configuration Android incomplète | OpenXR Android, support Quest et profils Touch configurés |
| Volume | Aucun réglage dans le menu | Slider utilisable par rayon et volume mémorisé |
| Interface et fin de partie | Lisibilité perfectible, retour immédiat au menu | HUD agrandi, écran final avec score et boutons Rejouer/Menu |

J'ai regroupé les ressources importées et EzySlice dans **Assets/ThirdParty**, en les déplaçant dans l'éditeur pour conserver les GUID. Les éléments du jeu sont répartis entre Scenes, Scripts, Prefabs/Fruits, Prefabs/Bombs, Prefabs/Fx, Prefabs/UI, Materials, Models et Sounds. Les actions XRI utilisées restent dans Samples.

La scène de démarrage est **MainMenu (index 0)**, suivie de **PlayScene (index 1)**. TestScene reste réservée aux essais. Le contrôle des deux scènes et des 17 prefabs du jeu ne signale aucun composant ou référence manquants. Le tag **s7-initial** conserve l'état de départ.

J'ai également ajouté une animation du score, une impulsion haptique à chaque coupe et un compteur FPS activable. Le spawn, EzySlice, le score, les combos, les vies et la difficulté conservent leur logique S7. Le volume est enregistré avec PlayerPrefs.

![Volume réglable par rayon en PCVR à gauche ; score final et boutons de fin de partie sur Quest à droite.](preuves/captures/volume_et_fin.jpg)

<!-- PAGE BREAK -->

## 2. Effets de coupe et d'explosion

Une coupe déclenche le jus à la position du fruit ; une bombe touchée déclenche l'explosion et la perte d'une vie. J'ai créé quatre Prefab Variants, avec les suffixes **_PCVR** et **_Quest**, à partir des effets S7. Le composant **PlatformFxSelector** sélectionne la variante Android ou Windows à partir des références assignées dans l'Inspector.

### Coupe de fruit : JuiceFx

| Plateforme | Max particles | Burst | Lifetime | Start size | Collision |
|---|---:|---:|---:|---:|---|
| PCVR | 1 000 | 15 | 0,9 s | 0,1 | Activée |
| Quest | 120 | 1–6 | 0,675 s | 0,1 | Désactivée |

Le matériau **JuiceMat / Shader Graphs/Juice** est commun aux deux variantes. Shape, Emission, Size over Lifetime et Trails sont actifs. L'émission continue est nulle : chaque coupe produit un burst. Sur Quest, j'ai réduit le nombre de gouttes et leur durée, et désactivé les collisions pour alléger la simulation.

![Jus : PCVR à gauche (00:32,50), Quest autonome à droite (00:17,50). La variante Quest conserve les gouttes orange et les traînées, avec une densité réduite.](preuves/captures/comparaison_jus.jpg)

### Bombe : ExplosionFx

| Système | Start size | Max PC / Quest | Burst PC / Quest | Lifetime PC / Quest |
|---|---:|---:|---|---|
| Explosion principale | 3 | 1 000 / 80 | 30 / 1–12 | 0,7 / 0,525 s |
| Fumée | 2 | 1 000 / 80 | 30 / 1–12 | 0,7 / 0,525 s |
| Étincelles | 0,05 | 1 000 / 80 | 20 / 1–8 | 0,5 / 0,375 s |

Explosion et fumée utilisent **ParticlesUnlit / URP Particles Unlit**, les étincelles **SparkMat / URP Lit**. Les collisions sont désactivées. Shape et Emission sont actifs ; Color over Lifetime agit sur l'explosion/fumée, Size over Lifetime sur la fumée et Trails sur les étincelles. Noise et Velocity over Lifetime restent désactivés.

![Explosion : PCVR à gauche (00:37,55), Quest autonome à droite (00:45,35). Le flash jaune-orangé accompagne la perte de la dernière vie.](preuves/captures/comparaison_explosion.jpg)

J'ai gardé les mêmes matériaux et tailles pour conserver l'identité des effets. Sur Quest, les plafonds et les bursts sont plus faibles, et les durées sont réduites de **25 %**. En jeu, les deux événements restent lisibles ; l'effet Quest est moins dense et disparaît plus rapidement.

<!-- PAGE BREAK -->

## 3. Configuration et tests des builds

**Environnement :** Unity 6000.0.58f1, URP 17.0.4, XR Interaction Toolkit 3.2.1, OpenXR 1.15.1, XR Management 4.5.2 et EzySlice.

| Paramètre | Windows PCVR | Meta Quest autonome |
|---|---|---|
| Casque | Meta Quest 3 | Meta Quest 3 |
| Exécution | Player Windows via Virtual Desktop | APK installé sur le casque |
| Cible / backend | Windows x64 / Mono | Android / IL2CPP / ARM64 uniquement |
| XR | OpenXR, profils Oculus Touch / Quest Touch | OpenXR, Meta Quest Support et profils Touch |
| Qualité URP | PC : render scale 1, ombres 2048 | Mobile : render scale 0,8, ombres 1024 |
| Réglages graphiques | Linear, HDR actif, MSAA désactivé | Linear, HDR actif, MSAA désactivé ; Vulkan puis OpenGLES3 |
| API Android | Sans objet | Minimum 24 ; cible et compilation 36 |
| Compilation finale | Réussie : 0 erreur, 0 warning | Réussie : 0 erreur, 2 recommandations OpenXR |

Les deux builds utilisent MainMenu et PlayScene, avec la variante d'effets correspondant à la plateforme. L'application Android porte l'identifiant **fr.st2vaas.fruitninjavr.ben**, version 0.1.0. Les modules Android, SDK, NDK et OpenJDK sont installés avec l'éditeur.

La validation XR ne présente pas d'erreur bloquante. Les deux recommandations Android concernent le SSAO des pipelines configurés et une option de latence d'entrée. Le renderer mobile ne possède pas de Renderer Feature SSAO.

J'ai utilisé le même PC pour la compilation et les tests PCVR : **Intel i7-9700KF, RTX 2060 et 15,9 Gio de RAM**. Le runtime OpenXR actif pendant l'essai PCVR, la fréquence du casque et les paramètres Virtual Desktop n'ont pas été relevés.

### Résultats sur Meta Quest 3

J'ai testé le lancement, les sabres, les coupes, les bombes, l'évolution du score et des vies, puis le retour au menu sur les deux versions. Les enregistrements durent environ **86 s en PCVR** et **75 s en autonome**.

| Vérification | PCVR | Quest autonome | Résultat |
|---|---|---|---|
| Lancement | OK | OK | Accès au menu et à une partie |
| Tracking et sabres | OK | OK | Suivi de la vue et des contrôleurs, deux sabres utilisables |
| Fruits, bombes, coupe, score et vies | OK | OK | Fruits séparés, score mis à jour et pénalités appliquées |
| Effets visuels | OK | OK | Jus à la coupe, explosion à l'impact sur une bombe |
| Menu et volume | OK | OK | Sélection de difficulté et slider utilisables en VR |
| Écran de fin et retour au menu | OK | OK | Score final lisible et retour à la sélection |
| Retour haptique | OK | OK | Impulsion ressentie lors d'une coupe |
| Exécution des parties enregistrées | OK | OK | Aucun crash ou blocage constaté |

Les contrôles dans l'éditeur couvrent aussi les cinq difficultés, les combos, les règles de perte de vie et la réinitialisation du score et des vies avec Rejouer. Les niveaux 0–1 ne pénalisent pas les fruits manqués ; les niveaux 2–4 retirent une vie. Une bombe retire une vie dans chaque mode.

**FPS : non mesurés.** Les essais prolongés et la stabilité thermique n'ont pas été testés.

<!-- PAGE BREAK -->

## 4. Analyse de l'APK et comparaison

J'ai examiné la structure ZIP de l'APK et les Build Reports Unity pour identifier les principaux fichiers et les ressources les plus lourdes.

| Entrée / groupe | Mo compressés | Mo décompressés | Rôle |
|---|---:|---:|---|
| assets/bin/Data | 26,57 | 55,04 | Scènes, ressources sérialisées, métadonnées et données Unity regroupées |
| lib/arm64-v8a/libil2cpp.so | 15,37 | 61,65 | Code C# converti en bibliothèque native par IL2CPP |
| lib/arm64-v8a/libunity.so | 9,07 | 20,61 | Moteur Unity natif |
| classes.dex | 2,25 | 6,16 | Bytecode Java/Kotlin de la couche Android |
| resources.arsc | 0,24 | 0,24 | Table compilée des ressources Android |

Une partie des ressources est regroupée dans les fichiers Unity. Le Build Report permet de compléter cette vue avec une répartition par catégorie : **29,01 / 28,02 Mo de textures** pour Windows/Quest, **3,50 Mo de modèles** et **0,06 Mo d'audio** sur chaque plateforme. Les six faces de la skybox Epic_BlueSunset représentent environ **24 Mo packés**.

Les shaders occupent **1,82 Mo sur Windows** et **2,97 Mo sur Quest**, qui embarque des variantes pour deux API graphiques. La skybox constitue la principale piste de réduction du poids des ressources.

### Comparaison des tailles

| Livrable | Taille en Mo | Taille en octets |
|---|---:|---:|
| APK Quest | 55,17 | 57 851 307 |
| ZIP Windows | 55,28 | 57 963 597 |
| Dossier Windows complet | 133,10 | 139 566 307 |

**1 Mo = 1 048 576 octets.** L'APK et le ZIP sont compressés, contrairement à la somme des fichiers du dossier Windows. Leurs tailles proches recouvrent des contenus différents : Windows utilise Mono, tandis que Quest utilise IL2CPP et des bibliothèques ARM64. Le total brut du Build Report Android comprend des intermédiaires de compilation hors APK.

Les deux versions conservent le même gameplay et des interfaces lisibles. PCVR utilise des effets plus denses ; la version autonome réduit les particules, leur persistance et les collisions du jus. Les paramètres URP mobiles réduisent également la résolution interne et celle des ombres. La compression vidéo et le streaming Virtual Desktop influencent aussi le rendu des captures.

## 5. Difficultés et limites

Le premier build Android a été bloqué par le réglage **Input Handling Both**. Je l'ai remplacé par **Input System Package (New)**, déjà utilisé par le rig et les interfaces. L'APK a ensuite été généré avec succès.

Pour la fin de partie, j'ai ajouté un panneau opaque, masqué le HUD et les sabres, et conservé le score final à l'écran. Des fragments et particules encore actifs peuvent passer brièvement devant le panneau pendant la transition.

| Point restant | État |
|---|---|
| Analyse Android Studio APK Analyzer et captures | Non réalisées ; analyse ZIP et Build Reports disponibles |
| Mesures de latence et stabilité thermique | Non réalisées |
| Classement, sauvegarde JSON et bonus spécifiques | Absents de cette version |

Les livrables comprennent le projet source sur GitHub, le dossier Windows complet, l'APK Quest autonome et ce rapport. Les captures des essais sont conservées dans **docs/preuves/captures**.
