# Unity Baldur's Gate 3 Style Dice Roller

Welcome to the Unity Baldur's Gate Style Dice Roller repository! This project showcases a simple implementation of a dice roller system, inspired by classic RPG mechanics as seen in Baldur's Gate 3. The repository contains scripts to integrate a functional dice roller in your Unity projects.

## Features

- **Physics-Driven Rolling**: Realistic dice roll behavior using Unity's Rigidbody.
- **Customizable Dice Sides**: Editor tool for calculating sides and assigning values to each dice side.
- **Interactive Rolling**: Dice rolls can be initiated by clicking on the dice in the game view.

## Getting Started

To get started with this project, clone the repository and open it in Unity. The project is set up with all the necessary scripts and assets you need to start experimenting with the dice roller.

### Prerequisites

- Unity Editor (Version 2022 or later recommended)

## Usage

To use the dice roller in your game:

1. Add the `DiceRoller` and `DiceSides` script to your dice GameObject.
2. Ensure your dice GameObject has a Mesh Collider and Rigidbody for physics interactions.
3. Use the custom editor tool to define the dice sides.
4. Setup a second orthogonal camera to display the dice on the UI
   (Tips: Have all Dice objects on their own layer and Cull the camera to this layer, Mode: Orthogonal, Scale up 3D objects, Increase Clip Plane distance)

## YouTube

[**Tutorial Video**](https://youtu.be/54reJ6Ac_9k)

You can also check out my [YouTube channel](https://www.youtube.com/@git-amend?sub_confirmation=1) for more Unity content.
