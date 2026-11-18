# Organisation du projet

Les ressources importées et EzySlice sont regroupés sous **Assets/ThirdParty** :

- Assets/Imported_assets → Assets/ThirdParty/Imported_assets.
- Assets/ezy-slice-master → Assets/ThirdParty/EzySlice.

Les déplacements dans l'éditeur ont conservé les GUID et les références.

| Dossier | Contenu |
|---|---|
| Scenes | MainMenu, PlayScene et TestScene |
| Scripts | Gameplay et composants d'interface |
| Prefabs/Fruits | Fruits et références des effets de coupe |
| Prefabs/Bombs | Bombes et références des explosions |
| Prefabs/Fx | Bases et variantes PCVR/Quest |
| Prefabs/UI | Interface de volume |
| Models, Materials, Sounds, Sprites | Ressources du jeu |
| Settings, XR, XRI | Configuration du rendu, des builds et de XR |
| ThirdParty | Ressources importées et EzySlice |
| Samples | Actions XRI utilisées par le rig |

MainMenu est la scène d'entrée (index 0), PlayScene la scène de jeu (index 1). TestScene est exclue des builds. Les deux scènes et les 17 prefabs du jeu ont été contrôlés sans Missing.
