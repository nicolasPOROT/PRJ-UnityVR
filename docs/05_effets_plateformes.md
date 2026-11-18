# Effets PCVR / Quest — correction Task 3

Quatre véritables Prefab Variants créés via l'éditeur hors Play, sous `Assets/Prefabs/Fx`. Les deux prefabs hérités restent présents. Les cinq prefabs de fruits/bombes et les cinq sélecteurs de difficulté du menu portent `PlatformFxSelector`, avec références sérialisées vers les deux variantes adaptées. `Awake` choisit Quest pour un player Android et PCVR pour l'éditeur/Windows ; il ne crée aucun objet. L'instanciation de l'effet reste celle du script de coupe hérité.

| Variante / système | Max particles | Lifetime (s) | Start size | Burst | Collision | Matériau / shader | Modules actifs principaux |
|---|---:|---:|---:|---|---|---|---|
| Jus PCVR | 1000 | 0,9 | 0,1 | 15 | Oui | JuiceMat / Shader Graphs/Juice | Main, Shape, Emission, Size over Lifetime, Trails |
| Jus Quest | 120 | 0,675 | 0,1 | 1–6 | Non | Identique | Identiques sauf Collision |
| Explosion PCVR / racine | 1000 | 0,7 | 3 | 30 | Non | ParticlesUnlit / URP Particles Unlit | Main, Shape, Emission, Color over Lifetime |
| Explosion PCVR / Smoke | 1000 | 0,7 | 2 | 30 | Non | ParticlesUnlit / URP Particles Unlit | Main, Shape, Emission, Color/Size over Lifetime |
| Explosion PCVR / Sparks | 1000 | 0,5 | 0,05 | 20 | Non | SparkMat / URP Lit | Main, Shape, Emission, Trails |
| Explosion Quest / racine | 80 | 0,525 | 3 | 1–12 | Non | Identique | Identiques |
| Explosion Quest / Smoke | 80 | 0,525 | 2 | 1–12 | Non | Identique | Identiques |
| Explosion Quest / Sparks | 80 | 0,375 | 0,05 | 1–8 | Non | Identique | Identiques |

Emission continue nulle ; émission par burst. Noise et Velocity over Lifetime désactivés dans ces systèmes. Les durées et tailles sont les constantes maximales relevées via l'API Unity, pas des mesures de durée visuelle sur casque.

Justification : PCVR conserve le rendu hérité. Quest réduit les bursts, les plafonds et la durée de vie ; désactiver la collision du jus retire ce coût physique. Matériaux conservés pour limiter les changements visuels. Aucun gain FPS ni qualité visuelle sur Quest n'est affirmé avant test physique.

Vérification : quatre assets classés Variant par PrefabUtility ; audit hors Play des deux scènes et 16 prefabs sans Missing. Smoke test logique après configuration : +1 simple, +4 combo de deux, bombe −1 vie, retour MainMenu fonctionnel. Geste de coupe, effets perçus sur casque et sélection dans un APK final : NON TESTÉS.

Recettes d'édition conservées hors Assets dans tools. Elles ne s'exécutent pas au lancement du jeu. Ne pas relancer Tune-QuestFx aveuglément : la réduction des bursts est multiplicative.

## Observation sur les builds — 18 novembre 2026

Vidéos reçues sur Meta Quest 3 : Windows via Virtual Desktop et APK autonome. Jus et explosion sont visibles sur les deux ; les comparaisons réelles sont dans preuves/captures et la section 2 du rapport. Matériaux/tailles identiques : l'apparence reste proche, mais les bursts Quest sont moins denses, les durées réduites de 25 % et la collision du jus désactivée. Les plafonds Max particles sont des limites, pas le nombre réellement émis à chaque coupe. Les prises de vue ne sont pas synchronisées ; elles ne démontrent pas un gain FPS. Les mentions NON TESTÉS ci-dessus décrivent la vérification initiale du 17 novembre.
