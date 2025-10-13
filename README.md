# Fruit Ninja VR

### Projet Unity VR – Majeure T2IA 2025/2026  
**Auteurs :** Fabio Malta, Nicolas Porot, Matéo Biaut, Benjamin Joannard  
**Technologies utilisées :** Unity 6, C#, EzySlice, XR Interaction Toolkit, Blender  

---

## Objectif du projet
Développer un mini-jeu en réalité virtuelle inspiré de *Fruit Ninja*, dans lequel le joueur doit trancher des fruits projetés en l’air à l’aide de sabres virtuels.  
L’expérience est conçue pour fonctionner sur Meta Quest via OpenXR avec le XR Interaction Toolkit.

---

## Inspirations
- Le célèbre jeu *Fruit Ninja* 🥷  
- Une volonté de recréer une expérience immersive et intuitive en VR  

---


## Fonctionnement général
1. Le joueur tient deux sabres VR (droite et gauche).  
2. Les canons projettent aléatoirement des fruits et bombes.  
3. Le joueur tranche les fruits pour marquer des points.  
4. Trancher une bombe retire une vie.  
5. Après trois bombes ➜ “Game Over”.  
6. Le score et les vies sont mis à jour en temps réel dans l’UI.  

---

## Installation et exécution

### Prérequis
- Unity 6.x (ou Unity 2023.3+)  
- Meta Quest ou Quest 2 avec **Meta Quest Link / Air Link**
- Modules XR installés :
  - ✅ *XR Plugin Management*
  - ✅ *OpenXR*
  - ✅ *XR Interaction Toolkit*
  - ✅ *Input System (New)*

### Étapes
1. **Cloner le dépôt :**
   ```bash
   git clone https://github.com/nicolasPOROT/PRJ-UnityVR.git
   ```
2. **Ouvrir le projet dans Unity**
3. **Configurer OpenXR :**
   - *Edit → Project Settings → XR Plugin Management → OpenXR*  
4. **Brancher le casque Meta Quest**
5. **Se placer sur la scène principale : `Scenes/mainMenu.unity`**
6. **Appuyer sur Play** pour tester dans l’éditeur ou via le casque

---

## Outils et Frameworks utilisés

| Outil / Package | Utilisation principale |
|------------------|------------------------|
| **Unity 6** | Moteur de jeu principal |
| **XR Interaction Toolkit** | Gestion du rig VR et des interactions (sabres, objets, raycast UI) |
| **Input System (XRI)** | Gestion des contrôleurs Meta Quest |
| **EzySlice** | Découpe réaliste des fruits en temps réel |
| **Blender** | Modélisation 3D des fruits et objets |
| **GitHub** | Gestion de version et travail collaboratif |

---

## Fonctionnalités principales

### 1. Système XR et Armes
- XR Rig complet configuré pour Meta Quest  
- Sabres virtuels dans chaque main  
- Découpe dynamique des fruits via EzySlice

### 2. Génération et comportement des fruits
- Fruits projetés aléatoirement dans les airs avec trajectoires variées  
- Bombes incluse 

### 3. Système de score et feedback
- +1 point par fruit tranché  
- Combo si plusieurs fruits sont coupés en un seul geste  
- Interface affichant **score** et **vies restantes**  
- Trancher une bombe = perte d’une vie  
- Après 3 bombes = Game Over  

### 4. Niveaux et difficulté
- Différents modes de jeu

### 5. UI et paramètres
- Menu principal accessible en VR  
- Choix du niveau de difficulté  
- Affichage des scores et des vies  
- Accès aux options via découpage d’un fruit dans le menu (interaction originale)

### 6. Implémentations futures
- Tableau des scores (classement local)  
- Sauvegarde du score en fichier JSON  
- Ajout de fruits bonus et d’effets spéciaux  

---

**Équipe :**
- Fabio Malta  
- Nicolas Porot  
- Matéo Biaut  
- Benjamin Joannard  
