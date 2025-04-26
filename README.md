# Software Design & Development Capstone Project 2024-2025
An interactive VR game designed to help incoming CAMS students discover their interests in Biotechnology, Engineering, or Computer Science through immersive experiences.

# Unity Version
2021.3.39f1

# Bugs & Feature Implementation List
As of 4/19/2025:

CS Pathway Minigames (Jayden)
- Find a better way to restrict XR Grab Ray movement into a 2D grid for snapping blocks together in CS Flowchart minigame 
a.) current approach: uses XR Grab Interactable component with track position disabled, result: functional but makes interactions with ray sluggish & causes ray to bend
b.) possible solution: make a subclass of the XR Grab Interactable for customizability, cons: unpredictable behavior with Unity/existing scripts 

- Add attach point for axe

- Add cool reward at the top of the mountain for completing the minigame objective

- Add sound effects for climbing and make more interactive

- Fix bug in buildZone script which causes additional ladders to be built with insufficient materials when more than three planks are dropped at once

- Fix (a since unreplicabled bug) with ladder climbing which causes player to fall through terrain


