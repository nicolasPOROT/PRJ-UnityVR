# Historique — Benjamin JOANNARD

## Éléments hérités de S7

- Gameplay : canons, fruits, bombes, sabres et découpe EzySlice.
- Score, combos, vies et cinq difficultés.
- Menu avec choix de difficulté par découpe.
- Effets JuiceFx et ExplosionFx.

## 17 novembre 2026 — reprise et déploiement

- Conservation de l'état initial au tag s7-initial ; projet sous Unity 6000.0.58f1.
- Correction de l'exclusion des .meta EzySlice.
- Regroupement des ressources importées et d'EzySlice sous ThirdParty.
- Création de JuiceFx_PCVR, JuiceFx_Quest, ExplosionFx_PCVR et ExplosionFx_Quest.
- Ajout de PlatformFxSelector et assignation des variantes aux fruits, bombes et sélecteurs.
- Amélioration des textes du menu, du score et des icônes de vie.
- Ajout de ScorePop, de l'haptique et de l'écran de fin avec Rejouer/Menu.
- Ajout du réglage de volume mémorisé et du compteur FPS activable.
- Configuration des profils Windows_PCVR et Android_Quest, d'OpenXR et des profils Touch.
- Passage à Input System uniquement pour résoudre le blocage Android avec Both.
- Builds Windows et Quest produits ; Windows : 0 erreur/0 warning, Quest : 0 erreur/2 recommandations OpenXR.
- Contrôle des références de deux scènes et 17 prefabs sans Missing.
- Analyse des tailles : APK 55,17 Mo, ZIP Windows 55,28 Mo, dossier Windows 133,10 Mo.

## 18 novembre 2026 — essais et remise

- Essais sur Meta Quest 3 : PCVR via Virtual Desktop et APK autonome.
- Même PC pour la compilation et le test PCVR : Intel i7-9700KF, RTX 2060 et 15,9 Gio RAM.
- Vérification des sabres, du gameplay, des effets, du volume, de l'écran de fin et de l'haptique sur les deux builds.
- Captures des parties, comparaisons d'effets et relevé des paramètres.
- Rapport individuel avec logo Efrei Paris / Paris Panthéon-Assas Université.
- Notice du projet et archive de remise réunissant Windows, Quest et rapport.
- FPS non mesurés ; analyse Android Studio APK Analyzer et captures non réalisées.
