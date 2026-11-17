# Analyse APK et comparaison des tailles

**Analyse NON RÉALISÉE : aucun APK final disponible à ce stade.**

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
