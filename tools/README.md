# Outils du projet

| Outil | Fonction |
|---|---|
| Compare-BuildSizes.ps1 | Comparer les tailles de l'APK, du ZIP et du dossier Windows en Mo base 1024 |
| Inspect-ApkContents.ps1 | Relever les entrées ZIP de l'APK, les tailles et le SHA256 |
| Export-Report.ps1 | Exporter docs/rapport.md en PDF avec Edge |
| Render-PdfPreview.ps1 | Produire des aperçus PNG des pages du PDF avec le moteur Windows |
| Extract-VideoEvidence.ps1 | Extraire les captures horodatées avec FFmpeg |
| Inspect-UnityProject.cs, Inspect-Ui.cs | Contrôler les objets, composants et références dans l'éditeur |
| Read-BuildReport.cs | Lire le Build Report Unity |
| Smoke-Gameplay.cs, Smoke-Difficulties.cs | Vérifier le gameplay et les cinq difficultés dans l'éditeur |
| Preview-GameOver.cs | Prévisualiser le panneau de fin de partie |
| Package-Submission.ps1 | Assembler et vérifier l'archive de remise |

Les scripts C# de ce dossier sont des outils d'édition et de contrôle, placés hors Assets. Les fichiers d'assemblage et de réglage correspondent à des opérations déjà appliquées aux scènes et prefabs ; certaines opérations sont cumulatives.

Les vidéos locales se trouvent dans docs/preuves/videos. Le rapport est exporté vers docs/rapport_part1_benjamin_joannard.pdf.
