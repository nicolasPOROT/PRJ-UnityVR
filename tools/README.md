# Outils Part 1

- `Compare-BuildSizes.ps1` : lire APK, ZIP et dossier Windows ; produire le tableau Markdown (Mo base 1024).
- `Inspect-ApkContents.ps1` : lire les entrées ZIP de l'APK, leur taille compressée/décompressée et le hash SHA256 ; ne remplace pas APK Analyzer.
- `Export-Report.ps1` : exporter docs/rapport.md avec Edge local en arrière-plan vers docs/rapport.pdf.
- `Inspect-UnityProject.cs`, `Inspect-Ui.cs`, `Read-BuildReport.cs` : inspections via Pipeline `script eval --file tools/<nom>.cs`, hors Play pour les audits de scènes.
- `Smoke-Gameplay.cs`, `Smoke-Difficulties.cs`, `Preview-GameOver.cs` : scénarios automatisés de l'éditeur en Play depuis MainMenu ; aucune interaction physique simulée n'est déclarée testée sur casque. Smoke-Gameplay injecte une vitesse dans l'estimateur du sabre pour exercer EzySlice et restaure le volume dans finally.

Les autres fichiers C# sont les **recettes d'édition déjà exécutées** : assemblage de prefabs/UI, réglages Inspector, déplacements AssetDatabase et paramètres de build. Ils sont hors Assets et ne sont jamais compilés ni exécutés par le jeu. Ils servent de trace de travail ; ouvrir le projet ne nécessite pas de les relancer. Certaines opérations sont additives ou multiplicatives et ne doivent pas être répétées sur les assets déjà préparés.
