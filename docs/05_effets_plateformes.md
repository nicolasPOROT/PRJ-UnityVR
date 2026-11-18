# Effets PCVR / Quest

Les quatre Prefab Variants sont dans Assets/Prefabs/Fx. JuiceFx et ExplosionFx sont conservés comme bases. PlatformFxSelector choisit les variantes Quest pour Android et PCVR pour Windows, avec des références assignées dans l'Inspector des fruits, bombes et sélecteurs de difficulté.

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

L'émission continue est nulle ; chaque événement déclenche un burst. Noise et Velocity over Lifetime sont désactivés. Les valeurs de durée et de taille correspondent aux constantes maximales des systèmes.

La version PCVR conserve les réglages S7. Quest réduit les bursts, les plafonds et les durées de vie de 25 %, et désactive la collision du jus. Les matériaux et les tailles restent communs.

Les deux effets ont été testés sur Meta Quest 3 en PCVR via Virtual Desktop et en autonome. Les captures comparatives sont dans preuves/captures et dans le rapport. FPS non mesurés.
