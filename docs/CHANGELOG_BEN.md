# Changelog Benjamin — Part 1

## 17 novembre 2026

- Élément hérité S7 : gameplay, menu par découpe et deux effets communs audités sans modification. Voir `01_etat_initial.md`.
- Ajout Part 1 : branche `Mini-projet-Ben`, tag initial `s7-initial`, audit, propositions validées A à G, procédures de tests et d'analyse, squelette de rapport et comparaison des tailles en PowerShell.
- Ajout Part 1 autorisé : `com.unity.pipeline` 1.0.0 pour piloter l'éditeur. Import et migration 6000.0.58f1 en cours de vérification ; aucune amélioration A–G encore déclarée réalisée.
- Ajout Part 1 : export du squelette `rapport.pdf` avec Edge local ; README réécrit pour distinguer PCVR et Quest autonome, avec état des validations explicite. Script de tailles vérifié avec trois fichiers/dossiers temporaires de 1,00 / 2,00 / 0,50 Mo et avec les livrables absents.
- Correction Part 1 de Task 3 : quatre Prefab Variants PCVR/Quest, PlatformFxSelector sérialisé sur fruits/bombes et sélecteurs du menu ; réglages détaillés dans `05_effets_plateformes.md`. Gameplay et deux effets S7 conservés. Compilation réussie, audit sans Missing, smoke test logique répété avec succès ; casque non testé.
- Correction Part 1 de Task 2 : assets importés et EzySlice regroupés sous ThirdParty par MoveAsset, GUID conservés, aucun asset supprimé ; compilation/audit/smoke test réussis. Voir `06_organisation.md`.
- Ajout Part 1 A : libellés français et taille/contraste du menu améliorés dans MainMenu, instruction de sélection clarifiée. Aucun changement de sélection/difficulté. Audit sans Missing et smoke test logique réussis, lisibilité casque à confirmer.
- Ajout Part 1 B : score plus grand et gras, zones texte élargies, icônes de vie agrandies dans le prefab GameplayZone ; valeurs de score/vies inchangées. Audit sans Missing et smoke test logique réussis ; lisibilité casque à confirmer.
- Ajout Part 1 C : ScorePop, animation visuelle de 0,2 s à chaque changement du score affiché, composant et référence présents dans GameplayZone avant Play. Calcul des points inchangé. Compilation, audit et smoke test réussis ; perception casque à confirmer.
- Ajout Part 1 D : SliceHapticFeedback sur les deux sabres du prefab Player, référence au sabre et main XR sérialisées, impulsion 0,15 / 40 ms à la coupe réussie. SliceObject expose seulement un événement après sa logique existante. Compilation et audit sans erreur ; smoke test renforcé avec une vraie découpe EzySlice à vitesse synthétique réussi. Vibration physique non testée.
