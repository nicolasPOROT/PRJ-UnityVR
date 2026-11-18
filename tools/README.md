# Outils Part 1

- `Compare-BuildSizes.ps1` : lire APK, ZIP et dossier Windows ; produire le tableau Markdown (Mo base 1024).
- `Inspect-ApkContents.ps1` : lire les entrées ZIP de l'APK, leur taille compressée/décompressée et le hash SHA256 ; ne remplace pas APK Analyzer.
- `Export-Report.ps1` : exporter docs/rapport.md avec Edge local en arrière-plan vers docs/rapport_part1_benjamin_joannard.pdf ; chemins de sortie et aperçu HTML facultatifs.
- `Render-PdfPreview.ps1` : rendre les pages du PDF en PNG avec le moteur Windows pour contrôler la mise en page réelle ; sorties dans .utmp/report-pages par défaut.
- `Extract-VideoEvidence.ps1` : extraire les captures horodatées et comparaisons depuis docs/preuves/videos avec FFmpeg local (fourni avec Virtual Desktop sur ce poste) ; les vidéos ne sont pas versionnées.
- `Inspect-UnityProject.cs`, `Inspect-Ui.cs`, `Read-BuildReport.cs` : inspections via Pipeline `script eval --file tools/<nom>.cs`, hors Play pour les audits de scènes.
- `Smoke-Gameplay.cs`, `Smoke-Difficulties.cs`, `Preview-GameOver.cs` : scénarios automatisés de l'éditeur en Play depuis MainMenu ; aucune interaction physique simulée n'est déclarée testée sur casque. Smoke-Gameplay injecte une vitesse dans l'estimateur du sabre pour exercer EzySlice et restaure le volume dans finally.

Les autres fichiers C# sont les **recettes d'édition déjà exécutées** : assemblage de prefabs/UI, réglages Inspector, déplacements AssetDatabase et paramètres de build. Ils sont hors Assets et ne sont jamais compilés ni exécutés par le jeu. Ils servent de trace de travail ; ouvrir le projet ne nécessite pas de les relancer. Certaines opérations sont additives ou multiplicatives et ne doivent pas être répétées sur les assets déjà préparés.
