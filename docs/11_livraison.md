# Livraison

**Benjamin JOANNARD — Fruit Ninja VR Part 1 — 2026–2027**

Archive de remise : **Livrable/Remise_FruitNinjaVR_Part1_Benjamin_JOANNARD.zip**.

| Contenu du ZIP | Description |
|---|---|
| Windows_PCVR/ | Build Windows complet : exe, _Data, MonoBleedingEdge et DLL |
| Quest/FruitNinjaVR_Quest.apk | Build Android ARM64 autonome |
| Rapport_Benjamin_JOANNARD.pdf | Rapport synthétique de quatre pages |
| LISEZ_MOI.txt | Version Unity, contrôles, lancement et lien GitHub des sources |

Projet source : https://github.com/nicolasPOROT/PRJ-UnityVR/tree/Mini-projet-Ben

Le dépôt contient Assets et ses .meta, Packages et ProjectSettings. Les builds sont distribués dans l'archive. Les deux versions ont été testées sur Meta Quest 3, avec Virtual Desktop pour le PCVR.

## Intégrité

L'archive contient **187 fichiers** : 184 fichiers Windows, un APK, un PDF et une notice. Les fichiers Windows, APK et PDF ont été comparés à leurs sources par SHA256 ; le texte de la notice a également été vérifié.

| Fichier | Octets | SHA256 |
|---|---:|---|
| Remise_FruitNinjaVR_Part1_Benjamin_JOANNARD.zip | 115090185 | 21D775AF9E35302BEC672B660FA4250159341A52958438F51196DAA246F745D3 |
| Quest/FruitNinjaVR_Quest.apk | 57851307 | BEB89D96902D651B30F116E4FB14DF865CA2C7EEDD5E901E8D40C60E258053FC |
| Builds/FruitNinjaVR_Windows.zip | 57963597 | 43EC532B0D6A9F8F70883682FF1136C175E91E3404794B74B55FE5B3003601F4 |

La commande tools/Package-Submission.ps1 assemble et vérifie le paquet. La sortie existante est conservée ; pour une nouvelle version, choisir un autre nom avec -OutputZip.
