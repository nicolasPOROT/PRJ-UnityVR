# Organisation retenue — correction Task 2

Déplacements effectués par `AssetDatabase.MoveAsset` dans l'éditeur, hors Play :

- `Assets/Imported_assets` → `Assets/ThirdParty/Imported_assets`.
- `Assets/ezy-slice-master` → `Assets/ThirdParty/EzySlice`.

Les GUID et métadonnées sont conservés. Aucun asset supprimé. `Samples` reste au chemin conventionnel des imports de samples Unity et `TextMesh Pro` au chemin hérité ; aucune suppression ni réorganisation massive de ces dépendances. Les dossiers propres au jeu (Scripts, Scenes, Prefabs/Fruits, Prefabs/Bombs, Prefabs/Fx, Models, Materials, Sounds, Sprites, Settings, XR, XRI) restent stables.

Après déplacement : compilation réussie, audit des deux scènes et 16 prefabs sans Missing, smoke test logique de PlayScene réussi (spawn, score/combos, bombe/vies, retour menu). Geste EzySlice et casque restent à tester.
