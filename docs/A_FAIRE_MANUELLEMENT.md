# Compléments avant remise

État au 18 novembre 2026 : les deux builds fonctionnent sur **Meta Quest 3**, avec **Virtual Desktop** pour le PCVR. Benjamin JOANNARD confirme volume, écran de fin et haptique sur les deux. Les captures réelles sont intégrées au rapport ; voir [résultats](03_tests_builds.md) et [provenance des vidéos](preuves/README.md). La Part 1 est individuelle : auteur **Benjamin JOANNARD**.

## 1. APK Analyzer — complément demandé dans le sujet

L'analyse ZIP et le Build Report sont disponibles ; **l'ouverture dans Android Studio et ses captures n'ont pas été réalisées**.

1. Dans Android Studio, choisir **Build > Analyze APK**, puis `Builds/Android_Quest/FruitNinjaVR_Quest.apk` (ou sa copie identique dans Livrable/Quest).
2. Vérifier le fichier : 57 851 307 octets, SHA256 `BEB89D96902D651B30F116E4FB14DF865CA2C7EEDD5E901E8D40C60E258053FC`.
3. Prendre une vue d'ensemble avec le total, puis développer `assets/bin/Data` et `lib/arm64-v8a`. Faire apparaître `libil2cpp.so`, `libunity.so`, `classes.dex` et `resources.arsc` ; deux ou trois captures lisibles suffisent.
4. Noter les intitulés exacts des colonnes et leurs valeurs : taille brute, taille compressée ou estimation de téléchargement ne sont pas interchangeables. Comparer aux mesures ZIP de [07_contenu_apk.md](07_contenu_apk.md).
5. Enregistrer les captures dans docs/preuves/captures/ et les ajouter à la section 4 du rapport, puis réexporter le PDF. Les ressources Unity sont regroupées ; APK Analyzer ne présente pas chaque texture comme un fichier distinct.

## 2. Contexte des essais — précisions utiles si disponibles

Noter le PC réellement utilisé pour le test PCVR (est-ce bien i7-9700KF / RTX 2060 / 15,9 Gio ?), le runtime OpenXR actif (VDXR ou SteamVR), la fréquence du Quest et les réglages Virtual Desktop (résolution/qualité et débit). Pour l'autonome : version système et fréquence si connues. Sinon conserver « non relevé » ; aucune valeur n'est supposée dans le rapport.

La stabilité prolongée, la chauffe, la latence et le bouton Rejouer utilisé physiquement restent des compléments possibles ; le rapport ne les déclare pas mesurés ou démontrés par la vidéo.

## 3. FPS — facultatif, pas nécessaire pour rendre un rapport honnête

Le sujet autorise un relevé chiffré si une mesure a été faite. **On peut conserver « FPS non mesurés »**. Les vidéos autour de 30 images/s ne donnent pas la cadence du jeu. Le compteur existant est masqué dans les builds de livraison.

Pour effectuer un relevé simple si souhaité :

1. Ouvrir Assets/Prefabs/Player.prefab. Sous Camera Offset/Main Camera/DebugFpsCanvas, sélectionner le composant **FpsOverlay** et cocher **Show Overlay**. Sauvegarder le prefab ; ne pas créer de nouvel objet.
2. Produire un build de test séparé, par exemple Builds/Windows_PCVR_TestFPS/ ou Builds/Android_Quest_TestFPS/. Sur Quest, installer cet APK de test avec `adb install -r` ; il remplace l'installation actuelle si l'identifiant est identique. Conserver les fichiers Release de livraison.
3. Jouer avec le casque : attendre 30 s, puis tester 60 s en Facile et 60 s en Extrême avec coupes/explosions. Relever le compteur toutes les 10 s, le casque, sa fréquence, les réglages et la connexion. Noter moyenne et minimum **des échantillons** ; ce ne sont pas les statistiques de toutes les frames.
4. Ce compteur échantillonne les frames applicatives sur 0,5 s. Il ne mesure ni le temps GPU, ni la latence, ni les images reprojetées du compositor. Identifier la méthode dans le rapport.
5. Décocher Show Overlay et sauvegarder le prefab après le test. Ne pas remplacer les livrables existants par inadvertance. Aucun nouveau build n'a été produit pour ce relevé à ce jour.

## 4. Remise et accès aux sources

Vérifier l'accès enseignant au dépôt et publier la branche **Mini-projet-Ben** avant remise si elle n'est pas encore distante. **Aucun push n'est effectué sans demande explicite.** Conserver l'archive Livrable/FruitNinjaVR_Part1.zip avec Windows complet et APK autonome.

Le PDF actuel est [rapport_part1_benjamin_joannard.pdf](rapport_part1_benjamin_joannard.pdf). L'ancien rapport.pdf reste une version antérieure. Les vidéos locales ont été rangées dans docs/preuves/videos/ et sont ignorées par Git ; les captures sélectionnées sont versionnées.

Les optimisations supplémentaires (ASTC, imports, retrait de Samples/skybox) attendent une validation explicite. Aucune suppression d'asset ni modification du gameplay n'a été faite pour cette mise à jour documentaire.
