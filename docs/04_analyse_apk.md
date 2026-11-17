# Analyse APK et comparaison des tailles

**Analyse Android Studio NON RÉALISÉE.** Un APK final est disponible ; son contenu ZIP a été mesuré automatiquement, voir `07_contenu_apk.md`. Cette lecture ne remplace pas APK Analyzer ni les captures demandées.

## Procédure APK Analyzer

1. Dans Android Studio, choisir Build > Analyze APK (le libellé peut varier selon version).
2. Ouvrir `Builds/Android_Quest/FruitNinjaVR_Quest.apk` et noter date, commit source, configuration Development/Release et version Unity.
3. Relever taille totale du fichier, taille compressée et taille décompressée des principales entrées ; préciser la colonne utilisée et son unité.
4. Déplier les entrées suivantes et enregistrer des captures `[CAPTURE À INSÉRER]`.

| Entrée | Rôle | Taille compressée | Taille décompressée |
|---|---|---|---|
| assets/bin/Data | Données Unity : scènes, ressources, fichiers sérialisés | À relever | À relever |
| lib/arm64-v8a/libil2cpp.so | Code C# converti en code natif via IL2CPP | À relever | À relever |
| lib/arm64-v8a/libunity.so | Moteur Unity natif | À relever | À relever |
| classes.dex (éventuellement plusieurs) | Bytecode Java/Kotlin pour Android et plugins | À relever | À relever |
| resources.arsc | Table de ressources Android compilées, si présente | À relever | À relever |

Une entrée peut être absente ou différer selon packaging : le signaler. Les assets Unity sont souvent regroupés ; APK Analyzer ne permet pas à lui seul d'attribuer tout le poids à chaque texture/préfab. Ne pas confondre poids Git des sources, poids des données importées, taille APK et espace occupé après installation.

## Tailles comparées

Depuis la racine du dépôt :

```powershell
./tools/Compare-BuildSizes.ps1
```

Le script lit les livrables et produit un tableau Markdown. Mo = octets / 1 048 576. Un livrable absent est indiqué ABSENT ; aucune taille n'est inventée. Le dossier Windows est la somme des tailles des fichiers, pas leur allocation physique sur disque.

## Build Report Unity

Après les builds, consulter le rapport de l'éditeur et/ou l'Editor.log (`%LOCALAPPDATA%/Unity/Editor/Editor.log`) ; `Library/LastBuild.buildreport` est un rapport binaire à lire via l'éditeur. Ne pas le modifier ou le versionner. Archiver les résultats textuels utiles dans docs, avec la plateforme et le commit.

| Catégorie | Windows | Quest | Source de la mesure |
|---|---|---|---|
| Textures | À relever | À relever | |
| Modèles | À relever | À relever | |
| Audio | À relever | À relever | |
| Shaders | À relever | À relever | |
| Assets les plus lourds | À relever | À relever | |

## Optimisations proposées — application après validation seulement

- Évaluer ASTC pour les textures Android et réduire la résolution des textures surdimensionnées après comparaison visuelle sur Quest.
- Examiner les skyboxes AllSkyFree et les textures du décor identifiées dans le Build Report ; vérifier leur utilisation avant toute action.
- Contrôler les imports audio (compression, mono si pertinent, durée) et les variantes de shaders conservées.
- Examiner Samples, exemples TextMesh Pro et assets tiers ; leur présence dans Assets ne prouve pas leur inclusion dans le build. Ne supprimer ni déplacer sans le workflow éditeur et l'accord requis.
- Réduire le coût des effets Quest via leurs variantes, puis mesurer réellement la fluidité.

Aucune optimisation ci-dessus n'est appliquée. Taille, qualité et fluidité ne peuvent être comparées avant builds et tests sur casque.

Attention aux dépendances effectivement utilisées : `Assets/Samples/XR Interaction Toolkit/3.2.1/XR Interaction Simulator/XRI Default Input Actions.inputactions` est référencé par le suivi et les nouvelles interactions UI du Player. Supprimer Samples en bloc casserait ces références. Toute extraction de cet asset doit passer par l'éditeur, conserver son GUID et être validée avant retrait d'exemples.

## Premier constat mesuré — Build Report Windows

Le build Windows réussi (0 erreur/0 warning) indique 30 424 071 octets de textures packées, 3 665 432 de modèles, 1 907 084 de shaders et 64 376 d'audio dans les PackedAssetInfo. Catégories calculées par extension du chemin source : ce n'est pas la taille compressée du ZIP ni le total du player. Voir `build_report_windows.json`.

Les six faces de skybox **Epic_BlueSunset** dominent les entrées les plus lourdes, environ 4 Mio chacune. Contrairement aux gros fichiers source non utilisés relevés dans l'audit, ces six textures sont réellement incluses. Recommandation après validation : comparer leur réduction de résolution et leur compression Android, avec contrôle visuel. Des ressources TextMesh Pro Examples sont également présentes dans le rapport ; examiner les dépendances/Resources avant toute suppression. Aucune optimisation appliquée.

## Mesures de l'APK final

Fichier APK : **57 851 307 octets, 55,17 Mo**. Lecture ZIP : données Unity 26,57 Mo compressés / 55,04 Mo décompressés ; libil2cpp.so 15,37 / 61,65 Mo ; libunity.so 9,07 / 20,61 Mo ; classes.dex 2,25 / 6,16 Mo ; resources.arsc 0,24 / 0,24 Mo. Hash SHA256 et dix plus grosses entrées dans `07_contenu_apk.md`.

Le Build Report Android annonce 821 114 124 octets pour son ensemble de fichiers, qui inclut notamment des sources C++ générées d'Il2CppBackup et des bibliothèques intermédiaires sous Library. **Ce total n'est pas la taille de l'APK.** Les comparaisons de livrables utilisent les fichiers réellement livrés, mesurés par Compare-BuildSizes.
