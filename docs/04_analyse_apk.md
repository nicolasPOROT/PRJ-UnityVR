# Analyse APK

L'APK mesure **55,17 Mo**. Sa structure ZIP et les Build Reports sont relevés dans [07_contenu_apk.md](07_contenu_apk.md) et [10_repartition_builds.md](10_repartition_builds.md). Analyse Android Studio APK Analyzer et captures : **non réalisées**.

## Procédure APK Analyzer

1. Ouvrir Build > Analyze APK dans Android Studio.
2. Sélectionner Builds/Android_Quest/FruitNinjaVR_Quest.apk.
3. Relever le total et les colonnes de taille, avec leurs unités.
4. Développer assets/bin/Data et lib/arm64-v8a, puis capturer les principaux groupes.

| Groupe | Rôle |
|---|---|
| assets/bin/Data | Données Unity, scènes et ressources sérialisées |
| lib/arm64-v8a/libil2cpp.so | Code natif issu des scripts C# |
| lib/arm64-v8a/libunity.so | Moteur Unity |
| classes.dex | Bytecode Java/Kotlin |
| resources.arsc | Table des ressources Android |

Les données Unity regroupent plusieurs ressources dans un même fichier. Le Build Report complète cette vue par catégories et chemins d'assets.

## Comparaison des tailles

La commande **./tools/Compare-BuildSizes.ps1** compare l'APK, le ZIP Windows et le dossier Windows. Un Mo correspond à 1 048 576 octets. L'APK et le ZIP sont compressés ; le dossier Windows est mesuré par somme de ses fichiers.

## Pistes d'optimisation

- Réduire la résolution des faces de la skybox Epic_BlueSunset.
- Étudier la compression ASTC des textures Quest.
- Ajuster les import settings des textures et des modèles les plus volumineux.
- Examiner les dépendances réellement incluses avant de retirer des exemples ou ressources tierces.
- Comparer les variantes de shaders Vulkan/OpenGLES3 et le poids associé.

Ces pistes restent à évaluer. Les ressources et les réglages d'import existants sont conservés.
